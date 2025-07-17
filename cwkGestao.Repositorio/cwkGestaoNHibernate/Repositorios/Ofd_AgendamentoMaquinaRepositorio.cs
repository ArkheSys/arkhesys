using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Ofd_AgendamentoMaquinaRepositorio : RepositorioAbstrato<Ofd_AgendamentoMaquina>, IRepositorioOfd_AgendamentoMaquina
    {
        protected override string HqlLoadById()
        {
            return "from Ofd_AgendamentoMaquina n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Ofd_AgendamentoMaquina n ";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            return "from Ofd_AgendamentoMaquina n where n.Data between :dataInicio and :dataFim";
        }

        public override IList<Ofd_AgendamentoMaquina> GetAllByPeriod(DateTime start, DateTime end, Dictionary<string, object> parametros)
        {
            IList<Ofd_AgendamentoMaquina> result = new List<Ofd_AgendamentoMaquina>();
            result = base.GetAllByPeriod(start, end, null);
            return result;
        }
    }
}
