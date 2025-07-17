using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using NHibernate;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class CondicaoRepositorio : RepositorioAbstrato<Condicao>, IRepositorioCondicao
    {
        protected override string HqlLoadById()
        {
            return "from Condicao c left join fetch c.CondicaoParcelas where c.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Condicao c ";
        }

        public Condicao GetByCodigo(int codigo)
        {
            using (var session = GetSession())
            {
                try
                {
                    string hql = "from Condicao c left join fetch c.CondicaoParcelas where c.Codigo = :codigo";
                    Condicao condicao = session.CreateQuery(hql).SetInt32("codigo", codigo).UniqueResult<Condicao>();
                    return condicao;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
               
            }
        }

        public Condicao GetAVista(int id)
        {
            using (var session = GetSession())
            {
                try
                {
                    string hql = "from Condicao c left join fetch c.CondicaoParcelas where c.ID = :id or (c.Nome like '%vista' and c.QtParcela = 1)";
                    Condicao condicao = session.CreateQuery(hql).SetInt32("id", id).UniqueResult<Condicao>();
                    return condicao;
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }


        public override Condicao LoadObjectById(int id)
        {
            using (var session = GetSession())
            {
                Condicao objeto = session.Get<Condicao>(id);
                if(objeto != null)
                    NHibernateUtil.Initialize(objeto.CondicaoParcelas);
               

                return objeto;
            }
        }

        public override int MaxCodigo()
        {
            using (var session = GetSession())
            {
                return session.CreateQuery("select max(c.Codigo) from Condicao c").UniqueResult<int>() + 1;
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
