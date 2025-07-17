using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Repositorio.Interfaces.Particulares
{
    public interface IRepositorioTabelaPreco : IRepositorio<TabelaPreco>
    {
        IList<TabelaPreco> hqlGetAllAtiva();

        IList<pxyProdutoTabelaPreco> GetProdutosPorTabelaPreco(int? IDTabelaPreco, int? CodigoInicial, int? CodigoFinal, bool? ProdutosAtivos);
    }
}

