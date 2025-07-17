using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Telefone")]
    public class Tel_ClienteTelefone : ModeloBase
	{
        public override int ID { get; set; }
        public virtual Tel_Cliente Cliente { get; set; }
        [InformacaoExibicao(0, "Telefone", 100)]
        public virtual Tel_GrupoClienteTelefone GrupoClienteTelefone { get; set; }
        [InformacaoExibicao(2, "Data Ativação", 120, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual DateTime DataAtivacao { get; set; }
        [InformacaoExibicao(4, "Data Canc.", 120, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual DateTime? DataDesligamento { get; set; }
        [InformacaoExibicao(5, "Motivo Canc.", 200)]
        public virtual string MotivoDesligamento { get; set; }
        [InformacaoExibicao(6, "Contato Canc.", 200)]
        public virtual string ContatoPediuDesligamento { get; set; }
        [InformacaoExibicao(3, "Piloto", 80)]
        public virtual bool BAgrupador { get; set; }
        [InformacaoExibicao(1, "LEN", 80)]
        public virtual string LEN { get; set; }
    }
}
		