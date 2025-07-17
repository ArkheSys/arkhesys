using System;

namespace cwkGestao.Modelo
{
    public class FluxoCaixa : ModeloBase
    {
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int? _IDUsuario;
        public virtual int? IDUsuario { get { return _IDUsuario; } set { _IDUsuario = value; } }

        private decimal? _ValorCaixa;
        public virtual decimal? ValorCaixa { get { return _ValorCaixa; } set { _ValorCaixa = value; } }

        private decimal? _ValorFechamento;
        public virtual decimal? ValorFechamento { get { return _ValorFechamento; } set { _ValorFechamento = value; } }

        private int? _Aberto;
        public virtual int? Aberto { get { return _Aberto; } set { _Aberto = value; } }

        private DateTime? _DataAbertura;
        public virtual DateTime? DataAbertura { get { return _DataAbertura; } set { _DataAbertura = value; } }

        private DateTime? _DataFechamento;
        public virtual DateTime? DataFechamento { get { return _DataFechamento; } set { _DataFechamento = value; } }

        private string _Observacao;
        public virtual string Observacao { get { return _Observacao; } set { _Observacao = value; } }

        private string _ObservacaoFechamento;
        public virtual string ObservacaoFechamento { get { return _ObservacaoFechamento; } set { _ObservacaoFechamento = value; } }
    }
}
