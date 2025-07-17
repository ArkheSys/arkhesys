using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyRelTempoEstoque
    {
        public pxyRelTempoEstoque() { }

        public pxyRelTempoEstoque(string _CodProduto, 
            string _grupo, string _classificacao, 
            DateTime _DataEntrada, int _Quantidade, int _Ano, 
            int _Sequencia, 
            int _TempoEstoque)
        {
            CodProduto = _CodProduto;
            grupo = _grupo;
            classificacao = _classificacao;
            DataEntrada = _DataEntrada;
            Quantidade = _Quantidade;
            Ano = _Ano;
            Sequencia = _Sequencia;
            TempoEstoque = _TempoEstoque;
            
        }

        public string CodProduto { get; set; }
        public string grupo { get; set; }
        public string classificacao { get; set; }
        public int Quantidade { get; set; }
        public int Ano { get; set; }
        public int Sequencia { get; set; }
        public DateTime DataEntrada { get; set; }
        public int TempoEstoque { get; set; }
    }
}
