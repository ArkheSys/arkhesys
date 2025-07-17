using System;

namespace cwkGestao.Modelo.Proxy
{
    public class pxSuprimentoCaixa
    {
        public int IDSuprimentoCaixa { get; set; }
        public string Usuario { get; set; }
        public DateTime? DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public DateTime? DataSuprimento { get; set; }

        public decimal ValorSuprimento { get; set; }
        public string Observacao { get; set; }

        public pxSuprimentoCaixa() { }

        public pxSuprimentoCaixa(int IDSuprimentoCaixa,
                              string Usuario,
                              DateTime? DataAbertura,
                              DateTime? DataFechamento,
                              DateTime? DataSuprimento,
                              decimal ValorSuprimento,
                              string Observacao) 
        {
            this.IDSuprimentoCaixa = IDSuprimentoCaixa;
            this.Usuario = Usuario;
            this.DataAbertura = DataAbertura;
            this.DataFechamento = DataFechamento;
            this.DataSuprimento = DataSuprimento;
            this.ValorSuprimento = ValorSuprimento;
            this.Observacao = Observacao;        
        }
    }
}
