using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioExpedicaoHistorico : IRepositorio<ExpedicaoHistorico>
    {
        IList<ExpedicaoHistorico> GetHistoricosPorCliente(DateTime dtInicio, DateTime dtFinal, int idCliente);
        IList<ExpedicaoHistorico> GetHistoricos(DateTime dtInicio, DateTime dtFinal, int idEmpresa);
        IList<ExpedicaoHistorico> GetHistoricosPorExpedicaoRastreamento(int id);
    }
}

