using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Objeto
{
    public class Movimento
    {
        public int ID { get; set; }

        public int Codigo { get; set; }

        public int Sequencia { get; set; }

        public global::Modelo.OrigemMovimentoType Origem { get; set; }

        public int IDDocumento { get; set; }

        public int IDMovCaixa { get; set; }

        public DateTime? Dt { get; set; }

        public global::Modelo.OperacaoMovimentoType Operacao { get; set; }

        public global::Modelo.TipoMovimentoType Tipo { get; set; }

        public int IDPlanoConta { get; set; }

        public int IDCentroCusto { get; set; }

        public int IDHistorico { get; set; }

        public string ComplementoHist { get; set; }

        public decimal? Valor { get; set; }
    }
}
