using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Grupo1")]
	public class Grupo1 : ModeloBase
	{
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        [InformacaoExibicao(1, "Código", 100)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private string _Nome;
        [InformacaoExibicao(2, "Nome", 397)]
        public virtual string Nome { get { return _Nome; } set { _Nome = value; } }

        private string _NomeReduzido;
        public virtual string NomeReduzido { get { return _NomeReduzido; } set { _NomeReduzido = value; } }

        public override string ToString()
        {
            return Nome;
        }
	}
}
		