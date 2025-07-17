using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace cwkGestao.Modelo
{
    public class Cw_acesso : ModeloBase
    {
        public Cw_acesso()
        {
            Cw_acessocampos = new List<Cw_acessocampo>();
            Grupo = new Cw_grupo();
        }

        public override int ID { get; set; }
        public virtual int Codigo { get; set; }
        public virtual Cw_grupo Grupo { get; set; }
        public virtual string Formulario { get; set; }
        public virtual int Acesso { get; set; }
        public virtual IList<Cw_acessocampo> Cw_acessocampos { get; set; }
        public virtual DateTime Incdata { get; set; }
        public virtual DateTime Inchora { get; set; }
        public virtual string Incusuario { get; set; }
        public virtual DateTime Altdata { get; set; }
        public virtual DateTime Althora { get; set; }
        public virtual string Altusuario { get; set; }
    }
}
