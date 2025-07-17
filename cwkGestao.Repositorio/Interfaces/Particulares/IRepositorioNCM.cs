using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioNCM : IRepositorio<NCM>
    {
        NCM GetByNcm(string ncm);
    }
}

