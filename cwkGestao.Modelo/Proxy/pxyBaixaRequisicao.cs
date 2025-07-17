using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyBaixaRequisicao
    {
        public bool Selecionado { get; set; }
        public string Produto { get; private set; }
        public string Descricao { get; private set; }
        public decimal QuantidadeAnt { get; private set; }
        public string Unidade { get; private set; }

        private decimal quantidade;
        public decimal Quantidade
        {
            get { return quantidade; }
            set
            {
                if (value > QuantidadeAnt)
                    throw new Exception("A quantidade não pode exceder " + QuantidadeAnt + " " + Unidade + ".");

                if (value <= 0)
                    throw new Exception("A quantidade deve ser maior que zero.");

                quantidade = value;
            }
        }

        private PedidoItem pedidoItem;
        public PedidoItem PedidoItem
        {
            get
            { return pedidoItem; }
            set
            {
                pedidoItem = value;
                Produto = pedidoItem.CodigoProduto;
                Descricao = pedidoItem.ProdutoNome;
                QuantidadeAnt = pedidoItem.Quantidade - pedidoItem.QtdRetirada + pedidoItem.QtdCancelada;
                Quantidade = QuantidadeAnt;
                Unidade = pedidoItem.Unidade;
            }
        }
    }

    public class pxyBaixaRequisicaoLote
    {
        public pxyBaixaRequisicaoLote(Pedido requisicao)
        {
            Codigo = requisicao.Codigo;
            Data = requisicao.Dt;
            Digitacao = requisicao.DtDigitacao;
            QtdItens = requisicao.Items.Count;
            IdRequisicao = requisicao.ID;
        }

        public bool Selecionado { get; set; }
        public int Codigo { get; set; }
        public DateTime Data { get; set; }
        public DateTime Digitacao { get; set; }
        public int QtdItens { get; set; }

        public int IdRequisicao { get; set; }
    }
}
