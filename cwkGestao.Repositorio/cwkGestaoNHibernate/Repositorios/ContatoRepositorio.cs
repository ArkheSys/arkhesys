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
    partial class ContatoRepositorio : RepositorioAbstrato<Contato>, IRepositorioContato
    {
        protected override string HqlLoadById()
        {
            return "from Contato n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Contato n";
        }
		
		protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public pxyAvisos GetAvisosAnteriores(int IDUsuario)
        {
            using (var session = GetSession())
            {
                string sql = @"select c.ID as ContatoID, 
                                      p.Nome as NomePessoa,
	                                  u.nome as NomeUsuario,
	                                  c.Contato,
	                                  cd.ID as ContatoDetalheID,
	                                  sc.Descricao as StatusContato,
	                                  cd.Assunto,
	                                  cd.Data,
	                                  convert(varchar, cd.HoraInicial) as HoraInicial,
	                                  convert(varchar, cd.HoraFinal) as HoraFinal,
	                                  cd.Observacao
                               from ContatoDetalhe cd
                               join StatusContato sc on sc.ID = cd.IDStatusContato
                               join Contato c on c.ID = cd.IDContato
                               join Pessoa p on p.ID = c.IDPessoa
                               join cw_usuario u on u.id = c.IDCw_usuario
                               where sc.bFinalizado = 0
                                  and c.IDCw_usuario = :IDUsuario
                                  and (cd.Data = convert(date,GETDATE()) and cd.HoraInicial <=  convert(time,GETDATE()))
                               order by cd.Data, cd.HoraInicial";

                var query = session.CreateSQLQuery(sql).SetInt32("IDUsuario", IDUsuario);

                ConstructorInfo construtor = typeof(pxyAvisos).GetConstructor(new Type[] {
                typeof(int),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(int),
                typeof(string),
                typeof(string),
                typeof(DateTime),
                typeof(string),
                typeof(string),
                typeof(string)
                });

                var retorno = query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyAvisos>().FirstOrDefault();

                return retorno;
            }
        }

        public pxyAvisos GetAvisosPosteriores(int IDUsuario)
        {
            using (var session = GetSession())
            {
                string sql = @"select c.ID as ContatoID, 
                                      p.Nome as NomePessoa,
	                                  u.nome as NomeUsuario,
	                                  c.Contato,
	                                  cd.ID as ContatoDetalheID,
	                                  sc.Descricao as StatusContato,
	                                  cd.Assunto,
	                                  cd.Data,
	                                  convert(varchar, cd.HoraInicial) as HoraInicial,
	                                  convert(varchar, cd.HoraFinal) as HoraFinal,
	                                  cd.Observacao
                               from ContatoDetalhe cd
                               join StatusContato sc on sc.ID = cd.IDStatusContato
                               join Contato c on c.ID = cd.IDContato
                               join Pessoa p on p.ID = c.IDPessoa
                               join cw_usuario u on u.id = c.IDCw_usuario
                               where sc.bFinalizado = 0
                                  and c.IDCw_usuario = :IDUsuario
                                  and (cd.Data = convert(date,GETDATE()) and cd.HoraInicial >  convert(time,GETDATE()))
                               order by cd.Data, cd.HoraInicial";

                var query = session.CreateSQLQuery(sql).SetInt32("IDUsuario", IDUsuario);

                ConstructorInfo construtor = typeof(pxyAvisos).GetConstructor(new Type[] {
                typeof(int),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(int),
                typeof(string),
                typeof(string),
                typeof(DateTime),
                typeof(string),
                typeof(string),
                typeof(string)
                });

                var retorno = query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyAvisos>().FirstOrDefault();

                return retorno;
            }
        }

        public pxyAvisosPorPeriodo GetAvisosPorPeriodo(int IDUsuario)
        {
            using (var session = GetSession())
            {
                string sql = @"select  sum(I.eventoPassado) eventoPassado,
			                           sum(I.eventoOntem) eventoOntem,
			                           sum(I.hoje) hoje,
			                           sum(I.eventoAmanha) eventoAmanha,
			                           sum(I.eventoFuturo) eventoFuturo,
                                       convert(varchar,max(I.eventoAnterior)) eventoAnterior,
	                                   convert(varchar,min(I.eventoProximo)) eventoProximo
                                from (
		                            select case when cd.Data < convert(date,dateadd(day,-1,GETDATE())) then 1 else 0 end eventoPassado,
			                               case when cd.Data = convert(date,dateadd(day,-1,GETDATE())) then 1 else 0 end eventoOntem,
			                               case when cd.Data = convert(date,GETDATE()) then 1 else 0 end hoje,
			                               case when cd.Data = convert(date,dateadd(day,1,GETDATE())) then 1 else 0 end eventoAmanha,
			                               case when cd.Data > convert(date,dateadd(day,1,GETDATE())) then 1 else 0 end eventoFuturo,
		                                   case when (cd.Data = convert(date,GETDATE()) and HoraInicial <= convert(time,GETDATE())) then HoraInicial else null end eventoAnterior,
		                                   case when (cd.Data = convert(date,GETDATE()) and HoraInicial >  convert(time,GETDATE())) then HoraInicial else null end eventoProximo
			                         from ContatoDetalhe as cd
		                             inner join Contato as c on cd.idcontato = c.id
		                             inner join StatusContato as sc on cd.IDStatusContato = sc.ID
		                             where sc.bFinalizado = 0
		                             and c.IDCw_usuario = :IDUsuario
			                            ) I";

                var query = session.CreateSQLQuery(sql).SetInt32("IDUsuario", IDUsuario);


                ConstructorInfo construtor = typeof(pxyAvisosPorPeriodo).GetConstructor(new Type[] {
                typeof(int),
                typeof(int),
                typeof(int),
                typeof(int),
                typeof(int),
                typeof(string),
                typeof(string)
                });

                var retorno = query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyAvisosPorPeriodo>().FirstOrDefault();

                return retorno;
            }
        }

       

    }
}
