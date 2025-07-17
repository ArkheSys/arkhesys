using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public class ExtratoCliente
    {
        public int IdDocumento { get; set; }
        public int Codigo { get; set; }
        public int Filial { get; set; }
        public string Documento { get; set; }
        public string Parcela { get; set; }
        public DateTime Data { get; set; }
        public DateTime Vencimento { get; set; }
        public int DiasAtraso { get; set; }
        public Decimal ValorDoc { get; set; }
        public Decimal Saldo { get; set; }
        public Decimal Juro { get; set; }
        public Decimal Multa { get; set; }

        public decimal Taxa { get; set; }
        public Decimal? ValorCorrigido { get; set; }
        public Modelo.Acrescimo objAcrecimo { get; set; }
        public Modelo.TipoDocumentoType Tipo { get; set; }
    }
}
