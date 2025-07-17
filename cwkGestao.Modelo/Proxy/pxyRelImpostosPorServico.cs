using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyRelImpostosPorServico
    {
        public int Codigo { get; set; }
        public string Nome { get; set; }
        public decimal Valor { get; set; }
        public string ImpostoNome { get; set; }
        public decimal ImpostoAliquota { get; set; }

        public pxyRelImpostosPorServico(int codigo, string nome, decimal valor, string impostoNome, decimal impostoAliquota)
        {
            Codigo = codigo;
            Nome = nome;
            Valor = valor;
            ImpostoNome = impostoNome;
            ImpostoAliquota = impostoAliquota;
        }
    }
}
