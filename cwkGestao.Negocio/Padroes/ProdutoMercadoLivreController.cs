using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class ProdutoMercadoLivreController : BaseController<ProdutoMercadoLivre>
    {
        #region Singleton
        private static ProdutoMercadoLivreController instance;
        private static IRepositorioProdutoMercadoLivre repositorioProdutoMercadoLivre;

        private ProdutoMercadoLivreController()
        { }

        static ProdutoMercadoLivreController()
        {
            instance = new ProdutoMercadoLivreController();
            repositorioT = RepositorioFactory<ProdutoMercadoLivre>.GetRepositorio();
            repositorioProdutoMercadoLivre = (IRepositorioProdutoMercadoLivre)repositorioT;
        }

        public static ProdutoMercadoLivreController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(ProdutoMercadoLivre objeto)
        {
            return new Dictionary<string, string>();
        }

        public Produto LoadProdutoByIDMercadoLivre(int id)
        {
            return repositorioProdutoMercadoLivre.LoadProdutoByIDMercadoLivre(id);
        }
    }
}
