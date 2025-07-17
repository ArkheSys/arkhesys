using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ContratoServicoRepositorio : RepositorioAbstrato<ContratoServico>, IRepositorioContratoServico
    {
        protected override string HqlLoadById()
        {
            return "from ContratoServico n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from ContratoServico n";
        }

        public IList<ContratoServico> GetContratosParaFaturar(DateTime referencia, Int32 diavcto = 0)
        {
            using (var session = GetSession())
            {
                if(diavcto != 0)
                {
                    string hql = " FROM ContratoServico cs" +
                                        " LEFT JOIN FETCH cs.Contrato c " +
                                        " WHERE cs.DataInicioFaturamento <= :referencia" +
                                        " AND (cs.DataCancelamento > :referencia OR cs.DataCancelamento is NULL)" +
                                        " AND cs.DiaVencimento = :diavcto";

                    var query = session.CreateQuery(hql);
                    query.SetDateTime("referencia", referencia);
                    query.SetInt32("diavcto", diavcto);
                    IList<ContratoServico> contratosServicos = query.List<ContratoServico>();
                    return contratosServicos;
                }
                else
                {
                    string hql = " FROM ContratoServico cs" +
                    " LEFT JOIN FETCH cs.Contrato c " +
                    " WHERE cs.DataInicioFaturamento <= :referencia" +
                    " AND (cs.DataCancelamento > :referencia OR cs.DataCancelamento is NULL)";

                    var query = session.CreateQuery(hql);
                    query.SetDateTime("referencia", referencia);

                    IList<ContratoServico> contratosServicos = query.List<ContratoServico>();
                    return contratosServicos;
                }
            }
        }

        public IList<ContratoServico> GetContratosServicoPorIDContrato(int idContrato)
        {
            using (var session = GetSession())
            {
                string hql = " FROM ContratoServico cs" +
                                    " LEFT JOIN FETCH cs.Contrato c " +
                                    " WHERE cs.Contrato.ID = :idContrato";

                var query = session.CreateQuery(hql);
                query.SetInt32("idContrato", idContrato);

                IList<ContratoServico> contratosServicos = query.List<ContratoServico>();
                return contratosServicos;
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
