using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioTipoNota : IRepositorio<TipoNota>
    {
        IList<TipoNota> GetAllPorCategoria(Enum categoria);
    }
}

