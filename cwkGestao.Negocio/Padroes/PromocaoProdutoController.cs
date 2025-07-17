using System.Collections.Generic;

using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class PromocaoProdutoController : BaseController<PromocaoProduto>
    {
        private static PromocaoProdutoController instance;
        private static IRepositorioPromocaoProduto repositorioPromocaoProduto;

        private PromocaoProdutoController()
        { }

        static PromocaoProdutoController()
        {
            instance = new PromocaoProdutoController();
            repositorioT = RepositorioFactory<PromocaoProduto>.GetRepositorio();
            repositorioPromocaoProduto = (IRepositorioPromocaoProduto)repositorioT;
        }

        public static PromocaoProdutoController Instancia
        {
            get
            {
                return instance;
            }
        }

        public override Dictionary<string, string> ValidaObjeto(PromocaoProduto objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}