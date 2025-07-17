using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioTipoPedido : IRepositorio<TipoPedido>
    {
        IList<TipoPedido> GetAll(InOutType entSai, TipoPedidoType? tipoPedido);
        IList<TipoPedido> GetByTipoAndEnt_Sai(InOutType EntSai);
    }
}

