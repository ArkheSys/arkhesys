using cwkGestao.Modelo.Auxiliares;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Movimentação Cheque")]
    public class MovimentacaoCheque : ModeloBase
    {
        public MovimentacaoCheque()
        {
            Dt = DateTime.Now;
            DtDigitacao = DateTime.Now;
            UsuarioProvider usuarioProvider = new UsuarioProvider();
            IncUsuario = usuarioProvider.GetLoginUsuarioLogado();
            IncData = DateTime.Now;
            AltUsuario = usuarioProvider.GetLoginUsuarioLogado();
            AltData = DateTime.Now;
        }

        public enum TipoMovimentacao
        {
            Deposito = 0,
            Devolucao = 1,
            Receber = 2
        }

        public override int ID { get; set; }
        public virtual int Codigo { get; set; }
        public virtual Filial Filial { get; set; }
        private DateTime _Dt;
        public virtual DateTime Dt { get { return _Dt; } set { _Dt = value; } }

        private DateTime _DtDigitacao;
        public virtual DateTime DtDigitacao { get { return _DtDigitacao; } set { _DtDigitacao = value; } }
        public virtual Pessoa Pessoa { get; set; }
        public virtual string NumeroCheque { get; set; }
        public virtual decimal Valor { get; set; }
        public virtual DateTime DataMovimentacao { get; set; }
        public virtual Banco Banco { get; set; }
        public virtual PlanoConta PlanoConta { get; set; }
        public virtual Historico Historico { get; set; }
        public virtual DateTime DataCheque { get; set; }
        public virtual Banco BancoDestino { get; set; }
        public virtual string ComplementoHist { get; set; }
        public virtual TipoMovimentacao Movimentacao { get; set; }
        public virtual string MovimentacaoLegivel
        {
            get
            {
                return TipoMovimentacaoToString(this.Movimentacao);
            }
        }

        public static string TipoMovimentacaoToString(TipoMovimentacao movimentacao)
        {
            string movimentacaoLegivel = "";
            switch (movimentacao)
            {
                case TipoMovimentacao.Deposito:
                    movimentacaoLegivel = "Depósito";
                    break;
                case TipoMovimentacao.Devolucao:
                    movimentacaoLegivel = "Devolução";
                    break;
                case TipoMovimentacao.Receber:
                    movimentacaoLegivel = "Receber";
                    break;
            }
            return movimentacaoLegivel;
        }

        public override System.DateTime? IncData
        {
            get
            {
                return base.IncData;
            }
            set
            {
                base.IncData = value;
            }
        }

        public override string IncUsuario
        {
            get
            {
                return base.IncUsuario;
            }
            set
            {
                base.IncUsuario = value;
            }
        }

        public override DateTime? AltData
        {
            get
            {
                return base.AltData;
            }
            set
            {
                base.AltData = value;
            }
        }

        public override string AltUsuario
        {
            get
            {
                return base.AltUsuario;
            }
            set
            {
                base.AltUsuario = value;
            }
        }
    }
}
