namespace cwkGestao.Modelo.Cte
{
    public class CteCarga : ModeloBase
    {
        public override int ID { get; set; }

        public virtual CteIdentificacao Cte { get; set; }
        public virtual decimal ValorCarga { get; set; }
        public virtual string ProPred { get; set; }
    }
}
