using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class InventarioProdutoController : BaseController<InventarioProduto>
    {
        #region Singleton

        private static readonly IRepositorioInventarioProduto repositorioInventarioProduto;

        private InventarioProdutoController()
        { }

        static InventarioProdutoController()
        {
            Instancia = new InventarioProdutoController();
            repositorioT = RepositorioFactory<InventarioProduto>.GetRepositorio();
            repositorioInventarioProduto = (IRepositorioInventarioProduto)repositorioT;
        }

        public static InventarioProdutoController Instancia { get; }

        #endregion

        public override Dictionary<string, string> ValidaObjeto(InventarioProduto objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<pxInventarioProduto> GetProdutosPorInventario(int IDInventario)
        {
            return repositorioInventarioProduto.GetProdutosPorInventario(IDInventario);
        }
    }
}
