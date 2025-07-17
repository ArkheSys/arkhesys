using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class PCP_LoteController : BaseController<PCP_Lote>
	{
		#region Singleton
        private static PCP_LoteController instance;
        private static IRepositorioPCP_Lote repositorioPCP_Lote;

        private PCP_LoteController()
        { }
		
		static PCP_LoteController()
        {
            instance = new PCP_LoteController();
            repositorioT = RepositorioFactory<PCP_Lote>.GetRepositorio();
            repositorioPCP_Lote = (IRepositorioPCP_Lote)repositorioT;
        }
			
		public static PCP_LoteController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(PCP_Lote objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
