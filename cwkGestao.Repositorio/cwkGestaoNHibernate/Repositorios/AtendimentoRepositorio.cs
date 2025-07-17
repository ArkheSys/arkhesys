using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class AtendimentoRepositorio : RepositorioAbstrato<Atendimento>, IRepositorioAtendimento
    {
        protected override string HqlLoadById()
        {
            return "from Atendimento n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Atendimento n";
        }
		
		protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IList<Atendimento> GetAllByAnalistaPeriodoEFaturados(Cw_usuario usuario, DateTime start, DateTime end, bool ConsiderarFaturados)
        {
            IList<Atendimento> result = new List<Atendimento>();

            DateTime inicio = start.Date;
            DateTime fim = end.Date.AddHours(23).AddMinutes(59).AddSeconds(59);

            using (var session = GetSession())
            {
                string hql = "";
                
                if (ConsiderarFaturados)
                {
                    hql = "from Atendimento a where a.DtAtendimento between :inicio and :fim";
                }
                else
                {
                    hql = "from Atendimento a where a.DtAtendimento between :inicio and :fim and a.Nota is null";
                }
                if (usuario != null)
                {
                    hql += " and a.Analista.ID = :idAnalista";
                }
                try
                {
                    IQuery query = session.CreateQuery(hql)
                                .SetDateTime("inicio", inicio)
                                .SetDateTime("fim", fim);
                    if (usuario != null)
                    {
                        query.SetInt32("idAnalista", usuario.ID);
                    }
                    result = query.List<Atendimento>();
                }
                catch (Exception e)
                {
                    throw e;
                }
            }
            return result;
        }

        public IList<Atendimento> GetAllPorIDNota(int IDNota)
        {
            IList<Atendimento> result = new List<Atendimento>();

            using (var session = GetSession())
            {
                string hql = "from Atendimento a where a.Nota.ID = :idNota";
                try
                {
                    result = session.CreateQuery(hql).SetInt32("idNota", IDNota).List<Atendimento>();
                }
                catch (Exception)
                {
                    throw;
                }
            }

            return result;
        }
    }
}
