using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Tipo Serviço de Transporte")]
    public class TipoServicoTransporte : ModeloBase, ICloneable
	{
        public override int ID { get; set; }
        [InformacaoExibicao(0, "Código", 100)]
		public virtual int Codigo { get; set; }
        [InformacaoExibicao(1, "Descrição", 300)]
		public virtual string Descricao { get; set; }
        [InformacaoExibicao(2, "Correios", 80)]
		public virtual bool Correios { get; set; }
        [InformacaoExibicao(3, "Controle Numeração", 120)]
		public virtual bool ControleNumeracao { get; set; }
        [InformacaoExibicao(4, "CaminhoImagem", 120)]
        public virtual string CaminhoImagem { get; set; }
        [InformacaoExibicao(5, "CodigoServico", 120)]
        public virtual string CodigoServico { get; set; }
        public virtual ServicoEnvioCorreio IDServicoEnvioCorreio { get; set; }
        [InformacaoExibicao(6, "Acrescimo Frete", 80)]
        public virtual decimal AcrescimoFrete { get; set; }
        public virtual IList<CorreiosPrecoPrazo> CorreiosPrecoPrazo { get; set; }

        public TipoServicoTransporte()
        {
            ID = 0;
            Codigo = 0;
            Descricao = "";
            Correios = false;
            ControleNumeracao = false;
            CaminhoImagem = "";
            CodigoServico = "";
            IDServicoEnvioCorreio = new ServicoEnvioCorreio();
            AcrescimoFrete = 0;
            CorreiosPrecoPrazo = new List<CorreiosPrecoPrazo>();
        }

        public override string ToString()
        {
            return this.Codigo + " | " + this.Descricao;
        }

        #region ICloneable Members

        public virtual object Clone()
        {
            return this.MemberwiseClone() as TipoServicoTransporte;
        }

        #endregion
    }
}
