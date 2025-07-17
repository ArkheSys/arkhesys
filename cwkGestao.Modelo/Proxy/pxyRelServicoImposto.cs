using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyRelServicoImposto
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Aliquota { get; set; }
        public string PlanoContaNome { get; set; }

        public pxyRelServicoImposto(int codigo, string nome, decimal aliquota, string planoContaNome)
        {
            Codigo = codigo;
            Nome = nome;
            Aliquota = aliquota;
            PlanoContaNome = planoContaNome;
        }
    }
}
