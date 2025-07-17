using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioCw_usuario : IRepositorio<Cw_usuario>
    {
        Cw_usuario FindByLogin(string login);

    }
}

