using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class RotaDetalheRepositorio : RepositorioAbstrato<RotaDetalhe>, IRepositorioRotaDetalhe
    {
        protected override string HqlLoadById()
        {
            return "from RotaDetalhe n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from RotaDetalhe n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public int getMaxCodigo()
        {
            using (var session = GetSession())
            {
                string hql = @"select ISNUll(max(r.Codigo), 0) Codigo from RotaDetalhe r";

                var retorno = session.CreateSQLQuery(hql).UniqueResult();

                return Convert.ToInt32(retorno);
            }
        }

        public IList<RotaDetalhe> getListaDetalheRepetido(DateTime dtInicio, DateTime dtFim, int idResponsavel)
        {
            using (var session = GetSession())
            {
                StringBuilder str = new StringBuilder();
                str.AppendLine(" from RotaDetalhe rd ");
                str.AppendLine("left join fetch rd.Cliente c ");
                str.AppendLine("left join fetch rd.Rota r ");
                str.AppendLine("left join fetch r.Responsavel pr ");
                str.AppendLine("where rd.Data ");
                str.AppendLine("between :dtInicio and :dtFim ");
                str.AppendLine("and pr.ID = :idResponsavel");
                string hql = str.ToString();

                IList<RotaDetalhe> retorno = session.CreateQuery(hql).SetDateTime("dtInicio", dtInicio).SetDateTime("dtFim", dtFim).SetInt32("idResponsavel", idResponsavel).List<RotaDetalhe>();

                return retorno;
            }
        }
    }
}
