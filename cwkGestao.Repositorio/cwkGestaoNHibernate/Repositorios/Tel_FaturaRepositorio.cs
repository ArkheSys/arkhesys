using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;
using NHibernate.Transform;


namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Tel_FaturaRepositorio : RepositorioAbstrato<Tel_Fatura>, IRepositorioTel_Fatura
    {
        
        public override Tel_Fatura LoadObjectById(int id)
        {
            using (var session = GetSession())
            {
                string hql = "from Tel_Fatura g" +
                    " join fetch g.Cliente c " +
                    " left join fetch g.Documento d " +
                    " left join fetch g.Ligacoes l " +
                    " where g.ID = :id";

                Tel_Fatura fatura = session.CreateQuery(hql).SetInt32("id", id).UniqueResult<Tel_Fatura>();
                NHibernateUtil.Initialize(fatura.Servicos);

                return fatura;
            }
        }

        protected override string HqlLoadById()
        {
            return @"from Tel_Fatura g 
                    join fetch g.Cliente c
                    left join fetch g.Documento d
                    left join fetch g.Ligacoes l
                    left join fetch g.Servicos ts
                    left join fetch ts.Servico s
                    where g.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return @"select new Tel_Fatura(g.ID, g.Codigo, g.Data, g.Referencia, g.Vencimento, g.Valor, g.DataInicial, g.DataFinal, p.Nome, t.Nome, gc.Nome, c.NumeroPiloto, g.NossoNumero, g.DataEnvio, g.bEnviadoEmail)
                    from Tel_Fatura g
                    join g.Cliente c
                    join c.GrupoCliente gc
                    join c.Pessoa p
                    left join g.Documento d
                    left join d.TipoDocumento t";
        }

        public Tel_Fatura GetFaturaByDocumento(int idDocumento)
        {
            using (var sessao = GetSession())
            {
                string hql = "from Tel_Fatura g" +
                    " join fetch g.Cliente c " +
                    " left join fetch g.Documento d " +
                    " left join fetch g.Ligacoes l " +
                    " where d.ID = " + idDocumento;
                return sessao.CreateQuery(hql).List<Tel_Fatura>().FirstOrDefault();
            }
        }

        public Tel_Fatura GetByClienteEReferencia(Tel_Cliente cliente, DateTime referencia)
        {
            using (var sessao = GetSession())
            {
                string hql = @"from Tel_Fatura f 
                               left join fetch f.Ligacoes l 
                               left join fetch f.Servicos ts
                               left join fetch ts.Servico s
                               left join fetch f.Cliente c 
                               left join fetch c.Precos 
                               where f.Documento is null and f.Cliente.ID = :idCliente and f.Referencia = :referencia";
                var fatura = sessao.CreateQuery(hql)
                                   .SetInt32("idCliente", cliente.ID)
                                   .SetDateTime("referencia", referencia)
                                   .List<Tel_Fatura>().FirstOrDefault();
                if (fatura != null)
                    NHibernateUtil.Initialize(fatura.Servicos);
                return fatura;
            }
        }

        public IList<Tel_Fatura> GetByGrupoEReferencia(Tel_GrupoCliente grupo, DateTime referencia)
        {
            using (var sessao = GetSession())
            {
                string hql = @"from Tel_Fatura f 
                               join fetch f.Cliente c
                               join fetch c.Pessoa p
                               where f.Documento is null
                               and f.Referencia = :referencia
                               and c.GrupoCliente.ID = :idGrupo";
                return sessao.CreateQuery(hql)
                                   .SetInt32("idGrupo", grupo.ID)
                                   .SetDateTime("referencia", referencia)
                                   .List<Tel_Fatura>();
            }
        }

        public IList<Tel_Fatura> GetInformacaoLiagacoes(int ID)
        {
            using (var sessao = GetSession())
            {
                string hql = @"from Tel_Fatura f 
                               join fetch f.Ligacoes l
                               join fetch l.Ligacoes
                               where f.ID = :ID";
                return sessao.CreateQuery(hql).SetInt32("ID", ID).List<Tel_Fatura>();
            }
        }

        public void DesfazerFechamentoFaturas(Tel_GrupoCliente grupoCliente, DateTime referencia)
        {
            using (var sessao = GetSession())
            {
                if (GetQuantidadeFaturasBaixadas(grupoCliente, referencia, sessao) > 0)
                    throw new Exception("Não foi possível desfazer o fechamento. Já existem faturas baixadas no financeiro.");

                using (var trans = sessao.BeginTransaction())
                {
                    string idsDocs = GetDocumentosVinculados(grupoCliente, referencia, sessao);
                    string hqlUpdateFaturas = "update Tel_Fatura f set f.Documento = null where f.Documento.ID in " + idsDocs;
                    string hqlDeleteDocumentos = @"delete from Documento d 
                                            where d.ID in " + idsDocs;

                    sessao.CreateQuery(hqlUpdateFaturas).ExecuteUpdate();
                    sessao.CreateQuery(hqlDeleteDocumentos).ExecuteUpdate();
                    trans.Commit();
                }
            }
        }

        public void DesfazerFechamentoFaturasPorCliente(Tel_Cliente cliente, DateTime referencia)
        {
            using (var sessao = GetSession())
            {
                if (GetQuantidadeFaturasBaixadasPorCliente(cliente, referencia, sessao) > 0)
                    throw new Exception("Não foi possível desfazer o fechamento. Já existem faturas baixadas no financeiro.");

                using (var trans = sessao.BeginTransaction())
                {
                    string idsDocs = GetDocumentosVinculadosPorCliente(cliente, referencia, sessao);
                    string hqlUpdateFaturas = "update Tel_Fatura f set f.Documento = null where f.Documento.ID in " + idsDocs;
                    string hqlDeleteDocumentos = @"delete from Documento d 
                                            where d.ID in " + idsDocs;

                    sessao.CreateQuery(hqlUpdateFaturas).ExecuteUpdate();
                    sessao.CreateQuery(hqlDeleteDocumentos).ExecuteUpdate();
                    trans.Commit();
                }
            }
        }

        private string GetDocumentosVinculados(Tel_GrupoCliente grupoCliente, DateTime referencia, ISession sessao)
        {
            var hqlSelectDocumentos = "select d.ID from Tel_Fatura f join f.Documento d join f.Cliente c where c.GrupoCliente.ID = :idGrupoCliente and f.Referencia = :referencia";
            var documentos = sessao.CreateQuery(hqlSelectDocumentos)
                                   .SetInt32("idGrupoCliente", grupoCliente.ID).SetDateTime("referencia", referencia)
                                   .List<int>();

            if (documentos.Count == 0)
                throw new Exception("Não foram encontradas faturas fechadas para os filtros selecionados.");

            StringBuilder idsDocs = new StringBuilder("(");
            for (int i = 0; i < documentos.Count; i++)
            {
                if (i > 0)
                    idsDocs.Append(", ");
                idsDocs.Append(documentos[i]);
            }
            idsDocs.Append(")");
            return idsDocs.ToString();
        }

        private string GetDocumentosVinculadosPorCliente(Tel_Cliente cliente, DateTime referencia, ISession sessao)
        {
            var hqlSelectDocumentos = "select d.ID from Tel_Fatura f join f.Documento d join f.Cliente c where c.ID = :idCliente and f.Referencia = :referencia";
            var documentos = sessao.CreateQuery(hqlSelectDocumentos)
                                   .SetInt32("idCliente", cliente.ID).SetDateTime("referencia", referencia)
                                   .List<int>();

            if (documentos.Count == 0)
                throw new Exception("Não foram encontradas faturas fechadas para os filtros selecionados.");

            StringBuilder idsDocs = new StringBuilder("(");
            for (int i = 0; i < documentos.Count; i++)
            {
                if (i > 0)
                    idsDocs.Append(", ");
                idsDocs.Append(documentos[i]);
            }
            idsDocs.Append(")");
            return idsDocs.ToString();
        }

        private long? GetQuantidadeFaturasBaixadas(Tel_GrupoCliente grupoCliente, DateTime referencia, ISession sessao)
        {
            var hqlQuantidade = "select count(*) from Tel_Fatura f join f.Documento d join f.Cliente c where c.GrupoCliente.ID = :idGrupoCliente and f.Referencia = :referencia and (d.Situacao = 'BxT' or d.Situacao = 'BxP')";
            var quantidadeBaixado = sessao.CreateQuery(hqlQuantidade)
                                          .SetInt32("idGrupoCliente", grupoCliente.ID)
                                          .SetDateTime("referencia", referencia)
                                          .UniqueResult<Int64?>();
            return quantidadeBaixado;
        }

        private long? GetQuantidadeFaturasBaixadasPorCliente(Tel_Cliente cliente, DateTime referencia, ISession sessao)
        {
            var hqlQuantidade = "select count(*) from Tel_Fatura f join f.Documento d join f.Cliente c where c.ID = :idCliente and f.Referencia = :referencia and (d.Situacao = 'BxT' or d.Situacao = 'BxP')";
            var quantidadeBaixado = sessao.CreateQuery(hqlQuantidade)
                                          .SetInt32("idCliente", cliente.ID)
                                          .SetDateTime("referencia", referencia)
                                          .UniqueResult<Int64?>();
            return quantidadeBaixado;
        }

        public void ExcluirLoteFaturas(Tel_GrupoCliente grupoCliente, DateTime referencia)
        {
            using (var sessao = GetSession())
            {
                var hqlQuantidade = "select count(*) from Tel_Fatura f join f.Cliente c where c.GrupoCliente.ID = :idGrupoCliente and f.Referencia = :referencia and f.Documento is not null";
                var quantidade = sessao.CreateQuery(hqlQuantidade)
                                              .SetInt32("idGrupoCliente", grupoCliente.ID)
                                              .SetDateTime("referencia", referencia)
                                              .UniqueResult<Int64?>();
                if (quantidade > 0)
                    throw new Exception("Não foi possível excluir o lote de faturas. Já existem faturas fechadas.");

                string hqlDelete = @"delete from Tel_Fatura f 
                                     where f.ID in 
                                    (select f.ID from Tel_Fatura f join f.Cliente c where c.GrupoCliente.ID = :idGrupoCliente and f.Referencia = :referencia)";
                int qtdDeletado = sessao.CreateQuery(hqlDelete)
                                       .SetInt32("idGrupoCliente", grupoCliente.ID)
                                       .SetDateTime("referencia", referencia)
                                       .ExecuteUpdate();
                if (qtdDeletado == 0)
                    throw new Exception("Não foram encontradas faturas para os filtros selecionados.");
            }
        }

        public IList<Documento> GetDocumentosAtrasados(Tel_GrupoCliente grupoCliente)
        {
            using (var sessao = GetSession())
            {
                var hql = @"select d from Tel_Fatura f 
                            join f.Documento d 
                            join fetch d.Acrescimo a
                            where f.Cliente.GrupoCliente.ID = :idGrupoCliente 
                            and d.DtUltimaBaixa is not null and d.DtUltimaBaixa > d.DtVencimento
                            and d.ID not in (select fs.Documento.ID from Tel_FaturaServico fs where fs.Documento is not null)";
                return sessao.CreateQuery(hql).SetInt32("idGrupoCliente", grupoCliente.ID).List<Documento>();
            }
        }

        public bool ReferenciaExiste(DateTime referencia, int idPessoa, int idFatura)
        {
            using (var sessao = GetSession())
            {
                var hql = "select count (*) from Tel_Fatura f where f.Referencia = :referencia and f.Cliente.Pessoa.ID = :idPessoa and f.ID <> :idFatura";
                var qtd = sessao.CreateQuery(hql)
                                .SetDateTime("referencia", referencia)
                                .SetInt32("idFatura", idFatura)
                                .SetInt32("idPessoa", idPessoa)
                                .UniqueResult<long?>();
                return qtd > 0;
            }
        }

        public IList<pxyTel_Fatura> GetRelatorioFaturas(Tel_GrupoCliente grupoCliente, DateTime referencia)
        {
            using (var sessao = GetSession())
            {
                return sessao.CreateQuery(GetQueryPxyFatura(" and c.GrupoCliente.ID = :idGrupoCliente and f.Referencia = :referencia"))
                             .SetInt32("idGrupoCliente", grupoCliente.ID)
                             .SetDateTime("referencia", referencia)
                             .SetResultTransformer(Transformers.AliasToBeanConstructor(GetConstructorInfoPxyFatura()))
                             .List<pxyTel_Fatura>();
            }
        }

        public IList<pxyTel_Fatura> GetRelatorioFaturas(IList<int> idsFaturas)
        {
            using (var sessao = GetSession())
            {
                StringBuilder where = new StringBuilder(" and f.ID in (");
                for (int i = 0; i < idsFaturas.Count(); i++)
                {
                    if (i > 0)
                        where.Append(", ");
                    where.Append(idsFaturas[i]);
                }
                where.Append(")");

                return sessao.CreateQuery(GetQueryPxyFatura(where.ToString()))
                             .SetResultTransformer(Transformers.AliasToBeanConstructor(GetConstructorInfoPxyFatura()))
                             .List<pxyTel_Fatura>();
            }
        }

        private string GetQueryPxyFatura(string complementoWhere)
        {
            return @"select f.ID as id,
                               f.Vencimento as vencimento,                               
                               f.Valor as valorPagar,
                               f.Codigo as codigoFatura,
                               gct.Telefone as numeroPiloto,
                               pes.Nome as nome,
                               endereco.Contato as contato,
                               endereco.Endereco as endereco,
                               endereco.Numero as numeroEndereco,
                               endereco.Bairro as bairro,
                               endereco.Complemento as complemento,
                               cid.Nome as cidade,
                               endereco.CEP as cep,
                               f.Documento.ID as idDocumento,
                               c.ImprimirLigacoesLocais as imprimirLigacoesLocais,
                               pes.Fantasia as fantasia,
                               estado.Sigla as estado
                               from Tel_Fatura f
                               join f.Cliente c
                               join c.Pessoa pes
                               left join pes.PessoaEnderecos endereco
                               left join endereco.Cidade cid 
                               left join c.Telefones tel
                               left join tel.GrupoClienteTelefone gct
                               left join cid.UF estado
                               where f.Documento is not null and endereco.BPrincipal = true and tel.BAgrupador = true " + complementoWhere;
        }

        private ConstructorInfo GetConstructorInfoPxyFatura()
        {
            Type tipo = typeof(pxyTel_Fatura);
            ConstructorInfo construtor = tipo.GetConstructor(new Type[] { 
                                                                typeof(int), typeof(DateTime), typeof(decimal), typeof(int)
                                                                , typeof(string), typeof(string), typeof(string), typeof(string), typeof(string)
                                                                , typeof(string), typeof(string), typeof(string) , typeof(string), typeof(int)
                                                                , typeof(bool), typeof(string),typeof(string)});
            return construtor;
        }

        public IList<pxyPesquisaTel_Fatura> GetPesquisaFatura(DateTime dataInicio, DateTime dataFim, Tel_Cliente cliente)
        {
            using (var sessao = GetSession())
            {
                string hql = @"select d.Codigo as codigoDoc,
                               f.Referencia as referencia, 
                               f.Codigo as codigoFatura,
                               d.NumDocumento as NumeroDoc, 
                               d.DtVencimento as vencimento,
                               d.Saldo as saldo,
                               d.Situacao as situacao,
                               d.ValorTotal as total,
                               d.ID as idDoc,
                               f.ID as idFatura,
                               f.DataEnvio as DataEnvio,
                               f.bEnviadoEmail as BEnviadoEmail
                               from Tel_Fatura f
                               join f.Documento d
                               join f.Cliente c
                               where c.ID = :idCliente and d.DtVencimento >= :dataInicio and d.DtVencimento <= :dataFim";

                Type tipo = typeof(pxyPesquisaTel_Fatura);
                ConstructorInfo construtor = tipo.GetConstructor(new Type[] { typeof(int), typeof(DateTime), typeof(int), typeof(string)
                                                                , typeof(DateTime), typeof(decimal), typeof(string), typeof(decimal)
                                                                , typeof(int),typeof(int), typeof(DateTime?), typeof(bool) });

                IList<pxyPesquisaTel_Fatura> retorno = sessao.CreateQuery(hql.ToString())
                             .SetDateTime("dataInicio", dataInicio)
                             .SetDateTime("dataFim", dataFim)
                             .SetInt32("idCliente", cliente.ID)
                             .SetResultTransformer(Transformers.AliasToBeanConstructor(construtor))
                             .List<pxyPesquisaTel_Fatura>();
                return retorno;
            }
        }

        public IList<pxyPesquisaTel_Fatura> GetPesquisaFaturaEmLote(DateTime dataInicio, DateTime dataFim, Tel_GrupoCliente grupoCliente)
        {
            using (var sessao = GetSession())
            {
                string hql = @"select f.Codigo as codigoFatura,
                               d.Codigo as codigoDoc,
                               d.DtVencimento as vencimento,
                               f.Referencia as referencia,
                               d.ValorTotal as total,
                               g.Nome as grupo,
                               p.Nome as cliente, 
                               p.Fantasia as fantasia,                            
                               d.ID as idDoc,
                               f.ID as idFatura,
                               f.DataEnvio as dataEnvio,
                               f.bEnviadoEmail as bEnviadoEmail,
                               c.NumeroPiloto as numeroPiloto,
                               f.Cliente as clienteCarregado
                               from Tel_Fatura f
                               inner join f.Documento d
                               join f.Cliente c
                               join c.GrupoCliente g
                               join c.Pessoa p 
                               where g.ID = :idGrupoCliente and d.DtVencimento >= :dataInicio and d.DtVencimento <= :dataFim";

                Type tipo = typeof(pxyPesquisaTel_Fatura);
                ConstructorInfo construtor = tipo.GetConstructor(new Type[] { typeof(int), typeof(int), typeof(DateTime), typeof(DateTime), typeof(decimal), 
                                                                              typeof(string), typeof(string), typeof(string), typeof(int), 
                                                                              typeof(int), typeof(DateTime?), typeof(bool), typeof(string), 
                                                                              typeof(Tel_Cliente)});

                IList<pxyPesquisaTel_Fatura> retorno = sessao.CreateQuery(hql.ToString())
                             .SetDateTime("dataInicio", dataInicio)
                             .SetDateTime("dataFim", dataFim)
                             .SetInt32("idGrupoCliente", grupoCliente.ID)
                             .SetResultTransformer(Transformers.AliasToBeanConstructor(construtor))
                             .List<pxyPesquisaTel_Fatura>();
                return retorno;
            }
        }

        public IList<pxyPesquisaTel_Fatura> GetPesquisaFaturaCliente(Tel_Cliente cliente)
        {
            using (var sessao = GetSession())
            {
                string hql = @"select d.Codigo as codigoDoc,
                               f.Referencia as referencia, 
                               f.Codigo as codigoFatura,
                               d.NumDocumento as NumeroDoc, 
                               d.DtVencimento as vencimento,
                               d.Saldo as saldo,
                               d.Situacao as situacao,
                               d.ValorTotal as total,
                               d.ID as idDoc,
                               f.ID as idFatura,
                               f.DataEnvio as DataEnvio,
                               f.bEnviadoEmail as BEnviadoEmail
                               from Tel_Fatura f
                               join f.Documento d
                               join f.Cliente c
                               where c.ID = :idCliente";

                Type tipo = typeof(pxyPesquisaTel_Fatura);
                ConstructorInfo construtor = tipo.GetConstructor(new Type[] { typeof(int), typeof(DateTime), typeof(int), typeof(string)
                                                                , typeof(DateTime), typeof(decimal), typeof(string), typeof(decimal)
                                                                , typeof(int),typeof(int), typeof(DateTime?), typeof(bool) });

                IList<pxyPesquisaTel_Fatura> retorno = sessao.CreateQuery(hql.ToString())
                             .SetInt32("idCliente", cliente.ID)
                             .SetResultTransformer(Transformers.AliasToBeanConstructor(construtor))
                             .List<pxyPesquisaTel_Fatura>();

                return retorno;
            }
        }

        public IList<pxyHistoricoFaturaCliente> GetHistoricoClientes(DateTime dataInicial, DateTime dataFinal, IList<int> idsClientes)
        {
            using (var sessao = GetSession())
            {
                var where = new StringBuilder("where d.DtVencimento >= :dataInicio and d.DtVencimento <= :dataFim and c.ID in (");
                for (int i = 0; i < idsClientes.Count; i++)
                {
                    if (i > 0)
                        where.Append(", ");
                    where.Append(idsClientes[i]);
                }
                where.Append(")");

                return sessao.CreateQuery(GetQueryHistoricoFaturaCliente(where.ToString()))
                             .SetDateTime("dataInicio", dataInicial)
                             .SetDateTime("dataFim", dataFinal)
                             .SetResultTransformer(Transformers.AliasToBeanConstructor(GetConstructorPxyHistoricoFaturaCliente()))
                             .List<pxyHistoricoFaturaCliente>();
            }
        }

        public IList<pxyHistoricoFaturaCliente> GetFaturasInadimplencia(DateTime referencia, Tel_GrupoCliente grupoCliente)
        {
            var hql = @"select 
                   g.Nome as grupo,
                   f.Referencia as referencia, 
                   f.Codigo as codigoFatura,
                   d.Codigo as codigoDoc,
                   d.DtVencimento as vencimento,
                   d.DtUltimaBaixa as dataPagamento,
                   f.Valor as valor,
                   p.Nome as cliente,
                   d.Saldo as saldo,
                   (select SUM(m.Valor) AS Valor from Movimento m where m.Documento.ID = d.ID and m.Operacao in (2, 3)) as totalBaixado,
                   c.NumeroPiloto as numeroPiloto,
                   p.Fantasia as fantasia
                   from Tel_Fatura f
                   join f.Documento d
                   join f.Cliente c
                   join c.GrupoCliente g
                   join c.Pessoa p ";

            using (var sessao = GetSession())
            {
                hql += "where f.Referencia = :referencia and d.Saldo > 0";

                if (grupoCliente == null)
                {
                    return sessao.CreateQuery(hql)
                                 .SetDateTime("referencia", referencia)
                                 .SetResultTransformer(Transformers.AliasToBeanConstructor(GetConstructorPxyHistoricoFaturaCliente()))
                                 .List<pxyHistoricoFaturaCliente>();
                }
                else
                {
                    hql += " and c.GrupoCliente.ID = :idGrupo";

                    return sessao.CreateQuery(hql)
                                 .SetDateTime("referencia", referencia)
                                 .SetInt32("idGrupo", grupoCliente.ID)
                                 .SetResultTransformer(Transformers.AliasToBeanConstructor(GetConstructorPxyHistoricoFaturaCliente()))
                                 .List<pxyHistoricoFaturaCliente>();
                }
            }
        }

        private string GetQueryHistoricoFaturaCliente(string where)
        {
            return @"select 
                   g.Nome as grupo,
                   f.Referencia as referencia, 
                   f.Codigo as codigoFatura,
                   d.Codigo as codigoDoc,
                   d.DtVencimento as vencimento,
                   d.DtUltimaBaixa as dataPagamento,
                   f.Valor as valor,
                   p.Nome as cliente,
                   d.Saldo as saldo,
                   (select m.Valor from Movimento m where m.Documento.ID = d.ID and m.Operacao in (2, 3)) as totalBaixado,
                   c.NumeroPiloto as numeroPiloto,
                   p.Fantasia as fantasia
                   from Tel_Fatura f
                   join f.Documento d
                   join f.Cliente c
                   join c.GrupoCliente g
                   join c.Pessoa p " + where;
        }

        private ConstructorInfo GetConstructorPxyHistoricoFaturaCliente()
        {
            Type tipo = typeof(pxyHistoricoFaturaCliente);
            ConstructorInfo construtor = tipo.GetConstructor(new Type[] { typeof(string),typeof(DateTime), typeof(int), typeof(int), typeof(DateTime)
                                        , typeof(DateTime?), typeof(decimal), typeof(string),
                                        typeof(decimal) , typeof(decimal),typeof(string), typeof(string) });
            return construtor;
        }

        public IList<Tel_Fatura> GetAll(DateTime referenciaInicial)
        {
            IList<Tel_Fatura> faturas = new List<Tel_Fatura>();
            using (var sessao = GetSession())
            {
                string hql = hqlGetAll() + @" where g.Referencia >= :referenciaInicial";
                faturas = sessao.CreateQuery(hql)
                                   .SetDateTime("referenciaInicial", referenciaInicial)
                                   .List<Tel_Fatura>();

                return faturas;
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
