using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System.Data;

namespace cwkGestao.Negocio
{
    public partial class CwkPedidoEmpresaController : BaseController<CwkPedidoEmpresa>
    {
        #region Singleton
        private static CwkPedidoEmpresaController instance;
        private static IRepositorioCwkPedidoEmpresa repositorioCwkPedidoEmpresa;

        private CwkPedidoEmpresaController()
        { }

        static CwkPedidoEmpresaController()
        {
            instance = new CwkPedidoEmpresaController();
            repositorioT = RepositorioFactory<CwkPedidoEmpresa>.GetRepositorio();
            repositorioCwkPedidoEmpresa = (IRepositorioCwkPedidoEmpresa)repositorioT;
        }

        public static CwkPedidoEmpresaController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(CwkPedidoEmpresa objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (objeto.ClienteRevenda == null)
            {
                ret.Add("cbIdClienterevenda", "Selecione o cliente da revenda.");
            }

            if (objeto.Produto == null && objeto.Servico == null)
            {
                ret.Add("rgTipoServico", "Selecione um produto ou serviço.");
            }

            return ret;
        }

        public Dictionary<string, string> Salvar(CwkPedidoEmpresa objeto, Acao acao, CwkPedido objcwkPedido)
        {
            if (acao == Acao.Excluir)
            {
                objcwkPedido.CwkPedidoEmpresas.Remove(objeto);
                return new Dictionary<string, string>();
            }
            else
            {
                Dictionary<string, string> ret = ValidaObjeto(objeto);
                if (ret.Count == 0 && acao == Acao.Incluir)
                {
                    objeto.Pedido = objcwkPedido;
                    objeto.IDPedido = objcwkPedido.ID;
                    objcwkPedido.CwkPedidoEmpresas.Add(objeto);
                }
                return ret;
            }
        }

        public int MaxCodigo(IList<Modelo.CwkPedidoEmpresa> pedidoEmpresas)
        {
            try
            {
                return pedidoEmpresas.Max(v => v.Seq) + 1;
            }
            catch
            {
                return 1;
            }
        }

        public IList<int> GetPorInfMaquina(string infMaquina)
        {
            return new List<int>();
        }

        public DataTable GetPedidosIn(IList<int> ids)
        {
            throw new NotImplementedException();
        }

        public int GeraNumeroSerie(int pIdSistema, int pIdPedido)
        {
            int num = repositorioCwkPedidoEmpresa.UltimoNumeroSerie(pIdSistema, pIdPedido);
            return num + 1;
        }

        public void GerarNumeroSerie(CwkPedido objcwkPedido, CwkPedidoEmpresa objcwkPedidoEmpresa)
        {
            if (!objcwkPedido.BDemonstracao)
            {
                objcwkPedidoEmpresa.NumeroSerie = GeraNumeroSerie(objcwkPedido.Sistema.ID, objcwkPedido.ID);
                //Se o último número de série no banco já estiver no objeto pedido, incrementa para a o próximo numero
                //isso acontece quando esta inserindo mais de uma filial e o objeto pedido ainda não foi persistido.
                while (objcwkPedido.CwkPedidoEmpresas.Where(x => x.NumeroSerie == objcwkPedidoEmpresa.NumeroSerie && x != objcwkPedidoEmpresa).Count() > 0)
                {
                    objcwkPedidoEmpresa.NumeroSerie++;
                }
            }
            else { objcwkPedidoEmpresa.NumeroSerie = 0; }
        }

        public void RegerarNumeroSerie(CwkPedido objcwkPedido)
        {
            if (objcwkPedido != null && objcwkPedido.CwkPedidoEmpresas.Count() > 0)
            {
                foreach (CwkPedidoEmpresa _cwkPedidoEmpresa in objcwkPedido.CwkPedidoEmpresas)
                {
                    _cwkPedidoEmpresa.NumeroSerie = null;
                }

                foreach (CwkPedidoEmpresa _cwkPedidoEmpresa in objcwkPedido.CwkPedidoEmpresas)
                {
                    GerarNumeroSerie(objcwkPedido, _cwkPedidoEmpresa);
                }
            }
        }
    }
}
