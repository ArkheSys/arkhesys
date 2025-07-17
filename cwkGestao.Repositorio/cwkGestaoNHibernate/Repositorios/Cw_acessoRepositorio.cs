using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Cw_acessoRepositorio : RepositorioAbstrato<Cw_acesso>, IRepositorioCw_acesso
    {
        protected override string HqlLoadById()
        {
            throw new NotImplementedException();
        }

        protected override string hqlGetAll()
        {
            throw new NotImplementedException();
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

        public Cw_acesso LoadAcessoByGrupoTextoForm(int idGrupo, String textoFormulario)
        {
            using (var session = GetSession())
            {
                var hql = " from Cw_acesso a " +
                   " left join fetch a.Cw_acessocampos ac" +
                   " left join fetch a.Grupo g " +
                   " where g.ID = :idGrupo and a.Formulario = :textoFormulario";

                var query = session.CreateQuery(hql).SetInt32("idGrupo", idGrupo).SetString("textoFormulario", textoFormulario);
                return query.UniqueResult<Cw_acesso>();
            }
        }
    }
}
