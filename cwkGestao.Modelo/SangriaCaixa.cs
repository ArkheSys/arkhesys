using System;

namespace cwkGestao.Modelo
{
    public class SangriaCaixa : ModeloBase
    {
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int? _IDUsuario;
        public virtual int? IDUsuario { get { return _IDUsuario; } set { _IDUsuario = value; } }

        private int? _IDFluxoCaixa;
        public virtual int? IDFluxoCaixa { get { return _IDFluxoCaixa; } set { _IDFluxoCaixa = value; } }

        private DateTime? _DataSangria;
        public virtual DateTime? DataSangria { get { return _DataSangria; } set { _DataSangria = value; } }

        private decimal? _Valor;
        public virtual decimal? Valor { get { return _Valor; } set { _Valor = value; } }

        private string _Observacao;
        public virtual string Observacao { get { return _Observacao; } set { _Observacao = value; } }

        private int? _Retirada;
        public virtual int? Retirada { get { return _Retirada; } set { _Retirada = value; } }
    }
}
