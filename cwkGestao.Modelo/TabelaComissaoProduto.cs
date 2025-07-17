using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class TabelaComissaoProduto : ModeloBase
    {
        public override int ID { get; set; }
        public virtual TabelaComissao TabelaComissao { get; set; }
        public virtual int Codigo { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual decimal Comissao { get; set; }
    }
}
