using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Objeto
{
    public class Documento
    {
        public int ID { get; set; }

        public int Codigo { get; set; }

        public global::Modelo.TipoDocumentoType Tipo { get; set; }

        public int IDFilial { get; set; }

        public int IDPessoa { get; set; }

        public int IDBanco { get; set; }

        public int IDPortador { get; set; }

        public int IDAcrescimo { get; set; }

        public int IDHistorico { get; set; }

        public int IDCondicao { get; set; }

        public DateTime? Dt { get; set; }

        public DateTime? DtDigitacao { get; set; }

        public DateTime? DtVencimento { get; set; }

        public DateTime? DtPrevisao { get; set; }

        public string NumDocumento { get; set; }

        public string NumRequisicao { get; set; }

        public int Parcela { get; set; }

        public int QtParcela { get; set; }

        public decimal? Valor { get; set; }

        public decimal? ValorTotal { get; set; }

        public string ComplementoHist { get; set; }

        public int CodigoBanco { get; set; }

        public string BancoStr { get; set; }

        public string Agencia { get; set; }

        public string Conta { get; set; }

        public string Cheque { get; set; }

        public string Emitente { get; set; }

        public bool bEntrada { get; set; }

        public Modelo.Nota Nota { get; set; }

        public decimal? Saldo { get; set; }

        public string Situacao { get; set; }

        public string MovientoRemessa { get; set; }
    }
}
