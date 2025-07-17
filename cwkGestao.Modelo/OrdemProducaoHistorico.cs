using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class OrdemProducaoHistorico : ModeloBase
    {
        public override int ID { get; set; }
        public virtual int Sequencia { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual OrdemProducao OrdemProducao { get; set; }
        public virtual Classificacao Classificacao { get; set; }
    }
}
