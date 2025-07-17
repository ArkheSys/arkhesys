using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioCorreiosPrecoPrazo : IRepositorio<CorreiosPrecoPrazo>
    {
        IList<CorreiosPrecoPrazo> GetPorIDPedido(int idPedido);
        IList<CorreiosPrecoPrazo> GetPorIDRastreamento(int idRastreamento);
        IList<CorreiosPrecoPrazo> GetSelecionadosPorIDPedido(int IdPedido);
    }
}

