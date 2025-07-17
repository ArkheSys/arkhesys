using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class ProdutoAnexoController : BaseController<ProdutoAnexo>
    {
        #region Singleton

        private static readonly IRepositorioProdutoAnexo repositorioProdutoAnexo;

        private ProdutoAnexoController() { }

        static ProdutoAnexoController()
        {
            Instancia = new ProdutoAnexoController();
            repositorioT = RepositorioFactory<ProdutoAnexo>.GetRepositorio();
            repositorioProdutoAnexo = (IRepositorioProdutoAnexo)repositorioT;
        }

        public static ProdutoAnexoController Instancia { get; }

        #endregion

        public override Dictionary<string, string> ValidaObjeto(ProdutoAnexo objeto)
        {
            return new Dictionary<string, string>();
        }

        public override IList<ProdutoAnexo> GetAll()
        {
            return repositorioProdutoAnexo.GetAll();
        }

    }
}