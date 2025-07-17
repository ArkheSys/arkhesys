using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public partial class AcompanhamentoOSController : BaseController<AcompanhamentoOS>
    {
        #region Singleton
        private static AcompanhamentoOSController instance;
        private static IRepositorioAcompanhamentoOS repositorioAcompanhamentoOS;

        private AcompanhamentoOSController()
        { }

        static AcompanhamentoOSController()
        {
            instance = new AcompanhamentoOSController();
            repositorioT = RepositorioFactory<AcompanhamentoOS>.GetRepositorio();
            repositorioAcompanhamentoOS = (IRepositorioAcompanhamentoOS)repositorioT;
        }

        public static AcompanhamentoOSController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(AcompanhamentoOS objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }

        public IList<cwkGestao.Modelo.Proxy.pxyAcompanhamentoOS> GetAcompanhamentoOS(int idOrdemServico)
        {
            return repositorioAcompanhamentoOS.GetAcompanhamentoOS(idOrdemServico);
        }
    }
}
