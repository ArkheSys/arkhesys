namespace cwkGestao.Modelo
{
    public class LacreRodoviarioMdfe : ModeloBase
    {
        public override int ID { get; set; }
        public virtual string Guid { get; set; }

        public virtual Mdfe Mdfe { get; set; }

        public virtual string Numero { get; set; }
    }
}