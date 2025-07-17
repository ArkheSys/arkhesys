using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class PCP_Lote : ModeloBase
	{
        public override int ID { get; set; }

        [InformacaoExibicao(0, "Código", 75)]
		public virtual int Codigo { get; set; }

        [InformacaoExibicao(1, "Descrição", 200)]
		public virtual string Descricao { get; set; }

        [InformacaoExibicao(2, "Data", 75)]
		public virtual DateTime Data { get; set; }

        public virtual IList<Pedido> Pedidos { get; set; }

    }
}
