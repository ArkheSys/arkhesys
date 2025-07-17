using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class BaixaDocumento : ModeloBase
    {
        public override int ID { get; set; }
        public virtual Baixa Baixa { get; set; }
        public virtual Documento Documento { get; set; }
        public virtual decimal VlrDocumento { get; set; }
        public virtual decimal VlrMulta { get; set; }
        public virtual decimal VlrJuro { get; set; }
        public virtual decimal VlrDesc { get; set; }
        public virtual decimal VlrTotalBx { get; set; }
    }
}
