using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System.Collections.Generic;

namespace cwkGestao.Negocio.Padroes
{
    public class ProdutoVarejoController : BaseController<ProdutoVarejo>, IRepositorioProdutoVarejo
    {
        private static ProdutoVarejoController instance;
        private static IRepositorioProdutoVarejo repositorioProdutoVarejo;

        private ProdutoVarejoController()
        { }

        static ProdutoVarejoController()
        {
            instance = new ProdutoVarejoController();
            repositorioT = RepositorioFactory<ProdutoVarejo>.GetRepositorio();
            repositorioProdutoVarejo = (IRepositorioProdutoVarejo)repositorioT;
        }

        public static ProdutoVarejoController Instancia
        {
            get
            {
                return instance;
            }
        }

        public override Dictionary<string, string> ValidaObjeto(ProdutoVarejo objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<ProdutoVarejo> GetPorProduto(int IDProduto)
        {
            return repositorioProdutoVarejo.GetPorProduto(IDProduto);
        }
    }
}
