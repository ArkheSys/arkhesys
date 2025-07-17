namespace cwkGestao.Modelo
{
    public class ProprietarioVeiculoMdfe : ModeloBase
    {
        public override int ID { get; set; }
        public virtual string Cnpj { get; set; }
        public virtual string Cpf { get; set; }
        public virtual string Rntrc { get; set; }
        public virtual string Nome { get; set; }
        public virtual string InscricaoEstadual { get; set; }
        public virtual UF Uf { get; set; }
        public virtual int TipoProprietario { get; set; }
        public virtual string CodigoAgenciaPorto { get; set; }

        public virtual string CpfCnpj { get { return !string.IsNullOrEmpty(Cpf) ? Cpf : Cnpj; } }
    }
}