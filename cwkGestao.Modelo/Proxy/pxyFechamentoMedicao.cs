using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo.Util;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyFechamentoMedicao
    {
        public pxyFechamentoMedicao(int id, int diametro, decimal quantidade, decimal metros, string estaca)
        {
            ID = id;
            DescricaoServico = "Perfuração " + ((TipoDiametro)diametro).TipoDiametroToString();
            Diametro = diametro;
            Estaca = estaca;
            Quantidade = quantidade;
            Metros = metros;
        }

        public int ID { get; set; }
        public int Diametro { get; set; }
        public string Estaca { get; set; }
        public string DescricaoServico { get; set; }
        public decimal Quantidade { get; set; }
        public decimal Metros { get; set; }

    }
}
