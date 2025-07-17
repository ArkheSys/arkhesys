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
	public class CteDocumentosInformadosController : BaseController<CteDocumentosInformados>
	{
		#region Singleton
        private static CteDocumentosInformadosController instance;
        private static IRepositorioCteDocumentosInformados repositorioCteDocumentosInformados;

        private CteDocumentosInformadosController()
        { }
		
		static CteDocumentosInformadosController()
        {
            instance = new CteDocumentosInformadosController();
            repositorioT = RepositorioFactory<CteDocumentosInformados>.GetRepositorio();
            repositorioCteDocumentosInformados = (IRepositorioCteDocumentosInformados)repositorioT;
        }
			
		public static CteDocumentosInformadosController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(CteDocumentosInformados objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
