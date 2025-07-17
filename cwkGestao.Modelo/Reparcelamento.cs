using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class Reparcelamento : ModeloBase
    {
        public Reparcelamento()
        {
            ReparcelamentoDocs = new List<ReparcelamentoDoc>();
        }

        public override int ID { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual Pessoa Pessoa { get; set; }
        public virtual decimal ValorTotal { get; set; }
        public virtual decimal Juros { get; set; }
        public virtual string Observacao { get; set; }
        public virtual DateTime Data_Base { get; set; }
        public virtual IList<ReparcelamentoDoc> ReparcelamentoDocs { get; set; }

    }
}
