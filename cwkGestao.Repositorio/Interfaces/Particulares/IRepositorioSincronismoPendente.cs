using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioSincronismoPendente : IRepositorio<SincronismoPendente>
    {
        IList<SincronismoPendente> _GetPedidosPendentes();
        SincronismoPendente _GetPedidoPendente(int IDIntegracao);
    }
}

