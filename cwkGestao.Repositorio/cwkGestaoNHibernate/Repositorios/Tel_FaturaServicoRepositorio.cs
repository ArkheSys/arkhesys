using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Tel_FaturaServicoRepositorio : RepositorioAbstrato<Tel_FaturaServico>, IRepositorioTel_FaturaServico
    {
        protected override string HqlLoadById()
        {
            return "from Tel_FaturaServico g where g.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Tel_FaturaServico g";
        }

        public IList<cwkGestao.Modelo.Tel_FaturaServico> GetServicos(int IDFatura)
        {
            using (var session = GetSession())
            {
                string hql = hqlGetAll() + " WHERE g.Fatura.ID = : IDFatura";

                return session.CreateQuery(hql).SetInt32("IDFatura", IDFatura).List<cwkGestao.Modelo.Tel_FaturaServico>();
            }
        }

        public IList<Tel_FaturaServico> GetByGrupoEReferencia(Tel_GrupoCliente grupoCliente, DateTime referencia)
        {
            using (var session = GetSession())
            {
                string hql = @"from Tel_FaturaServico t
                               join fetch t.Fatura f
                               join fetch f.Cliente c
                               where c.GrupoCliente.ID = :idGrupoCliente and f.Referencia = :referencia";

                return session.CreateQuery(hql)
                              .SetInt32("idGrupoCliente", grupoCliente.ID)
                              .SetDateTime("referencia", referencia)
                              .List<cwkGestao.Modelo.Tel_FaturaServico>();
            }
        }

        public IList<Tel_FaturaServico> GetByFaturas(IList<int> idsFaturas)
        {
            using (var session = GetSession())
            {
                StringBuilder hql = new StringBuilder();
                hql.AppendLine("from Tel_FaturaServico t");
                hql.AppendLine("join fetch t.Fatura f");
                hql.AppendLine("join fetch f.Cliente c");
                hql.AppendLine("where f.ID in (");
                for (int i = 0; i < idsFaturas.Count(); i++)
                {
                    if (i > 0)
                        hql.Append(", ");
                    hql.Append(idsFaturas[i]);
                }
                hql.Append(")");

                return session.CreateQuery(hql.ToString())
                              .List<cwkGestao.Modelo.Tel_FaturaServico>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
