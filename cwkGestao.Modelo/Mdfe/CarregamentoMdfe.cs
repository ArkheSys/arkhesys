namespace cwkGestao.Modelo
{
    public class CarregamentoMdfe : ModeloBase
    {
        public override int ID { get; set; }

        public virtual Mdfe Mdfe { get; set; }
        public virtual UF UFCarregamento { get; set; }
        public virtual Cidade CidadeCarregamento { get; set; }
    }
}
