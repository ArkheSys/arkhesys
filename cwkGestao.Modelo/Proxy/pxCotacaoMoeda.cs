using System;

namespace cwkGestao.Modelo.Proxy
{
    public class pxCotacaoMoeda
    {
        public int ID { get; set; }
        public string Moeda { get; set; }
        public DateTime DataCotacao { get; set; }
        public decimal ValorCotacao { get; set; }
        public int HabilitaCotacao { get; set; }

        public pxCotacaoMoeda(int ID, string Moeda, DateTime DataCotacao, decimal ValorCotacao)
        {
            this.ID = ID;
            this.Moeda = Moeda;
            this.DataCotacao = DataCotacao;
            this.ValorCotacao = ValorCotacao;
            this.HabilitaCotacao = HabilitaCotacao;
        }
    }

    public class pxPrecoProduto
    {
        public string Tabela { get; set; }
        public decimal PrecoNormal { get; set; }
        public decimal PrecoMinimo { get; set; }
        public decimal PrecoMaximo { get; set; }

        public pxPrecoProduto() { }
    }
}