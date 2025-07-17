namespace cwkGestao.Modelo
{
    public class OSServicoItem : ModeloBase
    {
        public override int ID { get; set; }
        public virtual OSOrdemServico OSOrdemServico { get; set; }
        public virtual int Seq { get; set; }
        public virtual Servico Servico { get; set; }
        public virtual string Descricao { get; set; }
        public virtual decimal Quantidade { get; set; }
        public virtual TipoServico Tipo { get; set; }
        public virtual Pessoa Responsavel { get; set; }
        public virtual decimal PrecoOriginal { get; set; }
        public virtual decimal Preco { get; set; }
        public virtual decimal DescontoPerc { get; set; }
        public virtual decimal Total { get; set; }

        public virtual decimal PrecoTerceiro { get; set; }

        public virtual string GuidIdentificacao { get; set; }
    }

    public enum TipoServico
    {
        Propria = 0, Terceira = 1
    }
}

