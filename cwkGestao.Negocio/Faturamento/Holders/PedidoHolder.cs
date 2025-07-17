using System;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio.Faturamento.Holders
{
    internal class PedidoHolder : IProdutoHolder, IServicoHolder
    {
        private PedidoItem pedidoItem;
        private PedidoItemServico pedidoItemServico;

        public PedidoHolder(PedidoItem pedidoItem)
        {
            this.pedidoItem = pedidoItem;
        }
        public PedidoHolder(PedidoItemServico pedidoItemServico)
        {
            this.pedidoItemServico = pedidoItemServico;
        }

        public Produto Produto
        {
            get { return this.pedidoItem.Produto; }
        }

        public decimal Quantidade
        {
            get { return this.pedidoItem.Quantidade; }
        }

        public string Unidade
        {
            get { return this.pedidoItem.Unidade; }
        }

        public decimal Preco
        {
            get { return this.pedidoItem.Valor; }
        }

        public decimal PrecoOriginal
        {
            get { return this.pedidoItem.ValorCalculado; }
        }

        public decimal DescontoPerc
        {
            get { return this.pedidoItem.PercDesconto; }
        }

        public decimal Total
        {
            get { return this.pedidoItem.Total; }
        }

        public DateTime Data
        {
            get { return this.pedidoItem.Dt; }
        }

        public PedidoItem PedidoItem
        {
            get { return this.pedidoItem; }
        }

        public string InformacoesAdicionais
        {
            get { return this.pedidoItem.InformacoesAdicionais; }
        }

        #region IServicoHolder Members

        public Servico Servico
        {
            get { return pedidoItemServico.Servico; }
        }

        public decimal ValorSvc
        {
            get { return pedidoItemServico.Valor; }
        }

        public decimal ValorSvcOriginal
        {
            get { return pedidoItemServico.Servico.Valor; }
        }

        public decimal DescontoSvc
        {
            get { return pedidoItemServico.ValorDesconto; }
        }

        public decimal TotalSvc
        {
            get { return pedidoItemServico.Total; }
        }

        public DateTime DataSvc
        {
            get { return pedidoItemServico.Pedido.Dt; }
        }

        public PedidoItemServico PedidoItemServico
        {
            get { return pedidoItemServico; }
        }

        #endregion
    }
}