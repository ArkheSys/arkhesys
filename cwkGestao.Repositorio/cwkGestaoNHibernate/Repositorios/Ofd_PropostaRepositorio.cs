using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Ofd_PropostaRepositorio : RepositorioAbstrato<Ofd_Proposta>, IRepositorioOfd_Proposta
    {
        protected override string HqlLoadById()
        {
            return "from Ofd_Proposta n left join fetch n.Obra o where n.ID = :id";
        }

        public override Ofd_Proposta LoadObjectById(int id)
        {
            using (var session = GetSession())
            {
                string sql = HqlLoadById();
                var retorno = session.CreateQuery(sql).SetParameter("id", id).List<Ofd_Proposta>().FirstOrDefault();
                if (retorno != null)
                    NHibernateUtil.Initialize(retorno.Arquivos);
                return retorno;
            }
        }

        public Ofd_Proposta LoadObjectByIdWithMedicoes(int id)
        {
            using (var session = GetSession())
            {
                string sql = HqlLoadById();
                var retorno = session.CreateQuery(sql).SetParameter("id", id).List<Ofd_Proposta>().FirstOrDefault();
                if (retorno != null)
                {
                    NHibernateUtil.Initialize(retorno.Arquivos);
                    NHibernateUtil.Initialize(retorno.Medicoes);
                }
                return retorno;
            }
        }

        protected override string hqlGetAll()
        {
            return "from Ofd_Proposta n left join fetch n.Servico s ";
        }

        public IList<Ofd_Proposta> GetAllByObra(Ofd_Obra obra)
        {
            using (var session = GetSession())
            {
                var hql = @"from Ofd_Proposta p 
                            join fetch p.Obra a                             
                            where p.Obra.ID = :idObra";
                return session.CreateQuery(hql).SetInt32("idObra", obra.ID).List<Ofd_Proposta>();
            }
        }

        public Object GetMaxNumMedicao(int prop)
        {
            Object retorno = new Object();
            int medicao = 0;
            using (var session = GetSession())
            {
                StringBuilder sql = new StringBuilder();
                sql.Append(" select MAX(pm.Medicao) " +
                "from Ofd_PropostaMedicao pm " +
                "where PM.IDOfd_Medicao is not null " +
                "and pm.IDProposta = :idproposta");
                medicao = Convert.ToInt32(session.CreateSQLQuery(sql.ToString()).SetInt32("idproposta", prop).List<object>().FirstOrDefault<object>()) + 1;
                retorno = (Object)medicao;
                return retorno;
            }
        }

        public int GetUltimoID_Ofd_Proposta()
        {
            using (var session = GetSession())
            {
                string hql = @"select isnull(MAX(p.CodigoOrcamento), 1) from Ofd_Proposta p
                             where p.TipoLancamento = 1";
                int retorno = session.CreateSQLQuery(hql).List<int>().FirstOrDefault();
                return retorno;
            }
        }
        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

    }
}
