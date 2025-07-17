using System;

namespace cwkGestao.Modelo.Proxy
{
    public class pxFechamentoCaixaRateio
    {
        public string FormaPagamento { get; set; }
        public decimal ValorCaixa { get; set; }
        public decimal ValorInformado { get; set; }


        public pxFechamentoCaixaRateio() { }

        public pxFechamentoCaixaRateio(string FormaPagamento,
                              decimal ValorCaixa,
                              decimal ValorInformado)
        {
            this.FormaPagamento = FormaPagamento;
            this.ValorCaixa = ValorCaixa;
            this.ValorInformado = ValorInformado;
        }
    }
}
