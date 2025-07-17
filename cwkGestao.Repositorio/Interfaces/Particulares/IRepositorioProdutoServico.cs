using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioProdutoServico : IRepositorio<ProdutoServico>
    {
        IList<Produto> GetAllProdutoPorIDServico(int idServico);
        ProdutoServico GetSelecionadoPorIDProdutoEIDServico(int idProduto, int idServico);
    }
}

