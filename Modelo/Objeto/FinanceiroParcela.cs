using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Objeto
{
    public class FinanceiroParcela
    {
        public int Parcela { get; set; }

        public DateTime? DtContabil { get; set; }

        public DateTime? Vencimento { get; set; }

        public decimal? Valor { get; set; }

        public int bAlterado { get; set; }

        public int IDTipoDocumento { get; set; }

        public string NumBanco { get; set; }
        public string NumAgencia { get; set; }
        public string NumContaCorrente { get; set; }
        public string NumCheque { get; set; }
        public string Emitente { get; set; }
        public string CpfCnpj { get; set; }
        public string NomeBanco { get; set; }
        
        private byte? _bEntrada;
        public byte? bEntrada 
        { 
            get { return _bEntrada; } 
            set { _bEntrada = value; } 
        }
    }
}
