using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class ExpedicaoItem : ModeloBase
	{
        public ExpedicaoItem() 
        {
            QuantidadeConferida = 0;
        }

        public ExpedicaoItem(PedidoItem item) : this()
        {
            Pedido = item.Pedido;
            Produto = item.Produto;
            Quantidade = item.Quantidade;
        }

        public override int ID { get; set; }
		public virtual Pedido Pedido { get; set; }
		public virtual int Sequencia { get; set; }
		public virtual Produto Produto { get; set; }
        public virtual decimal Quantidade { get; set; }
		public virtual decimal QuantidadeConferida { get; set; }
        public virtual string UsuarioExpedicao { get; set; }
        public virtual DateTime? DataHoraExpedicao { get; set; }

        public virtual bool Conferido { get { return Quantidade == QuantidadeConferida; } }
        public virtual string Marca
        {
            get
            {
                if (Produto != null && Produto.Grupo1 != null)
                {
                    return Produto.Grupo1.Nome;
                }
                return String.Empty;
            }
        }
    }
}
