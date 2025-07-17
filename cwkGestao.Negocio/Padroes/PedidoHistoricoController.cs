using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public partial class PedidoHistoricoController : BaseController<PedidoHistorico>
    {
        #region Singleton
        private static PedidoHistoricoController instance;
        private static IRepositorioPedidoHistorico repositorioUF;

        private PedidoHistoricoController()
        { }

        static PedidoHistoricoController()
        {
            instance = new PedidoHistoricoController();
            repositorioT = RepositorioFactory<PedidoHistorico>.GetRepositorio();
            repositorioUF = (IRepositorioPedidoHistorico)repositorioT;
        }

        public static PedidoHistoricoController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(PedidoHistorico objeto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

           

            return ret;
        }
    }
}
