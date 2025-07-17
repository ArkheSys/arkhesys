using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public partial class FluxoCaixaController : BaseController<FluxoCaixa>
    {
        #region Singleton

        private static FluxoCaixaController instance;
        private static IRepositorioFluxoCaixa repositorioFluxoCaixa;

        private FluxoCaixaController()
        { }

        static FluxoCaixaController()
        {
            instance = new FluxoCaixaController();
            repositorioT = RepositorioFactory<FluxoCaixa>.GetRepositorio();
            repositorioFluxoCaixa = (IRepositorioFluxoCaixa)repositorioT;
        }

        public static FluxoCaixaController Instancia
        {
            get
            {
                return instance;
            }
        }
        public override Dictionary<string, string> ValidaObjeto(FluxoCaixa objeto)
        {
            return new Dictionary<string, string>();
        }
        #endregion

        public FluxoCaixa GetFluxoDeCaixaPorUsuario(int IDUsuario)
        {
            return repositorioFluxoCaixa.GetByUsuario(IDUsuario);
        }

        public FluxoCaixa GetFluxoDeCaixa(int IDFluxoCaixa)
        {
            return repositorioFluxoCaixa.GetFluxoDeCaixa(IDFluxoCaixa);
        }

        public IList<pxFluxoCaixa> GetFluxosCaixa()
        {
            return repositorioFluxoCaixa.GetFluxosCaixa();
        }
    }
}
