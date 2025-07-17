using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioFechamentoCaixaRateio : IRepositorio<FechamentoCaixaRateio>
    {
        IList<pxFechamentoCaixaRateio> GetRateiosPorFluxoDeCaixa(int IDFluxoCaixa);
    }
}