namespace cwkGestao.Modelo.Proxy
{
    public class pxClientesEPagamentosPedidoFrenteCaixa
    {
        public int IDFormaPagamento { get; set; }
        public string FormaPagamento { get; set; }
        public string IDPessoa { get; set; }
        public string Pessoa { get; set; }
        public decimal Valor { get; set; }

        public pxClientesEPagamentosPedidoFrenteCaixa() { }
        public pxClientesEPagamentosPedidoFrenteCaixa(int IDFormaPagamento, string FormaPagamento, string IDPessoa, string Pessoa, decimal Valor)
        {
            this.IDFormaPagamento = IDFormaPagamento;
            this.FormaPagamento = FormaPagamento;

            this.IDPessoa = IDPessoa;
            this.Pessoa = Pessoa;

            this.Valor = Valor;
        }
    }
}