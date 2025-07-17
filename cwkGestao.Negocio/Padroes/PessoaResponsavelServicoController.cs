using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class PessoaResponsavelServicoController : BaseController<PessoaResponsavelServico>
    {
        #region Singleton
        private static PessoaResponsavelServicoController instance;
        private static IRepositorioPessoaResponsavelServico repositorioPessoaResponsavelServico;

        private PessoaResponsavelServicoController()
        { }

        static PessoaResponsavelServicoController()
        {
            instance = new PessoaResponsavelServicoController();
            repositorioT = RepositorioFactory<PessoaResponsavelServico>.GetRepositorio();
            repositorioPessoaResponsavelServico = (IRepositorioPessoaResponsavelServico)repositorioT;
        }

        public static PessoaResponsavelServicoController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(PessoaResponsavelServico objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<pxResponsavelServicoCliente> GetResponsaveisPorCliente(int? IDResponsavel, int? IDServico)
        {
            return repositorioPessoaResponsavelServico.GetResponsaveisPorCliente(IDResponsavel, IDServico);
        }

    }
}
