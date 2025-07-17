using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioContatoDetalhe : IRepositorio<ContatoDetalhe>
    {
        IList<ContatoDetalhe> GetPorIDContato(int IDContato);
    }
}

