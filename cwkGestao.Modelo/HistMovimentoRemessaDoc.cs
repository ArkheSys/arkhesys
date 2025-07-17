using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class HistMovimentoRemessaDoc : ModeloBase
    {
        public override int ID { get; set; }
        public virtual Documento Documento { get; set; }
        public virtual string MovimentoRemessa { get; set; }
        public virtual DateTime Dt { get; set; }
    }
}
