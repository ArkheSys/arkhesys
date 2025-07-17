using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo.Auxiliares;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Imposto Serviço")]
    public class ServicoImposto : ModeloBase, IEnumInterpreter
    {
        public override int ID { get; set; }

        [InformacaoExibicao(0, "Código", 100, InformacaoExibicao.HAlinhamento.Direita)]

        public virtual int Codigo { get; set; }

        [InformacaoExibicao(1, "Nome", 550)]

        public virtual string Nome { get; set; }
        public virtual TipoImposto Tipo { get; set; }
        public virtual TipoRetencao TipoRetencao { get; set; }
        public virtual PlanoConta PlanoConta { get; set; }
        [InformacaoExibicao(2, "Alíquota", 100, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Porcentagem4Casas)]
        public virtual decimal Aliquota { get; set; }
        public virtual decimal LimiarPorMes { get; set; }
        public virtual decimal LimiarPorVenda { get; set; }
        public virtual Imposto TipoImposto { get; set; }
        public virtual CodigoImposto CSTImposto { get; set; }

        public virtual int? RegimeEspecialTributacao { get; set; }

        public virtual IList<ServicoServicoImposto> Servicos { get; set; }

        public virtual string GetReadableName(object enumvalue)
        {
            return Enum.GetName(typeof(TipoImposto), enumvalue);
        }

        public override string ToString()
        {
            return Nome;
        }
    }
}
