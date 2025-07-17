namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Responsável")]
    public class Responsavel : ModeloBase
    {
        public override int ID { get; set; }

        [InformacaoExibicao(1, "Código", 150)]
        public virtual int? Codigo { get; set; }

        [InformacaoExibicao(1, "Descrição", 285)]
        public virtual string Descricao { get; set; }

        public Responsavel() { }

        public override string ToString()
        {
            return Descricao;
        }
    }
}
