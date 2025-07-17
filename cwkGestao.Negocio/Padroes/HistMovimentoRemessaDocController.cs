using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class HistMovimentoRemessaDocController : BaseController<HistMovimentoRemessaDoc>
	{
		#region Singleton
        private static HistMovimentoRemessaDocController instance;
        private static IRepositorioHistMovimentoRemessaDoc repositorioHistMovimentoRemessaDoc;

        private HistMovimentoRemessaDocController()
        { }
		
		static HistMovimentoRemessaDocController()
        {
            instance = new HistMovimentoRemessaDocController();
            repositorioT = RepositorioFactory<HistMovimentoRemessaDoc>.GetRepositorio();
            repositorioHistMovimentoRemessaDoc = (IRepositorioHistMovimentoRemessaDoc)repositorioT;
        }
			
		public static HistMovimentoRemessaDocController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(HistMovimentoRemessaDoc objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
