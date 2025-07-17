using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System.Text.RegularExpressions;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{

    partial class Tel_GrupoClienteTelefoneRepositorio : RepositorioAbstrato<Tel_GrupoClienteTelefone>, IRepositorioTel_GrupoClienteTelefone
    {
        protected override string HqlLoadById()
        {
            return "from Tel_GrupoClienteTelefone g where g.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Tel_GrupoClienteTelefone g";
        }

        public IList<Tel_GrupoClienteTelefone> GetDisponiveisPorGrupo(Tel_GrupoCliente grupoCliente, DateTime dataAtivacao, string telefone)
        {
            using (var session = GetSession())
            {
                Regex re = new Regex("[0-9]");
                StringBuilder s = new StringBuilder();

                foreach (Match m in re.Matches(telefone))
                {
                    s.Append(m.Value);
                }

                telefone = s.ToString();

                var hql = @"select gct from Tel_GrupoClienteTelefone gct
                            join gct.GrupoCliente gc
                            where gc.ID = :idGrupoCliente 
                            and gct.ID not in 
                            (select gct2.ID from Tel_ClienteTelefone ct join ct.GrupoClienteTelefone gct2 
                            where gct2.GrupoCliente.ID = :idGrupoCliente 
                            and ct.DataAtivacao <= :dataAtivacao 
                            and (ct.DataDesligamento is null or ct.DataDesligamento >= :dataAtivacao)) 
                            and gct.Telefone LIKE '%" + telefone + "%'";

                return session.CreateQuery(hql)
                       .SetInt32("idGrupoCliente", grupoCliente.ID)
                       .SetDateTime("dataAtivacao", dataAtivacao)
                       .List<Tel_GrupoClienteTelefone>();

            }
        }

        public bool ExisteTelefone(string telefone)
        {
            using (var session = GetSession())
            {
                var hql = @"select count(g.ID) from Tel_GrupoClienteTelefone g where g.Telefone = '" + telefone + "'";

                return session.CreateQuery(hql).UniqueResult<long>() > 0;
            }
        }

        public Tel_GrupoClienteTelefone GetByTelefone(string telefone)
        {
            using (var session = GetSession())
            {
                var hql = @"from Tel_GrupoClienteTelefone g where g.Telefone = :telefone";
                return session.CreateQuery(hql)
                              .SetString("telefone", telefone)
                              .List<Tel_GrupoClienteTelefone>()
                              .FirstOrDefault();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
