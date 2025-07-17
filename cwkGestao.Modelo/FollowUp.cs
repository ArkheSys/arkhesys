using System;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("FollowUp")]
    public class FollowUp : ModeloBase
    {
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private DateTime _Data;
        [InformacaoExibicao(2, "Data", 485)]
        public virtual DateTime Data { get { return _Data; } set { _Data = value; } }

        private string _Descricao;
        [InformacaoExibicao(4, "Descricao", 150, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual string Descricao { get { return _Descricao; } set { _Descricao = value; } }

        private string _Usuario;
        [InformacaoExibicao(5, "Usuario", 150, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual string Usuario { get { return _Usuario; } set { _Usuario = value; } }

        private int _IDPedido;
        [InformacaoExibicao(5, "IDPedido", 150, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int IDPedido { get { return _IDPedido; } set { _IDPedido = value; } }

        public virtual string DataFormatada { get { return Data.ToString(); } }

        public virtual bool Novo { get; set; }
        public virtual bool Editado { get; set; }
        public virtual bool Removido { get; set; }

        public override string ToString() { return Descricao; }

    }
}
