using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Objeto
{
    public class pxyDocumentoBaixa : ExtratoCliente
    {
        public Modelo.Documento DocumentoBase { get; set; }
        public bool Baixar { get; set; }
        public int idBanco { get; set; }
        public string Cliente { get; set; }
        public Decimal? Descontos { get; set; }
        public Decimal ValorTotal { get; set; }
        public Decimal ValorBaixa { get; set; }

        public decimal ValorTaxa { get; set; }
    }
}
