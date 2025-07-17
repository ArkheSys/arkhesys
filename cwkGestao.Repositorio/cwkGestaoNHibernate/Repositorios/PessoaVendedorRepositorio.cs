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
    partial class PessoaVendedorRepositorio : RepositorioAbstrato<PessoaVendedor>, IRepositorioPessoaVendedor
    {
        public override PessoaVendedor LoadObjectById(int id)
        {
            using (var session = GetSession())
            {
                PessoaVendedor vendedor = session.Get<PessoaVendedor>(id);

                NHibernateUtil.Initialize(vendedor.ProdutoVendedorComissoes);
                NHibernateUtil.Initialize(vendedor.Pessoa.Classificacao);
                NHibernateUtil.Initialize(vendedor.Pessoa.PessoaClientes);
                NHibernateUtil.Initialize(vendedor.Pessoa.PessoaEnderecos);
                NHibernateUtil.Initialize(vendedor.Pessoa.PessoaFuncionarios);
                NHibernateUtil.Initialize(vendedor.Pessoa.PessoaTelefones);
                NHibernateUtil.Initialize(vendedor.Pessoa.PessoaVendedores);
                NHibernateUtil.Initialize(vendedor.Pessoa.PessoaEmails);
                NHibernateUtil.Initialize(vendedor.Pessoa.PessoaAvisos);
                NHibernateUtil.Initialize(vendedor.Pessoa.Filial);
                try { NHibernateUtil.Initialize(vendedor.Pessoa.PessoaClientes.First().TabelaPreco); }
                catch { }
                try { NHibernateUtil.Initialize(vendedor.Pessoa.PessoaClientes.First().Condicao); }
                catch { }
                try { NHibernateUtil.Initialize(vendedor.Pessoa.PessoaClientes.First().Vendedor); }
                catch { }
                try
                {
                    NHibernateUtil.Initialize(vendedor.Pessoa.PessoaEnderecos.Where(e => e.BPrincipal == true).First().Cidade);
                    NHibernateUtil.Initialize(vendedor.Pessoa.PessoaEnderecos.Where(e => e.BPrincipal == true).First().Cidade.UF);
                }
                catch { }

                return vendedor;
            }
        }

        protected override string HqlLoadById()
        {
            throw new NotImplementedException();
        }

        protected override string hqlGetAll()
        {
            throw new NotImplementedException();
        }

        public IList<object> GetListaClientePorVendedor(string pVendedor, string pLetraInicial, string pLetraFinal, string pEmpresas)
        {
            using (var session = GetSession())
            {
                StringBuilder hql = new StringBuilder();
                hql.AppendLine("select vend.Nome AS Vendedor");
                hql.AppendLine(", p.Nome AS Cliente ");
                hql.AppendLine(", (case when p.BCliente = true then e.Endereco end) AS Endereco");
                hql.AppendLine(", (case when t.BPrincipal = true then t.Numero end) AS Telefone");
                hql.AppendLine(", (case when c.Tipo = 'Celular' then c.Numero end) AS Celular");
                hql.AppendLine("from PessoaCliente pc ");
                hql.AppendLine("join pc.Pessoa p ");
                hql.AppendLine("left join p.PessoaEnderecos e");
                hql.AppendLine("left join p.PessoaTelefones t with t.BPrincipal = true");
                hql.AppendLine("left join p.PessoaTelefones c with c.Tipo = 'Celular'");
                hql.AppendLine("join pc.Vendedor vend ");
                hql.AppendLine("join vend.Filial f");
                hql.AppendLine("where f.ID in " + pEmpresas);
                hql.AppendLine(" AND e.BPrincipal = true AND (p.BVendedor = true OR p.BCliente = true) ");

                if (pVendedor != null)
                    hql.AppendLine(" AND vend.ID = " + pVendedor);
                if (!String.IsNullOrEmpty(pLetraInicial))
                    hql.AppendLine(" AND UPPER(p.Nome) >= '" + pLetraInicial.ToUpper() + "'");
                if (!String.IsNullOrEmpty(pLetraFinal))
                    hql.AppendLine(" AND SUBSTRING(UPPER(p.Nome), 1, 1) <= '" + pLetraFinal.ToUpper() + "'");

                hql.AppendLine(" ORDER BY 1, 2");
                IQuery query = session.CreateQuery(hql.ToString());

                ConstructorInfo construtor = typeof(pxyRelClientePorVendedor).GetConstructor(new Type[] {                
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string),
                typeof(string)
                });

                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<object>();
            }
        }

        public PessoaVendedor GetVendedorPorIDPessoa(int id)
        {
            using (var session = GetSession())
            {
                string hql = "from PessoaVendedor where IDPessoa = :IDPessoa";

                return session.CreateQuery(hql.ToString()).SetInt32("IDPessoa", id).List<PessoaVendedor>()[0];
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
