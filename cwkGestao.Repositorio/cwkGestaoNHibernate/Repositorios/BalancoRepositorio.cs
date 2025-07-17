using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class BalancoRepositorio : RepositorioAbstrato<Balanco>, IRepositorioBalanco
    {
        protected override string HqlLoadById()
        {
            return @"from Balanco b 
                    left join fetch b.LocalEstoque le
                    left join fetch le.Filial 
                    left join fetch b.BalancoItems bi 
                    left join fetch bi.Produto p
                    left join fetch p.Grupo1
                    left join fetch p.Grupo2
                    left join fetch p.Grupo3
                    where b.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Balanco b join fetch b.LocalEstoque le join fetch le.Filial";
        }

        public Balanco LoadBalancoComNotas(int idBalanco)
        {
            using (var session = GetSession())
            {
                string sql = @"from Balanco b 
                            left join fetch b.LocalEstoque le
                            left join fetch le.Filial 
                            left join fetch b.BalancoItems bi 
                            left join fetch bi.Produto 
                            left join fetch bi.NotaItem ni
                            left join fetch ni.Nota n
                            where b.ID = :id";

                Balanco retorno = session.CreateQuery(sql).SetParameter("id", idBalanco).List<Balanco>().FirstOrDefault();

                return retorno;
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
