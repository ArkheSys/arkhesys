namespace cwkGestao.Modelo
{
    public class NfeReferenciadaMdfe : ModeloBase 
    {
        public override int ID { get; set; }

        public virtual Mdfe Mdfe { get; set; }
        public virtual string ChaveNfe { get; set; }
    }
}
