using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using NHibernate;
using NHibernate.Criterion;
using cwkGestao.Modelo.Proxy;
using System.Reflection;
using NHibernate.Transform;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class CwkPedidoRepositorio : RepositorioAbstrato<CwkPedido>, IRepositorioCwkPedido
    {
        protected override string HqlLoadById()
        {
            return "from CwkPedido where ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from CwkPedido";
        }

        public override CwkPedido LoadObjectById(int id)
        {
            using (var session = GetSession())
            {
                var objeto = session.Get<CwkPedido>(id);
                NHibernateUtil.Initialize(objeto.CwkPedidoEmpresas);
                NHibernateUtil.Initialize(objeto.Sistema);
                NHibernateUtil.Initialize(objeto.Versao);
                NHibernateUtil.Initialize(objeto.Pessoa);

                //NHibernateUtil.Initialize(objeto.);

                return objeto;
            }
        }

        public override int MaxCodigo()
        {
            using (var session = GetSession())
            {
                try
                {
                    return (from t in new NHibernate.Linq.NhQueryable<CwkPedido>(session) select t.Codigo).Max() + 1;
                }
                catch (Exception)
                {
                    return 1;
                }
            }
        }

        public IList<pxyCwkPedido> GetPedidos()
        {
            using (var session = GetSession())
            {
                string sql = @" select  t.ID,
     	                                t.Codigo,
		                                t.Data,
		                                t.Hora,
		                                t.revenda,
		                                t.cliente,
		                                t.NumeroSerie,
		                                t.Nome,
		                                t.BDemonstracao,
		                                t.Status,
		                                substring(t.NumeroSerieEmpresas,0,len(t.NumeroSerieEmpresas)) NumeroSerieEmpresas
                                  from (
	                                Select cp.ID,
		                                   cp.Codigo,
		                                   cp.Data,
		                                   cp.Hora,
		                                   p.Nome revenda,
		                                   case when cr2.Nome is null then cr.Nome else cr2.Nome end cliente,
		                                   cp.NumeroSerie,
		                                   s.Nome,
		                                   cp.BDemonstracao,
		                                   cp.Status,
				                                (
					                                Select convert(varchar,cpe1.NumeroSerie) + ',' AS [text()]
					                                From dbo.CwkPedidoEmpresa cpe1
					                                Where cpe1.idpedido = cpe.idpedido
					                                ORDER BY cpe1.NumeroSerie
					                                For XML PATH ('')
				                                ) NumeroSerieEmpresas
			                                From CwkPedido as cp 
			                                left join CwkPedidoEmpresa as cpe on cp.ID = cpe.idpedido
			                                left join Sistema as s on s.id = cp.IDSistema
			                                left join Pessoa as p on cp.IDPessoa = p.id
		                                   inner join ClienteRevenda as cr on cpe.IDClienteRevenda = cr.ID
			                                left join ClienteRevenda as cr2 on cr2.ID = cr.IDClienteRevenda
		                                   where cr.Principal = 1 or cr2.Principal = 1
                                       ) t
	                                Group by t.ID,
     		                                 t.Codigo,
		                                     t.Data,
		                                     t.Hora,
		                                     t.revenda,
		                                     t.cliente,
		                                     t.NumeroSerie,
		                                     t.Nome,
		                                     t.BDemonstracao,
		                                     t.Status,
		                                     t.NumeroSerieEmpresas";

                ConstructorInfo construtor = typeof(pxyCwkPedido).GetConstructor(new Type[] {
                typeof(int),
                typeof(int),
                typeof(DateTime?),
                typeof(DateTime?),
                typeof(string),
                typeof(string),
                typeof(int?),
                typeof(string),
                typeof(bool),
                typeof(OrcamentoStatus),
                typeof(string)
                });

                return session.CreateSQLQuery(sql).SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyCwkPedido>();
            }

        }

        public int UltimoNumeroSerie(int pIdSistema)
        {
            using (var session = GetSession())
            {
                string hql = "select max(cp.NumeroSerie) from CwkPedido cp where cp.Sistema.ID = :idsistema";
                var result = session.CreateQuery(hql).SetInt32("idsistema", pIdSistema).UniqueResult<int?>();
                if (result != null)
                    return Convert.ToInt32(result);
                else
                    return 0;
            }
        }

        public IList<object> GetPedidosFaturamento(List<int> idsRevendas, int tipoFiltro, DateTime dataInicial, DateTime dataFinal)
        {
            using (var session = GetSession())
            {
                StringBuilder Hql = new StringBuilder();

                Hql.AppendLine("select CLI.Nome AS NomeCliente");
                Hql.AppendLine(", CLI.Codigo AS CodigoCliente");
                Hql.AppendLine(", (case when SER.Nome is not null then SER.Nome else PROD.Nome end) AS NomeServico");
                Hql.AppendLine(", (case when SER.Nome is not null then SER.Codigo else PROD.Codigo end) AS Codigo");
                Hql.AppendLine(", PEMP.ValorProduto as Valor");
                if (tipoFiltro == 0)
                    Hql.AppendLine(", PED.Data");
                else
                    Hql.AppendLine(", PED.DataSite as Data");
                Hql.AppendLine(", SIS.Codigo as CodigoSistema");
                Hql.AppendLine(", PEMP.NumeroSerie as NumeroSerie");
                Hql.AppendLine(", PES.Nome AS NomeRevenda");
                Hql.AppendLine("from CwkPedido PED");
                Hql.AppendLine("join PED.Pessoa PES");
                Hql.AppendLine("join PED.CwkPedidoEmpresas PEMP");
                Hql.AppendLine("join PEMP.ClienteRevenda CLI");
                Hql.AppendLine("left join PEMP.Servico SER");
                Hql.AppendLine("left join PEMP.Produto PROD");
                Hql.AppendLine("left join PED.Sistema SIS");
                if (tipoFiltro == 0)
                    Hql.AppendLine("where PED.Data >=  :dataInicial AND PED.Data <= :dataFinal");
                else
                    Hql.AppendLine("where PED.DataSite >=  :dataInicial AND PED.DataSite <= :dataFinal");
                
                if (idsRevendas.Count > 0)
                {
                    Hql.Append("and PES.ID in (");
                    for (int i = 0; i < idsRevendas.Count; i++)
                    {
                        if (i > 0)
                            Hql.Append(", ");
                        Hql.Append(idsRevendas[i].ToString());
                    }
                    Hql.AppendLine(")");
                }
                Hql.AppendLine("order by SER.Nome, PED.Data");

                Type tipoFaturamentoRevendas = typeof(pxyFaturamentoRevendas);
                ConstructorInfo construtor = tipoFaturamentoRevendas.GetConstructor(new Type[] { typeof(string), 
                                                                                                    typeof(int), 
                                                                                                    typeof(string),
                                                                                                    typeof(int?),
                                                                                                    typeof(decimal?), 
                                                                                                    typeof(DateTime),
                                                                                                    typeof(int),
                                                                                                    typeof(int),
                                                                                                    typeof(string)});
                IQuery query = session.CreateQuery(Hql.ToString());
                query = query.SetDateTime("dataInicial", dataInicial).SetDateTime("dataFinal", dataFinal);

                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<object>();
            }
        }

        public IList<cwkGestao.Modelo.CwkPedido> GetPedidos(cwkGestao.Modelo.Pessoa pessoa)
        {

            using (var session = GetSession())
            {
                StringBuilder Hql = new StringBuilder();

                Hql.Append(@" from CwkPedido ped join fetch ped.Sistema join fetch ped.Versao where not ped.BDemonstracao and ped.Pessoa.ID = " + pessoa.ID);
                IList<cwkGestao.Modelo.CwkPedido> retorno = session.CreateQuery(Hql.ToString()).List<cwkGestao.Modelo.CwkPedido>();

                return retorno;

            }
        }

        public IList<cwkGestao.Modelo.CwkPedido> GetPedidos(cwkGestao.Modelo.Pessoa pessoa, DateTime inicio, DateTime fim)
        {
            String inicioS = inicio.ToString("yyyy") + (inicio.Month).ToString("00") + inicio.ToString("dd");
            String fimS = fim.ToString("yyyy") + (fim.Month).ToString("00") + fim.ToString("dd");

            using (var session = GetSession())
            {
                StringBuilder Hql = new StringBuilder();
                Hql.Append(@" from CwkPedido ped join fetch ped.Sistema join fetch ped.Versao where not ped.BDemonstracao and ped.Pessoa.ID = " + pessoa.ID);
                Hql.Append(@" and (ped.Data between '" + inicioS + "' and '" + fimS + "')");
                String hql = Hql.ToString();
                IList<cwkGestao.Modelo.CwkPedido> retorno = session.CreateQuery(hql).List<cwkGestao.Modelo.CwkPedido>();

                return retorno;

            }
        }

        public IList<cwkGestao.Modelo.CwkPedido> GetCwkPedidos()
        {
            using (var session = GetSession())
            {
                string hql = @"from CwkPedido cp
                                left join fetch cp.Sistema s
                                left join fetch cp.Pessoa p";

                var x = session.CreateQuery(hql).List();
                int a = x.Count;

                IList<cwkGestao.Modelo.CwkPedido> retorno = session.CreateQuery(hql).List<cwkGestao.Modelo.CwkPedido>();

                return retorno;
            }

        }

        public IList<cwkGestao.Modelo.CwkPedido> GetPedidos(cwkGestao.Modelo.CwkPedido pexemplo)
        {
            using (var session = GetSession())
            {
                ICriteria criteria = session.CreateCriteria(typeof(CwkPedido));
                Example exemplo = Example.Create(pexemplo);
                IList<CwkPedido> cwkPedidos = criteria.Add(exemplo
                                        .ExcludeNone()
                                        .EnableLike(MatchMode.Anywhere)
                                        .IgnoreCase()
                                        .ExcludeZeroes())
                                    .List<CwkPedido>();

                return cwkPedidos.ToList();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
        
    }
}
