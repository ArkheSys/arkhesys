using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("COFINS")]
    public class TabelaCOFINS : ModeloBase
    {
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        [InformacaoExibicao(1, "Código", 100)]
        public virtual int Codigo { get; set; }

        private string _COFINS;
        [InformacaoExibicao(3, "COFINS", 100)]
        public virtual string COFINS { get { return _COFINS; } set { _COFINS = value; } }

        private string _Descricao;
        [InformacaoExibicao(2, "Descrição", 500, InformacaoExibicao.FormaOrdenacao.Crescente)]
        public virtual string Descricao { get { return _Descricao; } set { _Descricao = value; } }

        public override string ToString()
        {
            return Descricao;
        }
    }
}
