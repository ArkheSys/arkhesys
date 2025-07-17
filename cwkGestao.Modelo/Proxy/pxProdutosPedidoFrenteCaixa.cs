namespace cwkGestao.Modelo.Proxy
{
    public class pxProdutosPedidoFrenteCaixa
    {
        public int IDPessoa { get; set; }
        public string Pessoa { get; set; }
        public int IDProduto { get; set; }
        public string Produto { get; set; }
        public string Unidade { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Valor { get; set; }
        public decimal Total { get; set; }


        public pxProdutosPedidoFrenteCaixa() { }
        public pxProdutosPedidoFrenteCaixa(int IDPessoa, string Pessoa, int IDProduto, string Produto, string Unidade, decimal Quantidade, decimal Valor, decimal Total)
        {
            this.IDPessoa = IDPessoa;
            this.Pessoa = Pessoa;
            this.IDProduto = IDProduto;
            this.Produto = Produto;
            this.Unidade = Unidade;
            this.Quantidade = Quantidade;
            this.Valor = Valor;
            this.Total = Total;
        }
    }
}