using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo.Proxy;
using NHibernate.Transform;
using System.Reflection;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Tel_LigacaoRepositorio : RepositorioAbstrato<Tel_Ligacao>, IRepositorioTel_Ligacao
    {
        protected override string HqlLoadById()
        {
            return "from Tel_Ligacao l where l.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Tel_Ligacao l";
        }

        public Dictionary<string, Tel_Ligacao> GetHashLigacoes()
        {
            using (var session = GetSession())
            {
                string hql = "select new Tel_Ligacao(l.ID, l.Origem, l.Destino, l.DataHora) from Tel_Ligacao l";
                var result = session.CreateQuery(hql).List<Tel_Ligacao>();
                Dictionary<string, Tel_Ligacao> set = new Dictionary<string, Tel_Ligacao>();
                foreach (var item in result)
                {
                    set.Add(item.GetChaveUnica(), item);
                }
                return set;
            }
        }
        //nosso
        public Dictionary<string, Tel_Ligacao> GetHashLigacoesArquivo(DateTime referencia)
        {
            using (var session = GetSession())
            {
                string hql = "select new Tel_Ligacao(l.ID, l.Origem, l.Destino, l.DataHora) from Tel_Ligacao l " +
                             "where convert(date,l.DataHora) >= DATEADD(month,-3,CONVERT(date,:referencia)) ";

                var result = session.CreateQuery(hql).SetDateTime("referencia", referencia).List<Tel_Ligacao>();

                Dictionary<string, Tel_Ligacao> set = new Dictionary<string, Tel_Ligacao>();
                foreach (var item in result)
                {
                    set.Add(item.GetChaveUnica(), item);
                }
                return set;

            }
        }

        public IList<pxyTel_LigacaoCliente> GetByReferencia(DateTime referencia)
        {
            using (var session = GetSession())
            {
                string hql = @"select ct.Cliente.ID as idCliente
                               , l.ID as idLigacao
                               , l.TempoSegundos as tempoSegundos
                               , l.Origem as origem
                               , l.TipoUso as tipoUso
                               , l.Destino as destino
                               from Tel_Ligacao l, Tel_ClienteTelefone ct
                               join l.ArquivoLigacao al
                               join ct.GrupoClienteTelefone gt
                               where l.Origem = gt.Telefone and al.Referencia = :referencia
                               and (ct.DataDesligamento is null or cast(l.DataHora as date) <= cast(ct.DataDesligamento as date))
                               and cast(l.DataHora as date) >= cast(ct.DataAtivacao as date)
                               and l.ID not in (select fl.Ligacao.ID from Tel_FaturaLigacao fl where fl.Ligacao is not null)";

                var construtor = typeof(pxyTel_LigacaoCliente).GetConstructor(new Type[] { typeof(int), typeof(int), typeof(int), typeof(string), typeof(int), typeof(string) });

                return session.CreateQuery(hql)
                                   .SetDateTime("referencia", referencia)
                                   .SetResultTransformer(Transformers.AliasToBeanConstructor(construtor))
                                   .List<pxyTel_LigacaoCliente>();
            }
        }

        public Dictionary<int, IList<Tel_Ligacao>> GetByReferenciaEClientes(DateTime referencia, IList<Tel_Cliente> clientes)
        {
            using (var session = GetSession())
            {
                var ret = new Dictionary<int, IList<Tel_Ligacao>>();

                string hql = @"select l 
                               from Tel_Ligacao l, Tel_ClienteTelefone ct
                               join l.ArquivoLigacao al
                               join ct.Cliente c
                               join ct.GrupoClienteTelefone gt
                               where l.Origem = gt.Telefone and al.Referencia = :referencia
                               and (ct.DataDesligamento is null or cast(l.DataHora as date) <= cast(ct.DataDesligamento as date))
                               and c.ID = :idCliente and cast(l.DataHora as date) >= cast(ct.DataAtivacao as date)
                               and l.ID not in (select fl.Ligacao.ID from Tel_FaturaLigacao fl where fl.Ligacao is not null)";
                var query = session.CreateQuery(hql);
                foreach (var cli in clientes)
                {
                    var tels = query.SetDateTime("referencia", referencia)
                                    .SetInt32("idCliente", cli.ID)
                                    .List<Tel_Ligacao>();
                    ret.Add(cli.ID, tels);
                }
                return ret;
            }
        }


        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
