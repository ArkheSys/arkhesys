using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwkGestao.Modelo.Proxy
{
    public class pxRelatorioPedidoModelo08
    {
        public int IdProduto { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string NCM { get; set; }
        public decimal? Quantidade { get; set; }
        public string Unidade { get; set; }
        public decimal ?PrecoUnitario { get; set; }
        public decimal ?Total { get; set; }
        public decimal ?Icms { get; set; }
        public decimal ?ValorST { get; set; }
        public decimal ?IPI { get; set; }
        public string EntregaDiasUteis { get; set; }

        public pxRelatorioPedidoModelo08() { }

        public pxRelatorioPedidoModelo08(int IDProduto, string Codigo, string Descricao, string NCM, decimal Quantidade, string Unidade, decimal PrecoUnitario, decimal Total, decimal ICMS, decimal ValorST, decimal IPI, string EntregaDiasUteis)
        {
            this.IdProduto = IDProduto;
            this.Codigo = Codigo;
            this.Descricao = Descricao;
            this.NCM = NCM;
            this.Quantidade = Quantidade;
            this.Unidade = Unidade;
            this.PrecoUnitario = PrecoUnitario;
            this.Total = Total;
            this.Icms = ICMS;
            this.ValorST = ValorST;
            this.IPI = IPI;
            this.EntregaDiasUteis = EntregaDiasUteis;
        }

    }
}
