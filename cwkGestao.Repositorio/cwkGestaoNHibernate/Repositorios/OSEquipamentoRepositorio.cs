using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;
using System.Reflection;
using cwkGestao.Modelo.Proxy;
using NHibernate.Transform;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class OSEquipamentoRepositorio : RepositorioAbstrato<OSEquipamento>, IRepositorioEquipamento
    {
        protected override string HqlLoadById()
        {
            return "select e from OSEquipamento e left join fetch e.OSTipoEquipamento where e.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from OSEquipamento e left join fetch e.OSTipoEquipamento";
        }

        public IList<object> GetListagemEquipamentos(TipoRelatorioEquipamento tipo)
        {
            using (var session = GetSession())
            {
                StringBuilder hql = new StringBuilder();
                hql.AppendLine("select e.ID");
                hql.AppendLine(", e.Codigo");
                hql.AppendLine(", e.Descricao");
                hql.AppendLine(", e.NumeroSerie");
                hql.AppendLine(", t.Descricao as TipoEquipamento");
                hql.AppendLine("from OSEquipamento e");
                hql.AppendLine("join e.OSTipoEquipamento t");
                hql.AppendLine("where 1 = 1");
                switch (tipo)
                {
                    case TipoRelatorioEquipamento.Codigo:
                        hql.AppendLine("order by e.Codigo");
                        break;
                    case TipoRelatorioEquipamento.Descricao:
                        hql.AppendLine("order by e.Descricao");
                        break;
                    case TipoRelatorioEquipamento.TipoEquipamento:
                        hql.AppendLine("order by t.Descricao, e.Descricao");
                        break;
                }

                IQuery query = session.CreateQuery(hql.ToString());

                ConstructorInfo construtor = typeof(pxyRelOSEquipamento).GetConstructor(new Type[] {
                typeof(int),
                typeof(int),
                typeof(string),
                typeof(string),
                typeof(string)
                });

                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<object>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
