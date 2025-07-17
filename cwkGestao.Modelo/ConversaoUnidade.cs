using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Conversão de Unidade")]
#pragma warning disable CS0659 // "ConversaoUnidade" substitui Object.Equals(object o), mas não substitui Object.GetHashCode()
    public class ConversaoUnidade : ModeloBase, ICloneable
#pragma warning restore CS0659 // "ConversaoUnidade" substitui Object.Equals(object o), mas não substitui Object.GetHashCode()
	{
        public override int ID { get; set; }
        [InformacaoExibicao(1,"Código",70)]
		public virtual string Codigo { get; set; }
        [InformacaoExibicao(2, "Descrição", 300)]
        public virtual string Descricao { get; set; }
        [InformacaoExibicao(3, "Unidade Entrada", 100)]
        public virtual Unidade UnidadeEnt { get; set; }
        [InformacaoExibicao(4, "Unidade Saída", 100)]
        public virtual Unidade UnidadeSai { get; set; }
        [InformacaoExibicao(5, "Fator de Conversão", 120)]
        public virtual decimal FatorConversao { get; set; }
        [InformacaoExibicao(1, "Padrão", 70)]
        public virtual bool Padrao { get; set; }

        public ConversaoUnidade()
        {
            Codigo = "";
            Descricao = "";
            FatorConversao = 0;
        }

        public override bool Equals(object obj)
        {
            try
            {
                ConversaoUnidade objeto = (ConversaoUnidade)obj;
                if (Codigo == objeto.Codigo &&
                    Descricao == objeto.Descricao &&
                    UnidadeEnt.ID == objeto.UnidadeEnt.ID &&
                    UnidadeSai.ID == objeto.UnidadeSai.ID &&
                    FatorConversao == objeto.FatorConversao)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
            catch (Exception)
            {
                return false;
            }
        }

        #region ICloneable Members

        public virtual object Clone()
        {
            return this.MemberwiseClone() as ConversaoUnidade;
        }

        #endregion
    }
}
