using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyRelCworkOrdemServico
    {
        public pxyRelCworkOrdemServico(int codigo, string revenda, string responsavel, string resumo, string status, DateTime incData, double tempo)
        {
            Codigo = codigo;
            Revenda = revenda;
            Responsavel = responsavel;
            Resumo = resumo;
            Status = status;
            IncData = incData;
            Tempo = tempo;
        }

        


        public int Codigo { get; set; }
        public string Revenda { get; set; }
        public string Responsavel { get; set; }
        public string Resumo { get; set; }
        public string Status { get; set; }
        public DateTime IncData { get; set; }
        public double Tempo { get; set; }
    }
}
