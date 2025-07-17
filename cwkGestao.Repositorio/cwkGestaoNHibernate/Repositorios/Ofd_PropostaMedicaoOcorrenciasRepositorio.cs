using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Ofd_PropostaMedicaoOcorrenciasRepositorio : RepositorioAbstrato<Ofd_PropostaMedicaoOcorrencias>, IRepositorioOfd_PropostaMedicaoOcorrencias
    {
        protected override string HqlLoadById()
        {
            return "from Ofd_PropostaMedicaoOcorrencias n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Ofd_PropostaMedicaoOcorrencias n";
        }



        #region IRepositorioOfd_PropostaMedicaoOcorrencias Members

        public IList<Ofd_PropostaMedicaoOcorrencias> GetByPropostaMedicao(int idPropostaMedicao)
        {
            using (var session = GetSession())
            {
                var hql = @"SELECT oPm
                            from Ofd_PropostaMedicao of
                            LEFT JOIN FETCH of.Ofd_PropostaMedicaoOcorrencias oPm
                            where of.ID = :idPropostaMedicao";
                return session.CreateQuery(hql)
                                   .SetInt32("idPropostaMedicao", idPropostaMedicao)
                                   .List<Ofd_PropostaMedicaoOcorrencias>();
            }
        }

        #endregion

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
