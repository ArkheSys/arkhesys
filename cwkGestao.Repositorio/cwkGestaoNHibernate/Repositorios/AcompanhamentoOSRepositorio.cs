using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System.Reflection;
using NHibernate.Transform;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class AcompanhamentoOSRepositorio : RepositorioAbstrato<AcompanhamentoOS>, IRepositorioAcompanhamentoOS
    {
        protected override string HqlLoadById()
        {
            return "from AcompanhamentoOS where id = :id";
        }

        protected override string hqlGetAll()
        {
            return "from AcompanhamentoOS";
        }

        public IList<cwkGestao.Modelo.Proxy.pxyAcompanhamentoOS> GetAcompanhamentoOS(int idOrdemServico)
        {
            using (var session = GetSession())
            {
                string select = @"  SELECT    aos.ID
                                    , aos.Codigo
                                    , usant.Nome AS ResponsavelAnt
                                    , usatu.Nome AS ResponsavelAtu
                                    , stant.Descricao AS StatusAnt
                                    , statu.Descricao AS StatusAtu
                                    , aos.Incdata AS Data
                            FROM AcompanhamentoOS aos
                            LEFT JOIN aos.ResponsavelAnt usant
                            LEFT JOIN aos.ResponsavelAtu usatu
                            LEFT JOIN aos.StatusAnt stant
                            LEFT JOIN aos.StatusAtu statu
                            WHERE aos.OrdemServico.ID = " + idOrdemServico.ToString();
                //int ,int? , string string string string DateTime? 
                ConstructorInfo construtor = typeof(pxyAcompanhamentoOS).GetConstructor(new Type[] {
                    typeof(int),
                    typeof(int?),
                    typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(DateTime?)
                });

                return session.CreateQuery(select).SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyAcompanhamentoOS>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
