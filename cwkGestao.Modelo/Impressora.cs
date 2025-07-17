using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Impressora")]
	public class Impressora : ModeloBase
	{
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }
        
        private int _Codigo;
        [InformacaoExibicao(0, "Código", 100)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }
        
        private string _Nome;
        [InformacaoExibicao(1, "Nome", 630)]
        public virtual string Nome { get { return _Nome; } set { _Nome = value; } }

        private string _Caminho;
        public virtual string Caminho { get { return _Caminho; } set { _Caminho = value; } }

        private bool _Tipo;
        public virtual bool Tipo { get { return _Tipo; } set { _Tipo = value; } }

        public virtual TipoImpressao TipoImpressao { get; set; }
	}
}
		