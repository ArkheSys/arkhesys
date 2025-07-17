using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo.Util;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyFaturamentoPerfuracao
    {
        public pxyFaturamentoPerfuracao(int id, int diametro, decimal quantidade, decimal metros)
        {
            ID = id;
            DescricaoServico = "Perfuração " + ((TipoDiametro)diametro).TipoDiametroToString();
            Quantidade = quantidade;
            Metros = metros;
        }

        public int ID { get; set; }
        public string DescricaoServico { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Metros { get; set; }
        public decimal Valor { get; set; }
        public decimal SubTotal { get { return Quantidade * Valor; } }
    }
}
