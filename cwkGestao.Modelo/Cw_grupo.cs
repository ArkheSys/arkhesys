using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace cwkGestao.Modelo
{
    public class Cw_grupo : ModeloBase
    {
        public override int ID { get; set; }
        public virtual int Codigo { get; set; }
        public virtual string Nome { get; set; }
        public virtual int Acesso { get; set; }
        public virtual DateTime Incdata { get; set; }
        public virtual DateTime Inchora { get; set; }
        public virtual string Incusuario { get; set; }
        public virtual DateTime Altdata { get; set; }
        public virtual DateTime Althora { get; set; }
        public virtual string Altusuario { get; set; }
        public virtual Boolean bAutorizadoVerPrecosMinMax { get; set; }
        public virtual IList<Cw_acesso> Cw_acessos { get; set; }

        public virtual IList<Cw_usuario> Cw_usuarios { get; set; }

    }
}