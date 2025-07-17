using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioExpedicaoRastreamento : IRepositorio<ExpedicaoRastreamento>
    {
        IList<ExpedicaoRastreamento> GetExpedicaoRastreamentoPorPedido(int id);

        IList<ExpedicaoRastreamento> GetExpedicaoRastreamentoPorPLP(PLP plp);

        ExpedicaoRastreamento GetExpedicaoRastreamentoByRastreamento(int idRastramento);

        IList<ExpedicaoRastreamento> GetExpedicaoRastreamentoNaoCompletas(int idPedido);
    }
}

