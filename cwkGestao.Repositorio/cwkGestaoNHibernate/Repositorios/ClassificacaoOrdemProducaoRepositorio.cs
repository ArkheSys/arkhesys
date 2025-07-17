using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ClassificacaoOrdemProducaoRepositorio : RepositorioAbstrato<ClassificacaoOrdemProducao>, IRepositorioClassificacaoOrdemProducao
    {
        protected override string HqlLoadById()
        {
            return "from ClassificacaoOrdemProducao c where c.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from ClassificacaoOrdemProducao c ";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
