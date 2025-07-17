using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class Ficha : ModeloBase
    {
        public override int ID { get; set; }
        public virtual int Codigo { get; set; }
        public virtual string Descricao { get; set; }
        public virtual DateTime Dt { get; set; }
        public virtual string Versao { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual IList<Ficha_Produto> Ficha_Produtos { get; set; }

    }
}
