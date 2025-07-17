using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyPLPDetalhe : ModeloBase
    {
        public pxyPLPDetalhe() { }

        public pxyPLPDetalhe(int IdPedido, int _CodigoDoPedido,string nomeFilial, string _Cliente, string _CodigosDeRastreamento, string _Vendedor,
                                string _Condicao, decimal _TotalProduto, decimal _PercDesconto, decimal _PercComissao,
                                decimal _ValorComissao, decimal _ValorDesconto, decimal _ValorAcrescimo, decimal _TotalPedido)
        {
            this.IdPedido = IdPedido;
            CodigoDoPedido = _CodigoDoPedido;
            this.nomeFilial = nomeFilial;
            Cliente = _Cliente;
            CodigosDeRastreamento = _CodigosDeRastreamento;
            Vendedor = _Vendedor;
            Condicao = _Condicao;
            TotalProduto = _TotalProduto;
            PercDesconto = _PercDesconto;
            PercComissao = _PercComissao;
            ValorComissao = _ValorComissao;
            ValorDesconto = _ValorDesconto;
            ValorAcrescimo = _ValorDesconto;
            TotalPedido = _TotalPedido;
        }

        public pxyPLPDetalhe(int IdPedido, int idPLP, int idPLPDet, int _CodigoDoPedido, string nomeFilial, string _Cliente, string _CodigosDeRastreamento, string _Vendedor,
                                string _Condicao, decimal _TotalProduto, decimal _PercDesconto, decimal _PercComissao,
                                decimal _ValorComissao, decimal _ValorDesconto, decimal _ValorAcrescimo, decimal _TotalPedido)
        {
            this.IdPedido = IdPedido;
            this.IDPLP = idPLP;
            this.IDPLPDet = idPLPDet;
            CodigoDoPedido = _CodigoDoPedido;
            this.nomeFilial = nomeFilial;
            Cliente = _Cliente;
            CodigosDeRastreamento = _CodigosDeRastreamento;
            Vendedor = _Vendedor;
            Condicao = _Condicao;
            TotalProduto = _TotalProduto;
            PercDesconto = _PercDesconto;
            PercComissao = _PercComissao;
            ValorComissao = _ValorComissao;
            ValorDesconto = _ValorDesconto;
            ValorAcrescimo = _ValorDesconto;
            TotalPedido = _TotalPedido;
        }
        public int IdPedido { get; private set; }

        public int IDPLP { get; private set; }

        public int IDPLPDet { get; private set; }

        [InformacaoExibicao(0, "Selecionado", 80)]
        public bool Selecionado { get; set; }

        [InformacaoExibicao(1, "Código do Pedido", 110)]
        public int CodigoDoPedido { get; set; }
        
        [InformacaoExibicao(2, "Cliente", 200)]
        public string Cliente { get; set; }

        [InformacaoExibicao(3, "Empresa", 200)]
        public string nomeFilial { get; set; }

        [InformacaoExibicao(4, "Códigos de Rastreamento", 230)]
        public string CodigosDeRastreamento { get; set; }

        [InformacaoExibicao(5, "Vendedor", 120)]
        public string Vendedor { get; set; }

        [InformacaoExibicao(6, "Condição", 110)]
        public string Condicao { get; set; }

        [InformacaoExibicao(7, "Total Produtos", 110)]
        public decimal TotalProduto { get; set; }

        [InformacaoExibicao(8, "Percentual de Desconto", 100)]
        public decimal PercDesconto { get; set; }

        [InformacaoExibicao(9, "Percentual de Comissão", 100)]
        public decimal PercComissao { get; set; }

        [InformacaoExibicao(10, "Valor Comissão", 110)]
        public decimal ValorComissao { get; set; }

        [InformacaoExibicao(11, "Valor Desconto", 100)]
        public decimal ValorDesconto { get; set; }

        [InformacaoExibicao(12, "Valor Acréscimo", 100)]
        public decimal ValorAcrescimo { get; set; }

        [InformacaoExibicao(13, "Total Pedido", 100)]
        public decimal TotalPedido { get; set; }

        //public IList<string> ListCodigosRastreamento { get { return ListCodigosRastreamento; } set { new List<string>(); } }
        public override int ID
        {
            get
            {
                return 0;
            }
            set
            {
            }
        }
    }
}
