using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;
using NHibernate.Transform;
using System.Reflection;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{

    partial class ContratoRepositorio : RepositorioAbstrato<Contrato>, IRepositorioContrato
    {
        protected override string HqlLoadById()
        {
            return "from Contrato n left join fetch n.Servicos s join fetch n.Condicao c where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Contrato n";
        }

        public Contrato GetContratoPorID(int id)
        {
            using (var session = GetSession())
            {
                string hql = " from Contrato c " +
                             " left join fetch c.Servicos " +
                             " left join fetch c.TipoNota " +
                             " left join fetch c.Condicao " +
                             " where c.ID = :id ";

                Contrato contrato = session.CreateQuery(hql).SetInt32("id", id).UniqueResult<Contrato>();
                return contrato;
            }
        }

        //typeof(bool),
        //typeof(DateTime),
        //typeof(string), 
        //typeof(string),
        //typeof(decimal),
        //typeof(string),
        //typeof(IList<PessoaEmail>),
        //typeof(DateTime?),

        public ConstructorInfo GetContructorInfoNotaComContrato()
        {
            return typeof(NotaEmMemoriaComContrato).GetConstructor(new Type[] {
                                                    typeof(int),
                                                    typeof(int),
                                                    typeof(int),
                                                    typeof(int),
                                                    typeof(string)
                                                    });
        }


        public IList<NotaEmMemoriaComContrato> GetNotaComContrato(Pessoa pessoaCliente, DateTime inicio, DateTime fim)
        {

            using (var session = GetSession())
            {
                StringBuilder sql = new StringBuilder();
                sql.Append("select n.ID as idNota,  " +
                           "    d.ID as idDocumento," +
                           "    c.ID as idContrato, " +
                           "    p.ID as idPessoa,   " +
                           "    n.Status            " +
                           "         from Nota n    " +
                           "             INNER JOIN Documento d ON d.IDNota = n.ID      " +
                           "             INNER JOIN Pessoa p    ON p.ID = d.IDPessoa    " +
                           "             INNER JOIN Contrato c  ON c.ID = d.IDContrato  " +
                           "         where n.Dt between :inicio and :fim");

                IQuery query;

                if (pessoaCliente != null)
                {
                    sql.Append(" and p.ID = :idCliente ");
                    query = session.CreateSQLQuery(sql.ToString()).SetInt32("idCliente", pessoaCliente.ID).SetDateTime("inicio", inicio.Date).SetDateTime("fim", fim.Date);
                }
                else
                {
                    query = session.CreateSQLQuery(Convert.ToString(sql)).SetDateTime("inicio", inicio.Date).SetDateTime("fim", fim.Date);
                }

                ConstructorInfo construtor = GetContructorInfoNotaComContrato();
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<NotaEmMemoriaComContrato>();
            }

        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IList<pxRelatorioProdutoContrato> GetDadosRelatorioProdutosEContratos(int? IDPessoa, DateTime dtInicio, DateTime dtFinal, int? ProdutoInativo)
        {
            using (var session = GetSession())
            {
                IQuery query = session.CreateSQLQuery($@"SELECT CONTRATO.ID AS IDCONTRATO,
                                                               CONTRATOPRODUTO.ID AS ID,
                                                        	   CONTRATO.CODIGO,
                                                        	   CONTRATO.DATA,
                                                        	   PESSOA.NOME AS PESSOA,
                                                        	   CAST(CONTRATO.OBSERVACAO AS VARCHAR) AS DESCRICAO,
                                                        	   CAST('PRODUTO' AS VARCHAR) AS TIPO
                                                          FROM CONTRATOPRODUTO
                                                            INNER JOIN CONTRATO ON (CONTRATOPRODUTO.IDCONTRATO = CONTRATO.ID)
                                                        	INNER JOIN PESSOA ON (CONTRATO.IDPESSOA = PESSOA.ID)
                                                        	INNER JOIN PRODUTO ON (CONTRATOPRODUTO.IDProduto = PRODUTO.ID)
                                                          WHERE CONTRATO.DATA BETWEEN :dataInicio AND :dataFim
                                                            {(IDPessoa.HasValue ? "AND PESSOA.ID = :idPessoa" : string.Empty)}
                                                        	{(ProdutoInativo.HasValue ? "AND PRODUTO.INATIVO = :Inativo" : string.Empty)}
                                                        UNION
                                                        SELECT CONTRATO.ID AS IDCONTRATO,
                                                               CONTRATOSERVICO.ID AS ID,
                                                        	   CONTRATO.CODIGO,
                                                        	   CONTRATO.DATA,
                                                        	   PESSOA.NOME AS PESSOA,
                                                        	   CAST(CONTRATO.OBSERVACAO AS VARCHAR) AS DESCRICAO,
                                                        	   CAST('SERVIÇO' AS VARCHAR) AS TIPO
                                                          FROM CONTRATOSERVICO
                                                            INNER JOIN CONTRATO ON (CONTRATOSERVICO.IDCONTRATO = CONTRATO.ID)
                                                        	INNER JOIN PESSOA ON (CONTRATO.IDPESSOA = PESSOA.ID)
                                                          WHERE CONTRATO.DATA BETWEEN :dataInicio AND :dataFim
                                                            {(IDPessoa.HasValue ? "AND PESSOA.ID = :idPessoa" : string.Empty)}
                                                        ORDER BY TIPO").SetDateTime("dataInicio", dtInicio)
                                                                       .SetDateTime("dataFim", dtFinal);
                if (ProdutoInativo.HasValue)
                    query = query.SetInt32("Inativo", ProdutoInativo.Value);

                if (IDPessoa.HasValue)
                    query = query.SetInt32("idPessoa", IDPessoa.Value);

                //int, int, int, DateTime?, string, string, string
                ConstructorInfo construtor = typeof(pxRelatorioProdutoContrato).GetConstructor(new Type[] {
                                                    typeof(int),
                                                    typeof(int),
                                                    typeof(int),
                                                    typeof(DateTime?),
                                                    typeof(string),
                                                    typeof(string),
                                                    typeof(string)
                });
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxRelatorioProdutoContrato>();
            }
        }

        public IList<pxRelatorioProdutoContratoValor> GetDadosRelatorioProdutosEContratosValor(int? IDPessoa, DateTime dtInicio, DateTime dtFinal, int? ProdutoInativo, int? situacaoPessoa)
        {
            using (var session = GetSession())
            {
                IQuery query = session.CreateSQLQuery($@"SELECT DISTINCT * FROM (
                                                         SELECT CONTRATO.ID AS IDCONTRATO,
                                                                CONTRATOPRODUTO.ID AS ID,
                                                         	    CONTRATO.CODIGO,
                                                         	    CONTRATO.DATA,
                                                         	    PESSOA.NOME AS PESSOA,
                                                         	    PESSOA.CNPJ_CPF,
                                                         	    CAST(CONTRATO.OBSERVACAO AS VARCHAR) AS DESCRICAO,
                                                         	    CAST('PRODUTO' AS VARCHAR) AS TIPO,
                                                         	    (SELECT SUM(VALOR) FROM CONTRATOPRODUTO A WHERE A.IDCONTRATO = CONTRATOPRODUTO.IDCONTRATO) AS VALOR
                                                           FROM CONTRATOPRODUTO
                                                             INNER JOIN CONTRATO ON (CONTRATOPRODUTO.IDCONTRATO = CONTRATO.ID)
                                                         	INNER JOIN PESSOA ON (CONTRATO.IDPESSOA = PESSOA.ID)
                                                         	INNER JOIN PRODUTO ON (CONTRATOPRODUTO.IDPRODUTO = PRODUTO.ID)
                                                           WHERE CONTRATOPRODUTO.DATAINICIOFATURAMENTO BETWEEN :dataInicio AND :dataFim
                                                             {(IDPessoa.HasValue ? "AND PESSOA.ID = :idPessoa" : string.Empty)}
                                                         	 {(ProdutoInativo.HasValue ? "AND PRODUTO.INATIVO = :Inativo" : string.Empty)}
                                                             {(situacaoPessoa.HasValue ? "AND PESSOA.BATIVO = :bAtivo" : string.Empty)}
                                                         UNION
                                                         SELECT CONTRATO.ID AS IDCONTRATO,
                                                                CONTRATOSERVICO.ID AS ID,
                                                         	   CONTRATO.CODIGO,
                                                         	   CONTRATO.DATA,
                                                         	   PESSOA.NOME AS PESSOA,
                                                         	   PESSOA.CNPJ_CPF,
                                                         	   CAST(CONTRATO.OBSERVACAO AS VARCHAR) AS DESCRICAO,
                                                         	   CAST('SERVIÇO' AS VARCHAR) AS TIPO,
                                                         	   (SELECT SUM(VALOR) FROM CONTRATOSERVICO A WHERE A.IDCONTRATO = CONTRATOSERVICO.IDCONTRATO) AS VALOR
                                                           FROM CONTRATOSERVICO
                                                             INNER JOIN CONTRATO ON (CONTRATOSERVICO.IDCONTRATO = CONTRATO.ID)
                                                         	INNER JOIN PESSOA ON (CONTRATO.IDPESSOA = PESSOA.ID)
                                                           WHERE CONTRATOSERVICO.DATAINICIOFATURAMENTO BETWEEN :dataInicio AND :dataFim
                                                            {(IDPessoa.HasValue ? "AND PESSOA.ID = :idPessoa" : string.Empty)}
                                                            {(situacaoPessoa.HasValue ? "AND PESSOA.BATIVO = :bAtivo" : string.Empty)}
                                                         ) AS X ORDER BY PESSOA;").SetDateTime("dataInicio", dtInicio)
                                                                         .SetDateTime("dataFim", dtFinal);
                if (ProdutoInativo.HasValue)
                    query = query.SetInt32("Inativo", ProdutoInativo.Value);

                if (IDPessoa.HasValue)
                    query = query.SetInt32("idPessoa", IDPessoa.Value);

                if (situacaoPessoa.HasValue)
                    query = query.SetInt32("bAtivo", situacaoPessoa.Value);

                ConstructorInfo construtor = typeof(pxRelatorioProdutoContratoValor).GetConstructor(new Type[] {
                                                    typeof(int),
                                                    typeof(int),
                                                    typeof(int),
                                                    typeof(DateTime?),
                                                    typeof(string),
                                                    typeof(string),
                                                    typeof(string),
                                                    typeof(string),
                                                    typeof(decimal)
                });
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxRelatorioProdutoContratoValor>();
            }
        }
    }
}
