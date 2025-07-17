using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class TabelaFaixaFatSimplesRepositorio : RepositorioAbstrato<TabelaFaixaFatSimples>, IRepositorioTabelaFaixaFatSimples
    {
        protected override string HqlLoadById()
        {
            return "from TabelaFaixaFatSimples where ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from TabelaFaixaFatSimples";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
