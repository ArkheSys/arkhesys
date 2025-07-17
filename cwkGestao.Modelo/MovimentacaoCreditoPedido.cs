using cwkGestao.Modelo.Auxiliares;
using System;

namespace cwkGestao.Modelo
{
    public class MovimentacaoCreditoPedido : ModeloBase, IEnumInterpreter
    {
        public enum TipoMovimentoCreditoPedido
        {
            DEBITO = 0,
            CREDITO = 1
        }

        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _IDCliente;
        public virtual int IDCliente { get { return _IDCliente; } set { _IDCliente = value; } }

        private int _IDPedido;
        public virtual int IDPedido { get { return _IDPedido; } set { _IDPedido = value; } }

        private int? _IDFrenteCaixaNota;
        public virtual int? IDFrenteCaixaNota { get { return _IDFrenteCaixaNota; } set { _IDFrenteCaixaNota = value; } }

        private int? _IDNota;
        public virtual int? IDNota { get { return _IDNota; } set { _IDNota = value; } }

        private int? _IDMotivoDevolucao;
        public virtual int? IDMotivoDevolucao { get { return _IDMotivoDevolucao; } set { _IDMotivoDevolucao = value; } }

        private string _MotivoDevolucao;
        public virtual string MotivoDevolucao { get { return _MotivoDevolucao; } set { _MotivoDevolucao = value; } }

        private DateTime? _Data;
        public virtual DateTime? Data { get { return _Data; } set { _Data = value; } }

        private decimal _Valor;
        public virtual decimal Valor { get { return _Valor; } set { _Valor = value; } }

        private decimal _Saldo;
        public virtual decimal Saldo { get { return _Saldo; } set { _Saldo = value; } }

        private TipoMovimentoCreditoPedido _Tipo;
        public virtual TipoMovimentoCreditoPedido Tipo { get { return _Tipo; } set { _Tipo = value; } }

        public virtual string GetReadableName(object enumvalue)
        {
            int enumint = 0;
            try { enumint = (int)enumvalue; }
            catch { throw new ArgumentException("valor não é um enum"); }

            if (typeof(TipoMovimentoCreditoPedido).Equals(enumvalue?.GetType()))
            {
                switch (enumint)
                {
                    case (int)TipoMovimentoCreditoPedido.DEBITO:
                        return "DEBITO";
                    case (int)TipoMovimentoCreditoPedido.CREDITO:
                        return "CREDITO";
                    default:
                        return null;
                }
            }
            return null;
        }
    }
}
