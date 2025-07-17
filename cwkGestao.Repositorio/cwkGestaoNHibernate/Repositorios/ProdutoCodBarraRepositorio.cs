using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Repositorio.cwkGestaoNHibernate.Repositorios
{
    partial class ProdutoCodBarraRepositorio : RepositorioAbstrato<ProdutoCodBarra>, IRepositorioProdutoCodBarra
    {
        protected override string HqlLoadById()
        {
            return "from ProdutoCodBarra n where n.ID = :id";
        }

        protected override string hqlGetAll()
        {
            return @" select new ProdutoCodBarra(produtoCodBarra.ID, produto, produtoCodBarra.BControleIdentificacao,
                    produtoCodBarra.CodigoBarra)                      
                    from ProdutoCodBarra produtoCodBarra 
                    join produtoCodBarra.Produto produto";
        }

        protected override string hqlGetAllByPeriod(DateTime start, DateTime end)
        {
            throw new NotImplementedException();
        }
    }
}
