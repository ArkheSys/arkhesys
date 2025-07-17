using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Histórico")]
	public class Historico : ModeloBase
	{
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        [InformacaoExibicao(1, "Código", 90)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private string _Nome;
        [InformacaoExibicao(2, "Nome", 200)]
        public virtual string Nome { get { return _Nome; } set { _Nome = value; } }

        private bool _BLiberaAlterarDtCaixa;
        public virtual bool BLiberaAlterarDtCaixa { get { return _BLiberaAlterarDtCaixa; } set { _BLiberaAlterarDtCaixa = value; } }
        public virtual IList<MovimentacaoCheque> MovimentacaoCheque { get; set; }
	}
}
		