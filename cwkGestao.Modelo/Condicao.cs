using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Condição de Pagamento")]
    public class Condicao : ModeloBase
    {
        public Condicao()
        {
            CondicaoParcelas = new List<CondicaoParcela>();
        }
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        [InformacaoExibicao(1, "Código", 100)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private string _Nome;
        [InformacaoExibicao(2, "Nome", 625)]
        public virtual string Nome { get { return _Nome; } set { _Nome = value; } }

        private int? _Divide;
        public virtual int? Divide { get { return _Divide; } set { _Divide = value; } }

        private int _QtParcela;
        [InformacaoExibicao(3, "Qtd de Parcelas", 100, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int QtParcela { get { return _QtParcela; } set { _QtParcela = value; } }

        private VencimentoFeriado _VencimentoFeriado;
        public virtual VencimentoFeriado VencimentoFeriado { get { return _VencimentoFeriado; } set { _VencimentoFeriado = value; } }

        private string _TipoDt;
        public virtual string TipoDt { get { return _TipoDt; } set { _TipoDt = value; } }

        private int? _DiaMes;
        public virtual int? DiaMes { get { return _DiaMes; } set { _DiaMes = value; } }

        private string _TipoVlr;
        public virtual string TipoVlr { get { return _TipoVlr; } set { _TipoVlr = value; } }

        private decimal? _VlrPerc;
        public virtual decimal? VlrPerc { get { return _VlrPerc; } set { _VlrPerc = value; } }

        private string _Descricao;
        public virtual string Descricao { get { return _Descricao; } set { _Descricao = value; } }
        public virtual bool TipoVariacao { get; set; }
        public virtual decimal Variacao { get; set; }

        private bool _HabilitaCondicaoPDVDelivery;
        public virtual bool HabilitaCondicaoPDVDelivery { get { return _HabilitaCondicaoPDVDelivery; } set { _HabilitaCondicaoPDVDelivery = value; } }

        private IList<CondicaoParcela> _CondicaoParcelas;
        public virtual IList<CondicaoParcela> CondicaoParcelas { get { return _CondicaoParcelas; } set { _CondicaoParcelas = value; } }

        public virtual decimal CalculaVariacao(decimal valor)
        {
            if (TipoVariacao)
            {
                return valor + Variacao;
            }

            return valor * ((100 + Variacao) / 100);
        }

        public virtual bool HabilitaNumeroPos { get; set; }
    }
}
