using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyApontamentoHoras
    {
        public pxyApontamentoHoras() { }

        public pxyApontamentoHoras(int op, DateTime dataInicio, DateTime dataTermino, string servico, string funcionario, decimal tempoHoras)
        {
            OP = op;
            DataInicio = dataInicio;
            DataTermino = dataTermino;
            Servico = servico;
            Funcionario = funcionario;
            TempoHoras = tempoHoras;
        }

        public int OP { get; set; }
        public DateTime DataInicio { get; set; }
        public DateTime DataTermino { get; set; }
        public string Servico { get; set; }
        public string Funcionario { get; set; }
        public decimal TempoHoras { get; set; }        
    }
}
