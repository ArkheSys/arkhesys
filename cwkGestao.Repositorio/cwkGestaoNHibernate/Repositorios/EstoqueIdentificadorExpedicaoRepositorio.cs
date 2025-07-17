using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class EstoqueIdentificadorExpedicaoRepositorio : RepositorioAbstrato<EstoqueIdentificadorExpedicao>, IRepositorioEstoqueIdentificadorExpedicao
    {
        protected override string HqlLoadById()
        {
            return "from EstoqueIdentificadorExpedicao n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from EstoqueIdentificadorExpedicao n";
        }

        public EstoqueIdentificadorExpedicao GetPorIdIdentificador(int IdIdentificador)
        {
            using (var session = GetSession())
            {
                string hql = "from EstoqueIdentificadorExpedicao n where n.IDIdentificador = " + IdIdentificador;
                return session.CreateQuery(hql).List<EstoqueIdentificadorExpedicao>().FirstOrDefault();
            }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
