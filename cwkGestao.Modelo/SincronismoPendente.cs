using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class SincronismoPendente : ModeloBase
	{
        public SincronismoPendente()
        {
            Descricao = String.Empty;
        }

        public override int ID { get; set; }
		public virtual string NomeCadastro { get; set; }
		public virtual int IDCadastro { get; set; }
        public virtual string Descricao { get; set; }
        public virtual string Erro { get; set; }
        public virtual bool Selecionado { get; set; }
        public virtual int IDIntegracao { get; set; }
        public virtual int IDPedidoMagento { get; set; }
        public virtual string ClienteNome { get; set; }
    }
}
