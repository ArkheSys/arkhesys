using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace cwkGestao.Modelo
{    
    public class PedidoItemEstoque : ModeloBase
	{
        public override int ID { get; set; }
		public virtual PedidoItem PedidoItem { get; set; }
		public virtual decimal CMVUnit { get; set; }
		public virtual decimal Quantidade { get; set; }
		public virtual Identificador Identificador { get; set; }

        public virtual PedidoItemEstoque Clone()
        {
            PedidoItemEstoque clone = new PedidoItemEstoque();

            foreach (PropertyInfo item in clone.GetType().GetProperties())
            {
                if (!item.PropertyType.ContainsGenericParameters)
                    item.SetValue(clone, item.GetValue(this, null), null);
            }

            return clone;
        }
    }
}
