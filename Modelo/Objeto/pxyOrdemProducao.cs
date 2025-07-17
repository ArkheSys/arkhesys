using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Objeto
{
    public class pxyOrdemProducao
    {
        public int ID { get; set; }
        public int Codigo { get; set; }
        public string Empresa { get; set; }
        public string Cliente { get; set; }
        public string Projeto { get; set; }
        public string Classificacao { get; set; }
        public string Descricao { get; set; }
    }
}
