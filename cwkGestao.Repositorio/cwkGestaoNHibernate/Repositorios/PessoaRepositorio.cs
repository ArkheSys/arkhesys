using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using NHibernate;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System.Collections;
using System.Reflection;
using NHibernate.Loader.Criteria;
using NHibernate.Transform;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    class PessoaRepositorio : RepositorioAbstrato<Pessoa>, IRepositorioPessoa
    {
        public override void Create(Pessoa saveObj)
        {
            using (var session = GetSession())
            {
                using (var trans = session.BeginTransaction())
                {
                    try
                    {
                        saveObj.Codigo = MaxCodigo();
                        session.Save(saveObj);
                        trans.Commit();
                    }
                    catch (Exception exc)
                    {
                        trans.Rollback();
                        throw exc;
                    }
                }
            }
        }

        protected override string HqlLoadById()
        {
            return " from Pessoa p " +
                   " left join fetch p.Classificacao " +
                   " left join fetch p.PessoaClientes " +
                   " left join fetch p.PessoaEnderecos e" +
                        " left join fetch e.Cidade cid" +
                            " left join fetch cid.UF " +
                   " left join fetch p.PessoaFuncionarios " +
                   " left join fetch p.PessoaTelefones " +
                   " left join fetch p.PessoaVendedors " +
                   " left join fetch p.PessoaAvisos " +
                   " left join fetch p.PessoaEmails " +
                   " left join fetch p.Filial " +
                   " where p.ID = :id";
        }

        public override Pessoa LoadObjectById(int id)
        {
            using (var session = GetSession())
            {
                Pessoa pessoa = session.Get<Pessoa>(id);
                if (pessoa != null)
                {
                    InitializeCamposPessoa(pessoa);
                }
                return pessoa;
            }
        }

        private static void InitializeCamposPessoa(Pessoa pessoa)
        {
            NHibernateUtil.Initialize(pessoa.Classificacao);
            NHibernateUtil.Initialize(pessoa.PessoaClientes);
            NHibernateUtil.Initialize(pessoa.PessoaEnderecos);
            NHibernateUtil.Initialize(pessoa.PessoaFuncionarios);

            NHibernateUtil.Initialize(pessoa.PessoaTelefones);
            NHibernateUtil.Initialize(pessoa.PessoaVendedores);
            NHibernateUtil.Initialize(pessoa.PessoaEmails);
            NHibernateUtil.Initialize(pessoa.PessoaAvisos);
            NHibernateUtil.Initialize(pessoa.Filial);
            try
            {
                NHibernateUtil.Initialize(pessoa.PessoaClientes.First().TabelaPreco);
            }
            catch
            {
            }
            try
            {
                NHibernateUtil.Initialize(pessoa.PessoaClientes.First().Condicao);
            }
            catch
            {
            }
            try
            {
                NHibernateUtil.Initialize(pessoa.PessoaClientes.First().Vendedor);
            }
            catch
            {
            }
            try
            {
                NHibernateUtil.Initialize(pessoa.PessoaVendedores.First().ProdutoVendedorComissoes);
                foreach (var produtoVendedorComissoe in pessoa.PessoaVendedores.First().ProdutoVendedorComissoes)
                {
                    switch (produtoVendedorComissoe.TipoComissaoProdutoServico)
                    {
                        case ProdutoVendedorComissao.TipoComissaoProdutoServicoType.Servico:
                            NHibernateUtil.Initialize(produtoVendedorComissoe.Servico);
                            break;
                        case ProdutoVendedorComissao.TipoComissaoProdutoServicoType.GrupoEstoque:
                            NHibernateUtil.Initialize(produtoVendedorComissoe.GrupoEstoque);
                            break;
                        default:
                            NHibernateUtil.Initialize(produtoVendedorComissoe.Produto);
                            break;
                    }
                }
            }
            catch
            {
            }
            try
            {
                NHibernateUtil.Initialize(pessoa.PessoaEnderecos.Where(e => e.BPrincipal == true).First().Cidade);
                NHibernateUtil.Initialize(pessoa.PessoaEnderecos.Where(e => e.BPrincipal == true).First().Cidade.UF);
            }
            catch
            {
            }
        }

        protected override string hqlGetAll()
        {
            return @"   select new Pessoa(pessoa.ID, pessoa.Codigo, pessoa.Nome, pessoa.Fantasia, pessoa.CNPJ_CPF,
                                        endereco.Endereco, endereco.Numero, endereco.Complemento, endereco.Bairro
                                        , cidade.Nome, estado.Sigla, telefone.Numero, telefone.Contato , classificacao.Descricao
                                        , pessoa.BCliente, pessoa.BFuncionario, pessoa.BVendedor, pessoa.BFornecedor
                                        , vendedor.Nome, pessoa.TipoPessoa, pessoa.Apelido, pessoa.IncData, pessoa.IncUsuario, pessoa.AltData 
                                        , pessoa.AltUsuario, pessoa.DataInativacao, pessoa.MotivoInativacao, pessoa.DiasPadraoLeitura) 
                        from Pessoa pessoa 

                        left join pessoa.Classificacao classificacao 
                        left join pessoa.PessoaTelefones telefone 
                        left join pessoa.PessoaEnderecos endereco
                        left join endereco.Cidade cidade 
                        left join cidade.UF estado 
                        left join pessoa.PessoaClientes cliente
                        left join cliente.Vendedor vendedor";
        }

        public override IList<Pessoa> GetAll()
        {
            using (var session = GetSession())
            {
                string hql = hqlGetAll()
                        + @"  where  
                             telefone.id in (select max(pt.ID)  from pessoa.PessoaTelefones pt  where pt.BPrincipal  = true)
                             and endereco.ID in (select max(e.ID) from pessoa.PessoaEnderecos e where e.BPrincipal = true)";
                IList<Pessoa> all = session.CreateQuery(hql).List<cwkGestao.Modelo.Pessoa>();
                return all;
            }
        }

        public IList<Pessoa> GetAllFornecedores()
        {
            using (var session = GetSession())
            {
                string hql = hqlGetAll()
                        + @" where pessoa.BFornecedor = true 
                             and telefone.id in (select max(pt.ID)  from pessoa.PessoaTelefones pt  where pt.BPrincipal  = true)
                             and endereco.ID in (select max(e.ID) from pessoa.PessoaEnderecos e where e.BPrincipal = true)";
                return session.CreateQuery(hql).List<cwkGestao.Modelo.Pessoa>();
            }
        }

        public IList<Pessoa> GetAllOperadoresCartao()
        {
            using (var session = GetSession())
            {
                string hql = hqlGetAll()
                        + @" where pessoa.TipoPessoa = 'Operadora'";
                return session.CreateQuery(hql).List<cwkGestao.Modelo.Pessoa>();
            }
        }


        public Pessoa GetClienteMercadoLivre(int id)
        {
            using (var session = GetSession())
            {
                string hql = hqlGetAll()
                            + @" where pessoa.BCliente = true and endereco.BPrincipal = true and telefone.id in 
                                (select max(pt.ID) from pessoa.PessoaTelefones pt where pt.BPrincipal = true) and pessoa.IDMercadoLivre = " + id;
                Pessoa objPessoa = session.CreateQuery(hql).UniqueResult<Pessoa>();
                return objPessoa;
            }
        }

        public IList<Pessoa> GetAllVendedores()
        {
            using (var session = GetSession())
            {
                string hql = hqlGetAll()
                        + @" where pessoa.BVendedor = true and endereco.BPrincipal = true and telefone.id in 
                                (select max(pt.ID) from pessoa.PessoaTelefones pt where pt.BPrincipal = true)";
                return session.CreateQuery(hql).List<cwkGestao.Modelo.Pessoa>();
            }
        }

        public IList<Pessoa> GetAllContabilistas()
        {
            using (var session = GetSession())
            {
                string hql = hqlGetAll()
                        + @" where pessoa.bContabilista = true and endereco.BPrincipal = true and telefone.id in 
                                (select max(pt.ID) from pessoa.PessoaTelefones pt where pt.BPrincipal = true)";
                return session.CreateQuery(hql).List<cwkGestao.Modelo.Pessoa>();
            }
        }

        public IList<Pessoa> GetAllFuncionarios()
        {
            using (var session = GetSession())
            {
                string hql = hqlGetAll()
                        + @" where pessoa.BFuncionario = true 
                             and telefone.id in (select max(pt.ID) from pessoa.PessoaTelefones pt  where pt.BPrincipal  = true)
                             and endereco.ID in (select max(e.ID) from pessoa.PessoaEnderecos e where e.BPrincipal = true)";
                return session.CreateQuery(hql).List<cwkGestao.Modelo.Pessoa>();
            }
        }

        public IList<Pessoa> GetAllEntregadores()
        {
            using (var session = GetSession())
            {
                string hql = hqlGetAll()
                        + @" where pessoa.BEntregador = true 
                             and telefone.id in (select max(pt.ID) from pessoa.PessoaTelefones pt  where pt.BPrincipal  = true)
                             and endereco.ID in (select max(e.ID) from pessoa.PessoaEnderecos e where e.BPrincipal = true)";
                return session.CreateQuery(hql).List<cwkGestao.Modelo.Pessoa>();
            }
        }

        public IList<Pessoa> GetAllClientes()
        {
            IList<Pessoa> result = new List<Pessoa>();
            using (var session = GetSession())
            {
                string hql = hqlGetAll()
                        + @" where pessoa.BCliente = true
                             and telefone.id in (select max(pt.ID)  from pessoa.PessoaTelefones pt  where pt.BPrincipal  = true)
                             and endereco.ID in (select max(e.ID) from pessoa.PessoaEnderecos e where e.BPrincipal = true)";
                result = session.CreateQuery(hql).List<cwkGestao.Modelo.Pessoa>().ToList();
                return result;
            }
        }

        public IList<Pessoa> GetAllTransportadoras()
        {
            using (var session = GetSession())
            {
                string hql = hqlGetAll() + @" where pessoa.BTransportadora = true 
                             and telefone.id in (select max(pt.ID)  from pessoa.PessoaTelefones pt  where pt.BPrincipal  = true)
                             and endereco.ID in (select max(e.ID) from pessoa.PessoaEnderecos e where e.BPrincipal = true)";

                return session.CreateQuery(hql).List<Pessoa>();
            }
        }

        public Pessoa GetByCodigo(int codigo, string propriedadeTrue)
        {
            using (var session = GetSession())
            {
                string hql = "from Pessoa p where p.Codigo = :codigo and p.BFuncionario = true ";
                return session.CreateQuery(hql).SetInt32("codigo", codigo).UniqueResult<Pessoa>();
            }
        }

        public Pessoa GetByNome(string value, string propriedadeTrue)
        {
            using (var session = GetSession())
            {
                string hql = "from Pessoa p where p.Nome like :nome and p." + propriedadeTrue + " = true";
                return session.CreateQuery(hql).SetString("nome", value + "%").SetFirstResult(0).SetMaxResults(1).UniqueResult<Pessoa>();
            }
        }

        public Pessoa GetByFantasia(string fantasia)
        {
            using (var session = GetSession())
            {
                string hql = "from Pessoa p where p.Fantasia = :fantasia";

                return session.CreateQuery(hql).SetString("fantasia", fantasia).UniqueResult<Pessoa>();
            }
        }

        public bool GetByCPFeCNPJ(string value, int? IDSelecionado)
        {
            using (var session = GetSession())
            {
                if (IDSelecionado.HasValue)
                {
                    string hql = "select count(*) from Pessoa p where replace(replace(replace(p.CNPJ_CPF, '.', ''), '-',''), '/', '') like :valor and ID != :id";
                    return session.CreateQuery(hql).SetString("valor", value + "%").SetInt32("id", IDSelecionado.Value).UniqueResult<Int64>() > 0;
                }
                else
                {
                    string hql = "select count(*) from Pessoa p where replace(replace(replace(p.CNPJ_CPF, '.', ''), '-',''), '/', '') like :valor"                        ;
                    return session.CreateQuery(hql).SetString("valor", value + "%").UniqueResult<Int64>() > 0;
                }
            }
        }

        public IList<Pessoa> GetAtivosByCPFeCNPJ(string value, bool bAtivo)
        {
            IList<Pessoa> retorno = new List<Pessoa>();
            using (var session = GetSession())
            {
                string hql = "from Pessoa p where p.CNPJ_CPF like :valor and p.BAtivo = :bAtivo";
                retorno = session.CreateQuery(hql).SetString("valor", value + "%").SetBoolean("bAtivo", bAtivo).List<Pessoa>();
            }

            return retorno;
        }

        public int GetIDByCPFeCNPJ(string value)
        {
            using (var session = GetSession())
            {
                string hql = "select id from Pessoa p where p.CNPJ_CPF like :valor";
                return session.CreateQuery(hql).SetString("valor", value + "%").UniqueResult<int>();
            }
        }

        public IList<ImpressaoEtiquetaPessoaView> GetImpressaoEtiquetaPessoa()
        {
            using (var session = GetSession())
            {
                StringBuilder hql = new StringBuilder();
                hql.AppendLine("select p.Nome as Nome");
                hql.AppendLine(", c.Nome as Cidade");
                hql.AppendLine(", u.Sigla as UF");
                hql.AppendLine(", e.Endereco as Endereco");
                hql.AppendLine(", e.Numero as Numero");
                hql.AppendLine(", e.Complemento as Complemento");
                hql.AppendLine(", e.Bairro as Bairro");
                hql.AppendLine(", e.CEP as CEP");
                hql.AppendLine(", p.Codigo as Codigo");
                hql.AppendLine(", p.BFuncionario as BFuncionario");
                hql.AppendLine(", p.BVendedor as BVendedor");
                hql.AppendLine("from PessoaEndereco e");
                hql.AppendLine("join e.Pessoa p");
                hql.AppendLine("join e.Cidade c");
                hql.AppendLine("join c.UF u");
                hql.AppendLine("where e.BPrincipal = true");

                IQuery query = session.CreateQuery(hql.ToString());

                ConstructorInfo construtor = typeof(ImpressaoEtiquetaPessoaView).GetConstructor(new Type[] {
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(int),
                typeof(bool),
                typeof(bool),
                });

                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<ImpressaoEtiquetaPessoaView>();
            }
        }

        public Hashtable GetPessoasImportadas()
        {
            using (var session = GetSession())
            {
                string hql = @" select p 
                                from Pessoa p
                                left join p.Filial filial
                                left join p.PessoaTelefones telefone 
                                left join p.PessoaEnderecos endereco
                                left join endereco.Cidade cidade 
                                left join cidade.UF estado 
                                where p.IDIntegracao > 0";

                var resultado = session.CreateQuery(hql).List<Pessoa>();
                Hashtable ret = new Hashtable();
                foreach (var item in resultado)
                {
                    if (!ret.ContainsKey(item.IDIntegracao))
                        ret.Add(item.IDIntegracao, item);
                }
                return ret;
            }
        }

        public IList<Pessoa> GetClienteTel_Clientes()
        {
            using (var session = GetSession())
            {
                //                string hql = @"select * from Pessoa as p
                //                             join Tel_Cliente tc on tc.IdPessoa = p.ID";

                String hql = @"Select p 
                               From Tel_Cliente tc
                               left join tc.Pessoa p
                               left join fetch p.Classificacao classificacao 
                               left join fetch p.PessoaTelefones telefone 
                               left join fetch p.PessoaEnderecos endereco
                               left join fetch endereco.Cidade cidade 
                               left join fetch cidade.UF estado 
                               left join fetch p.PessoaClientes cliente
                               left join fetch cliente.Vendedor vendedor";


                return session.CreateQuery(hql).List<Pessoa>();
            }
        }

        public IList<Pessoa> GetParaCadastroEngenheiro()
        {
            using (var session = GetSession())
            {
                string hql = hqlGetAll()
                          + " where endereco.BPrincipal = true and telefone.BPrincipal = true"
                           + " and pessoa.TipoPessoa = :tipo and pessoa.ID not in (select eng.Pessoa.ID from PessoaEngenheiro eng)";
                return session.CreateQuery(hql)
                                           .SetString("tipo", "Física")
                                           .List<cwkGestao.Modelo.Pessoa>();
            }
        }

        public int MaxCodigoIntegracao()
        {
            using (var session = GetSession())
            {
                try
                {
                    string hql = "select p from Pessoa p where p.ID = (select max(ID) from Pessoa)";
                    return session.CreateQuery(hql).UniqueResult<Pessoa>().ID + 1;
                }
                catch
                {
                    return 1;
                }
            }
        }

        public Pessoa GetByIdIntegracao(string idIntegracao)
        {
            using (var session = GetSession())
            {
                string hql = "from Pessoa p where p.IDIntegracao = :idIntegracao";
                var pessoa = session.CreateQuery(hql)
                                    .SetString("idIntegracao", idIntegracao)
                                    .SetFirstResult(0)
                                    .SetMaxResults(1)
                                    .UniqueResult<Pessoa>();
                if (pessoa != null)
                {
                    InitializeCamposPessoa(pessoa);
                }
                return pessoa;
            }
        }

        public Pessoa FindByTextField(string text)
        {
            using (var session = GetSession())
            {
                string hql = "from Pessoa p where p.Nome like :text OR p.Codigo like :text AND p.BTransportadora = true";

                var pessoa = session.CreateQuery(hql).SetString("text", text).UniqueResult<Pessoa>();

                if (pessoa != null)
                    InitializeCamposPessoa(pessoa);

                return pessoa;
            }
        }

        public Pessoa FindByIDSISeCommerce(int id)
        {
            using (var session = GetSession())
            {
                string hql = "from Pessoa p where p.IDSISeCommerce = :idSISeC";
                Pessoa p = session.CreateQuery(hql).SetInt32("idSISeC", id).UniqueResult<Pessoa>();
                if (p != null)
                {
                    InitializeCamposPessoa(p);
                }
                return p;
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public IList<Pessoa> GetAllPorDiaPadraoLeitura(int dtInicio, int dtFim)
        {
            using (var session = GetSession())
            {
                string hql = hqlGetAll() + @" where pessoa.BCliente = true
                             and telefone.id in (select max(pt.ID)  from pessoa.PessoaTelefones pt  where pt.BPrincipal  = true)
                             and endereco.ID in (select max(e.ID) from pessoa.PessoaEnderecos e where e.BPrincipal = true)
                             and pessoa.DiasPadraoLeitura 
                             between :dtInicio and :dtFim 
                             order by pessoa.DiasPadraoLeitura";

                var retorno = session.CreateQuery(hql).SetInt32("dtInicio", dtInicio).SetInt32("dtFim", dtFim).List<Pessoa>();
                return retorno;
            }
        }

        public IList<Pessoa> GetEntregadores()
        {
            using (var session = GetSession())
            {
                string hql = "from Pessoa p where p.BEntregador = :BEntregador";
                return session.CreateQuery(hql).SetInt32("BEntregador", 1).List<Pessoa>();
            }
        }
    }
}
