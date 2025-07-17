using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("CFOP")]
	public class TabelaCFOP : ModeloBase
	{
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private string _CFOP;
        [InformacaoExibicao(3, "CFOP", 100)]
        public virtual string CFOP { get { return _CFOP; } set { _CFOP = value; } }

        private string _Nome;
        [InformacaoExibicao(2, "Nome", 500, InformacaoExibicao.FormaOrdenacao.Crescente)]
        public virtual string Nome { get { return _Nome; } set { _Nome = value; } }

        private bool _bCalcICMS;
        public virtual bool BCalcICMS { get { return _bCalcICMS; } set { _bCalcICMS = value; } }

        private bool _bDevolucao;
        public virtual bool BDevolucao { get { return _bDevolucao; } set { _bDevolucao = value; } }

        [InformacaoExibicao(1, "Código", 100)]
        public virtual int Codigo { get; set; }

        public virtual string CfopVinculadoEntrada { get; set; }

        public virtual PlanoConta PlanoConta { get; set; }

    }
}
		