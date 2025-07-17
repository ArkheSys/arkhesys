using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class TipoServicoTransporteController : BaseController<TipoServicoTransporte>
	{
		#region Singleton
        private static TipoServicoTransporteController instance;
        private static IRepositorioTipoServicoTransporte repositorioTipoServicoTransporte;

        private TipoServicoTransporteController()
        { }
		
		static TipoServicoTransporteController()
        {
            instance = new TipoServicoTransporteController();
            repositorioT = RepositorioFactory<TipoServicoTransporte>.GetRepositorio();
            repositorioTipoServicoTransporte = (IRepositorioTipoServicoTransporte)repositorioT;
        }
			
		public static TipoServicoTransporteController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(TipoServicoTransporte objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
