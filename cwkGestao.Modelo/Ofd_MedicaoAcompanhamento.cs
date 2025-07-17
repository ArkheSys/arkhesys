using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Medição Acompanhamento")]
    public class Ofd_MedicaoAcompanhamento : ModeloBase
	{
        public Ofd_MedicaoAcompanhamento()
        {
           
        }

        public override int ID { get; set; }
		public virtual DateTime DataHora { get; set; }
        public virtual string Ocorrencia { get; set; }
        public virtual Ofd_Medicao Ofd_Medicao { get; set; }
        public virtual string Usuario { get; set; }
        public virtual bool EhAutomatica { get; set; }
  }
}
