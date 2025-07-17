using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Rateio_NotaItemController : BaseController<Rateio_NotaItem>
	{
		#region Singleton
        private static Rateio_NotaItemController instance;
        private static IRepositorioRateio_NotaItem repositorioRateio_NotaItem;

        private Rateio_NotaItemController()
        { }
		
		static Rateio_NotaItemController()
        {
            instance = new Rateio_NotaItemController();
            repositorioT = RepositorioFactory<Rateio_NotaItem>.GetRepositorio();
            repositorioRateio_NotaItem = (IRepositorioRateio_NotaItem)repositorioT;
        }
			
		public static Rateio_NotaItemController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Rateio_NotaItem objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
