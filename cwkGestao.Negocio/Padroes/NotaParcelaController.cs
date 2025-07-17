using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class NotaParcelaController : BaseController<NotaParcela>
	{
		#region Singleton
        private static NotaParcelaController instance;
        private static IRepositorioNotaParcela repositorioNotaParcela;

        private NotaParcelaController()
        { }
		
		static NotaParcelaController()
        {
            instance = new NotaParcelaController();
            repositorioT = RepositorioFactory<NotaParcela>.GetRepositorio();
            repositorioNotaParcela = (IRepositorioNotaParcela)repositorioT;
        }
			
		public static NotaParcelaController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(NotaParcela objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
