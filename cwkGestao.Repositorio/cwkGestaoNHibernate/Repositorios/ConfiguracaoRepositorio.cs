using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using NHibernate;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ConfiguracaoRepositorio : RepositorioAbstrato<Configuracao>, IRepositorioConfiguracao
    {
        protected override string HqlLoadById()
        {
            return " from Configuracao c left join fetch c.Filial left join fetch c.TabelaPreco left join fetch c.Filial.Cidade where c.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return " from Configuracao ";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public void InativarCliente()
        {
            using (var session = GetSession())
            {
                IQuery query = session.CreateSQLQuery("exec P_InativaCliente");
                query.ExecuteUpdate();
            }
        }
    }
}
