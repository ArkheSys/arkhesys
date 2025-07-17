using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Classificação Fiscal")]
    public class ClassificacaoFiscal : ModeloBase
    {
        public override int ID { get; set; }

        [InformacaoExibicao(1, "Código", 100)]
        public virtual int Codigo { get; set; }
        
        [InformacaoExibicao(2, "Descrição", 100)]
        public virtual string Descricao { get; set; }

        //public virtual bool bSTRevenda { get; set; }

        public virtual int ModBCST { get; set; }


        private ImpostosTributos _ImpostosTributos;
        public virtual ImpostosTributos ImpostosTributos { get { return _ImpostosTributos; } set { _ImpostosTributos = value; } }

        public override string ToString()
        {
            return Descricao;
        }
    }
}
