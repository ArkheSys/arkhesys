namespace cwkGestao.Modelo
{
    public class FechamentoCaixaRateio : ModeloBase
    {
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }


        private int? _IDFechamentoCaixa;
        public virtual int? IDFechamentoCaixa { get { return _IDFechamentoCaixa; } set { _IDFechamentoCaixa = value; } }

        private int? _IDFormaDePagamento;
        public virtual int? IDFormaDePagamento { get { return _IDFormaDePagamento; } set { _IDFormaDePagamento = value; } }

        private decimal? _ValorCaixa;
        public virtual decimal? ValorCaixa { get { return _ValorCaixa; } set { _ValorCaixa = value; } }

        private decimal? _ValorInformado;
        public virtual decimal? ValorInformado { get { return _ValorInformado; } set { _ValorInformado = value; } }
    }
}
