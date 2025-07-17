using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class OrdemProducaoExecucaoProduto : ModeloBase
    {
        public override int ID { get; set; }
        public virtual int Sequencia { get; set; }
        public virtual decimal Quantidade { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual OrdemProducaoExecucao OrdemProducaoExecucao { get; set; }
    }
}
