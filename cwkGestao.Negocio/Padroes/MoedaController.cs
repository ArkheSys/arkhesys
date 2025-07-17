using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class MoedaController : BaseController<Moeda>
    {
        #region Singleton
        private static MoedaController instance;
        private static IRepositorioMoeda repositorioMoeda;

        private MoedaController()
        { }

        static MoedaController()
        {
            instance = new MoedaController();
            repositorioT = RepositorioFactory<Moeda>.GetRepositorio();
            repositorioMoeda = (IRepositorioMoeda)repositorioT;
        }

        public static MoedaController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(Moeda objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<Moeda> GetTodasMoedas()
        {
            return repositorioMoeda.GetTodasMoedas();
        }
    }
}
