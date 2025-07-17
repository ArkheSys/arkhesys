using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioCondicao : IRepositorio<Condicao>
    {
        Condicao GetByCodigo(int codigo);
        Condicao GetAVista(int id);
    }
}

