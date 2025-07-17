using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using MO = Modelo;

namespace cwkGestao.Negocio
{
    public partial class OrdemServicoController : BaseController<OrdemServico>
    {
        #region Singleton
        private static OrdemServicoController instance;
        private static IRepositorioOrdemServico repositorioOrdemServico;

        private OrdemServicoController()
        { }

        static OrdemServicoController()
        {
            instance = new OrdemServicoController();
            repositorioT = RepositorioFactory<OrdemServico>.GetRepositorio();
            repositorioOrdemServico = (IRepositorioOrdemServico)repositorioT;
        }

        public static OrdemServicoController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(OrdemServico objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            if (objeto.Codigo == 0)
            {
                ret.Add("txtCodigo", "Valor tem que ser maior do que zero (0).");
            }

            if (String.IsNullOrEmpty(objeto.Resumo) || objeto.Resumo.Trim() == String.Empty)
            {
                ret.Add("txtResumo", "Campo obrigatório.");
            }

            if (objeto.IDCliente == 0)
            {
                ret.Add("cbIDCliente", "Campo obrigatório.");
            }

            if (objeto.IDStatus == 0)
            {
                ret.Add("cbIDStatus", "Campo obrigatório.");
            }

            if (objeto.IDResponsavel == 0)
            {
                ret.Add("cbIDResponsavel", "Campo obrigatório.");
            }

            if (!objeto.IDSistema.HasValue || objeto.IDSistema == 0)
            {
                ret.Add("cbIdSistema", "Campo obrigatório.");
            }

            if (!objeto.IDVersao.HasValue || objeto.IDVersao == 0)
            {
                ret.Add("cbIdVersao", "Campo obrigatório.");
            }
            return ret;
        }

        public override Dictionary<string, string> Salvar(OrdemServico objeto, Modelo.Acao acao)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            try
            {
                if (acao == Acao.Excluir)
                {
                    Delete(objeto);
                    return new Dictionary<string, string>();
                }
                ret = ValidaObjeto(objeto);
                if (ret.Count == 0)
                {
                    switch (acao)
                    {
                        case Modelo.Acao.Incluir:
                            objeto.Incdata = DateTime.Now;
                            objeto.Incusuario = MO.cwkGlobal.objUsuarioLogado.Login;
                            objeto.AcompanhamentoOS = new List<AcompanhamentoOS>();
                            cwkGestao.Modelo.AcompanhamentoOS objAcompanhamento = new cwkGestao.Modelo.AcompanhamentoOS();
                            objAcompanhamento.Codigo = 1;
                            objAcompanhamento.OrdemServico = objeto;
                            objAcompanhamento.IDResponsavelAtu = objeto.IDResponsavel;
                            objAcompanhamento.IDStatusAtu = objeto.IDStatus;
                            objAcompanhamento.Incdata = objeto.DataStatus;
                            objAcompanhamento.Incusuario = objeto.Incusuario;
                            objeto.AcompanhamentoOS.Add(objAcompanhamento);
                            Create(objeto);
                            break;
                        case Modelo.Acao.Alterar:
                            objeto.Altdata = DateTime.Now;
                            objeto.Altusuario = MO.cwkGlobal.objUsuarioLogado.Login;
                            Update(objeto);
                            break;

                    }
                    return ret;

                }
                else
                {
                    return ret;
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        protected override void Update(OrdemServico objeto)
        {
            OrdemServico ordemServicoOriginal = this.LoadObjectById(objeto.ID);
            if (ordemServicoOriginal.Responsavel.ID != objeto.Responsavel.ID ||
                ordemServicoOriginal.Status.ID != objeto.Status.ID)
            {
                AcompanhamentoOS objAcompanhamentoOS = new AcompanhamentoOS();
                objAcompanhamentoOS.Codigo = objeto.AcompanhamentoOS.Max(a => a.Codigo) + 1;
                objAcompanhamentoOS.IDResponsavelAnt = ordemServicoOriginal.IDResponsavel;
                objAcompanhamentoOS.IDStatusAnt = ordemServicoOriginal.IDStatus;
                objAcompanhamentoOS.IDResponsavelAtu = objeto.IDResponsavel;
                objAcompanhamentoOS.IDStatusAtu = objeto.IDStatus;
                objAcompanhamentoOS.OrdemServico = objeto;
                objAcompanhamentoOS.IDOrdemServico = objeto.ID;
                objAcompanhamentoOS.Incdata = objeto.DataStatus;
                objAcompanhamentoOS.Incusuario = MO.cwkGlobal.objUsuarioLogado.Login;

                objeto.AcompanhamentoOS.Add(objAcompanhamentoOS);
            }
            else
            {
                var objAcompanhamentoOS = objeto.AcompanhamentoOS.OrderByDescending(a => a.Incdata).First();
                objAcompanhamentoOS.Incdata = objeto.DataStatus;
            }

            base.Update(objeto);
        }

        public IList<cwkGestao.Modelo.Proxy.pxyGridOrdemServico> GetAllGrid()
        {
            return repositorioOrdemServico.GetAllGrid();
        }

        public void ReordenaServicos(IList<ServicoOS> pServicos)
        {
            int count = 1;
            foreach (Modelo.ServicoOS cp in pServicos)
            {
                cp.Codigo = count++;
            }
        }

        public IList<object> OrdemServicoRel(int idRevenda, int idResponsavel, int idStatus, DateTime DataInicial, DateTime DataFinal)
        {
            return repositorioOrdemServico.OrdemServicoRel(idRevenda, idResponsavel, idStatus, DataInicial, DataFinal);
        }
    }
}
