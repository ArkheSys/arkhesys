using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public partial class ServicoOSController : BaseController<ServicoOS>
    {
        #region Singleton
        private static ServicoOSController instance;
        private static IRepositorioServicoOS repositorioServicoOS;

        private ServicoOSController()
        { }

        static ServicoOSController()
        {
            instance = new ServicoOSController();
            repositorioT = RepositorioFactory<ServicoOS>.GetRepositorio();
            repositorioServicoOS = (IRepositorioServicoOS)repositorioT;
        }

        public static ServicoOSController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(ServicoOS objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            if (objeto.Codigo == 0)
            {
                ret.Add("txtCodigo", "Valor tem que ser diferente de zero (0).");
            }

            if (String.IsNullOrEmpty(objeto.Resumo))
            {
                ret.Add("txtResumo", "Campo obrigatório.");
            }

            if (String.IsNullOrEmpty(objeto.Descricao))
            {
                ret.Add("txtDescricao", "Campo obrigatório.");
            }
            return ret;
        }

        public Dictionary<string, string> Salvar(ServicoOS objeto, Acao acao, OrdemServico objOrdemServico)
        {
            if (acao == Acao.Excluir)
            {
                objOrdemServico.ServicoOS.Remove(objeto);
                return new Dictionary<string, string>();
            }
            else
            {
                Dictionary<string, string> ret = ValidaObjeto(objeto);
                if (ret.Count == 0 && acao == Acao.Incluir)
                {
                    objeto.OrdemServico = objOrdemServico;
                    objeto.IDOrdemServico = objOrdemServico.ID;
                    objOrdemServico.ServicoOS.Add(objeto);
                }
                return ret;
            }
        }

        public int MaxCodigo(IList<Modelo.ServicoOS> versoes)
        {
            try
            {
                return versoes.Max(v => v.Codigo) + 1;
            }
            catch
            {
                return 1;
            }
        }
    }
}
