using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Contatos Efetuados")]
    public class ContatoDetalhe : ModeloBase
	{
        public override int ID { get; set; }

        public virtual Contato Contato { get; set; }
        [InformacaoExibicao(3, "Data do Contato", 100)]
		public virtual DateTime Data { get; set; }
        [InformacaoExibicao(5, "Hora Início", 100)]
		public virtual TimeSpan HoraInicial { get; set; }
        [InformacaoExibicao(6, "Hora Término", 100)]
		public virtual TimeSpan HoraFinal { get; set; }

		public virtual StatusContato StatusContato { get; set; }
        [InformacaoExibicao(7, "Assunto", 250)]
		public virtual string Assunto { get; set; }
		public virtual string Observacao { get; set; }

        [InformacaoExibicao(0, "Responsável", 250)]
        public virtual string AtendenteVisivel
        {
            get { return this.Contato.Atendente.Nome; }
        }

        [InformacaoExibicao(1, "Pessoa", 250)]
        public virtual string PessoaVisivel
        {
            get { return this.Contato.Pessoa.Nome; }
        }


        [InformacaoExibicao(2, "Contato", 250)]
        public virtual string ContatoVisivel
        {
            get { return this.Contato.NomeContato; }
        }

        [InformacaoExibicao(4, "Status", 100)]
        public virtual string StatusContatoVisivel
        {
            get { return this.StatusContato.Descricao; }
        }
    }
}
