using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Unidade")]
	public class Unidade : ModeloBase, ICloneable
    {
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        [InformacaoExibicao(1, "Código", 100)]
        public virtual int Codigo { get; set; }

        private string _Sigla;
        [InformacaoExibicao(2, "Sigla", 150)]
        public virtual string Sigla { get { return _Sigla; } set { _Sigla = value; } }
        
        private string _Nome;
        [InformacaoExibicao(3, "Nome", 375)]
        public virtual string Nome { get { return _Nome; } set { _Nome = value; } }

        private bool _BQtdFracionada;
        [InformacaoExibicao(4, "Qtd. Fracionada", 150)]
        public virtual bool BQtdFracionada { get { return _BQtdFracionada; } set { _BQtdFracionada = value; } }

        public override string ToString()
        {
            return Nome;
        }

        #region ICloneable Members

        public virtual object Clone()
        {
            return this.MemberwiseClone() as Unidade;
        }

        #endregion
    }
}
		