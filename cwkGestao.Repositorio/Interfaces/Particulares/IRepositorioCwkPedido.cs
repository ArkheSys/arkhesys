using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioCwkPedido : IRepositorio<CwkPedido>
    {
        IList<cwkGestao.Modelo.Proxy.pxyCwkPedido> GetPedidos();
        int UltimoNumeroSerie(int pIdSistema);
        IList<object> GetPedidosFaturamento(List<int> idsRevendas, int tipoFiltro, DateTime dataInicial, DateTime dataFinal);
        IList<cwkGestao.Modelo.CwkPedido> GetPedidos(cwkGestao.Modelo.Pessoa pessoa);
        IList<cwkGestao.Modelo.CwkPedido> GetPedidos(cwkGestao.Modelo.Pessoa pessoa, DateTime inicio, DateTime fim);
        IList<cwkGestao.Modelo.CwkPedido> GetCwkPedidos();

        IList<CwkPedido> GetPedidos(CwkPedido exemplo);
    }
}
