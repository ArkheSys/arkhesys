namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Configuração Telefonia")]
    public class Tel_Configuracao : ModeloBase
    {
        public override int ID { get; set; }
        public virtual PlanoConta PlanoConta { get; set; }
        public virtual Portador Portador { get; set; }
        public virtual Historico Historico { get; set; }
        public virtual Condicao Condicao { get; set; }
        public virtual TipoDocumento TipoDocumento { get; set; }
        public virtual Acrescimo Acrescimo { get; set; }
        public virtual Tel_Servico ServicoJuros { get; set; }
        public virtual Tel_Servico ServicoMulta { get; set; }
        public virtual PeriodoListagemFaturasTelefonia PeriodoListagemFaturas { get; set; }
        public virtual bool ConsideraFixo { get; set; }
        public virtual bool SolicitaConfirmaEmailTelefonia { get; set; }
        public virtual Filial Filial { get; set; }

        public virtual string UrlBanco { get; set; }
        public virtual string ClientIDBanco { get; set; }
        public virtual string SecretIDBanco { get; set; }
        public virtual string UrlCertificado { get; set; }
        public virtual string ChavePix { get; set; }
    }
}
