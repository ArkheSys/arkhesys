namespace cwkGestao.Modelo
{
    public class Seguradora : ModeloBase
    {
        public override int ID { get; set; }

        public virtual string Descricao { get; set; }
        public virtual string Cnpj { get; set; }
        public virtual int Ativo { get; set; }
    }
}
