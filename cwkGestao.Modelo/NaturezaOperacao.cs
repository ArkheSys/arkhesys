using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Natureza da Operação")]
    public class NaturezaOperacao : ModeloBase
    {
        public override int ID { get; set; }

        public virtual int Codigo { get; set; }

        [InformacaoExibicao(1, "Cód. Natureza", 100, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual string CodNatureza { get; set; }

        [InformacaoExibicao(2, "Natureza", 500)]
        public virtual string Descricao { get; set; }

        public virtual int Situacao { get; set; }

        private PisCofinsNaturezaOperacao _PisCofinsNaturezaOperacao;
        public virtual PisCofinsNaturezaOperacao PisCofinsNaturezaOperacao { get { return _PisCofinsNaturezaOperacao; } set { _PisCofinsNaturezaOperacao = value; } }

        public override string ToString()
        {
            return Descricao;
        }
    }
}
