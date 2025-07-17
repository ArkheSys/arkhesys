using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyAcompanhamentoOS
    {
        public pxyAcompanhamentoOS() { }

        public pxyAcompanhamentoOS(int _id, int? _codigo, string _responsavelAnt, string _responsavelAtu, string _statusAnt, string _statusAtu, DateTime? _data)
        {
            ID = _id;
            Codigo = _codigo.HasValue == true? _codigo.Value : 0;
            ResponsavelAnt = _responsavelAnt;
            ResponsavelAtu = _responsavelAtu;
            StatusAnt = _statusAnt;
            StatusAtu = _statusAtu;
            Data = _data;
        }

        public int ID { get; set; }
        public int Codigo { get; set; }
        public string ResponsavelAnt { get; set; }
        public string ResponsavelAtu { get; set; }
        public string StatusAnt { get; set; }
        public string StatusAtu { get; set; }
        public DateTime? Data { get; set; }
    }
}
