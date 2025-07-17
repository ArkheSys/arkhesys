using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class cepbr_endereco : ModeloBase
	{
        public override int ID { get; set; }
		public virtual string cep { get; set; }
		public virtual string logradouro { get; set; }
		public virtual string tipo_logradouro { get; set; }
		public virtual string complemento { get; set; }
		public virtual string local_end { get; set; }
		public virtual cepbr_cidade cep_cidade { get; set; }
		public virtual cepbr_bairro cep_bairro { get; set; }

    }
}
