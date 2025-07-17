using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using NHibernate;
using NHibernate.Criterion;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo.Proxy;
using NHibernate.Transform;
using System.Reflection;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ArquivoRemessaRepositorio : RepositorioAbstrato<ArquivoRemessa>, IRepositorioArquivoRemessa
    {
        protected override string HqlLoadById()
        {
            return "from ArquivoRemessa arq left join fetch arq.Remessa rem left join fetch arq.Filial fil where arq.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return @" from ArquivoRemessa arq left join fetch arq.Remessa rem left join fetch arq.Filial fil ";
        }

        public IList<ArquivoRemessa> GetArquivoRemessaPorData(DateTime dataInicio, DateTime dataFim, int filtro, int IDEmpresa, int IDRemessa)
        {
            IList<ArquivoRemessa> arquivosRemessaRetorno = new List<ArquivoRemessa>();

            using (var session = GetSession())
            {
               string hql = " from ArquivoRemessa arq" +
                            " left join fetch arq.Remessa rem" +
                            " left join fetch arq.Filial fil " +
                            " where fil.ID = :idEmpresa " +
                            " and rem.ID = :idRemessa ";
               switch (filtro)
               {
                   case 0:
                       hql += " and arq.DataCriacao >= :dInicio and arq.DataCriacao <= :dFim ";
                       break;
                   case 1:
                       hql += " and arq.DataAlteracao >= :dInicio and arq.DataAlteracao <= :dFim ";
                       break;
                   default:
                       break;
               }

               var querry = session.CreateQuery(hql).SetInt32("idEmpresa", IDEmpresa);
               querry.SetInt32("idRemessa", IDRemessa);
               querry.SetDateTime("dInicio", dataInicio);
               querry.SetDateTime("dFim", dataFim);
               arquivosRemessaRetorno = querry.List<ArquivoRemessa>();

               return arquivosRemessaRetorno;
            }
        }



        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
