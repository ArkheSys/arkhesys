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
	public class CteCargaInfoController : BaseController<CteCargaInfo>
	{
		#region Singleton
        private static CteCargaInfoController instance;
        private static IRepositorioCteCargaInfo repositorioCteCargaInfo;

        private CteCargaInfoController()
        { }
		
		static CteCargaInfoController()
        {
            instance = new CteCargaInfoController();
            repositorioT = RepositorioFactory<CteCargaInfo>.GetRepositorio();
            repositorioCteCargaInfo = (IRepositorioCteCargaInfo)repositorioT;
        }
			
		public static CteCargaInfoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(CteCargaInfo objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
