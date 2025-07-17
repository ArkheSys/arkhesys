using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class BaixaDocumentoController : BaseController<BaixaDocumento>
	{
		#region Singleton
        private static BaixaDocumentoController instance;
        private static IRepositorioBaixaDocumento repositorioBaixaDocumento;

        private BaixaDocumentoController()
        { }
		
		static BaixaDocumentoController()
        {
            instance = new BaixaDocumentoController();
            repositorioT = RepositorioFactory<BaixaDocumento>.GetRepositorio();
            repositorioBaixaDocumento = (IRepositorioBaixaDocumento)repositorioT;
        }
			
		public static BaixaDocumentoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(BaixaDocumento objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
