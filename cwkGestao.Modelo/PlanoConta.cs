using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Plano de Contas")]
    public class PlanoConta : ModeloBase
    {
        public override int ID { get; set; }

        private int _Codigo;
        [InformacaoExibicao(1, "Código", 70)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private string _Classificacao;
        [InformacaoExibicao(2, "Classe", 80)]
        public virtual string Classificacao { get { return _Classificacao; } set { _Classificacao = value; } }

        private string _Nome;
        [InformacaoExibicao(3, "Nome", 508)]
        public virtual string Nome { get { return _Nome; } set { _Nome = value; } }

        [InformacaoExibicao(5, "Título", 50)]
        public virtual bool BTitulo { get; set; }

        [InformacaoExibicao(4, "Tipo", 70)]
        public virtual DebCreType DebCre { get; set; }

        private PlanoConta _PlanoContaPai;
        public virtual PlanoConta PlanoContaPai { get { return _PlanoContaPai; } set { _PlanoContaPai = value; } }

        private PlanoContaReferencial _PlanoContaReferencial;
        public virtual PlanoContaReferencial PlanoContaReferencial { get { return _PlanoContaReferencial; } set { _PlanoContaReferencial = value; } }

        public virtual string Cod_Nat_CC { get; set; }

        public virtual IList<PlanoConta> PlanoContaFilhos { get; set; }
        public virtual IList<MovimentacaoCheque> MovimentacaoCheque { get; set; }
    }
}
