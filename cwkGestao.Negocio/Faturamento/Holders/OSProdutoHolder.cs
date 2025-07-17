using System;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio.Faturamento.Holders
{
    class OSProdutoHolder : IProdutoHolder
    {
        private OSProdutoItem produtoItem;

        public OSProdutoHolder(OSProdutoItem produtoItem)
        {
            this.produtoItem = produtoItem;
        }

        public Produto Produto
        {
            get { return produtoItem.Produto; }
        }

        public decimal Quantidade
        {
            get { return produtoItem.Quantidade; }
        }

        public string Unidade
        {
            get { return produtoItem.Unidade; }
        }

        public decimal Preco
        {
            get { return produtoItem.Preco; }
        }

        public decimal PrecoOriginal
        {
            get { return produtoItem.PrecoOriginal; }
        }

        public decimal DescontoPerc
        {
            get { return produtoItem.DescontoPerc; }
        }

        public decimal Total
        {
            get { return produtoItem.Total; }
        }

        public DateTime Data
        {
            get { return produtoItem.OSOrdemServico.Data; }
        }

        public PedidoItem PedidoItem
        {
            get { return null; }
        }

        public string InformacoesAdicionais
        {
            get { return produtoItem.Produto.InfAdicionais; }
        }
    }
}
