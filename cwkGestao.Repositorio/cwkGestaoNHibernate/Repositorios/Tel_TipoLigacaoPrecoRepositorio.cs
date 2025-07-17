using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Tel_TipoLigacaoPrecoRepositorio : RepositorioAbstrato<Tel_TipoLigacaoPreco>, IRepositorioTel_TipoLigacaoPreco
    {
        
        protected override string HqlLoadById()
        {
            return "from Tel_TipoLigacaoPreco l where l.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Tel_TipoLigacaoPreco l";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
