using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioTabelaPrecoProduto : IRepositorio<TabelaPrecoProduto>
    {
        TabelaPrecoProduto GetByTabelaPrecoProduto(int idtabelapreco, int idproduto);
    }
}

