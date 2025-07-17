using System;

namespace cwkGestao.Modelo
{
    public class PercursoMdfe : ModeloBase
    {
        public override int ID { get; set; }

        public virtual Mdfe Mdfe { get; set; }
        public virtual UF UFPercurso { get; set; }
        public virtual DateTime InicioViagem { get; set; }
    }
}
