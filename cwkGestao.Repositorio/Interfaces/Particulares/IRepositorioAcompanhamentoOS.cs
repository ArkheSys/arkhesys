using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioAcompanhamentoOS : IRepositorio<AcompanhamentoOS>
    {
        IList<cwkGestao.Modelo.Proxy.pxyAcompanhamentoOS> GetAcompanhamentoOS(int idOrdemServico);
    }
}
