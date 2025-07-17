namespace cwkGestao.Modelo.Cte
{
    public class CtePrestacao : ModeloBase
    {
        public override int ID { get; set; }

        public virtual CteIdentificacao Cte { get; set; }
        public virtual decimal ValorTotal { get; set; }
        public virtual decimal ValorRecebimento { get; set; }
    }
}