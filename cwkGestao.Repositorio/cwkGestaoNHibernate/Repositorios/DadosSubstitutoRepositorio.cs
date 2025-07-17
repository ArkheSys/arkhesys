using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class DadosSubstitutoRepositorio : RepositorioAbstrato<DadosSubstituto>, IRepositorioDadosSubstituto
    {
        protected override string HqlLoadById()
        {
            return "from DadosSubstituto n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from DadosSubstituto n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IList<DadosSubstituto> GetByIDFilial(int IDFilial)
        {
            using (var session = GetSession())
            {
                try
                {
                    string hql = @"from DadosSubstituto d 
                                   left join fetch d.Filial f
                                   where f.ID = :IDFilial";
                    IList<DadosSubstituto> DadosSubstituicao = session.CreateQuery(hql).SetInt32("IDFilial", IDFilial).List<DadosSubstituto>();
                    return DadosSubstituicao;
                }
                catch (Exception ex)
                {
                    throw ex;
                }

            }
        }
    }
}
