using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class ContratoControle : ModeloBase
	{
        public override int ID { get; set; }
		public virtual Contrato Contrato { get; set; }
		public virtual DateTime MesGerado { get; set; }
        public virtual bool bServico { get; set; }
        public virtual ContratoServico ContratoServico { get; set; }
        public virtual ContratoProduto ContratoProduto { get; set; }
        public virtual NotaItem NotaItem { get; set; }
        public virtual NotaItemServico NotaItemServico { get; set; }
    }
}
