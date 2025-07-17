using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class CQProduto : ModeloBase
    {
        public override int ID { get; set; }
        public virtual int Codigo { get; set; }
        public virtual ControleQualidade ControleQualidade { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual decimal QtdProduzida { get; set; }
    }
}
