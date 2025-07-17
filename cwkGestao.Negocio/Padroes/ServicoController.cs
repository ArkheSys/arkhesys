using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public partial class ServicoController : BaseController<Servico>
    {
        #region Singleton
        private static ServicoController instance;
        private static IRepositorioServico repositorioServico;

        private ServicoController()
        { }

        static ServicoController()
        {
            instance = new ServicoController();
            repositorioT = RepositorioFactory<Servico>.GetRepositorio();
            repositorioServico = (IRepositorioServico)repositorioT;
        }

        public static ServicoController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(Servico objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();
            if (objeto.Codigo == 0)
            {
                ret.Add("txtCodigo", "Valor tem que ser maior do que zero (0).");
            }

            if (String.IsNullOrEmpty(objeto.Nome))
            {
                ret.Add("txtNome", "Campo obrigatório.");
            }
            if (objeto.Descricao != null)
            {
                if (String.IsNullOrEmpty(objeto.Descricao.Trim()))
                {
                    ret.Add("txtDescricao", "Campo obrigatório.");
                }
            }
            return ret;
        }

        public Servico GetByCodigo(int codigo)
        {
            return repositorioServico.GetByCodigo(codigo);
        }

        public IList<object> GetByInicial(string inicial, string final)
        {
            return repositorioServico.GetByInicial(inicial, final);
        }

        public IList<Modelo.Servico> GetServicosPorIDOfd_Proposta(int IDOfd_Proposta)
        {
            return repositorioServico.GetServicosPorIDOfd_Proposta(IDOfd_Proposta);
        }


    }
}
