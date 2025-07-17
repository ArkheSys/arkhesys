using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Objeto
{
    public class NotaItem
    {
        public NotaItem()
        {
            this.Quantidade = 0;
            this.Valor = 0;
            this.SubTotal = 0;
            this.ValorDesconto = 0;
            this.PercDesconto = 0;
            this.Total = 0;
            this.PesoBruto = 0;
            this.PesoLiquido = 0;

            this.IDProduto = 0;
            this.ProdutoNome = "";
            this.InfAdicionais = "";
            this.ProdutoDescReduzida = "";
            this.Unidade = "";

            this.BaseICMS = 0;
            this.AliqICMS = 0;
            this.AliqICMSNormal = 0;
            this.ValorICMS = 0;
            this.ValorIsentoICMS = 0;
            this.ValorOutroICMS = 0;
            this.ValorRetidoICMS = 0;
            this.BaseICMSSubst = 0;
            this.IDCFOP = 0;
            this.IDICMS = 0;
            this.TextoLei = "";
        }

        public int ID { get; set; }

        public int IDNota { get; set; }

        public int Sequencia { get; set; }

        public Modelo.Produto Produto { get; set; }

        public int IDProduto { get; set; }

        public string ProdutoNome { get; set; }

        public string InfAdicionais { get; set; }

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
		
		public decimal Total;

        public decimal? PesoBruto { get; set; }

        public decimal? PesoLiquido { get; set; }

        public DateTime? Dt { get; set; }

        public global::Modelo.TituloType bAtualizaEstoque { get; set; }

        public global::Modelo.InOutType Ent_Sai { get; set; }

        public bool? CalcCustoMedio { get; set; }

        public decimal ValorCustoMedio { get; set; }

        public int? IDICMS { get; set; }

        public decimal? BaseICMS { get; set; }

        public decimal? AliqICMS { get; set; }

        public decimal? AliqICMSNormal { get; set; }

        public decimal? ValorICMS { get; set; }

        public decimal? ValorIsentoICMS { get; set; }

        public decimal? ValorOutroICMS { get; set; }

        public decimal? ValorRetidoICMS { get; set; }

        public byte? bCancelado { get; set; }

        public string SitTrib { get; set; }

        public decimal? BaseICMSSubst { get; set; }

        public decimal? ValorICMSReducao { get; set; }

        public decimal? ValorICMSReducaoDif { get; set; }

        public int? IDPedidoItem { get; set; }

        public int? IDCFOP { get; set; }

        public Modelo.ICM ICM { get; set; }

        public string TextoLei { get; set; }

        public string TAG_CST { get; set; }

        public int modBC_N13 { get; set; }

        public int? modBCST_N18 { get; set; }

        public string CST_Pis { get; set; }

        public decimal? vBC_Q07 { get; set; }

        public decimal? pPIS_Q08 { get; set; }

        public decimal? vPIS_Q09 { get; set; }

        public string CST_Cofins { get; set; }

        public string CST_Ipi { get; set; }

        public int? orig_N11 { get; set; }

        public decimal? vBC_S07 { get; set; }

        public decimal? pCOFINS_S08 { get; set; }

        public decimal? vCOFINS_S11 { get; set; }

        public decimal? vBC_O10 { get; set; }

        public decimal? pIPI_O13 { get; set; }

        public decimal? vIPI_O14 { get; set; }

        public decimal? pRedBC_N14 { get; set; }

        public decimal? pICMSST_N22 { get; set; }

        public string cEnq_O06 { get; set; }

        public string AliqCupom { get; set; }

        public Modelo.PedidoItem ItemPedido { get; set; }

        public List<Modelo.Rateio_NotaItem> RateioProjeto { get; set; }


    }
}
