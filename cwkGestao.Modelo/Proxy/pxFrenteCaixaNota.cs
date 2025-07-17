namespace cwkGestao.Modelo.Proxy
{
    public class pxFrenteCaixaNota
    {
        public int IDFormaDePagamento { get; set; }
        public string FormaDePagamento { get; set; }
        public decimal ValorPago { get; set; }

        public pxFrenteCaixaNota() { }

        public pxFrenteCaixaNota(int IDFormaDePagamento,
                                 string FormaDePagamento,
                                 decimal ValorPago)
        {
            this.IDFormaDePagamento = IDFormaDePagamento;
            this.FormaDePagamento = FormaDePagamento;
            this.ValorPago = ValorPago;
        }
    }
}
