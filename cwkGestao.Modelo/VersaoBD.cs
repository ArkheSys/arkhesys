using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class VersaoBD : ModeloBase
    {
        public virtual int Codigo { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual string Versao { get; set; }
        public virtual string Alteracao { get; set; }
        public override int ID { get; set; }
    }
}
