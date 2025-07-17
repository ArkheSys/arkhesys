namespace cwkGestao.Modelo
{
    public class Fabricante : ModeloBase
    {
        public override int ID { get; set; }

        [InformacaoExibicao(1, "Código", 100)]
        public virtual int Codigo { get; set; }

        [InformacaoExibicao(2, "Descrição", 485)]
        public virtual string Descricao { get; set; }

        public Fabricante() { }

        public override string ToString()
        {
            return Descricao;
        }
    }
}
