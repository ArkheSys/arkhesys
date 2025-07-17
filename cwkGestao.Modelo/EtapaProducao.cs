using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class EtapaProducao : ModeloBase
    {
        public override int ID { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual Servico Servico { get; set; }
        public virtual bool Sequencia { get; set; }
        public virtual string Titulo { get; set; }
        public virtual string Observacao { get; set; }
    }
}
