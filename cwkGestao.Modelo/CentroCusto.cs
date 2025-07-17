using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class CentroCusto : ModeloBase
    {
        public override int ID { get; set; }
        public virtual int Codigo { get; set; }
        public virtual string Classificacao { get; set; }
        public virtual string Nome { get; set; }
        public virtual int TipoCCusto { get; set; }
        public virtual CentroCusto CentroCustoPai { get; set; }
        public virtual IList<CentroCusto> CentroCustos { get; set; }

        public virtual IList<Movimento> Movimentos { get; set; }

    }
}
		
