using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Cte;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class CteSeguradoraController : BaseController<CteSeguradora>
	{
		#region Singleton
        private static CteSeguradoraController instance;
        private static IRepositorioCteSeguradora repositorioCteSeguradora;

        private CteSeguradoraController()
        { }
		
		static CteSeguradoraController()
        {
            instance = new CteSeguradoraController();
            repositorioT = RepositorioFactory<CteSeguradora>.GetRepositorio();
            repositorioCteSeguradora = (IRepositorioCteSeguradora)repositorioT;
        }
			
		public static CteSeguradoraController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(CteSeguradora objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
