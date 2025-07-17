using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class ProprietarioVeiculoMdfeController : BaseController<ProprietarioVeiculoMdfe>
    {
        #region Singleton
        private static ProprietarioVeiculoMdfeController instance;
        private static IRepositorioProprietarioVeiculoMdfe repositorioProprietarioVeiculoMdfe;

        private ProprietarioVeiculoMdfeController()
        { }

        static ProprietarioVeiculoMdfeController()
        {
            instance = new ProprietarioVeiculoMdfeController();
            repositorioT = RepositorioFactory<ProprietarioVeiculoMdfe>.GetRepositorio();
            repositorioProprietarioVeiculoMdfe = (IRepositorioProprietarioVeiculoMdfe)repositorioT;
        }

        public static ProprietarioVeiculoMdfeController Instancia => instance;
        #endregion

        public override Dictionary<string, string> ValidaObjeto(ProprietarioVeiculoMdfe objeto)
        {
            return new Dictionary<string, string>();
        }
    }
}
