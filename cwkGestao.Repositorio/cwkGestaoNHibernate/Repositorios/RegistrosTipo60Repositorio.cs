using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using NHibernate;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System.Reflection;
using cwkGestao.Modelo.Proxy;
using NHibernate.Transform;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{

    partial class RegistrosTipo60Repositorio : RepositorioAbstrato<RegistrosTipo60>, IRepositorioRegistrosTipo60
    {
        protected override string HqlLoadById()
        {
            return "from RegistrosTipo60 r where r.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from RegistrosTipo60 ";
        }

        public bool VerificaRegistrosDeHojeSubtipoM()
        {
            using (var session = GetSession())
            {
                String hql = @"from RegistrosTipo60 r where r.Dt = :Data and TipoRegistro = :TipoRegistro";
                IQuery query = session.CreateQuery(hql).SetDateTime("Data", DateTime.Now.Date).SetString("TipoRegistro", "M");
                query.List();
                if (query.List().Count == 0)
                    return true;
                else
                    return false;
            }
        }

        public bool VerificaRegistrosDeHojeSubtipoA()
        {
            using (var session = GetSession())
            {
                String hql = @"from RegistrosTipo60 r where r.Dt = :Data and TipoRegistro = :TipoRegistro";
                IQuery query = session.CreateQuery(hql).SetDateTime("Data", DateTime.Now.Date).SetString("TipoRegistro", "A"); ;
                query.List();
                if (query.List().Count == 0)
                    return true;
                else
                    return false;
            }
        }

        public IList<RegistrosTipo60> PegaRegistros(DateTime DataInicial, DateTime DataFinal)
        {
            using (var session = GetSession())
            {
                String hql = @"From RegistrosTipo60 rt where Dt >= :DataInicial and Dt <= :DataFinal";

                return session.CreateQuery(hql).SetDateTime("DataInicial", DataInicial.Date).SetDateTime("DataFinal", DataFinal.Date).List<RegistrosTipo60>();

            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }


        public IList<pxyRegistro60R> GetPxyRegistro60R(DateTime dtIni, DateTime dtFin)
        {
            using (var session = GetSession())
            {
                string hql = @" select I.tipo, 
		                               I.SubTipo, 
		                               Replace(I.data,'/','') data,
		                               left(I.codigo+replicate(' ',14),14) codigo,
		                               right(replicate('0',13)+replace(replace(convert(decimal(13,3),I.Quantidade),'.',''),',',''),13) quantidade,
		                               right(replicate('0',16)+replace(replace(convert(decimal(16,2),I.Valor),'.',''),',',''),16) valor,
		                               right(replicate('0',16)+replace(replace(convert(decimal(16,2),I.BaseICMS),'.',''),',',''),16) BaseICMS,
		                               right(replicate('0',4) +replace(I.AliqCupom,'.',''),4) AliqCupom,
		                               replicate(' ',54) Branco
	                              from (
		                            select E.tipo, E.SubTipo, E.data, E.Codigo, sum(E.Quantidade) Quantidade, sum(E.valor) valor, sum(E.BaseICMS) BaseICMS, E.AliqCupom
		                              from (
			                            select '60' tipo, 
				                               'R' SubTipo, 
				                               RIGHT(convert(varchar,n.Dt,103),7) data,
				                               pr.Codigo,
				                               NI.Quantidade,
					                           round(round((NI.valor - round((ni.valor * ni.percdesconto / 100),4)), 3) * ni.Quantidade,2) valor,
				                               case when NI.BaseICMS > 0 then NI.BaseICMS else NI.ValorOutroICMS end BaseICMS,
				                               case when ni.valoricms = 0 then
							                            left(isnull(rtrim(ltrim(ni.AliqCupom)),NCM.AliqCupom)+replicate(' ',4),4)
						                            else right(replicate('0',4) +replace(ni.AliqICMS,'.',''),4) end AliqCupom
			                              from nota as n
			                             inner join NotaItem as ni on ni.IDNota = n.ID
			                             inner join Produto as pr on ni.IDProduto = pr.ID
			                              left join ncm on ncm.ncm = pr.NCM
			                             where n.ModeloDocto = 33
			                               and N.DT BETWEEN :dtIni AND :dtFin
			                               and n.CancDt is null
			                               and isnull(ni.bCancelado,0) = 0
			                               ) E
		                             group by E.tipo, E.SubTipo, E.data, E.Codigo, E.AliqCupom
		                               ) i
                             order by convert(date,DATEADD(s,-1,DATEADD(mm, DATEDIFF(m,0,convert(date,'01/'+i.data,103))+1,0))), i.codigo";

                ConstructorInfo construtor = GetConstructorInfopxyRegistro60R();

                IQuery query = session.CreateSQLQuery(hql.ToString()).SetDateTime("dtIni", dtIni).SetDateTime("dtFin", dtFin);
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyRegistro60R>();
            }
        }

        private ConstructorInfo GetConstructorInfopxyRegistro60R()
        {
            return typeof(pxyRegistro60R).GetConstructor(new Type[]{
                                                                  typeof(String),
                                                                  typeof(String),
                                                                  typeof(String),
                                                                  typeof(String),
                                                                  typeof(String),
                                                                  typeof(String),
                                                                  typeof(String),
                                                                  typeof(String),
                                                                  typeof(String)});
        }
    }
}
