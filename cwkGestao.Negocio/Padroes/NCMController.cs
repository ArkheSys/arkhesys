using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class NCMController : BaseController<NCM>
	{
		#region Singleton
        private static NCMController instance;
        private static IRepositorioNCM repositorioNCM;

        private NCMController()
        { }
		
		static NCMController()
        {
            instance = new NCMController();
            repositorioT = RepositorioFactory<NCM>.GetRepositorio();
            repositorioNCM = (IRepositorioNCM)repositorioT;
        }
			
		public static NCMController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(NCM objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            return ret;
        }

        public NCM GetByNcm(string ncm)
        {
            return repositorioNCM.GetByNcm(ncm);
        }
	}
}
