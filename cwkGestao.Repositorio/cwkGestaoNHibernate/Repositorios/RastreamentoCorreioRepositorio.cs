using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;
using System.Reflection;
using NHibernate.Transform;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class RastreamentoCorreioRepositorio : RepositorioAbstrato<RastreamentoCorreio>, IRepositorioRastreamentoCorreio
    {
        protected override string HqlLoadById()
        {
            return "from RastreamentoCorreio n where n.ID = :id ";
        }

        protected override string hqlGetAll()
        {
            return @"select new RastreamentoCorreio(rastreamento.ID, tipoServico, rastreamento.Letra, rastreamento.Numero, filial)                      
                     from RastreamentoCorreio rastreamento
                     left join rastreamento.TipoServico tipoServico
                     left join rastreamento.Filial filial";
        }

        public IList<RastreamentoCorreio> GetCodigosRastreamentosPorQuantidadeVolume(int quantidade, int idTipoServico)
        {
            using (var session = GetSession())
            {
                string hql = hqlGetAll() + "where tipoServico.ID = :idTipoServico order by rastreamento.ID";

                IList<RastreamentoCorreio> resultadoSQL = session.CreateQuery(hql).SetInt32("idTipoServico", idTipoServico).SetMaxResults(quantidade).List<RastreamentoCorreio>();

                if (resultadoSQL.Count == quantidade)
                    return resultadoSQL;
                else
                    return null;
            }
        }

        public IList<RastreamentoCorreio> GetCodigosDisponiveis()
        {
            IList<RastreamentoCorreio> Result = new List<RastreamentoCorreio>();
            try
            {
                using (var session = GetSession())
                {
                    string hql = hqlGetAll() + " where tipoServico.IDServicoEnvioCorreio != null";
                    string hql2 = "select rc.ID from CorreiosPrecoPrazo cpp left join cpp.IDRastreamento rc where cpp.IDRastreamento != null";
                    Result = session.CreateQuery(hql).List<RastreamentoCorreio>();
                    IList<object> ResultExped = session.CreateQuery(hql2).List<object>();
                    Result = Result.Where(w => !ResultExped.Contains(w.ID)).ToList();
                }
            }
            catch (Exception x)
            {
                throw x;
            }
            return Result;
        }

        public IList<RastreamentoCorreio> GetCodigosDisponiveisPorTipoServico(TipoServicoTransporte tst)
        {
            if (tst.IDServicoEnvioCorreio != null)
            {
                IList<RastreamentoCorreio> Result = GetCodigosDisponiveis().Where(
                    w => w.TipoServico.ID == tst.ID).ToList();
                return Result;
            }
            return new List<RastreamentoCorreio>();
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
