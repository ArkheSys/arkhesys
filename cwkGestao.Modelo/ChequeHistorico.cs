using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Cheque Histórico")]
    public class ChequeHistorico : ModeloBase
    {
        public override int ID { get; set; }
        public virtual Cheque Cheque { get; set; }
        public virtual Documento Documento { get; set; }
        public virtual MovCaixa MovCaixa { get; set; }
        public virtual cwkGestao.Modelo.Cheque.StatusCheque Status { get; set; }
        [InformacaoExibicao(3, "Data", 150, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual DateTime Data { get; set; }
        public virtual string Observacao { get; set; }

        [InformacaoExibicao(4, "Status", 250)]
        public virtual string StatusLegivel
        {
            get
            {
                return StatusToString(this.Status);
            }
        }
        
        private string StatusToString(Modelo.Cheque.StatusCheque statusCheque)
        {
            switch (statusCheque)
            {
                case Cheque.StatusCheque.EmCaixa:
                    return "Em Caixa";
                case Cheque.StatusCheque.Depositado:
                    return "Depositado";
                case Cheque.StatusCheque.Devolvido:
                    return "Devolvido";;
                case Cheque.StatusCheque.Redepositado:
                    return "Redepositado";
                case Cheque.StatusCheque.Pago:
                    return "Pago";
                case Cheque.StatusCheque.Baixado:
                    return "Baixado";
                default:
                    return "";
            }
        }

        [InformacaoExibicao(7, "Documento", 100)]

        public virtual string NumDocumento
        {
            get
            {
                if (Documento != null)
                    return Documento.Codigo.ToString();
                return String.Empty;
            }
        }
        public virtual string TipoDocumento
        {
            get
            {
                if (Documento != null)
                    return Documento.Tipo.ToString();
                return String.Empty;
            }
        }
        [InformacaoExibicao(6, "Mov Caixa", 100)]
        public virtual string CodigoMovCaixa
        {
            get
            {
                if (MovCaixa != null)
                    return MovCaixa.Codigo.ToString();
                return String.Empty;
            }
        }
        public virtual string ParcelaDocumento
        {
            get
            {
                if (Documento != null)
                    return Documento.Parcela + " / " + Documento.QtParcela;
                return String.Empty;
            }
        }
        public virtual string VencDocumento
        {
            get
            {
                if (Documento != null)
                    return String.Format("{0:dd/MM/yyyy}", Documento.DtVencimento);
                return null;
            }
        }

        [InformacaoExibicao(1, "Movimento", 100)]
        public virtual string Movimento { get; set; }
        [InformacaoExibicao(2, "Origem Mov.", 100)]
        public virtual string OrigemMovimento { get; set; }
        
        public virtual Historico Historico { get; set; }
        [InformacaoExibicao(5, "Histórico", 100)]
        public virtual string CodigoHistorico
        {
            get
            {
                if (Historico != null)
                    return Historico.Nome;
                return String.Empty;
            }
        }
    }
}
