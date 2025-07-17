using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class SincronismoPendenteSisECommerce : ModeloBase
	{
        public override int ID { get; set; }
        public virtual bool Selecionado { get; set; }
        public virtual int NumPedidoSisEcommerce { get; set; }
		public virtual string Erro { get; set; }
		public virtual Pessoa Pessoa { get; set; }
        public virtual string CaminhoArquivo { get; set; }

    }
}
