using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class ServicoEnvioCorreio : ModeloBase
	{
        public override int ID { get; set; }
        [InformacaoExibicao(0, "C�digo", 90)]
		public virtual string Codigo { get; set; }
        [InformacaoExibicao(1, "Servi�o", 150)]
		public virtual string Servico { get; set; }
        [InformacaoExibicao(2, "Empresa", 90)]
		public virtual string Empresa { get; set; }
        public virtual IList<ExpedicaoRastreamento> ExpedRastreamentos { get; set; }
        public virtual IList<TipoServicoTransporte> TipoServicosTransporte { get; set; }

        public ServicoEnvioCorreio()
        {
        }

        public override string ToString()
        {
            return this.Codigo + " | " + this.Servico + " - " + this.Empresa;
        }
    }
}
