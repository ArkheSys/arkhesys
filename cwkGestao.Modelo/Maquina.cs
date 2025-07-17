namespace cwkGestao.Modelo
{
    public class Maquina : ModeloBase
    {
        public override int ID { get; set; }

        [InformacaoExibicao(1, "Código", 100)]
        public virtual int Codigo { get; set; }

        [InformacaoExibicao(2, "Descrição", 485)]
        public virtual string Descricao { get; set; }

        [InformacaoExibicao(3, "Fabricante", 150, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int? IDFabricante { get; set; }
        public virtual string Fabricante { get; set; }

        public Maquina() { }

        public override string ToString()
        {
            return Descricao;
        }
    }
}
