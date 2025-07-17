using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyRelProduto
    {
        public pxyRelProduto() { }

        public pxyRelProduto(int _id, string _codigo, string _nome, string _ncm, string _unidade, string _grupo, string _barra)
        {
            ID = _id;
            Codigo = _codigo;
            Nome = _nome;
            NCM = _ncm;
            Unidade = _unidade;
            Grupo = _grupo;
            Barra = _barra;
        }

        public int ID { get; set; }
        public string Codigo { get; set; }
        public string Nome { get; set; }
        public string NCM { get; set; }
        public string Unidade { get; set; }
        public string Grupo { get; set; }
        public string Barra { get; set; }
    }
}
