using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Parcela")]
    public class PedidoParcela : ParcelaBase
	{
        public virtual Pedido Pedido { get; set; }

        public virtual int? BandeiraCartao { get; set; }
        public virtual string Operadora { get; set; }
        public virtual string CAut { get; set; }
    }
}
		