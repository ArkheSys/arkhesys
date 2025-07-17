using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioInventarioProduto : IRepositorio<InventarioProduto>
    {
        IList<pxInventarioProduto> GetProdutosPorInventario(int IDInventario);
    }
}

