namespace cwkGestao.Modelo
{
    public class ProdutoAnexo : ModeloBase
    {
        public override int ID { get; set; }

        public virtual string Descricao { get; set; }

        public virtual byte[] Anexo { get; set; }

        public virtual int IDProduto { get; set; }

        public virtual string Caminho { get; set; }

        public virtual bool Novo { get; set; }
        public virtual bool Editado { get; set; }
        public virtual bool Removido { get; set; }

        public ProdutoAnexo() { }

        public override string ToString()
        {
            return Descricao;
        }
    }
}
