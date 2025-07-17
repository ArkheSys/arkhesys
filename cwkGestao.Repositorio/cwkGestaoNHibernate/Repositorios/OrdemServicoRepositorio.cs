using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using NHibernate;
using System.Reflection;
using cwkGestao.Modelo.Proxy;
using NHibernate.Transform;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class OrdemServicoRepositorio : RepositorioAbstrato<OrdemServico>, IRepositorioOrdemServico
    {
        protected override string HqlLoadById()
        {
            return " from OrdemServico os " +
                   " left join fetch os.Cliente " +
                   " left join fetch os.ServicoOS sos " +
                   " left join fetch sos.Responsavel " +
                   " left join fetch sos.Servico " +
                   " left join fetch os.ClienteRevenda " +
                   " left join fetch os.Responsavel " +
                   " left join fetch os.Status " +
                   " left join fetch os.Sistema " +
                   " left join fetch os.TipoAtendimento " +
                   " left join fetch os.Versao " +
                   " where os.ID = :id";
        }

        public override OrdemServico LoadObjectById(int id)
        {
            using (var session = GetSession())
            {
                var objeto = session.CreateQuery(HqlLoadById()).SetInt32("id", id).List<OrdemServico>()[0];
                NHibernateUtil.Initialize(objeto.AcompanhamentoOS);
                return objeto;
            }
        }

        protected override string hqlGetAll()
        {
            return " from OrdemServico os " +
                   " left join fetch os.Cliente " +
                   " left join fetch os.AcompanhamentoOS " +
                   " left join fetch os.ServicoOS " +
                   " left join fetch os.ClienteRevenda " +
                   " left join fetch os.Status ";
        }

        public override int MaxCodigo()
        {
            using (var session = GetSession())
            {
                try
                {
                    return (from t in new NHibernate.Linq.NhQueryable<OrdemServico>(session) select t.Codigo).Max() + 1;
                }
                catch (Exception)
                {
                    return 1;
                }
            }
        }

        public IList<cwkGestao.Modelo.Proxy.pxyGridOrdemServico> GetAllGrid()
        {
            using (var session = GetSession())
            {
                string select = @"  SELECT    os.ID
                                    , os.Codigo
                                    , os.Resumo
                                    , cli.Nome AS Cliente
                                    , cr.Nome AS ClienteRevenda
                                    , us.nome AS Responsavel
                                    , os.Incdata AS DataCadastro
                                    , sos.Descricao AS Status
                                    , aos.Incdata AS DataStatus
                                    , os.DataPrevisao
                                    , os.DataprevisaoFinal
                                    , sistema.Nome
                                    , versao.Numero
                            FROM ordemServico os
                            LEFT JOIN Pessoa cli ON cli.ID = os.IDCliente
                            LEFT JOIN ClienteRevenda cr ON cr.ID = os.IDClienteRevenda
                            LEFT JOIN Cw_usuario us ON us.id = os.IDResponsavel
                            LEFT JOIN StatusOrdemServico sos ON sos.ID = os.IDStatus
                            LEFT JOIN AcompanhamentoOS aos ON aos.IDOrdemServico = os.ID 
                            LEFT JOIN Sistema sistema ON sistema.ID = os.IDSistema
                            LEFT JOIN Versao versao ON versao.ID = os.IDVersao
                            WHERE aos.id = (SELECT MAX(id) FROM AcompanhamentoOS WHERE AcompanhamentoOS.IDOrdemServico = os.ID)";

                ConstructorInfo construtor = typeof(pxyGridOrdemServico).GetConstructor(new Type[] {
                    typeof(int),
                    typeof(int),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(string),
                    typeof(DateTime?),
                    typeof(string),
                    typeof(DateTime?),
                    typeof(DateTime?),
                    typeof(DateTime?),
                    typeof(string),
                    typeof(string)
                });

                return session.CreateSQLQuery(select).SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyGridOrdemServico>();
            }
        }

        public IList<object> OrdemServicoRel(int idRevenda, int idResponsavel, int idStatus, DateTime DataInicial, DateTime DataFinal)
        {
            using (var session = GetSession())
            {
                string select = @"SELECT OS.Codigo, C.Nome as Revenda, R.Login as Responsavel, OS.Resumo, S.Descricao as Status, S.IncData, cast(SUM(HOUR(SOS.TempoHoras) + MINUTE(SOS.TempoHoras)/ cast(60 as float)) as float) as Tempo
                                  FROM OrdemServico OS
                                  JOIN OS.Status S
                                  JOIN OS.Cliente C
                                  JOIN OS.Responsavel R
                                  LEFT JOIN OS.ServicoOS SOS
                                  WHERE :DataInicial <= OS.Incdata AND :DataFinal >= OS.Incdata  ";

                if (idRevenda > 0)
                    select += " AND C.ID = '" + idRevenda + "' ";

                if (idResponsavel > 0)
                    select += " AND R.ID = '" + idResponsavel + "' ";

                if (idStatus > 0)
                    select += " AND S.ID = '" + idStatus + "' ";

                select += @" GROUP BY OS.Codigo, C.Nome, R.Login, OS.Resumo, S.Descricao, S.IncData ORDER BY S.IncData";

                IQuery query = session.CreateQuery(select).SetDateTime("DataInicial", DataInicial).SetDateTime("DataFinal", DataFinal.AddHours(23).AddMinutes(59).AddSeconds(59));

                ConstructorInfo construtor = typeof(pxyRelCworkOrdemServico).GetConstructor(new Type[] {
                typeof(int),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(DateTime),
                typeof(Double),
                });

                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<object>();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
