using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
#pragma warning disable CS0659 // "ServicoServicoImposto" substitui Object.Equals(object o), mas não substitui Object.GetHashCode()
    public class ServicoServicoImposto : ModeloBase
#pragma warning restore CS0659 // "ServicoServicoImposto" substitui Object.Equals(object o), mas não substitui Object.GetHashCode()
    {
        public override int ID { get; set; }
        public virtual Servico Servico { get; set; }

        [InformacaoExibicao(1, "Imposto", 300)]
        public virtual ServicoImposto ServicoImposto { get; set; }
        [InformacaoExibicao(0, "Retém",60)]
        public virtual bool BaseRetencaoImposto { get; set; }

        [InformacaoExibicao(2, "Alíquota", 98, cwkGestao.Modelo.InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Porcentagem4Casas)]
        public virtual decimal Aliquota { get { return ServicoImposto.Aliquota; } }

        public override bool Equals(object obj)
        {
            if (!(obj is ServicoServicoImposto))
                return false;

            ServicoServicoImposto servicoServicoImposto = (ServicoServicoImposto) obj;
            if (servicoServicoImposto.ServicoImposto.ID == this.ServicoImposto.ID 
                && servicoServicoImposto.Servico.ID == this.Servico.ID)
                return true;
            else
                return false;

        }

    }
}
