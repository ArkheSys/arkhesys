using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioNotaInutilizada : IRepositorio<NotaInutilizada>
    {
        IList<NotaInutilizada> GetAllByFilial(int idFilial);
        IList<NotaInutilizada> GetRelNotaPeriodo(String idsEmpresas, DateTime inicio, DateTime fim);
    }
}

