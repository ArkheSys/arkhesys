using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class NotaReferenciaController : BaseController<NotaReferencia>
	{
		#region Singleton
        private static NotaReferenciaController instance;
        private static IRepositorioNotaReferencia repositorioNotaReferencia;

        private NotaReferenciaController()
        { }
		
		static NotaReferenciaController()
        {
            instance = new NotaReferenciaController();
            repositorioT = RepositorioFactory<NotaReferencia>.GetRepositorio();
            repositorioNotaReferencia = (IRepositorioNotaReferencia)repositorioT;
        }
			
		public static NotaReferenciaController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(NotaReferencia objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
