using System.Collections.Generic;
using cwkGestao.Modelo;

using Modelo.ModeloProxy;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioMovimentacaoCreditoPedido : IRepositorio<MovimentacaoCreditoPedido>
    {
        IList<MovimentacaoCreditoPedido> GetTodasMovimentacoes();
        IList<pxyClientesComCredito> GetAllClientesComCredito();
    }
}

