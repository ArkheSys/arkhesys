using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Opção de Tributação")]
    public class OpcaoTributacao : ModeloBase
    {
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        [InformacaoExibicao(1, "Código", 90)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private string _Nome;

        [InformacaoExibicao(2, "Nome Tributação", 170)]
        public virtual string Nome { get { return _Nome; } set { _Nome = value; } }

    }
}
