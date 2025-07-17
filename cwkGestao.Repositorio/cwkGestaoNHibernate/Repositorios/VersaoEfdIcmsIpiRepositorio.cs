using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class VersaoEfdIcmsIpiRepositorio : RepositorioAbstrato<VersaoEfdIcmsIpi>, IRepositorioVersaoEfdIcmsIpi
    {
        protected override string HqlLoadById()
        {
            return "from VersaoEfdIcmsIpi n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from VersaoEfdIcmsIpi n";
        }
		
		protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            return "from VersaoEfdIcmsIpi x where x.DtInicioValidade <= :dataInicio and x.DtFimValidade >= :dataFim";
        }
    }
}
