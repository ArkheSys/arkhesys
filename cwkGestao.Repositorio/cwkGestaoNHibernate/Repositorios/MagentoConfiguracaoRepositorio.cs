using System;
using System.Collections.Generic;
using System.Linq; 
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class MagentoConfiguracaoRepositorio : RepositorioAbstrato<MagentoConfiguracao>, IRepositorioMagentoConfiguracao
    {
        protected override string HqlLoadById()
        {
            return "from MagentoConfiguracao m where m.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from MagentoConfiguracao m ";
        }

        public MagentoConfiguracao LoadConfiguracao()
        {
            using (var session = GetSession())
            {
                string hql = @"select m
                               from MagentoConfiguracao m
                               join m.Unidade 
                               join m.Fornecedor 
                               join m.GrupoEstoque
                               join m.TabelaPreco
                               join m.Condicao c ";
                var resultado = session.CreateQuery(hql).List<MagentoConfiguracao>();
                if ((resultado.Count > 0) && (resultado[0].Condicao != null))
                {
                    NHibernate.NHibernateUtil.Initialize(resultado[0].Condicao.CondicaoParcelas);
                    return resultado[0];
                }
                return null;
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
