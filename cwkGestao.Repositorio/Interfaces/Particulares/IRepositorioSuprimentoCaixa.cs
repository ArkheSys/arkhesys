using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using System;
using System.Collections.Generic;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioSuprimentoCaixa : IRepositorio<SuprimentoCaixa>
    {
        decimal GetSuprimentoVendasComSuprimento(int IDUsuario, int IDFluxoCaixa);

        List<SuprimentoCaixa> GetSuprimentosPorFluxoCaixa(int IDFluxoCaixa);

        SuprimentoCaixa GetSuprimento(int IDSuprimentoCaixa);

        IList<pxSuprimentoCaixa> GetSuprimentos(DateTime dtInicio, DateTime dtFinal);
    }
}
