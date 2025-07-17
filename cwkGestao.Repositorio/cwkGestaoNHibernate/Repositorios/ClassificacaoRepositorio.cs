using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ClassificacaoRepositorio : RepositorioAbstrato<Classificacao>, IRepositorioClassificacao
    {
        protected override string HqlLoadById()
        {
            return "from Classificacao where ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Classificacao";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
