using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioEntrega : IRepositorio<Entrega>
    {
        IList<pxyEntregas> GetPorAllPorIDPedido(int IDPedido);
    }
}

