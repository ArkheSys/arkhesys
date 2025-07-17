using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Tel_ClienteServicoRepositorio : RepositorioAbstrato<Tel_ClienteServico>, IRepositorioTel_ClienteServico
    {
        protected override string HqlLoadById()
        {
            return "from Tel_ClienteServico g where g.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Tel_ClienteServico g";
        }

        public IList<Tel_ClienteServico> GetByGrupoDataLimiteEReferencia(Tel_GrupoCliente grupoCliente, DateTime dataLimite, DateTime referencia)
        {
            using (var sessao = GetSession())
            {
                string hql = @"from Tel_ClienteServico cs 
                               join fetch cs.Servico s
                               join fetch cs.Cliente c 
                               join fetch c.GrupoCliente g
                               where g.ID = :idGrupo and (cs.DataDesligamento is null or cs.DataDesligamento > :dataLimite)
                               and s.ID not in 
                               (select fs.Servico.ID from Tel_FaturaServico fs join fs.Fatura f where f.Cliente.ID = c.ID and fs.Fatura.Referencia = :referencia)";
                return sessao.CreateQuery(hql)
                            .SetInt32("idGrupo", grupoCliente.ID)
                            .SetDateTime("dataLimite", dataLimite)
                            .SetDateTime("referencia", referencia)
                            .List<Tel_ClienteServico>();
            }
        }

        public void AtualizaValorServicoCliente(int IdTelServico, decimal ValorNovo)
        {
            string num = ValorNovo.ToString().Replace(',', '.');
            string sql = @"update Tel_ClienteServico set Valor = "  + num + " where IdTel_Servico = " + IdTelServico + " and DataDesligamento is null";
            using (var sessao = GetSession())
            {
                sessao.CreateSQLQuery(sql).ExecuteUpdate();
            }

        }


        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}