using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Objeto
{
    public class FinanceiroPlanoContas
    {
        public int ID { get; set; }

        public int Conta { get; set; }

        public string Descricao { get; set; }

        public decimal Valor { get; set; }

        public List<Modelo.Rateio_Mov> RateioProjeto { get; set; }
    }
}
