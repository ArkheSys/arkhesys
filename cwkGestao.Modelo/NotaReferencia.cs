using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("NotaReferencia")]
    public class NotaReferencia : ModeloBase
	{
        public NotaReferencia()  { }
        public override int ID { get; set; }
        public virtual Nota Nota { get; set; }
        public virtual string Chave { get; set; }
        public virtual int Sequencia { get; set; }
    }
}
		