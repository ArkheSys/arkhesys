using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class PessoaAvisoController : BaseController<PessoaAviso>
	{
		#region Singleton
        private static PessoaAvisoController instance;
        private static IRepositorioPessoaAviso repositorioPessoaAviso;

        private PessoaAvisoController()
        { }
		
		static PessoaAvisoController()
        {
            instance = new PessoaAvisoController();
            repositorioT = RepositorioFactory<PessoaAviso>.GetRepositorio();
            repositorioPessoaAviso = (IRepositorioPessoaAviso)repositorioT;
        }
			
		public static PessoaAvisoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(PessoaAviso objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
