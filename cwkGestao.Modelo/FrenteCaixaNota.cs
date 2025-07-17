using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Frente Caixa Nota")]
    public class FrenteCaixaNota : ModeloBase
    {
        public FrenteCaixaNota()
        {
            ListaFrenteCaixaNotaItem = new List<FrenteCaixaNotaItem>();
        }
        public FrenteCaixaNota(string DescFormaPagamento, decimal TotalPago) {
            _DescFormaPagamento = DescFormaPagamento;
            _TotalPago = TotalPago;
        }

        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        [InformacaoExibicao(0, "Código", 100)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }


        private byte _Status;
        public virtual byte Status { get { return _Status; } set { _Status = value; } }

        private DateTime _Data;
        [InformacaoExibicao(1, "Data", 100, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual DateTime Data { get { return _Data; } set { _Data = value; } }

        private Pessoa _Vendedor;
        public virtual Pessoa Vendedor { get { return _Vendedor; } set { _Vendedor = value; } }


        private TipoPedido _TipoPedido;
        public virtual TipoPedido TipoPedido { get { return _TipoPedido; } set { _TipoPedido = value; } }


        [InformacaoExibicao(2, "Vendedor", 200, InformacaoExibicao.HAlinhamento.Esquerda)]
        public virtual string VendedorNome { get { return Vendedor != null ? Vendedor.Nome : ""; } }

        private Pessoa _Pessoa;
        public virtual Pessoa Pessoa { get { return _Pessoa; } set { _Pessoa = value; } }

        [InformacaoExibicao(4, "Pessoa", 200, InformacaoExibicao.HAlinhamento.Esquerda)]
        public virtual string PessoaNome { get { return Pessoa != null ? Pessoa.Nome : ""; } }

        private Condicao _Condicao;
        public virtual Condicao Condicao { get { return _Condicao; } set { _Condicao = value; } }

        public virtual Nota Nota { get; set; }

        private decimal _TotalProdutos;
        public virtual decimal TotalProdutos { get { return _TotalProdutos; } set { _TotalProdutos = value; } }

        private decimal _Desconto;
        public virtual decimal Desconto { get { return _Desconto; } set { _Desconto = value; } }

        private decimal _PercDesconto;
        public virtual decimal PercDesconto { get { return _PercDesconto; } set { _PercDesconto = value; } }

        private int _IDFluxoCaixa;
        public virtual int IDFluxoCaixa { get { return _IDFluxoCaixa; } set { _IDFluxoCaixa = value; } }

        private int _IDPedido;
        public virtual int IDPedido { get { return _IDPedido; } set { _IDPedido = value; } }

        private decimal _TotalGeral;
        [InformacaoExibicao(3, "Total", 150, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal TotalGeral
        {
            get
            {
                return _TotalGeral;
            }
            set
            {
                Troco = value - TotalPago;
                _TotalGeral = value;
            }
        }

        public virtual IList<FrenteCaixaNotaItem> ListaFrenteCaixaNotaItem { get; set; }

        public virtual string GerenteDesconto { get; set; }

        private decimal _TotalPago;
        public virtual decimal TotalPago
        {
            get
            {
                return _TotalPago;
            }
            set
            {
                Troco = TotalGeral - value;
                _TotalPago = value;
            }
        }

        private decimal _Troco;
        public virtual decimal Troco
        {
            get
            {
                _Troco = TotalPago - TotalGeral;
                return _Troco;
            }
            private set
            {
                _Troco = value;
            }
        }

        private String _DescFormaPagamento;
        public virtual String DescFormaPagamento { get { return _DescFormaPagamento; } set { _DescFormaPagamento = value; } }

        public static string GetCabecalhoCupom(Filial objFilial, Pessoa objCliente, Pessoa objVendedor)
        {
            var linha = "";
            var endereco = "";
            var cidade = "";
            var telefone = "";
            const string separador = "--------------------------------------------------------------------------";

            endereco = objFilial.Endereco + ", " + objFilial.Numero + (objFilial.Bairro.Length > 0 ? (" - " + objFilial.Bairro) : "");
            cidade = (objFilial.Cidade.Nome.TrimEnd() + "-" + objFilial.Cidade.UF.Sigla.TrimEnd());
            telefone = objFilial.Telefone;

            linha = String.Format("{0,-42}", objFilial.Nome.Length > 42 ? objFilial.Nome.Substring(0, 41) : objFilial.Nome) + " " + NovaLinha() +
                    String.Format("{0,-42}", endereco) + NovaLinha() +
                    String.Format("{0,-18}", telefone.Length > 18 ? telefone.Substring(0, 17) : telefone) +
                    String.Format("{0,24}", cidade.Length > 24 ? cidade.Substring(0, 23) : cidade) + NovaLinha() +
                    String.Format("{0,-20}", "I.E.: " + objFilial.Inscricao) +
                    String.Format("{0,22}", System.DateTime.Now.ToString()) + NovaLinha();

            if (!String.IsNullOrEmpty(objCliente.Nome))
            {
                linha += String.Format("{0,22}", "Cliente: " + objCliente.Nome) + NovaLinha();
            }

            if (!String.IsNullOrEmpty(objVendedor.Nome))
            {
                linha += String.Format("{0,15}", "Vendedor: " + objVendedor.Nome) + NovaLinha();
            }

            linha += separador + Environment.NewLine + "Item " + "Código    " + "     Descrição" + "  Unidade   " + "Quantidade  " + "Vlr.Unitário  " + "Valor" + Environment.NewLine;
            //linha += separador + Environment.NewLine +  "Id  " + "Qnt" + Environment.NewLine ;


            return linha;
        }

        private static string NovaLinha()
        {
            return Environment.NewLine;
        }

        public virtual string NumeroPos { get; set; }

        public virtual bool Cancelado { get; set; }
        public virtual string DescricaoPDV { get; set; }
        public virtual int? IDFilial { get; set; }
    }
}
