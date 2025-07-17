using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo.Auxiliares;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Movimento")]
#pragma warning disable CS0659 // "Movimento" substitui Object.Equals(object o), mas não substitui Object.GetHashCode()
    public class Movimento : ModeloBase, ICloneable
#pragma warning restore CS0659 // "Movimento" substitui Object.Equals(object o), mas não substitui Object.GetHashCode()
    {
        public Movimento()
        {
            UsuarioProvider usuarioProvider = new UsuarioProvider();
            IncUsuario = usuarioProvider.GetLoginUsuarioLogado();
            IncData = DateTime.Now;

            AltUsuario = usuarioProvider.GetLoginUsuarioLogado();
            AltData = DateTime.Now;

            Rateio_Movs = new List<Rateio_Mov>();
            FormaPagamentoMovimentos = new List<FormaPgtoMovimento>();

            Origem = 1;

        }

        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private int _Sequencia;
        [InformacaoExibicao(0, "Sequencia", 60, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int Sequencia { get { return _Sequencia; } set { _Sequencia = value; } }

        private int _Origem;
        public virtual int Origem { get { return _Origem; } set { _Origem = value; } }

        private Documento _Documento;
        public virtual Documento Documento { get { return _Documento; } set { _Documento = value; } }

        private MovCaixa _MovCaixa;
        public virtual MovCaixa MovCaixa { get { return _MovCaixa; } set { _MovCaixa = value; } }

        private DateTime _Dt;
        [InformacaoExibicao(1, "Data", 90, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual DateTime Dt { get { return _Dt; } set { _Dt = value; } }

        private OperacaoMovimentoType _Operacao;
        [InformacaoExibicao(2, "Operação", 190)]
        public virtual OperacaoMovimentoType Operacao { get { return _Operacao; } set { _Operacao = value; } }

        private TipoMovimentoType _Tipo;
        [InformacaoExibicao(3, "Tipo", 120)]
        public virtual TipoMovimentoType Tipo { get { return _Tipo; } set { _Tipo = value; } }

        private PlanoConta _PlanoConta;
        [InformacaoExibicao(5, "Plano Conta", 190)]
        public virtual PlanoConta PlanoConta { get { return _PlanoConta; } set { _PlanoConta = value; } }

        [InformacaoExibicao(6, "Banco", 190)]
        public virtual String BancoString { get { return (MovCaixa != null && MovCaixa.Banco != null) ? MovCaixa.Banco.Nome : ""; } }

        public virtual String PlanoContaNome { get { return _PlanoConta.Nome; } }
        public virtual int PlanoContaCodigo { get { return _PlanoConta.Codigo; } }

        private Historico _Historico;
        public virtual Historico Historico { get { return _Historico; } set { _Historico = value; } }

        private string _ComplementoHist;
        [InformacaoExibicao(7, "Histórico", 75)]
        public virtual string ComplementoHist { get { return _ComplementoHist; } set { _ComplementoHist = value; } }

        private decimal _Valor;
        [InformacaoExibicao(4, "Valor", 120, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal Valor { get { return _Valor; } set { _Valor = value; } }

        public virtual CentroCusto CentroCusto { get; set; }

        private IList<Rateio_Mov> _Rateio_Movs;
        public virtual IList<Rateio_Mov> Rateio_Movs { get { return _Rateio_Movs; } set { _Rateio_Movs = value; } }

        public virtual IList<FormaPgtoMovimento> FormaPagamentoMovimentos { get; set; }

#pragma warning disable CS0114 // "Movimento.IncData" oculta o membro herdado "ModeloBase.IncData". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
        public virtual DateTime IncData { get; set; }
#pragma warning restore CS0114 // "Movimento.IncData" oculta o membro herdado "ModeloBase.IncData". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
#pragma warning disable CS0114 // "Movimento.IncUsuario" oculta o membro herdado "ModeloBase.IncUsuario". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
        public virtual string IncUsuario { get; set; }
#pragma warning restore CS0114 // "Movimento.IncUsuario" oculta o membro herdado "ModeloBase.IncUsuario". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
#pragma warning disable CS0114 // "Movimento.AltData" oculta o membro herdado "ModeloBase.AltData". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
        public virtual DateTime AltData { get; set; }
#pragma warning restore CS0114 // "Movimento.AltData" oculta o membro herdado "ModeloBase.AltData". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
#pragma warning disable CS0114 // "Movimento.AltUsuario" oculta o membro herdado "ModeloBase.AltUsuario". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.
        public virtual string AltUsuario { get; set; }
#pragma warning restore CS0114 // "Movimento.AltUsuario" oculta o membro herdado "ModeloBase.AltUsuario". Para que o membro atual substitua essa implementação, adicione a palavra-chave override. Caso contrário, adicione a palavra-chave new.

        public virtual string CodigoDoc
        {
            get
            {
                return Documento != null ? Documento.Codigo.ToString() : String.Empty;
            }
        }

        public virtual DateTime? VencimentoDoc
        {
            get
            {
                if (Documento != null)
                    return Documento.DtVencimento;
                return null;
            }
        }

        public override bool Equals(object obj)
        {
            return this == obj;
        }

        #region ICloneable Members

        public virtual object Clone()
        {
            return this.MemberwiseClone();
        }

        #endregion
    }
}
