using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class CotacaoMoedaController : BaseController<CotacaoMoeda>
    {
        #region Singleton
        private static CotacaoMoedaController instance;
        private static IRepositorioCotacaoMoeda repositorioCotacaoMoeda;

        private CotacaoMoedaController()
        { }

        static CotacaoMoedaController()
        {
            instance = new CotacaoMoedaController();
            repositorioT = RepositorioFactory<CotacaoMoeda>.GetRepositorio();
            repositorioCotacaoMoeda = (IRepositorioCotacaoMoeda)repositorioT;
        }

        public static CotacaoMoedaController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(CotacaoMoeda objeto)
        {
            return new Dictionary<string, string>();
        }
        public IList<CotacaoMoeda> GetTodasCotacaoMoedas()
        {
            return repositorioCotacaoMoeda.GetTodasCotacaoMoedas();
        }

        public IList<pxCotacaoMoeda> GetCotacoesMoeda()
        {
            return repositorioCotacaoMoeda.GetCotacoesMoeda();
        }
    }
}
