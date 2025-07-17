using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ContratoControleRepositorio : RepositorioAbstrato<ContratoControle>, IRepositorioContratoControle
    {
        protected override string HqlLoadById()
        {
            return "from ContratoControle n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from ContratoControle n";
        }

        public IList<ContratoControle> GetRegistrosContratosFaturados(int idContrato, int tpProdutoServico, int idItemContrato)
        {
            try
            {

                using (var session = GetSession())
                {
                    string hql = " FROM ContratoControle cc" +
                                        " LEFT JOIN FETCH cc.Contrato c " +
                                        " LEFT JOIN FETCH cc.ContratoServico cs " +
                                        " LEFT JOIN FETCH cc.ContratoProduto cp " +
                                        " WHERE cc.Contrato.ID = :idContrato " +
                                        " AND cc.bServico = :tpProdutoServico ";

                    if (tpProdutoServico == 0)
                    {
                        //Produto
                        hql += "  AND cc.ContratoProduto.ID = :idItemContrato ";
                    }
                    else
                    {
                        // Servico
                        hql += "  AND cc.ContratoServico.ID = :idItemContrato ";
                    }

                    var query = session.CreateQuery(hql);
                    query.SetInt32("idContrato", idContrato).SetInt32("tpProdutoServico", tpProdutoServico).SetInt32("idItemContrato", idItemContrato);
                    IList<ContratoControle> contratosFaturados = query.List<ContratoControle>();
                    return contratosFaturados;
                }
            }
            catch (Exception)
            {

                throw;
            }
        }

        public ContratoControle LoadObjectByIDNotaItem(int idNotaItem)
        {
            using (var session = GetSession())
            {
                string hql = " FROM ContratoControle cc" +
                                    " LEFT JOIN FETCH cc.NotaItem ni " +
                                    " WHERE ni.ID = :idNotaItem";

                var query = session.CreateQuery(hql).SetInt32("idNotaItem", idNotaItem);
                ContratoControle contrControle = query.List<ContratoControle>().FirstOrDefault();
                return contrControle;
            }
        }

        public ContratoControle LoadObjectByIDNotaItemServico(int idNotaItemServico)
        {
            using (var session = GetSession())
            {
                string hql = " FROM ContratoControle cc" +
                                    " LEFT JOIN FETCH cc.NotaItemServico nis " +
                                    " WHERE nis.ID = :idNotaItemServico";

                var query = session.CreateQuery(hql).SetInt32("idNotaItemServico", idNotaItemServico);
                ContratoControle contrControle = query.List<ContratoControle>().FirstOrDefault();
                return contrControle;
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
