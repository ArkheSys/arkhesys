using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{   
    [InformacaoExibicao("Ocorr�ncia de Agendamento de M�quina")]
    public class Ofd_AgendamentoMaquinaOcorrencia : ModeloBase
	{
        public override int ID { get; set; }
        [InformacaoExibicao(2, "Ocorr�ncia", 150)]
        public virtual Ofd_Ocorrencias Ocorrencia { get; set; }
		public virtual Ofd_AgendamentoMaquina Ofd_AgendamentoMaquina { get; set; }
        [InformacaoExibicao(3, "Observa��o", 150)]
		public virtual string Observacao { get; set; }
        [InformacaoExibicao(1, "Tempo Ocorr�ncia", 150)]
		public virtual DateTime TempoOcorrencia { get; set; }
        public virtual string HoraOcorrencia { get { return TempoOcorrencia.ToShortTimeString(); } set { HoraOcorrencia = value; } }

    }
}
