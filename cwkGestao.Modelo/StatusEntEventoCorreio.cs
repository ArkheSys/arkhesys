using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{  [InformacaoExibicao ("Status Entrega")]  
    public class StatusEntEventoCorreio : ModeloBase
	{
        public override int ID { get; set; }

		public virtual StatusEntrega StatusEntrega { get; set; }
		public virtual EventosCorreio EventosCorreio { get; set; }

        [InformacaoExibicao(1, "Status", 300)]
        public virtual string StatusEntregaVisual { get {return StatusEntrega.Descricao;} }

        [InformacaoExibicao(2, "Eventos", 300)]
        public virtual string EventosCorreioVisual { get {return EventosCorreio.Descricao;}}
    }
}
