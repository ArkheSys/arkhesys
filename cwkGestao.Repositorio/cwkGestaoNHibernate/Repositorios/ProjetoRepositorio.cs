using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{

    partial class ProjetoRepositorio : RepositorioAbstrato<Projeto>, IRepositorioProjeto
    {
        protected override string HqlLoadById()
        {
            return @"from Projeto p
                    left join fetch p.Filial
                    left join fetch p.Banco
                    left join fetch p.Condicao
                    left join fetch p.Portador
                    left join fetch p.Historico
                    left join fetch p.PlanoConta
                    left join fetch p.PlanoContaServico
                    left join fetch p.Acrescimo
                    where p.id = :id ";
        }

        protected override string hqlGetAll()
        {
            return @"from Projeto p ";
        }

        public IList<Projeto> GetCentrosDeCusto()
        {        
            using (var session = GetSession())
            {
                string hql = hqlGetAll() + " where p.Pessoa is null";
                HashSet<Projeto> set = new HashSet<Projeto>(session.CreateQuery(hql).List<Projeto>());

                return set.ToList<Projeto>();
            }
        }

        public IList<Projeto> GetCentrosDeCustoEProjetosAprovados()
        {
            using (var session = GetSession())
            {
                String hql = "from Projeto p left join fetch p.Pessoa pes left join fetch p.OrdemProducaos op where status = 2 or pes is null ";
                IQuery query = session.CreateQuery(hql);
                return query.List<Projeto>().Distinct().ToList();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
