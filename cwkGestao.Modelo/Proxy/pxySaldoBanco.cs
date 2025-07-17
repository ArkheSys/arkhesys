using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxySaldoBanco
    {
        public pxySaldoBanco() { }

        public pxySaldoBanco(int _id, string _nome, decimal _saldo)
        {
            ID = _id;
            Nome = _nome;
            Saldo = _saldo;
        }

        public int ID { get; set; }
        public string Nome { get; set; }
        public decimal Saldo { get; set; }
    }
}
