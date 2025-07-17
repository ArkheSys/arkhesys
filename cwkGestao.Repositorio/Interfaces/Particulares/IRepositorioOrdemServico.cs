using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioOrdemServico : IRepositorio<OrdemServico>
    {
        IList<cwkGestao.Modelo.Proxy.pxyGridOrdemServico> GetAllGrid();

        IList<object> OrdemServicoRel(int idRevenda, int idResponsavel, int idStatus, DateTime DataInicial, DateTime DataFinal);
    }
}
