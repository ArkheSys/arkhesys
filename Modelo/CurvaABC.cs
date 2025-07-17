using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public class CurvaABC
    {
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public decimal ValorConsumo { get; set; }
        public decimal ValorConsumoAcumulado { get; set; }
        public decimal PorcSobreTotalAcumulado { get; set; }
        public int Classificacao { get; set; }
        public decimal Quantidade { get; set; }
    }

    public struct ConsultaABC
    {
        public List<CurvaABC> ClasseA;
        public List<CurvaABC> ClasseB;
        public List<CurvaABC> ClasseC;
    }
}
