using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class ProdutoEcommerceSimplificadoController : BaseController<ProdutoEcommerceSimplificado>
    {
        #region Singleton
        private static ProdutoEcommerceSimplificadoController instance;
        private static IRepositorioProdutoEcommerceSimplificado repositorioProdutoEcommerceSimplificado;

        private ProdutoEcommerceSimplificadoController()
        { }

        static ProdutoEcommerceSimplificadoController()
        {
            instance = new ProdutoEcommerceSimplificadoController();
            repositorioT = RepositorioFactory<ProdutoEcommerceSimplificado>.GetRepositorio();
            repositorioProdutoEcommerceSimplificado = (IRepositorioProdutoEcommerceSimplificado)repositorioT;
        }

        public static ProdutoEcommerceSimplificadoController Instancia => instance;
        #endregion

        public override Dictionary<string, string> ValidaObjeto(ProdutoEcommerceSimplificado objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
