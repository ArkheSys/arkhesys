using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Contrato")]
    public class Contrato : ModeloBase
	{
        public Contrato()
        {
            Servicos = new List<ContratoServico>();
            Produtos = new List<ContratoProduto>();
        }

        public override int ID { get; set; }

        [InformacaoExibicao(1, "Código", 80)]
		public virtual int Codigo { get; set; }

        [InformacaoExibicao(2, "Criação", 80)]
		public virtual DateTime Data { get; set; }

        [InformacaoExibicao(3, "Digitação", 80)]
		public virtual DateTime DataDigitacao { get; set; }

        [InformacaoExibicao(4, "Empresa", 270)]
		public virtual Pessoa Pessoa { get; set; }

        [InformacaoExibicao(5, "Vendedor", 268)]
		public virtual Pessoa PessoaVendedor { get; set; }

		public virtual string Observacao { get; set; }

        public virtual TipoNota TipoNota { get; set; }

        public virtual Condicao Condicao { get; set; }

        public virtual IList<ContratoServico> Servicos { get; set; }

        public virtual IList<ContratoProduto> Produtos { get; set; } 

        public virtual bool Selecionado { get; set; }

        public virtual int bDocumentosGerados { get; set; }
    }
}
