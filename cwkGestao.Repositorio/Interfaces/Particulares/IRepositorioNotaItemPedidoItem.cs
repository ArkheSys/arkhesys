using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioNotaItemPedidoItem : IRepositorio<NotaItemPedidoItem>
    {
        decimal GetQtdRetirada(int IDPedidoItem, int IDNotaItem);

        IList<NotaItemPedidoItem> GetAllNiPiPorIDPedido(int idPedido);
        IList<NotaItemPedidoItem> GetAllNiPiPorIDNota(int idNota);
    }
}