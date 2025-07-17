using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ProdutoServicoRepositorio : RepositorioAbstrato<ProdutoServico>, IRepositorioProdutoServico
    {
        protected override string HqlLoadById()
        {
            return "from ProdutoServico n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from ProdutoServico n";
        }


        public IList<Produto> GetAllProdutoPorIDServico(int idServico)
        {
            List<Produto> listaRetorno = new List<Produto>();
            try
            {
                using (var session = GetSession())
                {
                    string hql = @"from ProdutoServico ps 
                                   join fetch ps.Produto p 
                                   join fetch ps.Servico s 
                                   where s.id = :idServico";
                    IList<ProdutoServico> result = session.CreateQuery(hql).SetInt32("idServico", idServico).List<ProdutoServico>();
                    listaRetorno.AddRange(result.Select(s => s.Produto));
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return listaRetorno;
        }

        public ProdutoServico GetSelecionadoPorIDProdutoEIDServico(int idProduto, int idServico)
        {
            ProdutoServico retorno;
            try
            {
                using (var session = GetSession())
                {
                    string hql = @"from ProdutoServico ps 
                                   join fetch ps.Produto p 
                                   join fetch ps.Servico s 
                                   where s.id = :idServico
                                   and p.ID = :idProduto";
                    retorno = session.CreateQuery(hql).SetInt32("idServico", idServico).SetInt32("idProduto", idProduto).List<ProdutoServico>().FirstOrDefault();
                }
            }
            catch (Exception e)
            {
                throw e;
            }
            return retorno;
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
