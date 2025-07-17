using System;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ProdutoAnexoRepositorio : RepositorioAbstrato<ProdutoAnexo>, IRepositorioProdutoAnexo
    {
        protected override string HqlLoadById()
        {
            return "select distinct p from ProdutoAnexo p where p.ID = :id";
        }

        public override ProdutoAnexo LoadObjectById(int id)
        {
            return GetSession().Get<ProdutoAnexo>(id);
        }

        protected override string hqlGetAll()
        {
            return "select distinct p from ProdutoAnexo p";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            return null;
        }
    }
}
