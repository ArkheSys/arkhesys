using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class PessoaSocio : ModeloBase
    {
        public override int ID { get; set; }

        public virtual Pessoa Pessoa { get; set; }
        public virtual Socio Socio { get; set; }
    }
}
