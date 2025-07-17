using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Objeto
{
    public class pxyOrdemProducaoPedido
    {
        public int ID { get; set; }
        public int Codigo { get; set; }
        public int Pedido { get; set; }
        public string Execucao { get; set; }
        public string Empresa { get; set; }
        public string Cliente { get; set; }
        public string Classificacao { get; set; }
    }

    public class pxyOrdemProducaoPedidoManut
    {
        public int ID { get; set; }
        public int Codigo { get; set; }
        public string Produto { get; set; }
        public string Classificacao { get; set; }
        public decimal Quantidade { get; set; }
    }

    public class pxyOrdemProducaoExecucaoList
    {
        public int Sequencia { get; set; }
        public string Descricao { get; set; }
        public string Servico { get; set; }
        public string Funcionario { get; set; }
        public decimal Quantidade { get; set; }
        public int ID { get; set; }
        public int OrdemProducao { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataTermino { get; set; }
    }
}
