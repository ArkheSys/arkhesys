using System;
using System.Collections.Generic;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioMovimento : IRepositorio<Movimento>
    {
        IList<Movimento> GetRelatorioPorPlanoContas(DateTime dataInicial, DateTime dataFinal, string classificacao, string idsEmpresas, bool filtrarPorDataCompetencia);
        int GetNovoCodigoMovimento();
    }
}

