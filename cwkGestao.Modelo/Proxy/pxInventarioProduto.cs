namespace cwkGestao.Modelo.Proxy
{
    public class pxInventarioProduto
    {
        public string Produto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Valor { get; set; }
        public decimal Total { get; set; }

        public pxInventarioProduto(string Produto, decimal Quantidade, decimal Valor, decimal Total)
        {
            this.Produto = Produto;
            this.Quantidade = Quantidade;
            this.Valor = Valor;
            this.Total = Total;
        }
    }
}
