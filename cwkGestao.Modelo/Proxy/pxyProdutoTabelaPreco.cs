namespace cwkGestao.Modelo.Proxy
{
    public class pxyProdutoTabelaPreco
    {
        public int IDTabelaPreco { get; set; }
        public string TabelaPreco { get; set; }
        public int IDProduto { get; set; }
        public string Codigo { get; set; }
        public string Produto { get; set; }
        public string Unidade { get; set; }
        public decimal PrecoBase { get; set; }

        public pxyProdutoTabelaPreco() { }
        public pxyProdutoTabelaPreco(int IDTabelaPreco, string TabelaPreco, int IDProduto, string Codigo, string Produto, string Unidade, decimal PrecoBase)
        {
            this.IDTabelaPreco = IDTabelaPreco;
            this.TabelaPreco = TabelaPreco;
            this.IDProduto = IDProduto;
            this.Codigo = Codigo;
            this.Produto = Produto;
            this.Unidade = Unidade;
            this.PrecoBase = PrecoBase;
        }
    }
}