using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using System.Collections.Generic;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioFluxoCaixa : IRepositorio<FluxoCaixa>
    {
        FluxoCaixa GetByUsuario(int IDUsuario);

        FluxoCaixa GetFluxoDeCaixa(int IDFluxoCaixa);

        IList<pxFluxoCaixa> GetFluxosCaixa();
    }
}
