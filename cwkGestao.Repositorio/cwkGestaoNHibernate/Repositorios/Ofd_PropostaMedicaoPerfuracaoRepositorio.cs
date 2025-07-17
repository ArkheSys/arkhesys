using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo.Proxy;
using System.Reflection;
using NHibernate.Transform;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Ofd_PropostaMedicaoPerfuracaoRepositorio : RepositorioAbstrato<Ofd_PropostaMedicaoPerfuracao>, IRepositorioOfd_PropostaMedicaoPerfuracao
    {
        protected override string HqlLoadById()
        {
            return "from Ofd_PropostaMedicaoPerfuracao n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Ofd_PropostaMedicaoPerfuracao n";
        }

        public IList<pxyFaturamentoPerfuracao> GetByPropostaENumeroMedicao(Ofd_Proposta proposta, int numero)
        {
            using (var session = GetSession())
            {
                var hql = @"select pp.ID as id,
                            pp.Diametro as diametro,
                            pp.Quantidade as quantidade,
                            pp.Profundidade as metros
                            from Ofd_PropostaMedicaoPerfuracao pp 
                            where pp.PropostaMedicao.Proposta.ID = :idProposta and pp.PropostaMedicao.Medicao = :numero";
                var query = session.CreateQuery(hql).SetInt32("idProposta", proposta.ID).SetInt32("numero", numero);

                ConstructorInfo construtor = typeof(pxyFaturamentoPerfuracao).GetConstructor(new Type[] {
                typeof(int),
                typeof(int),
                typeof(decimal),
                typeof(decimal)});
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor))
                            .List<pxyFaturamentoPerfuracao>();
            }
        }

        public IList<pxyFechamentoMedicao> GetFechamentoByProposta(Ofd_Proposta proposta)
        {
            using (var session = GetSession())
            {
                var hql = @"select pp.ID as id,
                            pp.Diametro as diametro,
                            pp.Quantidade as quantidade,
                            pp.Profundidade as metros,
                            pp.Estaca as estaca   
                            from Ofd_PropostaMedicaoPerfuracao pp
                            where pp.PropostaMedicao.Proposta.ID = :idProposta";
                var query = session.CreateQuery(hql).SetInt32("idProposta", proposta.ID);

                ConstructorInfo construtor = typeof(pxyFechamentoMedicao).GetConstructor(new Type[] {
                typeof(int),
                typeof(int),
                typeof(decimal),
                typeof(decimal),
                typeof(string)
                });
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor))
                            .List<pxyFechamentoMedicao>();
            }
        }

        public IList<Ofd_PropostaMedicaoPerfuracao> GetByProposta(Ofd_Proposta ofd_Proposta)
        {
            using (var session = GetSession())
            {
                var hql = @"from Ofd_PropostaMedicaoPerfuracao pp
                            join fetch pp.PropostaMedicao pm
                            where pm.Proposta.ID = :idProposta and pm.Ofd_Medicao is null";
                return session.CreateQuery(hql)
                                   .SetInt32("idProposta", ofd_Proposta.ID)
                                   .List<Ofd_PropostaMedicaoPerfuracao>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
