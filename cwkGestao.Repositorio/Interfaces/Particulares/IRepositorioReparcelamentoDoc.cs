using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioReparcelamentoDoc : IRepositorio<ReparcelamentoDoc>
    {
        IList<ReparcelamentoDoc> LoadByIdDocumento(int idDocumento);
        IList<ReparcelamentoDoc> LoadListByIdDocumento(List<int> idsDocumento);
    }
}

