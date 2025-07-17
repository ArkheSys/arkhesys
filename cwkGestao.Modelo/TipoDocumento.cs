using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Tipo de Documento")]
	public class TipoDocumento : ModeloBase
	{
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        [InformacaoExibicao(1, "Código", 100)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private string _Nome;
        [InformacaoExibicao(2, "Nome", 600)]
        public virtual string Nome { get { return _Nome; } set { _Nome = value; } }

        private string _DescReduzida;
        [InformacaoExibicao(3, "Descrição", 100)]
        public virtual string DescReduzida { get { return _DescReduzida; } set { _DescReduzida = value; } }

        public virtual FormaPagamento FormaPagamento { get; set; }

        private IList<CondicaoParcela> _CondicaoParcelas;
        public virtual IList<CondicaoParcela> CondicaoParcelas { get { return _CondicaoParcelas; } set { _CondicaoParcelas = value; } }
	}
}
		