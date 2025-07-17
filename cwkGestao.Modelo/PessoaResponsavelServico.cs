namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Responsável Serviço")]
    public class PessoaResponsavelServico : ModeloBase
    {
        public override int ID { get; set; }

        public virtual int? IDResponsavel { get; set; }
        public virtual int? IDPessoa { get; set; }
        public virtual int? IDServico { get; set; }

        [InformacaoExibicao(1, "Responsavel", 485)]
        public virtual string Responsavel { get; set; }
        
        [InformacaoExibicao(2, "Serviço", 200)]
        public virtual string Servico { get; set; }

        public virtual bool Novo { get; set; }
        public virtual bool Editado { get; set; }
        public virtual bool Removido { get; set; }

        public PessoaResponsavelServico()
        {

        }
    }
}
