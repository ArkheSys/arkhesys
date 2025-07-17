using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyGerarPedido
    {
        IList<PedidoItem> produtos;
        IList<PedidoItemServico> servicos;
        Pedido Pedido;
        
        IList<pxyGerarPedido> listaRetorno = new List<pxyGerarPedido>();
        public pxyGerarPedido(Pedido p, out IList<pxyGerarPedido> listaRetorno) 
        {
            Pedido = p;
            produtos = p.Items;
            servicos = p.ItemsServicos;
            listaRetorno = TransformaItens();
        }

        public pxyGerarPedido()
        {

        }

        public bool Selecionado { get; set; }
        public virtual string Item { get; set; }
        public virtual string Descricao { get; set; }
        public virtual decimal Qtd { get; set; }
        public virtual string Und { get; set; }
        public virtual string Categoria { get; set; }
        public virtual decimal Preco { get; set; }
        public virtual decimal Desc { get; set; }
        public virtual decimal Total { get; set; }
        public virtual PedidoItem PedItem { get; set; }
        public virtual PedidoItemServico PedItemServico { get; set; }
        public virtual Pedido Ped { get; set; }

        private IList<pxyGerarPedido> TransformaItens()
        {
            pxyGerarPedido obj;
            IList<pxyGerarPedido> listObj = new List<pxyGerarPedido>();
            foreach (var pedItemProd in produtos)
            {
                obj = new pxyGerarPedido();
                obj.Item = pedItemProd.ProdutoNome;
                obj.Descricao = pedItemProd.ProdutoDescReduzida;
                obj.Qtd = pedItemProd.Quantidade;
                obj.Und = pedItemProd.Unidade;
                obj.Categoria = "Produto";
                obj.Preco = pedItemProd.Valor;
                obj.Desc = pedItemProd.PercDesconto;
                obj.Total = pedItemProd.Total;
                obj.PedItem = pedItemProd;
                obj.PedItemServico = null;
                obj.Ped = Pedido;
                listObj.Add(obj);
            }
            foreach (var pedItemServ in servicos)
            {
                obj = new pxyGerarPedido();
                obj.Item = pedItemServ.ServicoNome;
                obj.Descricao = pedItemServ.Servico.Descricao;
                obj.Qtd = pedItemServ.Quantidade;
                obj.Und = "";
                obj.Categoria = "Serviço";
                obj.Preco = pedItemServ.Valor;
                obj.Desc = pedItemServ.PercDesconto;
                obj.Total = pedItemServ.Total;
                obj.PedItem = null;
                obj.PedItemServico = pedItemServ;
                obj.Ped = Pedido;
                listObj.Add(obj);
            }
            return listObj;
        }


    }
}
