using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;
using System;
using System.Collections.Generic;
using cwkGestao.Modelo.Proxy;
using System.Reflection;
using NHibernate.Transform;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class PessoaResponsavelServicoRepositorio : RepositorioAbstrato<PessoaResponsavelServico>, IRepositorioPessoaResponsavelServico
    {
        protected override string HqlLoadById()
        {
            return "from PessoaResponsavelServico t where t.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from PessoaResponsavelServico t";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            return null;
        }

        public IList<pxResponsavelServicoCliente> GetResponsaveisPorCliente(int? IDResponsavel, int? IDServico)
        {
            using (var session = GetSession())
            {
                string hql = $@"SELECT PESSOA.ID AS IDPESSOA,
                                	   PESSOA.NOME AS NOME,
                                
                                	   RESPONSAVEL.ID AS IDRESPONSAVEL,
                                	   RESPONSAVEL.DESCRICAO AS RESPONSAVEL,
                                
                                	   SERVICORESPONSAVEL.ID AS IDSERVICO,
                                	   SERVICORESPONSAVEL.DESCRICAO AS SERVICO
                                  FROM PESSOARESPONSAVELSERVICO
                                     INNER JOIN PESSOA ON (PESSOARESPONSAVELSERVICO.IDPESSOA = PESSOA.ID)
                                	 INNER JOIN RESPONSAVEL ON (PESSOARESPONSAVELSERVICO.IDRESPONSAVEL = RESPONSAVEL.ID)
                                	 INNER JOIN SERVICORESPONSAVEL ON (PESSOARESPONSAVELSERVICO.IDSERVICO = SERVICORESPONSAVEL.ID)
                                WHERE RESPONSAVEL.ID = {(IDResponsavel.HasValue ? IDResponsavel.Value.ToString() : "RESPONSAVEL.ID")}
                                  AND SERVICORESPONSAVEL.ID = {(IDServico.HasValue ? IDServico.Value.ToString() : "SERVICORESPONSAVEL.ID")}";
                IQuery query = session.CreateSQLQuery(hql);
                ConstructorInfo construtor = typeof(pxResponsavelServicoCliente).GetConstructor(new[]
                {
                    typeof(int),
                    typeof(string),
                    typeof(int),
                    typeof(string),
                    typeof(int),
                    typeof(string)
                });
                return query.SetResultTransformer(Transformers.AliasToBeanConstructor(construtor)).List<pxResponsavelServicoCliente>();
            }
        }
    }
}
