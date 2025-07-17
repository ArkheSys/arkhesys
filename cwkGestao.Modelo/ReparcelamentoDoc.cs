using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class ReparcelamentoDoc : ModeloBase
    {
        public override int ID { get; set; }
        public virtual Reparcelamento Reparcelamento { get; set; }
        public virtual bool Tipo { get; set; }
        public virtual Documento Documento { get; set; }
    }
}