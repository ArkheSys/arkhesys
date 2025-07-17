namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Produto Varejo")]

    public class ProdutoVarejo : ModeloBase
    {
        public ProdutoVarejo() { }

        public override int ID { get; set; }

        private int _IDProduto;
        public virtual int IDProduto { get { return _IDProduto; } set { _IDProduto = value; } }

        private int _QuantidadeInicial;
        public virtual int QuantidadeInicial { get { return _QuantidadeInicial; } set { _QuantidadeInicial = value; } }

        private int _QuantidadeFinal;
        public virtual int QuantidadeFinal { get { return _QuantidadeFinal; } set { _QuantidadeFinal = value; } }

        private decimal _Preco;
        public virtual decimal Preco { get { return _Preco; } set { _Preco = value; } }

        private bool _Removido;
        public virtual bool Removido { get { return _Removido; } set { _Removido = value; } }
    }
}
