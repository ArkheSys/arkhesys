using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyRelNotasPeriodo
    {
        public Int32 IDEmpresa { get; set; }
        public String Empresa { get; set; }
        public Int32 Serie { get; set; }
        public Int32 Numero { get; set; }
        public DateTime? DataNota { get; set; }
        public String DataCancelamento { get; set; }
        public String NomeCliente { get; set; }
        public Int32 Status { get; set; }
        public Decimal TotalNota { get; set; }
        public String StatusNota { get; set; }
        public Decimal TotalCancelada { get; set; }
        public Decimal TotalAutorizada { get; set; }
    }
}
