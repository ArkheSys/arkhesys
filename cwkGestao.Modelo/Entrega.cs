using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Entrega")]
    public class Entrega : ModeloBase
	{
        public override int ID { get; set; }
		public virtual DateTime Data { get; set; }
        public virtual int Sequencia { get; set; }
		public virtual string NumeroEntrega { get; set; }
		public virtual decimal QuantidadeEntrega { get; set; }
		public virtual decimal QuantidadeRestante { get; set; }
        public virtual string Responsavel { get; set; }
        public virtual string Solicitante { get; set; }
        public virtual Pedido Pedido { get; set; }
        public virtual IList<EntregaItem> ItensEntrega { get; set; }
    }
}
