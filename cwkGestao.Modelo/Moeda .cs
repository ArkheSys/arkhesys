namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Moeda")]
    public class Moeda : ModeloBase
    {
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int? _Codigo;
        [InformacaoExibicao(1, "Código", 100)]
        public virtual int? Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private string _Descricao;
        [InformacaoExibicao(2, "Descrição", 485)]
        public virtual string Descricao { get { return _Descricao; } set { _Descricao = value; } }

        private int? _IDPais;
        [InformacaoExibicao(3, "País", 150, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int? IDPais { get { return _IDPais; } set { _IDPais = value; } }              

        public override string ToString()
        {
            return Descricao;
        }
    }
}
