using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class InventarioRepositorio : RepositorioAbstrato<Inventario>, IRepositorioInventario
    {
        protected override string HqlLoadById()
        {
            return @"from Inventario n
                     left join fetch n.Filial
                     left join fetch n.ListaProdutos lp 
                     left join fetch lp.Produto 
                     where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return "from Inventario n left join fetch n.Filial";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
