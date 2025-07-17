using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioContato : IRepositorio<Contato>
    {
        pxyAvisosPorPeriodo GetAvisosPorPeriodo(int IDUsuario);
        pxyAvisos GetAvisosAnteriores(int IDUsuario);
        pxyAvisos GetAvisosPosteriores(int IDUsuario);
    }
}

