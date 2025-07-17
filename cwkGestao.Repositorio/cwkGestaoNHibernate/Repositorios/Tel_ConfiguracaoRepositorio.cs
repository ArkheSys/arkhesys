using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Tel_ConfiguracaoRepositorio : RepositorioAbstrato<Tel_Configuracao>, IRepositorioTel_Configuracao
    {
        
        protected override string HqlLoadById()
        {
            return "from Tel_Configuracao c left join fetch c.ServicoJuros s left join fetch c.ServicoMulta s1 where c.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Tel_Configuracao c left join fetch c.ServicoJuros s left join fetch c.ServicoMulta s1";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
