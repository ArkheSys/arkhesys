using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.ModeloProxy
{
    public class pxyDocumentoLista
    {
        public int Codigo { get; set; }
        public int Filial { get; set; }
        public DateTime? Vencimento { get; set; }
        public DateTime? Previsao { get; set; }
        public string Doc { get; set; }
        public string Requisicao { get; set; }
        public string Cliente { get; set; }
        public decimal Valor { get; set; }
        public decimal Saldo { get; set; }
        public string Situacao { get; set; }
        public DateTime? DtUltimaBaixa { get; set; }
        public string NomeFantasia { get; set; }
        public int ID { get; set; }
        public string TipoDocumento { get; set; }
        public DateTime? Emissao { get; set; }
        public string Portador { get; set; }
        public string Parcela { get; set; }
    }
}
