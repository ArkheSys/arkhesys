using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class FichaTecnica : ModeloBase
    {
        public override int ID { get; set; }
        public virtual bool Versao { get; set; }
        public virtual string Descricao { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual DateTime DataVersao { get; set; }
        public virtual IList<ItemFichaTecnica> ItemFichaTecnicas { get; set; }

    }
}