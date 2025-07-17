namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Texto Lei")]
    public class TextoLei : ModeloBase
    {
        public override int ID { get; set; }
        [InformacaoExibicao(0, "Codigo", 75)]

        public virtual int Codigo { get; set; }

        [InformacaoExibicao(1, "Descrição", 150)]
        public virtual string Descricao { get; set; }

        [InformacaoExibicao(2, "Texto Lei", 250)]
        public virtual string DescricaoTextoLei { get; set; }

    }
}
