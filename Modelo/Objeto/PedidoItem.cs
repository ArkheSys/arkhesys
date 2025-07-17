using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Objeto
{
    public class PedidoItem
    {
        public int ID { get; set; }

        public int IDPedido { get; set; }

        public int Sequencia { get; set; }

        public Modelo.Produto Produto { get; set; }

        public int IDProduto { get; set; }

        public string ProdutoNome { get; set; }

        public string ProdutoDescReduzida { get; set; }

        public decimal Quantidade { get; set; }

        public string Unidade { get; set; }

        public decimal ValorCalculado { get; set; }

        public decimal Valor { get; set; }

        public decimal SubTotal { get; set; }

        public decimal? PercDesconto { get; set; }

        public decimal? ValorDesconto { get; set; }

        public decimal? RAT_Desconto { get; set; }

        public decimal? RAT_Acrescimo { get; set; }

        public decimal Total { get; set; }

        public decimal? PesoBruto { get; set; }

        public decimal? PesoLiquido { get; set; }

        public DateTime? Dt { get; set; }

        public global::Modelo.TituloType bReservaEstoque { get; set; }

        public global::Modelo.InOutType Ent_Sai { get; set; }

        public DateTime? PrazoEntrega { get; set; }

        public decimal? QtdRetirada { get; set; }

        public decimal? QtdCancelada { get; set; }

        public int? OS { get; set; }

        public int? Gerente { get; set; }

        public decimal? BaseICMSSubst { get; set; }

        public decimal? ValorRetidoICMS { get; set; }
    }
}
