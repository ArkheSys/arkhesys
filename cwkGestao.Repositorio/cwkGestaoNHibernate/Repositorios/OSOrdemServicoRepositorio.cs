using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using NHibernate.Transform;
using NHibernate;
using System.Reflection;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class OSOrdemServicoRepositorio : RepositorioAbstrato<OSOrdemServico>, IRepositorioOSOrdemServico
    {
        protected override string HqlLoadById()
        {
            return @"from OSOrdemServico os 
                     left join fetch os.Equipamento eq
                     left join fetch os.Empresa ep
                     where os.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return @"from OSOrdemServico os 
                     left join fetch os.Equipamento eq
                     left join fetch os.Empresa ep";
        }

        //public IList<object> GetListagemOrdemServico(OSOrdemServico.StatusOS pStatus, string pCliente, DateTime pDataInicial, DateTime pDataFinal, string pEmpresas)

        public IList<object> GetListagemOrdemServico(int idStatus, string pCliente, DateTime pDataInicial, DateTime pDataFinal, string pEmpresas)
        {
            using (var session = GetSession())
            {
                StringBuilder hql = new StringBuilder();
                hql.AppendLine("Select os.Codigo, os.Data");
                hql.AppendLine(", s.Descricao AS Status");
                hql.AppendLine(", c.Nome AS Cliente,  e.Descricao AS Equipamento");
                hql.AppendLine(", os.ProblemaRelatado AS ServicoSolicitado");
                hql.AppendLine("FROM OSOrdemServico os ");
                hql.AppendLine("join os.Cliente c");
                hql.AppendLine("join os.Equipamento e");
                hql.AppendLine("join os.Empresa emp");
                hql.AppendLine("join os.Status s");
                hql.AppendLine("where 1 = 1 AND os.Data >= :dataInicial");
                hql.AppendLine(" AND os.Data <= :dataFinal");
                hql.AppendLine(" AND emp.ID in " + pEmpresas);

                if (idStatus >= 0)
                    hql.AppendLine(" AND s.ID = '" + idStatus + "'");

                if (pCliente != null)
                    hql.AppendLine(" AND c.ID = '" + pCliente + "'");


                IQuery query = session.CreateQuery(hql.ToString()).SetDateTime("dataInicial", pDataInicial).SetDateTime("dataFinal", pDataFinal.AddHours(23).AddMinutes(59));

                ConstructorInfo construtor = typeof(pxyRelOSOrdemServico).GetConstructor(new Type[] {
                typeof(int),
                typeof(DateTime),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string)
                });

                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<object>();
            }
        }

        public IList<object> GetOrdemServicoImpressao(int pIDOS)
        {
            using (var session = GetSession())
            {
                StringBuilder hql = new StringBuilder();
                hql.AppendLine(@"Select 
                                        e.Nome AS NomeEmpresa ,
                                        e.Endereco AS EndEmpresa,
                                        e.Numero AS NumEmpresa,
                                        e.Bairro AS BairroEmpresa,
                                        cid.Nome AS CidadeEmpresa,
                                        EmpUF.Sigla AS UFEmpresa,
                                        e.CEP AS CEPEmpresa,
                                        e.Telefone AS TelEmpresa,
                                        e.Email AS EmailEmpresa,
                                        os.Codigo AS CodigoOS,
                                        c.Nome AS ClienteOS,
                                        contato.Contato AS ContatoOS,
                                        endereco.Endereco AS EndOS,
                                        CidCli.Nome AS CidadeOS,
                                        pem.Email AS EmailOS,
                                        c.CNPJ_CPF AS CPF_CNPJOS,
                                        contato.Numero AS TelOS,
                                        c.Inscricao AS IEOS,
                                        endereco.CEP AS CEPOS,
                                        UFCli.Sigla AS UFOS,
                                        eq.Descricao AS EquipamentoOS,
                                        os.ProblemaRelatado AS ServicoSolicitadoOS,
                                        os.Acessorios AS AcessorioOS,
                                        endereco.Complemento AS ComplementoOS,
                                        endereco.Numero AS NumEndOS,
                                        eq.Codigo AS CodOS,
                                        os.Data AS DataOS
                                FROM OSOrdemServico os 
                                        join os.Cliente c
                                        join os.Equipamento eq
                                        join os.Empresa e
                                        join e.Cidade cid
                                        join cid.UF EmpUF
                                        join os.Status status
                                        left join c.PessoaEnderecos endereco
                                        left join endereco.Cidade CidCli
                                        join CidCli.UF UFCli
                                        left join c.PessoaTelefones contato
                                        left join c.PessoaEmails pem
                                where 1 = 1 
                                        and endereco.BPrincipal = true 
                                        and contato.BPrincipal = true 
                                        and os.ID = :IDOS");

                IQuery query = session.CreateQuery(hql.ToString()).SetInt32("IDOS", pIDOS);

                ConstructorInfo construtor = typeof(pxyOSOrdemServico).GetConstructor(new Type[] {
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(int),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(int),
                typeof(DateTime)
                });

                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<object>();
            }
        }

        public OSOrdemServico GetOSporCodigo(int pCodOS)
        {
            using (var session = GetSession())
            {
                StringBuilder hql = new StringBuilder();
                hql.Append("from OSOrdemServico os left join fetch os.Equipamento where os.Codigo = :CodOS");

                IQuery query = session.CreateQuery(hql.ToString()).SetInt32("CodOS", pCodOS);
                List<OSOrdemServico> result = query.List<OSOrdemServico>().ToList();
                if (result.Count() > 0)
                {
                    return query.List<OSOrdemServico>().First();
                }
                return new OSOrdemServico();
            }
        }


        public OSOrdemServico LoadFullObjectByID(int id)
        {
            using (var session = GetSession())
            {
                string hql = @"select distinct os from OSOrdemServico os 
                            left join fetch os.Cliente cli
                            left join fetch cli.PessoaTelefones
                            left join fetch os.Empresa
                            left join fetch os.Equipamento
                            left join fetch os.Vendedor
                            left join fetch os.Condicao
                            left join fetch os.Tecnico
                            left join fetch os.TabelaPreco

                               where os.ID = :id";

                IQuery query = session.CreateQuery(hql.ToString()).SetInt32("id", id);

                OSOrdemServico retorno = query.List<OSOrdemServico>().First();
                NHibernateUtil.Initialize(retorno.OSProdutoItems);
                NHibernateUtil.Initialize(retorno.OSServicoItems);

                return retorno;

            }
        }


        public IList<pxyFaturamentoOS> GetFaturamento(Pessoa pessoa, Filial filial, DateTime dataInicial, DateTime dataFinal)
        {
            using (var session = GetSession())
            {
                StringBuilder hql = new StringBuilder();
                hql.AppendLine(@"select
                                os.ID,
                                os.Codigo,
                                os.Data,                                
                                sum(produtos.Quantidade) as QtdTotProdutos,
                                sum(servicos.Quantidade) as QtdTotServicos,
                                os.ProdutoDescontoValor as DescontoProdutos, 
                                os.ServicoDescontoValor as DescontoServicos,
                                os.Total as TotalPedido,
                                equipamento.Descricao + '[' + equipamento.NumeroSerie + ']' as descequipamento,
                                os.DespesasAdicionais
                                from OSOrdemServico os 
                                left join os.OSProdutoItems produtos
                                left join os.OSServicoItems servicos
                                left join os.Equipamento equipamento
                                join os.Status status
                                where os.Cliente.ID = :idcliente
                                and os.Empresa.ID = :idempresa
                                and status.BFaturar = 1
                                and os.Data >= :datainicial and os.Data < :datafinal
                                group by os.ID, os.Codigo, os.Data, os.ServicoDescontoValor, os.ProdutoDescontoValor, os.Total, os.DespesasAdicionais, equipamento.Descricao, equipamento.NumeroSerie");

                IQuery query = session.CreateQuery(hql.ToString())
                               .SetInt32("idcliente", pessoa.ID)
                               .SetInt32("idempresa", filial.ID)
                               .SetDateTime("datainicial", dataInicial.Date)
                               .SetDateTime("datafinal", dataFinal.Date.AddDays(1));

                ConstructorInfo construtor = typeof(pxyFaturamentoOS).GetConstructor(new Type[] {                
                typeof(int),
                typeof(int),
                typeof(DateTime),
                typeof(decimal),
                typeof(decimal),
                typeof(decimal),
                typeof(decimal),
                typeof(decimal),
                typeof(string),
                typeof(decimal)
                });

                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxyFaturamentoOS>();
            }
        }

        public List<OSOrdemServico> Get(List<int> ids)
        {
            using (var session = GetSession())
            {
                StringBuilder hql = new StringBuilder();
                hql.AppendLine("select os from OSOrdemServico os");
                hql.AppendLine("left join fetch os.OSServicoItems servItems");
                hql.AppendLine("left join fetch servItems.Servico servico");
                hql.AppendLine("left join fetch os.OSProdutoItems prodItems");
                hql.AppendLine("left join fetch prodItems.Produto produto");
                hql.AppendLine("left join fetch produto.CFOP cfop");
                hql.Append("where os.ID in (");
                for (int i = 0; i < ids.Count; i++)
                {
                    if (i > 0)
                        hql.Append(", ");
                    hql.Append(ids[i]);
                }
                hql.AppendLine(")");
                HashSet<OSOrdemServico> set = new HashSet<OSOrdemServico>(session.CreateQuery(hql.ToString()).List<OSOrdemServico>());
                return set.ToList<OSOrdemServico>();
            }
        }

        public bool EstahAgendado(OSOrdemServico selecionado)
        {
            using (var session = GetSession())
            {
                string hql = "select count(*) from OSAgendamento oa where oa.OSOrdemServico.ID = :idOS";

                return session.CreateQuery(hql).SetInt32("idOS", selecionado.ID).UniqueResult<long>() > 0;

            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            string hql = "from OSOrdemServico x where x.Data between :dataInicio and :dataFim" ;
            return hql;
        }

        public TimeSpan GetTotHorasFaturadas(int idPessoa, DateTime primeiroDiaMes, DateTime ultimoDiaMes)
        {
            using (var session = GetSession())
            {
                string sql = @"select ISNULL(sum(fat.HorasFaturamento), 0) HorasJaContratadas 
                               from OSOrdemServico os
                               join FaturamentoAtendimento fat on os.ID = fat.IDOSOrdemServico
                               join Pessoa p on p.ID = os.IDPessoa
                               where p.ID = :IDPessoa
                               and os.Data between :DataInicial
                               and :DataFinal";

                var retornoDouble = Convert.ToInt64(session.CreateSQLQuery(sql).SetInt32("IDPessoa", idPessoa).SetDateTime("DataInicial", primeiroDiaMes).SetDateTime("DataFinal", ultimoDiaMes).UniqueResult());
                TimeSpan retorno = TimeSpan.FromTicks(retornoDouble);

                return retorno;
            }
        }
    }
}
