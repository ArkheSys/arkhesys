using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class RegistrosTipo60 : ModeloBase
    {
        public override int ID { get; set; }
        public virtual DateTime Dt { get; set; }
        public virtual string TipoRegistro { get; set; }
        public virtual string Registro { get; set; }
    }
}
