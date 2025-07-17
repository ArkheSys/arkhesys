using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace cwkGestao.Modelo
{
    public class PlanoNegocioOperacao : ModeloBase
    {
        public override int ID { get; set; }
        public virtual int Codigo { get; set; }
        public virtual PlanoNegocio PlanoNegocio { get; set; }
        public virtual Operacao Operacao { get; set; }
        public virtual TabelaICMS TabelaICMS { get; set; }
        public virtual TabelaCFOP CFOP_DEstado { get; set; }
        public virtual TabelaCFOP CFOP_FEstado_C { get; set; }
        public virtual TabelaCFOP CFOP_FEstado_NC { get; set; }
    }
}
