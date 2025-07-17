using System;

using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Tel_FaturaD8Repositorio : RepositorioAbstrato<Tel_FaturaD8>, IRepositorioTel_FaturaD8
    {

        // Verificar
        protected override string HqlLoadById()
        {
            return ""; //  "from Tel_Configuracao c left join fetch c.ServicoJuros s left join fetch c.ServicoMulta s1 where c.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return ""; // "from Tel_Configuracao c left join fetch c.ServicoJuros s left join fetch c.ServicoMulta s1";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }

    }
}
