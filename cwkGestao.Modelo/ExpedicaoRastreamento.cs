using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class ExpedicaoRastreamento : ModeloBase
	{
        public override int ID { get; set; }
		public virtual Pedido Pedido { get; set; }
		public virtual RastreamentoCorreio IDRastreamento { get; set; }
        public virtual decimal PesoBruto { get; set; }
        public virtual decimal PesoLiquido { get; set; }
        public virtual bool bRastroCompleto { get; set; }
        public virtual ServicoEnvioCorreio ServicoEnvioCorreio { get; set; }
        public virtual string Tipo { get; set; }
        public virtual string Status { get; set; }
    }
}
