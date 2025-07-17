using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioEstoque : IRepositorio<Estoque>
    {
        Estoque GetEstoque(Filial filial, Produto produto);
        /// <summary>
        /// Retorna os registros de estoque baseado nos filtros de filial e produto.
        ///Os dois parâmetros são opcionais.
        /// </summary>
        /// <param name="filiais">Lista com os ids das filiais no formato "(1,2,3,...,n)" </param>
        /// <param name="produto">Produto com o qual os registros de estoque devem estar relacionados</param>
        /// <returns>Registros de Estoque</returns>
        List<Estoque> GetEstoque(String filiais, Produto produto);
        List<Estoque> GetEstoque(String filiais, GrupoEstoque grupoEstoque);
        List<Estoque> GetEstoque(String filiais, String produtoNome);
        IList<object> GetRelMovimentacaoPorProduto(String filiais, String produtos, String classificacao, String tiponota, DateTime datainicial, DateTime datafinal);
        IList<object> GetRelTempoEstoque(String filiais, String produto, String grupo);
        IList<Estoque> GetEstoque(Filial filial);

        IList<cwkGestao.Modelo.EstoqueProdutoView> GetEstoquePorFilial(Filial filial);
        IList<cwkGestao.Modelo.Estoque> GetEstoquePorProduto(Produto produto);

        IList<cwkGestao.Modelo.EstoqueProdutoView> GetEstoquePorLocalEstoque(LocalEstoque localEstoque);

        Estoque GetEstoque(LocalEstoque localEstoque, Produto produto);
    }

}

