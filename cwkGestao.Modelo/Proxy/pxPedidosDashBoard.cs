namespace cwkGestao.Modelo.Proxy
{
    public class pxPedidosDashBoard
    {
        public int ID { get; set; }
        public int Status { get; set; }
        public int Tipo { get; set; }

        public pxPedidosDashBoard(int ID, int Status, int Tipo)
        {
            this.ID = ID;
            this.Status = Status;
            this.Tipo = Tipo;
        }
    }
}
