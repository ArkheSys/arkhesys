namespace cwkGestao.Modelo.Proxy
{
    public class pxRelatorioTabelaPrecoPorVenda
    {
        public string Codigo { get; set; }

        public pxRelatorioTabelaPrecoPorVenda() { }

        public pxRelatorioTabelaPrecoPorVenda(string Usuario)
        {
            this.Codigo = Usuario;
        }
    }
}
