using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
	public class TabelaICMS : ModeloBase
	{
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        [InformacaoExibicao(1, "Código", 100)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private string _Nome;
        [InformacaoExibicao(2, "Nome", 600)]
        public virtual string Nome { get { return _Nome; } set { _Nome = value; } }

        private IList<ICMS> _ICMSs;
        public virtual IList<ICMS> ICMSs { get { return _ICMSs; } set { _ICMSs = value; } }
	}
}
		