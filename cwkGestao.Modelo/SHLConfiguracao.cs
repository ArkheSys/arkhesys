using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class SHLConfiguracao : ModeloBase
	{
        public override int ID { get; set; }

        public virtual Classificacao Classificacao { get; set; }

        public virtual TipoPedido TipoPedido { get; set; }

        public virtual string ChaveSHL { get; set; }

        public virtual int InterfaceSHL { get; set; }

        public virtual string ChaveSHLHomologacao { get; set; }

    }
}
