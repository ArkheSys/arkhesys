using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioPlanoContaReferencial : IRepositorio<PlanoContaReferencial>
    {
        IList<PlanoContaReferencial> RetornarTodos();

        PlanoContaReferencial HqlLoadByCode(int p);
    }
}
