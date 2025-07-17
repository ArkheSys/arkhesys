using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Objeto
{
    public class pxyItemOrcamento
    {
        public bool Marcado { get; set; }
        public int Sequencia { get; set; }
        public string Produto { get; set; }
        public string Descricao { get; set; }
        public decimal Quantidade { get; set; }
        public decimal QuantidadeAnt { get; set; }
        public string Unidade { get; set; }
        public decimal ValorMin { get; set; }
        public decimal ValorCalc { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorSemDesc { get; set; }
        public decimal Desc { get; set; }
        public decimal Total { get; set; }
        public decimal ValorRetidoICMS { get; set; }
        public int ID { get; set; }
        public bool qtdFracionada { get; set; }
        public Modelo.PedidoItem Item { get; set; }
        public string InformacoesAdicionais { get; set; }
    }
}
