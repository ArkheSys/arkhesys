using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class FichaTecnicaPCPItemRepositorio : RepositorioAbstrato<FichaTecnicaPCPItem>, IRepositorioFichaTecnicaPCPItem
    {
        protected override string HqlLoadById()
        {
            return "from FichaTecnicaPCPItem n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from FichaTecnicaPCPItem n";
        }

        public IList<FichaTecnicaPCP> GetFichaTecnicaByIdProduto(int IdProduto)
        {
            using (var session = GetSession())
            {
                string hql = "from FichaTecnicaPCP n where n.Produto = :IdProduto";

                return session.CreateQuery(hql).SetInt32("IdProduto", IdProduto).List<FichaTecnicaPCP>();
            }
        }

        public IList<FichaTecnicaPCPItem> GetFichaTecnicaPCPItensVinculadosComFichaTecnica(int IDFichaTecnicaPCP)
        {
            IList<FichaTecnicaPCPItem> lista = new List<FichaTecnicaPCPItem>();
            using (var session = GetSession())
            {
                string hql = "from FichaTecnicaPCPItem i where i.FichaTecnicaPCPFilha.ID = :IDFichaTecnicaPCP";
                try
                {
                    lista = session.CreateQuery(hql).SetInt32("IDFichaTecnicaPCP", IDFichaTecnicaPCP).List<FichaTecnicaPCPItem>();
                }
                catch (Exception z)
                {
                    
                    throw z;
                }
            }
            return lista;
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
