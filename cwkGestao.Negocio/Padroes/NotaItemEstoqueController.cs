using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class NotaItemEstoqueController : BaseController<NotaItemEstoque>
	{
		#region Singleton
        private static NotaItemEstoqueController instance;
        private static IRepositorioNotaItemEstoque repositorioNotaItemEstoque;

        private NotaItemEstoqueController()
        { }
		
		static NotaItemEstoqueController()
        {
            instance = new NotaItemEstoqueController();
            repositorioT = RepositorioFactory<NotaItemEstoque>.GetRepositorio();
            repositorioNotaItemEstoque = (IRepositorioNotaItemEstoque)repositorioT;
        }
			
		public static NotaItemEstoqueController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(NotaItemEstoque objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
