using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class FechamentoCaixaRepositorio : RepositorioAbstrato<FechamentoCaixa>, IRepositorioFechamentoCaixa
    {
        protected override string HqlLoadById()
        {
            return "from FechamentoCaixa n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from FechamentoCaixa n";
        }

        public bool HqlPodeFecharCaixa(DateTime data, Banco banco)
        {
            data = data.AddDays(-1);
            using (var session = GetSession())
            {
                string hql = "from FechamentoCaixa fc " +
                             "JOIN FETCH fc.Banco b " +
                             "where fc.DtConciliacao = :dt and fc.bFechado = true and b.ID =" + banco.ID;
                var resultado = session.CreateQuery(hql).SetDateTime("dt", data).UniqueResult();

                return resultado == null ? false : true;
            }
        }
        public bool HqlPodeAbrirCaixa(DateTime data, Banco banco)
        {
            data = data.AddDays(+1);
            using (var session = GetSession())
            {
                string hql = "from FechamentoCaixa fc " +
                             "JOIN FETCH fc.Banco b " +
                             "where fc.DtConciliacao = :dt and bFechado = true and b.ID =" + banco.ID;
                var resultado = session.CreateQuery(hql).SetDateTime("dt", data).UniqueResult();

                return resultado == null ? true : false;
            }
        }

        public bool HqlExisteFechamentoPosterior(DateTime data, Banco banco)
        {
            using (var session = GetSession())
            {
                string hql = "from FechamentoCaixa fc " +
                             "JOIN FETCH fc.Banco b " +
                             "where fc.DtConciliacao > :dt and bFechado = true and b.ID =" + banco.ID;
                var resultado = session.CreateQuery(hql).SetDateTime("dt", data).List<FechamentoCaixa>();

                return resultado.Count == 0 ? true : false;
            }
        }

        public FechamentoCaixa HqlGetFechamentoPorData(DateTime data, Banco banco)
        {
            using (var session = GetSession())
            {
                string hql = "from FechamentoCaixa fc " +
                             "JOIN FETCH fc.Banco b " +
                             "where fc.DtConciliacao = :dt and b.ID =" + banco.ID;
                return session.CreateQuery(hql).SetDateTime("dt", data).List<FechamentoCaixa>().First();
            }
        }

        public IList<FechamentoCaixa> GetBancoPeriodo(Banco banco, DateTime dtInicio, DateTime dtFinal)
        {
            DateTime dtIn = DateTime.Parse(dtInicio.ToShortDateString());
            DateTime dtFn = DateTime.Parse(dtFinal.ToShortDateString());
            using (var session = GetSession())
            {
                string hql = @"  FROM FechamentoCaixa fc
                                 INNER JOIN FETCH fc.Banco b 
                                 WHERE fc.DtConciliacao <= :dtFinal
                                       AND fc.DtConciliacao >= :dtInicio 
                                       AND b.ID = :bancoID";
                return session.CreateQuery(hql).SetDateTime("dtFinal", dtFn)
                    .SetDateTime("dtInicio", dtIn).SetInt32("bancoID", banco.ID).List<FechamentoCaixa>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
