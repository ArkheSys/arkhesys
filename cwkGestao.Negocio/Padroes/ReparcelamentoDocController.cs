using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class ReparcelamentoDocController : BaseController<ReparcelamentoDoc>
	{
		#region Singleton
        private static ReparcelamentoDocController instance;
        private static IRepositorioReparcelamentoDoc repositorioReparcelamentoDoc;

        private ReparcelamentoDocController()
        { }
		
		static ReparcelamentoDocController()
        {
            instance = new ReparcelamentoDocController();
            repositorioT = RepositorioFactory<ReparcelamentoDoc>.GetRepositorio();
            repositorioReparcelamentoDoc = (IRepositorioReparcelamentoDoc)repositorioT;
        }
			
		public static ReparcelamentoDocController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(ReparcelamentoDoc objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }

        public IList<ReparcelamentoDoc> LoadByIdDocumento(int idDocumento)
        {
            return repositorioReparcelamentoDoc.LoadByIdDocumento(idDocumento);
        }

        public IList<ReparcelamentoDoc> LoadListByIdDocumento(List<int> idsDocumento)
        {
            return repositorioReparcelamentoDoc.LoadListByIdDocumento(idsDocumento);
        }
    }
}
