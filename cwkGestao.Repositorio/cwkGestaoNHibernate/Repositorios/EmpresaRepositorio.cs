using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class EmpresaRepositorio : RepositorioAbstrato<Empresa>, IRepositorioEmpresa
    {
        protected override string HqlLoadById()
        {
            throw new NotImplementedException();
        }

        protected override string hqlGetAll()
        {
            return "FROM Empresa e ";
        }

        public void InicializarBase()
        {
            using (var session = GetSession()) { session.CreateQuery("from Empresa").List(); }
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}