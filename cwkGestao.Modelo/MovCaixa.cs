using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo.Auxiliares;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Movimento Caixa")]
    public class MovCaixa : ModeloBase
    {
        public MovCaixa()
        {
            Dt = DateTime.Now;
            DtDigitacao = DateTime.Now;
            Movimentos = new List<Movimento>();
            UsuarioProvider usuarioProvider = new UsuarioProvider();
            IncUsuario = usuarioProvider.GetLoginUsuarioLogado();
            IncData = DateTime.Now;
            AltUsuario = usuarioProvider.GetLoginUsuarioLogado();
            AltData = DateTime.Now;
        }

        public MovCaixa(MovCaixa mov)
        {
            this.Codigo = mov.Codigo;
            this.Banco = mov.Banco;
            this.Filial = mov.Filial;
            this.Dt = mov.Dt;
            this.DtDigitacao = mov.DtDigitacao;
            this.NumDocumento = mov.NumDocumento;
            this.Ent_Sai = mov.Ent_Sai;
            this.Valor = mov.Valor;
            this.Historico = mov.Historico;
            this.ComplementoHist = mov.ComplementoHist;
            this.NumBanco = mov.NumBanco;
            this.NumAgencia = mov.NumAgencia;
            this.NumCheque = mov.NumCheque;
            this.Emitente = mov.Emitente;
            this.DtConciliacao = mov.DtConciliacao;
            this.Movimentos = mov.Movimentos;
            this.MovCaixaRelacionado = mov.MovCaixaRelacionado;
            this.FormaPagamento = mov.FormaPagamento;
            this.IDFluxoCaixa = mov.IDFluxoCaixa;
        }

        public MovCaixa(int id, int codigo, int origem, string filial, string banco, DateTime data, DateTime? dtConciliacao, string numDocumento, decimal valor
            , string complementoHist, string formaPagamento, Ent_SaiMovCaixa ent_sai, DateTime? incData, string incUsuario, DateTime? altData, string altUsuario, int? idFluxoCaixa)
            : this()
        {
            ID = id;
            Codigo = codigo;
            Movimentos.Add(new Movimento { Origem = origem });
            Filial = new Filial { Nome = filial };
            Banco = new Banco { Nome = banco };
            Dt = data;
            DtConciliacao = dtConciliacao;
            NumDocumento = numDocumento;
            Valor = valor;
            FormaPagamento = new FormaPagamento { Nome = formaPagamento };
            ComplementoHist = complementoHist;
            Ent_Sai = ent_sai;
            IncData = incData;
            IncUsuario = incUsuario;
            AltData = altData;
            AltUsuario = altUsuario;
            idFluxoCaixa = IDFluxoCaixa;
        }

        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        [InformacaoExibicao(0, "Código", 75)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private Banco _Banco;
        [InformacaoExibicao(3, "Banco", 132)]
        public virtual Banco Banco { get { return _Banco; } set { _Banco = value; } }

        private Filial _Filial;
        [InformacaoExibicao(2, "Empresa", 75)]
        public virtual Filial Filial { get { return _Filial; } set { _Filial = value; } }

        private DateTime _Dt;
        [InformacaoExibicao(4, "Data", 85)]
        public virtual DateTime Dt { get { return _Dt; } set { _Dt = value; } }

        private DateTime _DtDigitacao;
        public virtual DateTime DtDigitacao { get { return _DtDigitacao; } set { _DtDigitacao = value; } }

        private string _NumDocumento;
        public virtual string NumDocumento { get { return _NumDocumento; } set { _NumDocumento = value; } }

        private Ent_SaiMovCaixa _Ent_Sai;
        public virtual Ent_SaiMovCaixa Ent_Sai { get { return _Ent_Sai; } set { _Ent_Sai = value; } }

        private decimal _Valor;
        public virtual decimal Valor { get { return _Valor; } set { _Valor = value; } }

        private Historico _Historico;
        public virtual Historico Historico { get { return _Historico; } set { _Historico = value; } }

        private string _ComplementoHist;
        [InformacaoExibicao(9, "Histórico", 288)]
        public virtual string ComplementoHist { get { return _ComplementoHist; } set { _ComplementoHist = value; } }

        private string _NumBanco;
        public virtual string NumBanco { get { return _NumBanco; } set { _NumBanco = value; } }

        private string _NumAgencia;
        public virtual string NumAgencia { get { return _NumAgencia; } set { _NumAgencia = value; } }

        private string _NumContaCorrente;
        public virtual string NumContaCorrente { get { return _NumContaCorrente; } set { _NumContaCorrente = value; } }

        private string _NumCheque;
        public virtual string NumCheque { get { return _NumCheque; } set { _NumCheque = value; } }

        private string _Emitente;
        public virtual string Emitente { get { return _Emitente; } set { _Emitente = value; } }

        private DateTime? _DtConciliacao;
        [InformacaoExibicao(5, "Conciliação", 85)]
        public virtual DateTime? DtConciliacao { get { return _DtConciliacao; } set{ _DtConciliacao = value; } }

        [InformacaoExibicao(1, "Origem", 75)]
        public virtual String Origem
        {
            get
            {
                if (Movimentos == null || Movimentos.Count == 0) return "";
                Movimento first = Movimentos.First();
                return first.Origem == 1 ? "Financeiro" : "Caixa";
            }
        }

        [InformacaoExibicao(6, "Documento", 75)]
        public virtual String Documento
        {
            get
            {
                return NumDocumento;
                //if (Movimentos == null || Movimentos.Count == 0) return "";
                //Movimento first = Movimentos.First();
                //return first.Documento.NumDocumento;
            }
        }

        [InformacaoExibicao(7, "Entrada", 131, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal Entrada
        {
            get
            {
                return Ent_Sai == Ent_SaiMovCaixa.Entrada ? Valor : 0;
            }
        }

        [InformacaoExibicao(8, "Saída", 131, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal Saida
        {
            get
            {
                return Ent_Sai == Ent_SaiMovCaixa.Saída ? Valor : 0;
            }
        }

        private IList<Movimento> _Movimentos;
        public virtual IList<Movimento> Movimentos { get { return _Movimentos; } set { _Movimentos = value; } }

        public virtual MovCaixa MovCaixaRelacionado { get; set; }

        [InformacaoExibicao(10, "Forma Pagamento", 75)]
        public virtual FormaPagamento FormaPagamento { get; set; }

        public virtual Cheque Cheque { get; set; }

        [InformacaoExibicao(11, "Data Inclusão", 100)]
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
        [InformacaoExibicao(12, "Usuário Inclusão", 100)]
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

        [InformacaoExibicao(13, "Data Alteração", 100)]
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
        [InformacaoExibicao(14, "Usuário Alteração", 100)]
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

        public virtual int? IDFluxoCaixa { get; set; }

        public virtual decimal proporcaoPagamento { get; set; }
    }
}
