namespace cwkGestao.Modelo.Proxy
{
    public class pxRelatorioProdutosMargemDeLucro
    {
        public int IdProduto { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public string Unidade { get; set; }
        public decimal? PrecoVenda { get; set; }
        public decimal? PrecoCusto { get; set; }
        public decimal? Quantidade { get; set; }
        public decimal? Lucro { get; set; }
        public decimal? Total { get; set; }

        public pxRelatorioProdutosMargemDeLucro() { }

        public pxRelatorioProdutosMargemDeLucro(int IdProduto, string Codigo, string Descricao, string Unidade, decimal PrecoVenda, decimal PrecoCusto, decimal Quantidade, decimal Lucro, decimal Total )
        {
            this.IdProduto = IdProduto;
            this.Codigo = Codigo;
            this.Descricao = Descricao;
            this.Unidade = Unidade;
            this.PrecoVenda = PrecoVenda;
            this.PrecoCusto = PrecoCusto;
            this.Quantidade = Quantidade;
            this.Lucro = Lucro;
            this.Total = Total;
        }
    }
}
