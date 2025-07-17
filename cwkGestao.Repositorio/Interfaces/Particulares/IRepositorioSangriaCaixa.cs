using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using System;
using System.Collections.Generic;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioSangriaCaixa : IRepositorio<SangriaCaixa>
    {
        decimal GetSangriaVendasComSangriaESuprimentos(int IDUsuario, int IDFluxoCaixa);

        List<SangriaCaixa> GetSangriasPorFluxoCaixa(int IDFluxoCaixa);

        List<FrenteCaixaNota> GetNotasPorFluxoCaixa(int IDFluxoCaixa);

        IList<pxFrenteCaixaNota> GetRateiosPorFormaDePagamento(int IDFluxoCaixa);

        IList<pxSangriaCaixa> GetSangrias(DateTime dtInicio, DateTime dtFinal);
        pxyDetalhesFechamento GetDetalhesFechamento(int IDFluxoCaixa);
        SangriaCaixa GetSangria(int IDSangriaCaixa);
    }
}
