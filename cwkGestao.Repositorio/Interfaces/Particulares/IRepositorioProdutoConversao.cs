using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioProdutoConversao : IRepositorio<ProdutoConversao>
    {
        IList<ProdutoConversao> GetByIdProduto(int IdProduto);
        IList<ProdutoConversao> GetByIdConversaoUnidade(int IdConversaoUnidade);
        ProdutoConversao GetByIdConversaoUnidadeAndIdProduto(int IdConversaoUnidade, int IdProduto);
    }
}

