using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Objeto
{
    public class pxyAtualizaCotacaoLST
    {
        public int ID { get; set; }
        public int Sequencia { get; set; }
        public string Produto { get; set; }
        public int Quantidade { get; set; }
        public int IDProduto { get; set; }
        public bool Selec { get; set; }
        public int IDPedidoItem { get; set; }
        public string DescReduzida { get; set; }
    }
}
