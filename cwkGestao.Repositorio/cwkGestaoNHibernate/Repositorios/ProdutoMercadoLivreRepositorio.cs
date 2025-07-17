using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ProdutoMercadoLivreRepositorio : RepositorioAbstrato<ProdutoMercadoLivre>, IRepositorioProdutoMercadoLivre
    {
        protected override string HqlLoadById()
        {
            return "from ProdutoMercadoLivre n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from ProdutoMercadoLivre n";
        }

        public Produto LoadProdutoByIDMercadoLivre(int id)
        {
            using (var session = GetSession())
            {
                string hql = @"SELECT pml.Produto
                               FROM ProdutoMercadoLivre pml
                               WHERE pml.MercadoLivre = :id and bAtivo = true";
                return session.CreateQuery(hql).SetInt32("id", id).List < Produto>().FirstOrDefault();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
