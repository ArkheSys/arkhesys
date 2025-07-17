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
    partial class ServicoImpostoRepositorio : RepositorioAbstrato<ServicoImposto>, IRepositorioServicoImposto
    {
        protected override string HqlLoadById()
        {
            return "from ServicoImposto si where si.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from ServicoImposto si ";
        }

        public IList<object> GetByInicial(string inicial, string final)
        {
            using (var session = GetSession())
            {
                string getAll = @"select 
                                      si.Codigo
                                    , si.Nome
                                    , si.Aliquota
                                    , pc.Nome
                                from ServicoImposto si join si.PlanoConta pc
                                where 1 = 1";

                if (!String.IsNullOrEmpty(inicial))
                {
                    getAll += " AND si.Nome >= '" + inicial + "'";
                }
                if (!String.IsNullOrEmpty(final))
                {
                    getAll += " AND si.Nome <= '" + final + "'";
                }
                getAll += " order by si.Nome";

                ConstructorInfo construtor =
                    typeof(pxyRelServicoImposto).GetConstructor(new Type[]
                                                                         {
                                                                             typeof (Int32), typeof (string),
                                                                             typeof (decimal), typeof (string)
                                                                         });

                return
                    session.CreateQuery(getAll).SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).
                        List<object>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
