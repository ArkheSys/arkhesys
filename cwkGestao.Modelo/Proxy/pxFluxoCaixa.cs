using System;

namespace cwkGestao.Modelo.Proxy
{
    public class pxFluxoCaixa
    {
        public int ID { get; set; }
        public string Login { get; set; }
        public DateTime? DataAbertura { get; set; }
        public DateTime? DataFechamento { get; set; }
        public decimal? ValorAbertura { get; set; }
        public decimal? ValorFechamento { get; set; }
        public string Observacao { get; set; }

        public pxFluxoCaixa() { }

        public pxFluxoCaixa(int ID,
                            string Login,
                            DateTime? DataAbertura,
                            DateTime? DataFechamento,
                            decimal? ValorAbertura,
                            decimal? ValorFechamento,
                            string Observacao)
        {
            this.ID = ID;
            this.Login = Login;
            this.DataAbertura = DataAbertura;
            this.DataFechamento = DataFechamento;
            this.ValorAbertura = ValorAbertura;
            this.ValorFechamento = ValorFechamento;
            this.Observacao = Observacao;
        }
    }
}
