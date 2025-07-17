using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{   
    public class Ofd_PropostaMedicaoOcorrencias : ModeloBase
	{
        public override int ID { get; set; }
        
		public virtual DateTime TempoOcorrencia { get; set; }
        public virtual string HoraOcorrencia { get { return TempoOcorrencia.ToShortTimeString(); } set { HoraOcorrencia = value; } }
		public virtual string Observacao { get; set; }
        
        public virtual Ofd_Ocorrencias Ocorrencia { get; set; }
        public virtual Ofd_PropostaMedicao PropostaMedicao { get; set; }


    }
}
