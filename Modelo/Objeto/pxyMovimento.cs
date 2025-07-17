using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Objeto
{
    public class pxyMovimento
    {
        public int Sequencia { get; set; }
        public DateTime Data { get; set; }
        public Modelo.OperacaoMovimentoType Operacao { get; set; }
        public Modelo.TipoMovimentoType Tipo { get; set; }
        public decimal Valor { get; set; }
        public string PConta { get; set; }
        public int ID { get; set; }
        public int Acao { get; set; }
        public string Banco { get; set; }
        public string Hist { get; set; }
    }
}
