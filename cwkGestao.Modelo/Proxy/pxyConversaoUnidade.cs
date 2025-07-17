using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyConversaoUnidade
    {
        public pxyConversaoUnidade(int id, string codigo, string descricao, int idUnEntrada, string siglaUnEntrada, int idUnEstoque, string siglaUnEstoque, decimal fatorConversao)
        {
            Id = id;
            Codigo = codigo;
            Descricao = descricao;
            IdUnEntrada = idUnEntrada;
            SiglaUnEntrada = siglaUnEntrada;
            IdUnEstoque = idUnEstoque;
            SiglaUnEstoque = siglaUnEstoque;
            FatorConversao = fatorConversao;
        }

        public int? Id { get; set; }
        public string Codigo { get; set; }
        public string Descricao { get; set; }
        public int IdUnEntrada { get; set; }
        public string SiglaUnEntrada { get; set; }
        public int IdUnEstoque { get; set; }
        public string SiglaUnEstoque { get; set; }
        public decimal FatorConversao { get; set; }
    }
}
