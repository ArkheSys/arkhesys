using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Integracao.Telefonia
{
    public class Ligacao
    {
        public int TipoUso { get; set; }
        public string DescricaoUso { get; set; }
        public string Origem { get; set; }
        public string Destino { get; set; }
        public string Cidade { get; set; }
        public string Uf { get; set; }
        public DateTime DataHora { get; set; }
        public int TempoSegundos { get; set; }
        public string Horario { get; set; }
        public string ValorComImposto{ get; set; }
    }
}
