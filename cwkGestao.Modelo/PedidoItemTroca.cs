using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class PedidoItemTroca : ModeloBase
	{
        public override int ID { get; set; }
		public virtual int Codigo { get; set; }
		public virtual DateTime Data { get; set; }
		public virtual string Responsavel { get; set; }
		public virtual string Solicitante { get; set; }
		public virtual string Produto { get; set; }
		public virtual string Descricao { get; set; }
		public virtual decimal Valor { get; set; }
        public virtual decimal ValorPedidoItem { get; set; }
        public virtual decimal QtdCompra { get; set; }
        public virtual decimal QtdTroca { get; set; }
		public virtual DateTime DataInclusao { get; set; }
		public virtual string UsuarioInclusao { get; set; }
		public virtual string Motivo { get; set; }
		public virtual int idPedidoItem { get; set; }
        public virtual string Observacao { get; set; }
    }
}
