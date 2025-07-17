namespace cwkGestao.Modelo
{
    public class SeguradoraMdfe : ModeloBase
    {
        public override int ID { get; set; }

        public virtual Seguradora Seguradora { get; set; }
        public virtual Mdfe Mdfe { get; set; }
        public virtual string NumeroApolice { get; set; }
        public virtual ResponsavelSeguroCargaMdfe ResponsavelSeguroCargaMdfe { get; set; }
    }
}
