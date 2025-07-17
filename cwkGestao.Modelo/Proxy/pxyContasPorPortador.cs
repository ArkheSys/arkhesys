using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyContasPorPortador
    {
        public pxyContasPorPortador(int codigo, int filial, string doc, string parcqt,  int banco,  string cliente, decimal valor, DateTime? vencimento, DateTime? previsao, DateTime? dt,
            string situacao, decimal saldo, string portador)
        {            
            Codigo = codigo;
            Portador = portador;
            Situacao = situacao;
            Doc = doc;
            Cliente = cliente;
            Vencimento = vencimento;
            Previsao = previsao;
            Valor = valor;
            Banco = banco;
            ParcQt = parcqt;
            Filial = filial;
            Saldo = saldo;
            Dt = dt;
        }
        
        public int Codigo { get; set; }
        public string Portador { get; set; }
        public string Situacao { get; set; }
        public string Doc { get; set; }
        public string Cliente { get; set; }
        public DateTime? Vencimento { get; set; }
        public DateTime? Previsao { get; set; }
        public decimal Valor { get; set; }
        public int Banco { get; set; }
        public string ParcQt { get; set; }      
        public int Filial { get; set; }
        public decimal Saldo { get; set; }
        public DateTime? Dt { get; set; }
    }
}
