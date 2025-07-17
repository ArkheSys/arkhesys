using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyEntregas
    {
        public pxyEntregas(int idPedido, int seq, int idEntrega, DateTime dt, string numEntrega, int pedCodigo, decimal quantidadeEntregue, decimal quantidadeEntregar, 
                           decimal? quantidadeRestante, string nomeSolic, string nomeResp)
        {
            this.IDPedido = idPedido;
            this.Sequencia = seq;
            this.IDEntrega = idEntrega;
            this.Data = dt;
            this.NumeroEntrega = numEntrega;
            this.CodigoPedido = pedCodigo;
            this.QuantidadeEntregue = quantidadeEntregue;
            this.QuantidadeEntregar = quantidadeEntregar;
            this.QuantidadeRestante = quantidadeRestante;
            this.Solicitante = nomeSolic;
            this.Responsavel = nomeResp;
        }

        public int IDPedido { get; set; }
        public int Sequencia { get; set; } 
        public int IDEntrega { get; set; }
        public DateTime Data { get; set; }
        public string NumeroEntrega { get; set; }
        public int CodigoPedido { get; set; }
        public decimal QuantidadeEntregue { get; set; }
        public decimal QuantidadeEntregar { get; set; }
        public decimal? QuantidadeRestante { get; set; }
        public string Solicitante { get; set; }
        public string Responsavel { get; set; }
    }
}
