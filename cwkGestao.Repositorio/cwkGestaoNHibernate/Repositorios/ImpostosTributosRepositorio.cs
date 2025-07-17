using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ImpostosTributosRepositorio : RepositorioAbstrato<ImpostosTributos>, IRepositorioImpostosTributos
    {
        protected override string hqlGetAll()
        {
            return "from ImpostosTributos it";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        protected override string HqlLoadById()
        {
            return "from ImpostosTributos i where i.ID = :id";
        }

        public ImpostosTributos GetByClassificacaoFiscal(int pIDClassificacaoFiscal)
        {
            using (var session = GetSession())
            {
                //string hql = "select i from ImpostosTributos i where i.IDClassificacaoFiscal = :pIDClassificacaoFiscal";

                string hql = " from ImpostosTributos it " +
                " left join fetch it.ClassificacaoFiscal cf " +
                " where cf.ID = :IDClassificacaoFiscal ";

                var result = session.CreateQuery(hql).SetInt32("IDClassificacaoFiscal", pIDClassificacaoFiscal).List<ImpostosTributos>();

                if (result.Count > 0)
                    return result[0];
            }
            return null;
        }
    }
}
