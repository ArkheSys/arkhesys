using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioCodigoImposto : IRepositorio<CodigoImposto>
    {
        IList<CodigoImposto> GetAllPorTipoImposto(string p);
    }
}

