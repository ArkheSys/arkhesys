using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    [InformacaoExibicao("Obra/Fundação")]
    public class Ofd_Obra : ModeloBase
	{
        public Ofd_Obra()
        {
            Arquivos = new List<Ofd_ObraArquivos>();
            Status = StatusObraFundacao.Aberta;
            Diarios = new List<Ofd_ObraDiario>();
        }

        public override int ID { get; set; }
        [InformacaoExibicao(0, "Código", 100)]
		public virtual int Codigo { get; set; }
        [InformacaoExibicao(1, "Descrição", 200)]
		public virtual string Descricao { get; set; }
		public virtual Pessoa Construtora { get; set; }
        [InformacaoExibicao(2, "Responsável", 150)]
		public virtual string ResponsavelNaObra { get; set; }
		public virtual string TelefoneResponsavelNaObra { get; set; }
        [InformacaoExibicao(3, "Status", 100)]
        public virtual StatusObraFundacao Status { get; set; }
        public virtual IList<Ofd_ObraArquivos> Arquivos { get; set; }

        public virtual IList<Ofd_ObraDiario> Diarios { get; set; }

        public virtual IList<Ofd_AgendamentoMaquina> Agendamentos { get; set; }

        public override string ToString()
        {
            return Codigo + " | " + Descricao;
        }
    }
}
