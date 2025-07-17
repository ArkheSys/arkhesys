namespace cwkGestao.Modelo
{
    public class Condutor : ModeloBase
    {
        public override int ID { get; set; }

        public virtual string Cpf { get; set; }
        public virtual string Nome { get; set; }
        public virtual int Ativo { get; set; }
        public virtual UF Uf { get; set; }
    }
}