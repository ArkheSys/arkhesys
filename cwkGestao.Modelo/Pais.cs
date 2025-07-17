using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("País")]
	public class Pais : ModeloBase
	{
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        [InformacaoExibicao(1, "Código", 100)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private string _Nome;
        [InformacaoExibicao(2, "Nome",485)]
        public virtual string Nome { get { return _Nome; } set { _Nome = value; } }

        private string _IBGE;
        [InformacaoExibicao(3, "IBGE",150, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual string IBGE { get { return _IBGE; } set { _IBGE = value; } }

        private IList<UF> _UFs;
        public virtual IList<UF> UFs { get { return _UFs; } set { _UFs = value; } }

        public override string ToString()
        {
            return Nome;
        }
	}
}
		