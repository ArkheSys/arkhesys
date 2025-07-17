using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioPessoaCliente : IRepositorio<PessoaCliente>
    {
        IList<Modelo.Pessoa> GetRevendas();
    }
}

