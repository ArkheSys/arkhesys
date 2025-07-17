namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Forma Pagamento")]
    public class FormaPagamento : ModeloBase
    {
        public override int ID { get; set; }

        [InformacaoExibicao(1, "Código", 90)]
        public virtual int Codigo { get; set; }

        [InformacaoExibicao(2, "Nome", 170)]
        public virtual string Nome { get; set; }

        public virtual FormaPagamentoTipo Tipo { get; set; }

        public virtual string CodigoSisECommerce { get; set; }

        public virtual Pessoa OperadoraCartao { get; set; }

        public virtual string CodigoSefaz { get; set; }

        public override string ToString() { return Nome; }
        public virtual string Atalho { get; set; }
    }
}
