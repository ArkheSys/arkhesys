using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyEstoque
    {

        public pxyEstoque()
        {

        }
        public pxyEstoque(string localestoque, decimal? estoquefisico, decimal? qtdpedido, decimal? estoquemin, int? idestoque)
        {
            IDEstoque = idestoque;
            LocalEstoque = localestoque;
            EstoqueFisico = estoquefisico;
            QtdPedido = qtdpedido;
            EstoqueMin = estoquemin;
        }

        public int? IDEstoque { get; set; }
        public string LocalEstoque { get; set; }
        public decimal? EstoqueFisico { get; set; }
        public decimal? QtdPedido { get; set; }
        public decimal? Saldo 
        {
            get { return EstoqueFisico - QtdPedido; }
        }
        public decimal? EstoqueMin { get; set; }
        public Produto Produto { get; set; }
    }
}
