using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Estado")]
	public class UF : ModeloBase, IComparable<UF>
	{
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private string _Sigla;
        [InformacaoExibicao(2, "Sigla", 50)]
        public virtual string Sigla { get { return _Sigla; } set { _Sigla = value; } }

        private string _Nome;
        [InformacaoExibicao(1, "Nome", 425)]
        public virtual string Nome { get { return _Nome; } set { _Nome = value; } }

        private int _IDPais;
        public virtual int IDPais { get { return _IDPais; } set { _IDPais = value; } }

        private Pais _Pais;
        [InformacaoExibicao(4, "País", 200)]
        public virtual Pais Pais { get { return _Pais; } set { _Pais = value; } }

        private string _IBGE;
        [InformacaoExibicao(3, "IBGE", 100, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual string IBGE { get { return _IBGE; } set { _IBGE = value; } }

        public virtual int Codigo { get; set; }

        private IList<Cidade> _Cidades;
        public virtual IList<Cidade> Cidades { get { return _Cidades; } set { _Cidades = value; } }

        public override string ToString()
        {
            return Sigla;
        }

        public virtual int CompareTo(UF other)
        {
            return this.Nome.CompareTo(other.Nome);
        }
    }
}
		