using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System.Reflection;
using cwkGestao.Modelo.Proxy;
using NHibernate.Transform;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ServicoRepositorio : RepositorioAbstrato<Servico>, IRepositorioServico
    {
        protected override string HqlLoadById()
        {
            return "from Servico where ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Servico";
        }

        public override int MaxCodigo()
        {
            using (var session = GetSession())
            {
                try
                {
                    return (from t in new NHibernate.Linq.NhQueryable<Servico>(session) select t.Codigo).Max() + 1;
                }
                catch (Exception)
                {
                    return 1;
                }
            }
        }

        public Servico GetByCodigo(int codigo)
        {
            using (var session = GetSession())
            {
                string hql = "from Servico where Codigo = :codigo";

                return session.CreateQuery(hql).SetInt32("codigo", codigo).UniqueResult<Servico>();
            }
        }

        public IList<object> GetByInicial(string inicial, string final)
        {
            using (var session = GetSession())
            {
                string getAll = @"select 
                                      s.Codigo as Codigo
                                    , s.Nome as Nome
                                    , s.Valor as Valor
                                    , si.Nome as ImpostoNome
                                    , si.Aliquota as ImpostoAliquota
                                from Servico s
                                left join s.ServicoImpostos ssi
                                left join ssi.ServicoImposto si
                                where 1 = 1";

                if (!String.IsNullOrEmpty(inicial))
                {
                    getAll += " AND s.Nome >= '" + inicial + "'";
                }

                if (!String.IsNullOrEmpty(final))
                {
                    getAll += " AND s.Nome <= '" + final + "'";
                }

                ConstructorInfo construtor =
                    typeof(pxyRelImpostosPorServico).GetConstructor(new Type[]
                                                                         {
                                                                             typeof (Int32), typeof (string),
                                                                             typeof (decimal), typeof (string),
                                                                             typeof (decimal)
                                                                         });

                return
                    session.CreateQuery(getAll).SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).
                        List<object>();
            }
        }

        public IList<Modelo.Servico> GetServicosPorIDOfd_Proposta(int IDOfd_Proposta)
        {
            using (var session = GetSession())
            {
                string hql = "select s from Servico s " +
                             "left join fetch s.Ofd_Servicos os " +
                             "left join fetch os.Ofd_Proposta p " +
                             " where p.ID = :IDOfd_Proposta";
                return session.CreateQuery(hql).SetInt32("IDOfd_Proposta", IDOfd_Proposta).List<Modelo.Servico>();
            }
        }
        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

    }
}
