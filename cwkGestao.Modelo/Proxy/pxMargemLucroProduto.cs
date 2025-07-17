namespace cwkGestao.Modelo.Proxy
{
    public class pxMargemLucroProduto
    {
        public int IDProduto { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string Unidade { get; set; }

        public decimal PrecoVenda { get; set; }
        public decimal UltimoCusto { get; set; }
        public decimal Quantidade { get; set; }
        public decimal ValorDeLucro { get; set; }
        public decimal Total { get; set; }
        public decimal MargemLucro { get; set; }


        public pxMargemLucroProduto(int IDProduto, string Codigo, string Nome, string Unidade, decimal PrecoVenda, decimal UltimoCusto, decimal Quantidade, 
            decimal ValorDeLucro, decimal Total, decimal MargemLucro)
        {
            this.IDProduto = IDProduto;
            this.Codigo = Codigo;
            this.Nome = Nome;
            this.Unidade = Unidade;
            this.PrecoVenda = PrecoVenda;
            this.UltimoCusto = UltimoCusto;
            this.Quantidade = Quantidade;
            this.ValorDeLucro = ValorDeLucro;
            this.Total = Total;
            this.MargemLucro = MargemLucro;
        }
    }
}
