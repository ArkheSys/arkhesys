using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioProdutoSKU : IRepositorio<ProdutoSKU>
    {
        IList<ProdutoSKU> GetSKUPorProduto(int id);
        IList<ProdutoSKU> GetProdutoPorSKU(string sku);
        Produto FindProdutoPorSKU(string sku);
    }
}

