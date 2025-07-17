using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class LogRetornoDetalhe : ModeloBase
	{
        public override int ID { get; set; }

        public virtual LogRetorno logRetorno { get; set; }

        public virtual Documento documento { get; set; }

        [InformacaoExibicao(0, "Núm. Doc.", 65)]
        public virtual string NumeroDocumentoOrig
        {
            get { return documento != null ? documento.NumDocumento : ""; }
        }

        [InformacaoExibicao(1, "Parcela", 65)]
        public virtual string Parcela
        {
            get { return documento != null ? documento.ParcelasString : ""; }
        }

        [InformacaoExibicao(2, "Cód. Documento", 100)]
        public virtual string NumeroDocumento { get; set; }

        [InformacaoExibicao(3, "Nosso Número", 115)]
        public virtual string NossoNumero { get; set; }

        [InformacaoExibicao(4, "Ocorrência", 150)]
		public virtual string Ocorrencia { get; set; }

        [InformacaoExibicao(5, "Vl Documento", 75,InformacaoExibicao.HAlinhamento.Direita,InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal ValorDocumento
        {
            get { return documento != null ? documento.Valor : 0; }
        }

        [InformacaoExibicao(6, "Vl Crédito", 75, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
		public virtual decimal ValorCredito { get; set; }

        [InformacaoExibicao(7, "Vl Desconto", 75, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal ValorDesconto { get; set; }

        [InformacaoExibicao(8, "Vl Juros Pago", 75, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal ValorJurosPago { get; set; }

        [InformacaoExibicao(9, "Vl Multa Paga", 75, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal ValorMultaPaga { get; set; }

        [InformacaoExibicao(10, "Vl IOF", 75, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal ValorIOF { get; set; }

        [InformacaoExibicao(11, "Vl Outros Acréscimos", 75, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal ValorOutrosAcrescimos { get; set; }

        [InformacaoExibicao(12, "Vl Pago", 75, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal ValorPago { get; set; }

        [InformacaoExibicao(13, "Vl Taxa Cobrança", 75, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal ValorTaxaCobranca { get; set; }

        public virtual IList<LogRetorno> logRetornoList { get; set; }

        public virtual IList<Documento> documentoList { get; set; }

    }
}
