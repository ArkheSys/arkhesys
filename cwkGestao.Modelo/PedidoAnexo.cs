namespace cwkGestao.Modelo
{
    public class PedidoAnexo : ModeloBase
    {
        public override int ID { get; set; }

        public virtual string Descricao { get; set; }

        public virtual byte[] Anexo { get; set; }

        public virtual int IDPedido { get; set; }

        public virtual string Caminho { get; set; }

        public virtual bool Novo { get; set; }
        public virtual bool Editado { get; set; }
        public virtual bool Removido { get; set; }

        public PedidoAnexo() { }

        public override string ToString()
        {
            return Descricao;
        }
    }
}
