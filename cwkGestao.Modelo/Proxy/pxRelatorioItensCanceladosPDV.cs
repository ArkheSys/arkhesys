namespace cwkGestao.Modelo.Proxy
{
    public class pxRelatorioItensCanceladosPDV
    {
        public string Usuario { get; set; }
        public int IDProduto { get; set; }
        public string Codigo { get; set; }
        public string Produto { get; set; }
        public string Motivo { get; set; }
        public decimal? Quantidade { get; set; }
        public decimal? ValorUnitario { get; set; }
        public decimal? ValorTotal { get; set; }

        public pxRelatorioItensCanceladosPDV() { }

        public pxRelatorioItensCanceladosPDV(string Usuario, int IDProduto, string Codigo, string Produto, string Motivo, decimal? Quantidade, decimal? ValorUnitario, decimal? ValorTotal)
        {
            this.Usuario = Usuario;
            this.IDProduto = IDProduto;
            this.Codigo = Codigo;
            this.Produto = Produto;
            this.Motivo = Motivo;
            this.Quantidade = Quantidade;
            this.ValorUnitario = ValorUnitario;
            this.ValorTotal = ValorTotal;
        }
    }
}
