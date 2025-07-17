using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class NFe_Erro : ModeloBase
    {
        public override int ID { get; set; }
        public virtual string Descricao { get; set; }
    }
}
