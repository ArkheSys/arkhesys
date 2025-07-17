using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate.Transform;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class FechamentoCaixaRateioRepositorio : RepositorioAbstrato<FechamentoCaixaRateio>, IRepositorioFechamentoCaixaRateio
    {
        protected override string HqlLoadById()
        {
            return "from FechamentoCaixaRateio n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from FechamentoCaixaRateio n";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
        public IList<pxFechamentoCaixaRateio> GetRateiosPorFluxoDeCaixa(int IDFluxoCaixa)
        {
            using (var session = GetSession())
            {
                var query = session.CreateSQLQuery(@"select fc.Nome AS FormaPagamento,
                                                            --CASE 
										                    --  WHEN FC.NOME = 'DINHEIRO' THEN FCR.ValorCaixa - FCN.Troco
										                    --  ELSE FCR.ValorCaixa
									                        --END AS ValorCaixa,
                                                            FCR.ValorCaixa,
                                                            FCR.ValorInformado
                                                  from FechamentoCaixaRateio FCR
                                                    INNER JOIN FormaPagamento FC ON (FCR.IDFormaDePagamento = FC.ID)
                                                    --INNER JOIN (select IDFluxoCaixa, sum(case when troco < 0 then 0 else troco end) as Troco from FrenteCaixaNota group by IDFluxoCaixa) FCN ON (FCR.IDFechamentoCaixa = FCN.IDFluxoCaixa)
                                                where FCR.IDFechamentoCaixa = :IDFluxo").SetInt32("IDFluxo", IDFluxoCaixa);

                ConstructorInfo construtor = typeof(pxFechamentoCaixaRateio).GetConstructor(new Type[] { typeof(string), typeof(decimal), typeof(decimal) });
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxFechamentoCaixaRateio>();
            }
        }
    }
}
