namespace cwkGestao.Modelo.Proxy
{
    public class pxRelatorioTabelaPreco
    {
        public string Codigo { get; set; }

        public pxRelatorioTabelaPreco() { }

        public pxRelatorioTabelaPreco(string Usuario)
        {
            this.Codigo = Usuario;        
        }
    }
}
