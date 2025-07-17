using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioTel_ClienteTelefone : IRepositorio<Tel_ClienteTelefone>
    {
        IList<Tel_ClienteTelefone> GetAtivosByCliente(Tel_Cliente pessoa);
    }
}

