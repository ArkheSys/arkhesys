using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Perfil Pis & Cofins")]
    public class PerfilPisCofins : ModeloBase
    {
        public override int ID { get; set; }

        [InformacaoExibicao(1, "Código", 90)]
        public virtual int Codigo { get; set; }

        [InformacaoExibicao(2, "Descrição", 100)]
        public virtual string Descricao { get; set; }

        public virtual Boolean BComulativo { get; set; }
        public virtual Boolean BNaoComulativo { get; set; }
        public virtual Boolean BSubstituicaoTributaria { get; set; }
        public virtual Boolean BSemInsidencia { get; set; }
        public virtual decimal AliquotaPIS { get; set; }
        public virtual decimal AliquotaCOFINS { get; set; }

        public virtual int EntVinculoCredito { get; set; }
        public virtual int EntBaseCredito { get; set; }
        
        public virtual decimal EntAliquotaPISDif { get; set; }
        public virtual decimal EntAliquotaCOFINSDif { get; set; }

        public virtual decimal EntFatorConversao { get; set; }
        public virtual decimal EntValorPis { get; set; }
        public virtual decimal EntValorCofins { get; set; }

        public virtual decimal SaiAliquotaPISDif { get; set; }
        public virtual decimal SaiAliquotaCOFINSDif { get; set; }

        public virtual decimal SaiFatorConversao { get; set; }
        public virtual decimal SaiValorPis { get; set; }
        public virtual decimal SaiValorCofins { get; set; }

        private int _IDUnidadeEntrada;
        public virtual int IDUnidadeEntrada { get { return _IDUnidadeEntrada; } set { _IDUnidadeEntrada = value; } }

        private int _IDUnidadeSaida;
        public virtual int IDUnidadeSaida { get { return _IDUnidadeSaida; } set { _IDUnidadeSaida = value; } }

        private Unidade _UnidadeEntrada;
        public virtual Unidade UnidadeEntrada { get { return _UnidadeEntrada; } set { _UnidadeEntrada = value; } }

        private Unidade _UnidadeSaida;
        public virtual Unidade UnidadeSaida { get { return _UnidadeSaida; } set { _UnidadeSaida = value; } }

        private PisCofinsNaturezaOperacao _EntPisCofinsNaturezaOperacao;
        public virtual PisCofinsNaturezaOperacao EntPisCofinsNaturezaOperacao { get { return _EntPisCofinsNaturezaOperacao; } set { _EntPisCofinsNaturezaOperacao = value; } }
        
        private PisCofinsNaturezaOperacao _SaiPisCofinsNaturezaOperacao;
        public virtual PisCofinsNaturezaOperacao SaiPisCofinsNaturezaOperacao { get { return _SaiPisCofinsNaturezaOperacao; } set { _SaiPisCofinsNaturezaOperacao = value; } }

        public override string ToString()
        {
            return Descricao;
        }

    }
}
