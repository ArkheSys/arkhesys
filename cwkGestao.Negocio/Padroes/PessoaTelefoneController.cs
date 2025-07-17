using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class PessoaTelefoneController : BaseController<PessoaTelefone>
	{
		#region Singleton
        private static PessoaTelefoneController instance;
        private static IRepositorioPessoaTelefone repositorioPessoaTelefone;

        private PessoaTelefoneController()
        { }
		
		static PessoaTelefoneController()
        {
            instance = new PessoaTelefoneController();
            repositorioT = RepositorioFactory<PessoaTelefone>.GetRepositorio();
            repositorioPessoaTelefone = (IRepositorioPessoaTelefone)repositorioT;
        }
			
		public static PessoaTelefoneController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(PessoaTelefone objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
