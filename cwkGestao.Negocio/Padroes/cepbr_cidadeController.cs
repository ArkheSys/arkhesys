using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class cepbr_cidadeController : BaseController<cepbr_cidade>
	{
		#region Singleton
        private static cepbr_cidadeController instance;
        private static IRepositoriocepbr_cidade repositoriocepbr_cidade;

        private cepbr_cidadeController()
        { }
		
		static cepbr_cidadeController()
        {
            instance = new cepbr_cidadeController();
            repositorioT = RepositorioFactory<cepbr_cidade>.GetRepositorio();
            repositoriocepbr_cidade = (IRepositoriocepbr_cidade)repositorioT;
        }
			
		public static cepbr_cidadeController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(cepbr_cidade objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
