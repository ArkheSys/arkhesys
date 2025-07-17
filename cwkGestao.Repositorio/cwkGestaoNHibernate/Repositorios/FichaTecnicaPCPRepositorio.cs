using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class FichaTecnicaPCPRepositorio : RepositorioAbstrato<FichaTecnicaPCP>, IRepositorioFichaTecnicaPCP
    {
        protected override string HqlLoadById()
        {
            return "from FichaTecnicaPCP n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from FichaTecnicaPCP n";
        }

        public IList<FichaTecnicaPCP> GetImpressao()
        {
            IList<FichaTecnicaPCP> lista = new List<FichaTecnicaPCP>();
           
            using (var session = GetSession())
            {
                string hql = "FROM FichaTecnicaPCP f LEFT JOIN FETCH f.Produto";

                return session.CreateQuery(hql).List<FichaTecnicaPCP>();
            }
        }

        public IList<FichaTecnicaPCP> GetAllPorIDProduto(int id)
        {
            IList<FichaTecnicaPCP> lista = new List<FichaTecnicaPCP>();
            try
            {
                using (var session = GetSession())
                {
                    string hql = "FROM FichaTecnicaPCP f LEFT JOIN FETCH f.Produto p where p.ID = :id";

                    lista = session.CreateQuery(hql).SetInt32("id", id).List<FichaTecnicaPCP>();
                }
            }
            catch (Exception q)
            {
                
                throw q;
            }
            return lista;
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
