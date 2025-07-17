using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class Cor : ModeloBase
    {
        public override int ID { get; set; }
        public virtual int Codigo { get; set; }
        public virtual string Descricao { get; set; }
        public virtual IList<CQFuncionario> CQFuncionarios { get; set; }

    }
}
