namespace cwkGestao.Modelo.Cte
{
    public class CtePrestacaoComplemento : ModeloBase
    {
        public override int ID { get; set; }

        public virtual CtePrestacao Prestacao { get; set; }
        public virtual decimal ValorComplemento { get; set; }
        public virtual string Nome { get; set; }
    }
}
