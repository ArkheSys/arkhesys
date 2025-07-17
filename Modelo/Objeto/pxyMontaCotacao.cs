using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Objeto
{
     public class pxyMontaCotacao
    {
        public int ID { get; set; }
        public int Codigo { get; set; }
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public int IDProduto { get; set; }
        public bool Selec { get; set; }
        public int IDCotacao { get; set; }
        public int IDcot { get; set; }
    }
}
