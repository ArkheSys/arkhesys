using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class PessoaEmailController : BaseController<PessoaEmail>
	{
		#region Singleton
        private static PessoaEmailController instance;
        private static IRepositorioPessoaEmail repositorioPessoaEmail;

        private PessoaEmailController()
        { }
		
		static PessoaEmailController()
        {
            instance = new PessoaEmailController();
            repositorioT = RepositorioFactory<PessoaEmail>.GetRepositorio();
            repositorioPessoaEmail = (IRepositorioPessoaEmail)repositorioT;
        }
			
		public static PessoaEmailController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion

		public override Dictionary<string, string> ValidaObjeto(PessoaEmail objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }

        public IList<PessoaEmail> GetEmailFinanceiroPorIDPessoa(int idPessoa)
        {
            return repositorioPessoaEmail.GetEmailFinanceiroPorIDPessoa(idPessoa);
        }

        public IList<PessoaEmail> GetEmailNFSe(int idPessoa)
        {
            return repositorioPessoaEmail.GetEmailNFSe(idPessoa);
        }
	}
}
