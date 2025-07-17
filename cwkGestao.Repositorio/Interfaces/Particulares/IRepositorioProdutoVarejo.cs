using cwkGestao.Modelo;
using System.Collections.Generic;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioProdutoVarejo
    {
        IList<ProdutoVarejo> GetPorProduto(int IDProduto);
    }
}
