using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public class ItensOrcamento
    {
        public ItensOrcamento()
        {
            ItensProduto = new List<ItensOrcamento>();
        }

        public int Sequencia { get; set; }
        public string Produto { get; set; }
        public string Descricao { get; set; }
        public decimal Quantidade { get; set; }
        public string Unidade { get; set; }
        public decimal ValorMin { get; set; }
        public decimal ValorCalc { get; set; }
        public decimal Valor { get; set; }
        public decimal ValorSemDesc { get; set; }
        public decimal Desc { get; set; }
        public decimal Total { get; set; }
        public int Acao { get; set; }
        public decimal ValorRetidoICMS { get; set; }
        public int ID { get; set; }
        public int IDOrcamentoItem { get; set; }
        public bool qtdFracionada { get; set; }
        public string InformacoesAdicionais { get; set; }
        public string AplicacaoCompra { get; set; }
        public decimal ICMSCompra { get; set; }
        public decimal IPICompra { get; set; }

        public List<ItensOrcamento> ItensProduto { get; set; }
    }
}
