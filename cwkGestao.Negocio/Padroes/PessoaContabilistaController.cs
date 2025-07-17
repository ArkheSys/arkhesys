using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class PessoaContabilistaController : BaseController<PessoaContabilista>
	{
		#region Singleton
        private static PessoaContabilistaController instance;
        private static IRepositorioPessoaContabilista repositorioPessoaContabilista;

        private PessoaContabilistaController()
        { }
		
		static PessoaContabilistaController()
        {
            instance = new PessoaContabilistaController();
            repositorioT = RepositorioFactory<PessoaContabilista>.GetRepositorio();
            repositorioPessoaContabilista = (IRepositorioPessoaContabilista)repositorioT;
        }
			
		public static PessoaContabilistaController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(PessoaContabilista objeto)
        {
            return new Dictionary<string, string>();
        }

        public PessoaContabilista GetPessoaContabilistaPorIDPessoa(int idPessoa)
        {
            return repositorioPessoaContabilista.GetPessoaContabilistaPorIDPessoa(idPessoa);
        }
	}
}
