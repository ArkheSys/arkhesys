using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyTipoRelRecebimentos
    {
        public pxyTipoRelRecebimentos(int tipo, string descricaoTipo) 
        {
            Tipo = tipo;
            DescricaoTipo = descricaoTipo;
        }

        public int Tipo { get; set; }
        public string DescricaoTipo { get; set; }
    }
}
