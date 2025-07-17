using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    [InformacaoExibicao("Identificadores")]
    public class Identificador : ModeloBase
	{
        public override int ID { get; set; }
        public virtual int Ano { get; set; }
        public virtual int Sequencia { get; set; }

        [InformacaoExibicao(1, "Código", 100)]
        public virtual string Codigo { get { return String.Format("{0}{1:000}", Ano.ToString().Substring(2), Sequencia); }}
        [InformacaoExibicao(2, "Descricao", 400)]
		public virtual string Descricao { get; set; }

        public virtual bool bVerificado { get; set; }


        public override string ToString()
        {
            return Codigo;
        }
    }
}
