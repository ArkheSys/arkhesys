using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Tabela de Impostos Tributos Excessões")]
    public class TabelaImpostosTributosExcessoes : ModeloBase
    {

        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _IDImpostosTributos;
        public virtual int IDImpostosTributos { get { return _IDImpostosTributos; } set { _IDImpostosTributos = value; } }

        private int _Codigo;
        [InformacaoExibicao(1, "Código", 100)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private ImpostosTributos _ImpostosTributos;
        public virtual ImpostosTributos ImpostosTributos { get { return _ImpostosTributos; } set { _ImpostosTributos = value; } }
        public override string ToString()
        {
            return "ImpostosTributosExcessoes";
        }
    }
}
