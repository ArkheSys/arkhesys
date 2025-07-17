using System;

namespace cwkGestao.Modelo.Proxy
{
    public class pxyPedidoFrenteCaixaCarregar
    {
        public int ID { get; set; }
        public int? IDFrenteCaixaNota { get; set; }
        public int Codigo { get; set; }
        public DateTime Dt { get; set; }
        public string PessoaNome { get; set; }
        public int Status { get; set; }
        public string StatusNFCe { get; set; }
        public decimal TotalPedido { get; set; }

        public string TipoPedido { get; set; }


        public pxyPedidoFrenteCaixaCarregar(int ID, int? IDFrenteCaixaNota, int Codigo, DateTime Dt, string PessoaNome, int Status, string StatusNFCe, decimal TotalPedido, string TipoPedido)
        {
            this.ID = ID;
            this.IDFrenteCaixaNota = IDFrenteCaixaNota;
            this.Codigo = Codigo;
            this.Dt = Dt;
            this.PessoaNome = PessoaNome;
            this.Status = Status;
            this.TipoPedido = TipoPedido;
            this.TotalPedido = TotalPedido;
            this.StatusNFCe = StatusNFCe;
        }
    }
}
