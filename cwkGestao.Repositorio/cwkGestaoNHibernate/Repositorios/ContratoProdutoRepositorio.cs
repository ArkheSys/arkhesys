using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ContratoProdutoRepositorio : RepositorioAbstrato<ContratoProduto>, IRepositorioContratoProduto
    {
        protected override string HqlLoadById()
        {
            return "from ContratoProduto n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from ContratoProduto n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IList<ContratoProduto> GetItensContratoParaFaturar(DateTime referencia, Int32 diavcto)
        {
            using (var session = GetSession())
            {
                if (diavcto != 0)
                {
                    string hql = "FROM ContratoProduto cp" +
                                   " LEFT JOIN FETCH cp.Contrato c " +
                                   " WHERE cp.DataInicioFaturamento <= :referencia" +
                                   " AND (cp.DataCancelamento > :referencia OR cp.DataCancelamento is NULL)" +
                                   " AND (cp.DiaVencimento = :diavcto)";
                    var query = session.CreateQuery(hql);
                    query.SetDateTime("referencia", referencia);
                    query.SetInt32("diavcto", diavcto);
                    IList<ContratoProduto> itensContrato = query.List<ContratoProduto>();
                    return itensContrato;
                }
                else
                {
                    string hql = "FROM ContratoProduto cp" +
                                    " LEFT JOIN FETCH cp.Contrato c " +
                                    " WHERE cp.DataInicioFaturamento <= :referencia" +
                                    " AND (cp.DataCancelamento > :referencia OR cp.DataCancelamento is NULL)";
                    var query = session.CreateQuery(hql);
                    query.SetDateTime("referencia", referencia);

                    IList<ContratoProduto> itensContrato = query.List<ContratoProduto>();
                    return itensContrato;
                }
            }
        }
    }
}
