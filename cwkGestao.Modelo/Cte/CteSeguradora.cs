namespace cwkGestao.Modelo.Cte
{
    public class CteSeguradora : ModeloBase
    {
        public override int ID { get; set; }

        public virtual CteIdentificacao Cte { get; set; }
        public virtual int ResponsavelSeguradora { get; set; }
        public virtual string NumeroApolice { get; set; }
        public virtual string NumeroAverbacao { get; set; }
        public virtual decimal ValorCarga { get; set; }
        public virtual string XSeguro { get; set; }
    }
}
