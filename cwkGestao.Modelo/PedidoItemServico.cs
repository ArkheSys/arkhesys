using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;

namespace cwkGestao.Modelo
{    
    public class PedidoItemServico : ModeloBase
	{
        public override int ID { get; set; }
		public virtual Pedido Pedido { get; set; }
		public virtual int Sequencia { get; set; }
		public virtual Servico Servico { get; set; }
        public virtual string ServicoNome
        {
            get
            {
                if (Servico != null)
                {
                    return Servico.Nome;    
                }
                else
                {
                    return String.Empty;
                }
            }
            set
            {
                ;
            }
        }
		public virtual decimal Quantidade { get; set; }
		public virtual decimal Valor { get; set; }
		public virtual decimal PercDesconto { get; set; }
		public virtual decimal Total { get; set; }

        public virtual bool Selecionado { get; set; }

        public virtual decimal ValorDesconto { get; set; }
		public virtual decimal RAT_Desconto { get; set; }
		public virtual decimal RAT_Acrescimo { get; set; }

        public virtual PedidoItemServico OrcamentoItem { get; set; }

        //public virtual IList<NotaItemServicoPedidoItemServico> NotaItemSvcPedItemSvcs { get; set; }

        public virtual PedidoItemServico Clone()
        {
            PedidoItemServico clone = new PedidoItemServico();

            foreach (PropertyInfo item in clone.GetType().GetProperties())
            {
                if (!item.PropertyType.ContainsGenericParameters)
                    item.SetValue(clone, item.GetValue(this, null), null);
            }

            return clone;
        }

        public virtual string CustoEFormasDePagamento { get; set; }
        public virtual bool BFaturado { get; set; }

    }
}
