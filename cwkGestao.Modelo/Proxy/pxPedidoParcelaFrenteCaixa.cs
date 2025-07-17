namespace cwkGestao.Modelo.Proxy
{
    public class pxPedidoParcelaFrenteCaixa
    {
        public string FormaDePagamento { get; set; }
        public decimal Valor { get; set; }

        public pxPedidoParcelaFrenteCaixa() { }
        public pxPedidoParcelaFrenteCaixa(string FormaDePagamento, decimal Valor)
        {
            this.FormaDePagamento = FormaDePagamento;
            this.Valor = Valor;
        }
    }
}