using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Tel_ServicoRepositorio : RepositorioAbstrato<Tel_Servico>, IRepositorioTel_Servico
    {
        protected override string HqlLoadById()
        {
            return "from Tel_Servico g where g.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Tel_Servico g";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
