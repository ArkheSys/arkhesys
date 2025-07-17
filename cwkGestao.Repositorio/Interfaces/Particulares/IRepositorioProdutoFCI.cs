using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioProdutoFCI : IRepositorio<ProdutoFCI>
    {
        IList<pxyProdutoFCI> GetAllPXYProdutoFCIPorIDProduto(int IDProduto);
        IList<ProdutoFCI> GetFCIByIdProduto(int IdProduto);

    }
}

