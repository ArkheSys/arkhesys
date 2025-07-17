namespace cwkGestao.Modelo.Cte
{
    public class CteCargaInfo : ModeloBase
    {
        public override int ID { get; set; }

        public virtual CteCarga CteCarga { get; set; }
        public virtual int Unidade { get; set; }
        public virtual decimal Carga { get; set; }
        public virtual string TipoMedida { get; set; }
    }
}
