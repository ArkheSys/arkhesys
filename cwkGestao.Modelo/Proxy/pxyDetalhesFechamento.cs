namespace cwkGestao.Modelo.Proxy
{
    public class pxyDetalhesFechamento
    {
        public int TotalVendas { get; set; }
        public int TotalDevolucoes { get; set; }
        public decimal CreditoGerado { get; set; }
        public int ItensDevolvidos { get; set; }

        public pxyDetalhesFechamento() { }

        public pxyDetalhesFechamento(int TotalVendas,
                                 int TotalDevolucoes,
                                 decimal CreditoGerado,
                                 int ItensDevolvidos)
        {
            this.TotalVendas = TotalVendas;
            this.TotalDevolucoes = TotalDevolucoes;
            this.CreditoGerado = CreditoGerado;
            this.ItensDevolvidos = ItensDevolvidos;
        }
    }
}
