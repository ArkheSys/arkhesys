namespace cwkGestao.Modelo.Cte
{
    public class CteDocumentosInformados : ModeloBase
    {
        public override int ID { get; set; }

        public virtual CteIdentificacao Cte { get; set; }
        public virtual string Chave { get; set; }
    }
}
