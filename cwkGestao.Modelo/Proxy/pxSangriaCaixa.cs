using System;

namespace cwkGestao.Modelo.Proxy
{
    public class pxSangriaCaixa
    {
        public int IDSangriaCaixa { get; set; }
        public string Usuario { get; set; }
        public DateTime? DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public DateTime? DataSangria { get; set; }

        public decimal ValorSangria { get; set; }
        public string Observacao { get; set; }

        public string Tipo { get; set; }

        public pxSangriaCaixa() { }

        public pxSangriaCaixa(int IDSangriaCaixa,
                              string Usuario,
                              DateTime? DataAbertura,
                              DateTime? DataFechamento,
                              DateTime? DataSangria,
                              decimal ValorSangria,
                              string Observacao,
                              string Tipo)
        {
            this.IDSangriaCaixa = IDSangriaCaixa;
            this.Usuario = Usuario;
            this.DataAbertura = DataAbertura;
            this.DataFechamento = DataFechamento;
            this.DataSangria = DataSangria;
            this.ValorSangria = ValorSangria;
            this.Observacao = Observacao;
            this.Tipo = Tipo;
        }
    }
}
