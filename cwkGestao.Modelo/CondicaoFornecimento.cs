namespace cwkGestao.Modelo
{
    [InformacaoExibicao("CondicaoFornecimento")]
    public class CondicaoFornecimento : ModeloBase
    {
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        [InformacaoExibicao(1, "Código", 100)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private string _Descricao;
        [InformacaoExibicao(3, "Descrição", 285)]
        public virtual string Descricao { get { return _Descricao; } set { _Descricao = value; } }

        private int _Habilita;
        [InformacaoExibicao(3, "Habilita", 150, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int Habilita { get { return _Habilita; } set { _Habilita = value; } }

        private string _Titulo;
        [InformacaoExibicao(2, "Titulo", 200)]
        public virtual string Titulo { get { return _Titulo; } set { _Titulo = value; } }

        public override string ToString()
        {
            return Descricao;
        }
    }
}
