using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class Cw_acessocampo : ModeloBase
    {
        public override int ID { get; set; }
        public virtual int Codigo { get; set; }
        public virtual string Campo { get; set; }
        public virtual string Display { get; set; }
        public virtual int Acesso { get; set; }
        public virtual DateTime Incdata { get; set; }
        public virtual DateTime Inchora { get; set; }
        public virtual string Incusuario { get; set; }
        public virtual DateTime Altdata { get; set; }
        public virtual DateTime Althora { get; set; }
        public virtual string Altusuario { get; set; }

        public virtual Cw_acesso Cw_acesso { get; set; }

    }
}