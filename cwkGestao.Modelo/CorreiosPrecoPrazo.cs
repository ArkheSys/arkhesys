using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class CorreiosPrecoPrazo : ModeloBase, ICloneable
	{
        public override int ID { get; set; }
		public virtual Pedido IDPedido { get; set; }
        public virtual RastreamentoCorreio IDRastreamento { get; set; }
        public virtual string CepOrigem { get; set; }
        public virtual string CepDestino { get; set; }
		public virtual string NCdServico { get; set; }
        [InformacaoExibicao(5, "Peso", 90)]
        public virtual decimal NVlPeso { get; set; }
		public virtual int NCdFormato { get; set; }
        [InformacaoExibicao(3, "Comprimento", 90)]
        public virtual decimal NVlComprimento { get; set; }
        [InformacaoExibicao(2, "Altura", 90)]
        public virtual decimal NVlAltura { get; set; }
        [InformacaoExibicao(1, "Largura", 90)]
        public virtual decimal NVlLargura { get; set; }
        [InformacaoExibicao(4, "Diâmetro", 90)]
        public virtual decimal NVlDiametro { get; set; }
		public virtual string SCdMaoPropria { get; set; }
        [InformacaoExibicao(6, "Valor Declarado", 90)]
        public virtual decimal NVlValorDeclarado { get; set; }
		public virtual string SCdAvisoRecebimento { get; set; }
		public virtual bool bCalculadoPrecoEprazo { get; set; }
		public virtual decimal Valor { get; set; }
		public virtual decimal ValorMaoPropria { get; set; }
		public virtual decimal ValorAvisoRecebimento { get; set; }
		public virtual decimal ValorValorDeclarado { get; set; }
		public virtual int Erro { get; set; }
		public virtual string MsgErro { get; set; }
        public virtual DateTime DtPrazoEntrega { get; set; }
        public virtual bool bReplicado { get; set; }
        public virtual FormatoPacoteTransporte FormatoPacoteTransporte { get; set; }
        public virtual TipoServicoTransporte TipoServicoTransporte { get; set; }
        public virtual bool? bSelecionado { get; set; }

        public CorreiosPrecoPrazo()
        {
            CepOrigem = "";
            CepDestino = "";
            NCdFormato = 0;
            NCdServico = "";
            NVlAltura = 0;
            NVlComprimento = 0;
            NVlLargura = 0;
            NVlDiametro = 0;
            NVlValorDeclarado = 0;
            NVlPeso = 0;
            SCdMaoPropria = "";
            SCdAvisoRecebimento = "";
            Valor = 0;
            ValorMaoPropria = 0;
            ValorAvisoRecebimento = 0;
            Erro = 0;
            MsgErro = "";
            DtPrazoEntrega = DateTime.Now;
            bCalculadoPrecoEprazo = false;
            bReplicado = false;
            bSelecionado = false;
        }



        #region ICloneable Members

        public virtual object Clone()
        {
            CorreiosPrecoPrazo clone = this.MemberwiseClone() as CorreiosPrecoPrazo;
            clone.FormatoPacoteTransporte = new FormatoPacoteTransporte();
            clone.FormatoPacoteTransporte = this.FormatoPacoteTransporte;
            clone.TipoServicoTransporte = new TipoServicoTransporte();
            clone.TipoServicoTransporte = this.TipoServicoTransporte;
            return clone;
        }

        #endregion
    }
}
