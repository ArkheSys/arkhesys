using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class ItemFichaTecnicaController : BaseController<ItemFichaTecnica>
	{
		#region Singleton
        private static ItemFichaTecnicaController instance;
        private static IRepositorioItemFichaTecnica repositorioItemFichaTecnica;

        private ItemFichaTecnicaController()
        { }
		
		static ItemFichaTecnicaController()
        {
            instance = new ItemFichaTecnicaController();
            repositorioT = RepositorioFactory<ItemFichaTecnica>.GetRepositorio();
            repositorioItemFichaTecnica = (IRepositorioItemFichaTecnica)repositorioT;
        }
			
		public static ItemFichaTecnicaController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(ItemFichaTecnica objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
