using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyRelClientePorVendedor
    {
        public pxyRelClientePorVendedor() { }

        public pxyRelClientePorVendedor(string vendedor, string cliente, string endereco, string telefone, string celular)
        {
            Vendedor = vendedor;
            Cliente = cliente;
            
            Endereco = endereco;
            Telefone = telefone;
            Celular = celular;
        }

        public pxyRelClientePorVendedor(string vendedor, string cliente, string endereco)
        {
            Vendedor = vendedor;
            Cliente = cliente;

            Endereco = endereco;
            Telefone = "";
            Celular = "";
             
        }

        public string Vendedor { get; set; }
        public string Cliente { get; set; }
        public string Endereco { get; set; }
        public string Telefone { get; set; }
        public string Celular { get; set; }
       
    }
}
