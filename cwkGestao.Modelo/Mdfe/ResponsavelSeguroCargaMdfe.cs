namespace cwkGestao.Modelo
{
    public class ResponsavelSeguroCargaMdfe : ModeloBase
    {
        public override int ID { get; set; }

        public virtual int ResponsavelSeguro { get; set; }
        public virtual string Cnpj { get; set; }
        public virtual string Cpf { get; set; }
    }
}
