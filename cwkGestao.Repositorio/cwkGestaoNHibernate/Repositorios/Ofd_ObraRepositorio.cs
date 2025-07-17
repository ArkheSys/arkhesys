using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Ofd_ObraRepositorio : RepositorioAbstrato<Ofd_Obra>, IRepositorioOfd_Obra
    {
        protected override string HqlLoadById()
        {
            return "from Ofd_Obra o left join fetch o.Diarios where o.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Ofd_Obra n";
        }

        public override Ofd_Obra LoadObjectById(int id)
        {
            using (var session = GetSession())
            {
                string sql = HqlLoadById();
                var retorno = session.CreateQuery(sql).SetParameter("id", id).List<Ofd_Obra>().FirstOrDefault();
                if (retorno != null)
                    NHibernateUtil.Initialize(retorno.Arquivos);
                return retorno;
            }
        }

        public Ofd_Obra LoadObjectByIdWithMedicoes(int id)
        {
            using (var session = GetSession())
            {
                string sql = HqlLoadById();
                var retorno = session.CreateQuery(sql).SetParameter("id", id).List<Ofd_Obra>().FirstOrDefault();
                if (retorno != null)
                {
                    NHibernateUtil.Initialize(retorno.Arquivos);
                }
                return retorno;
            }
        }

        public IList<Servico> GetServicosPorObra(int IDObra)
        {
            IList<Servico> result = new List<Servico>();
            if (IDObra > 0)
            {
                using (var session = GetSession())
                {
                    string hql = "select s from Ofd_Proposta prop "
                                + "left join prop.Obra o "
                                + "left join prop.Servico s where o.ID = :id";
                    try
                    {
                        result = session.CreateQuery(hql).SetInt32("id", IDObra).List<Servico>();
                    }
                    catch (Exception e)
                    {
                        throw e;
                    }
                }
            }
            return result;
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
