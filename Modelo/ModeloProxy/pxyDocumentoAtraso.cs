using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.ModeloProxy
{
    public class pxyDocumentoAtraso
    {
        public int IDDocumento { get; set; }
        public int CodigoDocumento { get; set; }
        public string NumeroDocumento { get; set; }
        public DateTime DataDocumento { get; set; }
        public DateTime Vencimento { get; set; }
        public int IDFilial { get; set; }
        public int DiasAtraso { get; set; }
        public int AtrasoAcumulado { get; set; }
        public string Pessoa { get; set; }
        public int IDPessoa { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public string Situacao { get; set; }

        private Nullable<DateTime> ultimaBaixa;
        public Nullable<DateTime> UltimaBaixa
        {
            get
            {
                return ultimaBaixa;
            }
            set
            {
                ultimaBaixa = value;
            }
        }

        public string TipoDocumento { get; set; }
        public int Parcela { get; set; }
        public int QuantidadeParcela { get; set; }
        public decimal? Juros { get; set; }

        public int AtrasoTotal { get; set; }
    }
}
