using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class CondicaoFornecimentoController : BaseController<CondicaoFornecimento>
    {
        #region Singleton
        private static CondicaoFornecimentoController instance;
        private static IRepositorioCondicaoFornecimento repositorioCondicaoFornecimento;

        private CondicaoFornecimentoController()
        { }

        static CondicaoFornecimentoController()
        {
            instance = new CondicaoFornecimentoController();
            repositorioT = RepositorioFactory<CondicaoFornecimento>.GetRepositorio();
            repositorioCondicaoFornecimento = (IRepositorioCondicaoFornecimento)repositorioT;
        }

        public static CondicaoFornecimentoController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(CondicaoFornecimento objeto)
        {
            return new Dictionary<string, string>();
        }


        public IList<CondicaoFornecimento> GetTodasCondicaoFornecimento()
        {
            return repositorioCondicaoFornecimento.GetTodasCondicaoFornecimento();
        }
    }
}
