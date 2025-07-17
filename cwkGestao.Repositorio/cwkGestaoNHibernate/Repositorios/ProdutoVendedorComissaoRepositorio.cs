using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ProdutoVendedorComissaoRepositorio : RepositorioAbstrato<ProdutoVendedorComissao>, IRepositorioProdutoVendedorComissao
    {
        protected override string HqlLoadById()
        {
            throw new NotImplementedException();
        }

        protected override string hqlGetAll()
        {
            throw new NotImplementedException();
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IList<ProdutoVendedorComissao> GetComissaoProduto(Int32[] indicesProduto, Int32 IDVendedor)
        {
            using (var session = GetSession())
            {
                string hql = " from ProdutoVendedorComissao pvc" +
                             " left join fetch pvc.Produto p " +
                             " left join fetch pvc.Vendedor v " +
                             " where v.ID = :IDVendedor " +
                             " and p.ID in (:indices)";

                var query = session.CreateQuery(hql).SetInt32("IDVendedor", IDVendedor).SetParameterList("indices", indicesProduto);


                var notaItens = query.List<ProdutoVendedorComissao>();


                return notaItens;
            }
        }

        public IList<ProdutoVendedorComissao> GetComissaoGrupoEstoque(Int32[] indicesGrupoEstoque, Int32 IDVendedor)
        {
            using (var session = GetSession())
            {
                string hql = " from ProdutoVendedorComissao pvc" +
                             " left join fetch pvc.GrupoEstoque g " +
                             " left join fetch pvc.Vendedor v " +
                             " where v.ID = :IDVendedor " +
                             " and g.ID in (:indices)";


                var query = session.CreateQuery(hql).SetInt32("IDVendedor", IDVendedor).SetParameterList("indices", indicesGrupoEstoque);


                var notaItens = query.List<ProdutoVendedorComissao>();


                return notaItens;
            }
        }

        public IList<ProdutoVendedorComissao> GetComissaoServico(Int32[] indicesServico, Int32 IDVendedor)
        {
            using (var session = GetSession())
            {
                string hql = " from ProdutoVendedorComissao pvc" +
                         " left join fetch pvc.Servico s " +
                         " left join fetch pvc.Vendedor v " +
                         " where v.ID = :IDVendedor " +
                         " and s.ID in (:indices)";


                var query = session.CreateQuery(hql).SetInt32("IDVendedor", IDVendedor).SetParameterList("indices", indicesServico);


                var notaItens = query.List<ProdutoVendedorComissao>();


                return notaItens;
            }
        }
    }
}
