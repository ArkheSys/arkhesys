namespace cwkGestao.Modelo.Proxy
{
    public class pxRelatorioProdutosMaisvendidos
    {
        public int IDProduto { get; set; }
        public string Barra { get; set; }
        public string CodigoReferencia { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal? QuantidadeEstoque { get; set; }
        public decimal? Quantidade { get; set; }
        public decimal? Valor { get; set; }
        public decimal? Total { get; set; }

        public pxRelatorioProdutosMaisvendidos() { }

        public pxRelatorioProdutosMaisvendidos(int IDProduto, string Barra, string CodigoReferencia, string Codigo, string Nome, decimal? QuantidadeEstoque, decimal? Quantidade, decimal? Valor, decimal? Total)
        {
            this.IDProduto = IDProduto;
            this.Barra = Barra;
            this.CodigoReferencia = CodigoReferencia;
            this.Codigo = Codigo;
            this.Nome = Nome;
            this.QuantidadeEstoque = QuantidadeEstoque;
            this.Quantidade = Quantidade;
            this.Valor = Valor;
            this.Total = Total;
        }
    }
}
