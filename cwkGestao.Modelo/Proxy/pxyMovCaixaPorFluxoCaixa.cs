namespace cwkGestao.Modelo.Proxy
{
    public class pxyMovCaixaPorFluxoCaixa
    {
        public pxyMovCaixaPorFluxoCaixa() { }

        public pxyMovCaixaPorFluxoCaixa(int _id, string formaPagamento, decimal valor)
        {
            ID = _id;
            FormaPagamento = formaPagamento;
            Valor = valor;
        }

        public int ID { get; set; }
        public string FormaPagamento { get; set; }
        public decimal Valor { get; set; }
    }
}
