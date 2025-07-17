using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class Produto_IcmsRepositorio : RepositorioAbstrato<Produto_Icms>, IRepositorioProduto_Icms
    {
        protected override string HqlLoadById()
        {
            return "from Produto_Icms where ID = :id";
        }

        protected override string hqlGetAll()
        {
            throw new NotImplementedException();
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}