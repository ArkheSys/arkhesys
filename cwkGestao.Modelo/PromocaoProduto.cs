namespace cwkGestao.Modelo
{
    public class PromocaoProduto : ModeloBase
    {
        public override int ID { get; set; }
        public virtual Promocao Promocao { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual decimal PrecoBase { get; set; }
        public virtual decimal PrecoPromocao { get; set; }

        public virtual string Nome { get { return Produto?.Nome; } }
        public virtual string CodRef { get { return Produto?.CodigoReferencia; } }
        public virtual string Barra { get { return Produto?.Barra; } }
        public virtual string Codigo { get { return Produto?.Codigo; } }


        public virtual bool Removido { get; set; }
        public virtual string Guid { get; set; }

        public PromocaoProduto()
        {
        }
    }
}