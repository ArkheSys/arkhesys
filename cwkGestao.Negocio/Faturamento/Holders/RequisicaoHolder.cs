using System;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.Negocio.Faturamento.Holders
{
    internal class RequisicaoHolder : IProdutoHolder
    {
        private pxyBaixaRequisicao item;

        public RequisicaoHolder(pxyBaixaRequisicao item)
        {
            this.item = item;
        }

        public Produto Produto
        {
            get { return item.PedidoItem.Produto; }
        }

        public decimal Quantidade
        {
            get { return item.Quantidade; }
        }

        public string Unidade
        {
            get { return item.Unidade; }
        }

        public decimal Preco
        {
            get { return 0m; }
        }

        public decimal PrecoOriginal
        {
            get { return 0m; }
        }

        public decimal DescontoPerc
        {
            get { return 0m; }
        }

        public decimal Total
        {
            get { return 0m; }
        }

        public DateTime Data
        {
            get { return item.PedidoItem.Dt; }
        }

        public PedidoItem PedidoItem
        {
            get { return item.PedidoItem; }
        }

        public string InformacoesAdicionais
        {
            get { return ""; }
        }
    }
}