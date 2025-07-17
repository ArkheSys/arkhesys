using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Negocio
{
	public class LocalEstoqueController : BaseController<LocalEstoque>
	{
		#region Singleton
        private static LocalEstoqueController instance;
        private static IRepositorioLocalEstoque repositorioLocalEstoque;

        private LocalEstoqueController()
        { }
		
		static LocalEstoqueController()
        {
            instance = new LocalEstoqueController();
            repositorioT = RepositorioFactory<LocalEstoque>.GetRepositorio();
            repositorioLocalEstoque = (IRepositorioLocalEstoque)repositorioT;
        }
			
		public static LocalEstoqueController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(LocalEstoque objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<pxyListaEstoque> GetListaEstoque(int idLocalEstoque)
        {
            return repositorioLocalEstoque.GetListaEstoque(idLocalEstoque);
        }
	}
}
