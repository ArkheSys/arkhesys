using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class ICMS_InterestadualController : BaseController<ICMS_Interestadual>
	{
		#region Singleton
        private static ICMS_InterestadualController instance;
        private static IRepositorioICMS_Interestadual repositorioICMS_Interestadual;

        private ICMS_InterestadualController()
        { }
		
		static ICMS_InterestadualController()
        {
            instance = new ICMS_InterestadualController();
            repositorioT = RepositorioFactory<ICMS_Interestadual>.GetRepositorio();
            repositorioICMS_Interestadual = (IRepositorioICMS_Interestadual)repositorioT;
        }
			
		public static ICMS_InterestadualController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(ICMS_Interestadual objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
