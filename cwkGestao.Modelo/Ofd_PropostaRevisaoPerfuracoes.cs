using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Proposta Revis�o")]
    public class Ofd_PropostaRevisaoPerfuracoes : ModeloBase
	{
        public Ofd_PropostaRevisaoPerfuracoes()
        {
            Perfuracoes = new List<Ofd_PropostaPerfuracoes>();
        }

        public override int ID { get; set; }
        [InformacaoExibicao(0, "Revis�o", 80)]
		public virtual string Revisao { get; set; }
        [InformacaoExibicao(0, "Observa��o", 150)]
		public virtual string Observacao { get; set; }
		public virtual Ofd_Proposta Ofd_Proposta { get; set; }

        public virtual IList<Ofd_PropostaPerfuracoes> Perfuracoes { get; set; }

    }
}
