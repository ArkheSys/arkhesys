using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class TipoDocumentoController : BaseController<TipoDocumento>
	{
		#region Singleton
        private static TipoDocumentoController instance;
        private static IRepositorioTipoDocumento repositorioTipoDocumento;

        private TipoDocumentoController()
        { }
		
		static TipoDocumentoController()
        {
            instance = new TipoDocumentoController();
            repositorioT = RepositorioFactory<TipoDocumento>.GetRepositorio();
            repositorioTipoDocumento = (IRepositorioTipoDocumento)repositorioT;
        }
			
		public static TipoDocumentoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(TipoDocumento objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
