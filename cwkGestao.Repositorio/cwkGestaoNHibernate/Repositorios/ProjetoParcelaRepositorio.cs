using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ProjetoParcelaRepositorio : RepositorioAbstrato<ProjetoParcela>, IRepositorioProjetoParcela
    {
        protected override string HqlLoadById()
        {
            throw new NotImplementedException();
        }

        protected override string hqlGetAll()
        {
            return "from ProjetoParcela p";
        }

        public IList<ProjetoParcela> GetParcelasPorProjeto(Projeto proj)
        {
            using (var session = GetSession())
            {
                string hql = "from ProjetoParcela f left join fetch f.TipoDocumento where f.Projeto.ID = " + proj.ID;
                var resultado = session.CreateQuery(hql).List<ProjetoParcela>();
                return resultado;
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
