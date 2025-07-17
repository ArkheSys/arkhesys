namespace cwkGestao.Modelo
{
    public class ContratanteMdfe : ModeloBase
    {
        public override int ID { get; set; }

        public virtual Mdfe Mdfe { get; set; }
        public virtual string Cpf { get; set; }
        public virtual string Cnpj { get; set; }
    }
}
