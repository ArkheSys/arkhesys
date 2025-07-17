using cwkGestao.Modelo;

namespace Aplicacao.ValueObjects.Interfaces
{
    public interface DonoProduto
    {
        Produto Produto { get; set; }
        TabelaPreco TabelaPreco { get; set; }
        int Sequencia { get; set; }
        string ProdutoNome { get; set; }
        decimal Quantidade { get; set; }
        string Unidade { get; set; }
        decimal ValorOriginal { get; set; }
        decimal Valor { get; set; }
        decimal DescontoPerc { get; set; }
        decimal Total { get; set; }
        decimal AliquotaIPI { get; set; }

        string ObservacaoItem { get; set; }

        string QuantidadeEstoque { get; set; }
        string LocalEstoque { get; set; }
        string CodigoFabricante { get; set; }
        string CodigoOriginal { get; set; }
        string Marca { get; set; }
        string Aplicacao { get; set; }

        //DateTime? DataEntrega { get; set; }

        //string NCM { get; set; }

        void ReCalculaTotal();
    }
}
