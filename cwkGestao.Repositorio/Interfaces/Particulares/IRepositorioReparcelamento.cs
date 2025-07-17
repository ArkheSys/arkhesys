using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioReparcelamento : IRepositorio<Reparcelamento>
    {
        IList<Reparcelamento> LoadListByIdReparcelamento(List<int> idsReparcelamentos);
        IList<Reparcelamento> LoadListByIdReparcelamentoDoc(List<int> idsReparcelamentoDoc);
        IList<Reparcelamento> LoadByIdReparcelamentoDoc(int idReparcelamentoDoc);
    }
}

