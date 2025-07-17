namespace cwkGestao.Modelo
{
    public class VeiculoTracaoMdfe : ModeloBase
    {
        public override int ID { get; set; }
        public virtual ProprietarioVeiculoMdfe ProprietarioVeiculoMdfe { get; set; }
        public virtual Condutor Condutor { get; set; }
        public virtual Veiculo Veiculo { get; set; }
        public virtual Mdfe Mdfe { get; set; }
    }
}
