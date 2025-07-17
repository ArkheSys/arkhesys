using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class PessoaSocioController : BaseController<PessoaSocio>
	{
		#region Singleton
        private static PessoaSocioController instance;
        private static IRepositorioPessoaSocio repositorioPessoaSocio;

        private PessoaSocioController()
        { }
		
		static PessoaSocioController()
        {
            instance = new PessoaSocioController();
            repositorioT = RepositorioFactory<PessoaSocio>.GetRepositorio();
            repositorioPessoaSocio = (IRepositorioPessoaSocio)repositorioT;
        }
			
		public static PessoaSocioController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(PessoaSocio objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
