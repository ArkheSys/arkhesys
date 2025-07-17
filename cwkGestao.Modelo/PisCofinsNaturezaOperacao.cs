using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Pis/Cofins - Natureza de Operação")]
    public class PisCofinsNaturezaOperacao : ModeloBase
    {
        public PisCofinsNaturezaOperacao()
        {
            NaturezaOperacao = new List<NaturezaOperacao>();
        }

        public override int ID { get; set; }

        [InformacaoExibicao(1, "Código", 50)]
        public virtual int Codigo { get; set; }

        [InformacaoExibicao(2, "Natureza", 200)]
        public virtual string Descricao { get; set; }

        public virtual IList<NaturezaOperacao> NaturezaOperacao { get; set; }
        public virtual TabelaPIS TabelaPIS { get; set; }
        public virtual TabelaCOFINS TabelaCOFINS { get; set; }

        public override string ToString()
        {
            return Descricao;
        }
    }
}
