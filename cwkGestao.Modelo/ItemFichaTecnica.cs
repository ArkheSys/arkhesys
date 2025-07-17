using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class ItemFichaTecnica : ModeloBase
    {
        public override int ID { get; set; }
        public virtual FichaTecnica FichaTecnica { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual bool Quantidade { get; set; }
    }
}
