using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Objeto
{
    public class pxyItemFichaTecnica
    {
        public int ID { get; set; }
        public int IDProduto { get; set; }
        public short Quantidade { get; set; }

        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int Acao { get; set; }
        public int Sequencia { get; set; }
    }
}
