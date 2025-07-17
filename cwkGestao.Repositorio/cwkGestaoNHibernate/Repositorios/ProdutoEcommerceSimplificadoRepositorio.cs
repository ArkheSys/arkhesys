using System;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ProdutoEcommerceSimplificadoRepositorio : RepositorioAbstrato<ProdutoEcommerceSimplificado>, IRepositorioProdutoEcommerceSimplificado
    {
        protected override string HqlLoadById()
        {
            return "select t from ProdutoEcommerceSimplificado t where t.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from ProdutoEcommerceSimplificado t";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            return null;
        }
    }
}