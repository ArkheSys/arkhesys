using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Auxiliares
{
    public class MovCaixaBaixa
    {
        public MovCaixaBaixa() 
        {
            chequeHistorico = new List<ChequeHistorico>();
            cheque = new List<Cheque>();
        }

        public MovCaixa movCaixa { get; set; }
        public List<Cheque> cheque { get; set; }
        public List<ChequeHistorico> chequeHistorico { get; set; }
    }
}
