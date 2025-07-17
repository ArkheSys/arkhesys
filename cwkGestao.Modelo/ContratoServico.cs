using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Serviço")]
    public class ContratoServico : ModeloBase
	{
        public override int ID { get; set; }
		public virtual Contrato Contrato { get; set; }
		public virtual int Sequencia { get; set; }
        public virtual Servico Servico { get; set; }
		public virtual int Quantidade { get; set; }
		public virtual decimal Valor { get; set; }
		public virtual decimal ValorDesconto { get; set; }
		public virtual decimal PercDesconto { get; set; }
		public virtual decimal Total { get; set; }
		public virtual Pessoa PessoaServico { get; set; }
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
		public virtual string MovitoCancelamento { get; set; }
        public virtual bool bDocumentosGerados { get; set; }
        public virtual int DiaVencimento { get; set; }
    }
}
