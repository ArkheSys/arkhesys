using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.ModeloProxy
{
    public class pxyProduto
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string DescReduzida { get; set; }
        public string Unidade { get; set; }
        public string Grupo { get; set; }
        public string NomeTrocado { get; set; }
        public int ID { get; set; }
        public bool Marcado { get; set; }
        public string Barra { get; set; }
        public string BarraFornecedor { get; set; }
    }
}
