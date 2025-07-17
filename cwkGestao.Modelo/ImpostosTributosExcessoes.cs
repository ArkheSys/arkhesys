using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Impostos, Tributos e Excessões")]
    public class ImpostosTributosExcessoes : ModeloBase
    {
        public override int ID { get; set; }

        //public virtual int IDImpostosTributos { get; set; }

        [InformacaoExibicao(1, "Código", 20)]
        public virtual int Codigo { get; set; }
        
        [InformacaoExibicao(2, "Descrição", 100)]
        public virtual string Descricao { get; set; }
        public virtual int CSTCSOSNIcms { get; set; }

        //[InformacaoExibicao(3, "% ICMS", 20)]
        public virtual decimal PIcms { get; set; }
        public virtual decimal PReducaoIcms { get; set; }
        public virtual decimal PIcmsSt { get; set; }
        public virtual decimal PReducaoIcmsSt { get; set; }
        public virtual decimal PMva { get; set; }
        public virtual decimal PDiferimento { get; set; }
        public virtual decimal PDesoneracao { get; set; }

        public virtual string InfAdicionais { get; set; }


        private UF _UF;
        public virtual UF UF { get { return _UF; } set { _UF = value; } }

        private TabelaCFOP _TabelaCFOP;
        public virtual TabelaCFOP TabelaCFOP { get { return _TabelaCFOP; } set { _TabelaCFOP = value; } }

        private ImpostosTributos _ImpostosTributos;
        public virtual ImpostosTributos ImpostosTributos { get { return _ImpostosTributos; } set { _ImpostosTributos = value; } }

        public override string ToString()
        {
            return Descricao;
        }
    }
}
