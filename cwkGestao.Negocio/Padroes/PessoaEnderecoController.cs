using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class PessoaEnderecoController : BaseController<PessoaEndereco>
	{
		#region Singleton
        private static PessoaEnderecoController instance;
        private static IRepositorioPessoaEndereco repositorioPessoaEndereco;

        private PessoaEnderecoController()
        { }
		
		static PessoaEnderecoController()
        {
            instance = new PessoaEnderecoController();
            repositorioT = RepositorioFactory<PessoaEndereco>.GetRepositorio();
            repositorioPessoaEndereco = (IRepositorioPessoaEndereco)repositorioT;
        }
			
		public static PessoaEnderecoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(PessoaEndereco objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
