using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Auxiliares
{
    public class ParametrosBaixa
    {
        public ParametrosBaixa()
        {
            MovCaixas = new List<MovCaixa>();
        }

        public Banco Banco { get; set; }
        public Historico Historico { get; set; }
        public String ComplementoHist { get; set; }
        public Documento Documento { get; set; }
        public decimal ValorBaixa { get; set; }
        public decimal ValorTotal { get { return ValorBaixa + ValorJuros + ValorMulta - ValorDesconto; } }
        public decimal ValorJuros { get; set; }
        public decimal ValorMulta { get; set; }
        public decimal ValorDesconto { get; set; }
        public decimal ValorIOF { get; set; }
        public decimal ValorTaxaCobranca { get; set; }
        public decimal ValorDescontoDuplicata { get; set; }
        public bool BaixarEmLote { get; set; }
        public Remessa remessa { get; set; }
        
        public List<MovCaixa> MovCaixas { get; set; }
        public DateTime DataBaixa { get; set; }

        public Historico HistoricoTroco { get; set; }
        public String ComplementoHistTroco { get; set; }
        public bool Renegociacao { get; set; }

        public ParametrosBaixa Clone()
        {
            List<MovCaixa> m = new List<MovCaixa>();
            m.AddRange(this.MovCaixas);
            ParametrosBaixa clone = new ParametrosBaixa()
            {
                Banco = this.Banco,
                ComplementoHist = this.ComplementoHist,
                ComplementoHistTroco = this.ComplementoHistTroco,
                DataBaixa = this.DataBaixa,
                Documento = this.Documento,
                Historico = this.Historico,
                HistoricoTroco = this.HistoricoTroco,
                ValorBaixa = this.ValorBaixa,
                ValorJuros = this.ValorJuros,
                ValorMulta = this.ValorMulta,
                ValorDesconto = this.ValorDesconto,
                ValorIOF = this.ValorIOF,
                ValorTaxaCobranca = this.ValorTaxaCobranca,
                ValorDescontoDuplicata = this.ValorDescontoDuplicata,
                remessa = this.remessa,
                BaixarEmLote = this.BaixarEmLote,
                MovCaixas = m  
            };
            return clone;
        }

        public override string ToString()
        {
            return BaixarEmLote ? "Lote" : "NaoLote" + " - " + ValorBaixa + "/" + ValorMulta + "/" + ValorJuros + "/" + ValorTotal;
        }
    }
    
    public class ParametrosReparcelamento
    {
        public Pessoa Cliente { get; set; }
        public Filial Fililal { get; set; }
        public Acrescimo Acrescimo { get; set; }
        public DateTime DataBase { get; set; }
        public Condicao Condicao { get; set; }
        public decimal Juros { get; set; }
        public Banco Banco { get; set; }
        public Portador Portador { get; set; }
        public Historico Historico { get; set; }
        public String ComplementoHistorico { get; set; }
        public IList<PedidoParcela> Parcelas {get; set; }
        public IList<Documento> Documentos { get; set; }
        public bool Renegociacao { get; set; }

        public TipoDocumento TipoDocumento{get; set; }
    }
}
