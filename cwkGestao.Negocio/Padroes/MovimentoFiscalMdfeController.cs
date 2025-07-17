using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class MovimentoFiscalMdfeController : BaseController<MovimentoFiscalMdfe>
    {
        #region Singleton
        private static MovimentoFiscalMdfeController instance;
        private static IRepositorioMovimentoFiscalMdfe repositorioMovimentoFiscalMdfe;

        private MovimentoFiscalMdfeController()
        { }

        static MovimentoFiscalMdfeController()
        {
            instance = new MovimentoFiscalMdfeController();
            repositorioT = RepositorioFactory<MovimentoFiscalMdfe>.GetRepositorio();
            repositorioMovimentoFiscalMdfe = (IRepositorioMovimentoFiscalMdfe)repositorioT;
        }

        public static MovimentoFiscalMdfeController Instancia => instance;
        #endregion

        public override Dictionary<string, string> ValidaObjeto(MovimentoFiscalMdfe objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
