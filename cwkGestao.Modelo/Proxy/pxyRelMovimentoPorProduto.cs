using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyRelMovimentoPorProduto
    {
        public pxyRelMovimentoPorProduto() { }

        public pxyRelMovimentoPorProduto(bool _Titulo, string _produto, string _grupo, string _classificacao, DateTime _data, 
            int? _nf, int _tipomvt, int _codigonf, decimal _entradas, decimal _saidas, decimal _SaldoAnt, string _unidade)
        {
            Titulo = _Titulo;
            produto = _produto;
            grupo = _grupo;
            classificacao = _classificacao;
            data = _data;
            nf = _nf;
            tipomvt = _tipomvt;
            codigonf = _codigonf;
            entradas = _entradas;
            saidas = _saidas;
            SaldoAnt = _SaldoAnt;
            unidade = _unidade;
        }

        public string produto { get; set; }
        public string grupo { get; set; }
        public string classificacao { get; set; }
        public int? nf { get; set; }
        public int tipomvt { get; set; }
        public int codigonf { get; set; }
        public DateTime data { get; set; }
        public decimal entradas { get; set; }
        public decimal saidas { get; set; }
        public decimal SaldoAnt { get; set; }
        public string unidade { get; set; }
        public bool Titulo { get; set; }
    }
}
