using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Objeto
{
    public class pxyEtapaProducao
    {
        public int ID { get; set; }
        public int IDProduto { get; set; }
        public int IDServico { get; set; }
        public string Titulo { get; set; }
        public string Observacao { get; set; }
        public short Sequencia { get; set; }

        public int Acao { get; set; }
    }
}
