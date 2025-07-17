using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Produto")]
    public class ContratoProduto : ModeloBase
	{
        public override int ID { get; set; }
		public virtual Contrato Contrato { get; set; }
		public virtual int Sequencia { get; set; }
        [InformacaoExibicao(2, "Desc Reduzida", 250)]
        public virtual string DescReduzida { get; set; }

        private Produto prod;
        [InformacaoExibicao(1, "Produto", 150)]
        public virtual Produto Produto {
            get
            {
                return prod;
            }
            set
            {
                prod = value;
                DescReduzida = prod.DescReduzida;
            }
        }

        [InformacaoExibicao(3, "Quantidade", 100)]
		public virtual decimal Quantidade { get; set; }

        [InformacaoExibicao(4, "Valor", 75)]
		public virtual decimal Valor { get; set; }

		public virtual decimal ValorDesconto { get; set; }
		public virtual decimal PercDesconto { get; set; }
        [InformacaoExibicao(5, "Total", 75)]
		public virtual decimal Total { get; set; }
		public virtual DateTime DataInicioFaturamento { get; set; }
		public virtual bool BJaneiro { get; set; }
		public virtual bool BFevereiro { get; set; }
		public virtual bool BMarco { get; set; }
		public virtual bool BAbril { get; set; }
		public virtual bool BMaio { get; set; }
		public virtual bool BJunho { get; set; }
        public virtual bool BJulho { get; set; }
		public virtual bool BAgosto { get; set; }
		public virtual bool BSetembro { get; set; }
		public virtual bool BOutubro { get; set; }
		public virtual bool BNovembro { get; set; }
		public virtual bool BDezembro { get; set; }
		public virtual DateTime? DataCancelamento { get; set; }
		public virtual string MotivoCancelamento { get; set; }
		public virtual bool bDocumentosGerados { get; set; }
		public virtual int DiaVencimento { get; set; }
        public virtual string InfAdicionais { get; set; }
        public virtual IList<NotaItem> NotaItem { get; set; }
        public virtual DateTime Vencimento { get; set; }
    }
}
