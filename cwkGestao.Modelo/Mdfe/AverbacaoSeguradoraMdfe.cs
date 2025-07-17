namespace cwkGestao.Modelo
{
    public class AverbacaoSeguradoraMdfe : ModeloBase
    {
        public override int ID { get; set; }

        public virtual SeguradoraMdfe SeguradoraMdfe { get; set; }
        public virtual string NumeroAverbacao { get; set; }
    }
}
