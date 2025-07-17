using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Objeto
{
    public class pxyOrdemProducaoExecucao
    {
        public int Acao { get; set; }
        
        public int ID { get; set; }

        public int Sequencia { get; set; }

        public string Descricao { get; set; }

        public System.Nullable<decimal> Quantidade { get; set; }

        public int IdServico { get; set; }

        public System.Nullable<int> IdFuncionario { get; set; }

        public System.Nullable<System.DateTime> DataInicio { get; set; }

        public System.Nullable<System.DateTime> DataTermino { get; set; }
    }
}
