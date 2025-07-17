using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioPedidoParcela : IRepositorio<PedidoParcela>
    {
        IList<pxyRelFluxoCaixa> GetItemsFluxoDeCaixa(DateTime dataInicial, DateTime dataFinal, ConsiderarPedidos filtroPedidos);

        IList<PedidoParcela> GetParcelasPedido(Pedido p);
    }
}

