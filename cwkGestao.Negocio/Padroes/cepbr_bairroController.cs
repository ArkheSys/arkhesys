using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class cepbr_bairroController : BaseController<cepbr_bairro>
	{
		#region Singleton
        private static cepbr_bairroController instance;
        private static IRepositoriocepbr_bairro repositoriocepbr_bairro;

        private cepbr_bairroController()
        { }
		
		static cepbr_bairroController()
        {
            instance = new cepbr_bairroController();
            repositorioT = RepositorioFactory<cepbr_bairro>.GetRepositorio();
            repositoriocepbr_bairro = (IRepositoriocepbr_bairro)repositorioT;
        }
			
		public static cepbr_bairroController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(cepbr_bairro objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
