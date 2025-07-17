using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class TabelaImpostosTributosExcessoesRepositorio : RepositorioAbstrato<TabelaImpostosTributosExcessoes>, IRepositorioTabelaImpostosTributosExcessoes
    {
        protected override string HqlLoadById()
        {
            return "from TabelaImpostosTributosExcessoes where ID = :id";
        }

        //public Produto GetProdutoByCodigo(String codigo)
        //{
        //    using (var session = GetSession())
        //    {
        //        string hql = "from Produto p " +
        //                     " left join fetch p.Unidade " +
        //                     " where p.Codigo = :codigo ";

        //        var produto = session.CreateQuery(hql).SetString("codigo", codigo).UniqueResult<Produto>();
        //        if (produto != null)
        //        {
        //            NHibernateUtil.Initialize(produto.Estoques);
        //            NHibernateUtil.Initialize(produto.ProdutoCodBarra);
        //        }

        //        return produto;
        //    }
        //}




        protected override string hqlGetAll()
        {
            throw new NotImplementedException();
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}

