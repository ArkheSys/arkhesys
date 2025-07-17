using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class cepbr_estadoController : BaseController<cepbr_estado>
	{
		#region Singleton
        private static cepbr_estadoController instance;
        private static IRepositoriocepbr_estado repositoriocepbr_estado;

        private cepbr_estadoController()
        { }
		
		static cepbr_estadoController()
        {
            instance = new cepbr_estadoController();
            repositorioT = RepositorioFactory<cepbr_estado>.GetRepositorio();
            repositoriocepbr_estado = (IRepositoriocepbr_estado)repositorioT;
        }
			
		public static cepbr_estadoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(cepbr_estado objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
