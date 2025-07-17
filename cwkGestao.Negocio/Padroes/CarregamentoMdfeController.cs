using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class CarregamentoMdfeController : BaseController<CarregamentoMdfe>
    {
        #region Singleton
        private static CarregamentoMdfeController instance;
        private static IRepositorioCarregamentoMdfe repositorioCarregamentoMdfe;

        private CarregamentoMdfeController()
        { }

        static CarregamentoMdfeController()
        {
            instance = new CarregamentoMdfeController();
            repositorioT = RepositorioFactory<CarregamentoMdfe>.GetRepositorio();
            repositorioCarregamentoMdfe = (IRepositorioCarregamentoMdfe)repositorioT;
        }

        public static CarregamentoMdfeController Instancia => instance;
        #endregion

        public override Dictionary<string, string> ValidaObjeto(CarregamentoMdfe objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
