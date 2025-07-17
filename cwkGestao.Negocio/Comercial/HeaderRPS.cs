using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Negocio.Comercial
{
    public class HeaderRPS
    {
        public string IdLote { get; set; }
        public int NumeroLote { get; set; }
        public string CpfCnpjRemetente { get; set; }
        public string InscricaoMunicipalRemetente { get; set; }
        public string RazaoSocialRemetente { get; set; }
        public int QuantidadeRPS { get; set; }
        public string Versao { get; set; }
        public decimal ValorTotalServicos { get; set; }
        public decimal ValorTotalDeducoes { get; set; }
        public string Logradouro { get; set; }

        

        public HeaderRPS(string IdLote,
            int NumeroLote,
            string CpfCnpjRemetente,
            string InscrEstadualRem,
            string RazaoSocialRem,
            int QuantidadeRPS,
            string CodCidadeRem,
            string Transacao,
            DateTime DataInicio,
            DateTime DataFim,
            string Versao,
            string MetodoEnvio,
            decimal VlrTotalServicos,
            decimal VlrTotalDeducoes,
            decimal VlrTotalBC
            )
        {
            this.IdLote = IdLote;
            this.NumeroLote = NumeroLote;
            this.CpfCnpjRemetente = CpfCnpjRemetente;
            this.InscricaoMunicipalRemetente = InscrEstadualRem;
            this.RazaoSocialRemetente = RazaoSocialRem;
            this.QuantidadeRPS = QuantidadeRPS;
            this.Versao = Versao;
        }

        public HeaderRPS()
        {
            this.IdLote = "";
            this.NumeroLote = 0;
            this.CpfCnpjRemetente = "";
            this.InscricaoMunicipalRemetente = "";
            this.RazaoSocialRemetente = "";
            this.QuantidadeRPS = 0;
            this.Versao = "";
        }
    }
}
