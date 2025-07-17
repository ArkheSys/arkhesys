using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioCwkPedidoEmpresa : IRepositorio<CwkPedidoEmpresa>
    {
        int UltimoNumeroSerie(int pIdSistema, int pIdPedido);
    }
}
