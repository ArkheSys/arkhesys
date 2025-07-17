using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class PedidoHistorico : ModeloBase
    {
        public override int ID { get; set; }
        public virtual string UsuarioLogado { get; set; }
        public virtual string GerenteResponsavel { get; set; }
        public virtual DateTime Data { get; set; }
        public virtual decimal ValorTotalAnterior { get; set; }
        public virtual decimal ValorTotalPosterior { get; set; }
        public virtual string Descricao { get; set; }
        public virtual Pedido Pedido { get; set; }
    }
}
