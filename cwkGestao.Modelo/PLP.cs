using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class PLP : ModeloBase
	{
        [InformacaoExibicao(0, "C�digo", 75)]
        public override int ID { get; set; }

        [InformacaoExibicao(1, "Data", 75)]
		public virtual DateTime Data { get; set; }

        [InformacaoExibicao(2, "Usu�rio", 140)]
        public virtual string Usuario { get; set; }
        
        [InformacaoExibicao(3, "Identifica��o PLP", 120)]
        public virtual int IDPLPWS { get; set; }

        public virtual bool Status { get; set; }

        public virtual IList<PLPDetalhe> PLPDetalhes { get; set; }

        [InformacaoExibicao(3, "Status", 100)]
        public virtual string StatusLegivel
        {
            get
            {
                switch (this.Status)
                {
                    case false:
                        return "N�o enviada";
                    case true:
                        return "Enviada";
                    default:
                        return "";
                }
            }

            protected set
            {

            }

        }
    }
}
