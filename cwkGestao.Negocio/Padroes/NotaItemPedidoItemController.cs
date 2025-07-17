using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Repositorio;

namespace cwkGestao.Negocio
{
    public class NotaItemPedidoItemController : BaseController<NotaItemPedidoItem>
    {
        #region Singleton
        private static NotaItemPedidoItemController instance;
        private static IRepositorioNotaItemPedidoItem repositorioNotaItemPedidoItem;

        private NotaItemPedidoItemController()
        { }

        static NotaItemPedidoItemController()
        {
            instance = new NotaItemPedidoItemController();
            repositorioT = RepositorioFactory<NotaItemPedidoItem>.GetRepositorio();
            repositorioNotaItemPedidoItem = (IRepositorioNotaItemPedidoItem)repositorioT;
        }

        public static NotaItemPedidoItemController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(NotaItemPedidoItem objeto)
        {
            return new Dictionary<string, string>();
        }

        public decimal GetQtdRetirada(int p, int p_2)
        {
            return repositorioNotaItemPedidoItem.GetQtdRetirada(p, p_2);
        }

        public IList<NotaItemPedidoItem> GetAllNiPiPorIDPedido(int idPedido)
        {
            return repositorioNotaItemPedidoItem.GetAllNiPiPorIDPedido(idPedido);
        }

        public IList<NotaItemPedidoItem> GetAllNiPiPorIDNota(int idNota)
        {
            return repositorioNotaItemPedidoItem.GetAllNiPiPorIDNota(idNota);
        }
    }
}
