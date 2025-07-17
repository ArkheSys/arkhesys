using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioClienteRevenda : IRepositorio<ClienteRevenda>
    {
        IList<ClienteRevenda> GetAllByPessoa(int pIdPessoa);
        IList<ClienteRevenda> GetAllByMatriz(int pIdMatriz);

    }
}
