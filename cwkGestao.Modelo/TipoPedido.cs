using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Tipo de Orçamento")]
    public class TipoPedido : ModeloBase
    {
        public enum BaseCalculoParcela { Data = 0, PrazoEntrega = 1 }

        public TipoPedido()
        {
            Ent_Sai = 1;
            Tipo = 0;
        }

        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        [InformacaoExibicao(1, "Código", 150)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private string _Ordem;
        [InformacaoExibicao(2, "Ordem", 140)]
        public virtual string Ordem { get { return _Ordem; } set { _Ordem = value; } }

        private string _Nome;
        [InformacaoExibicao(3, "Nome", 270, InformacaoExibicao.FormaOrdenacao.Crescente)]
        public virtual string Nome { get { return _Nome; } set { _Nome = value; } }

        private bool _BReservaEstoque;
        public virtual bool BReservaEstoque { get { return _BReservaEstoque; } set { _BReservaEstoque = value; } }

        private bool _BPermiteEstoqueNegativo;
        public virtual bool BPermiteEstoqueNegativo { get { return _BPermiteEstoqueNegativo; } set { _BPermiteEstoqueNegativo = value; } }

        private int _Ent_Sai;
        public virtual int Ent_Sai { get { return _Ent_Sai; } set { _Ent_Sai = value; } }

        private TipoPedidoType _Tipo;
        public virtual TipoPedidoType Tipo { get { return _Tipo; } set { _Tipo = value; } }

        public virtual string TipoString
        {
            get
            {
                switch (Tipo)
                {
                    case TipoPedidoType.Orçamento:
                        return "Orçamento";
                    case TipoPedidoType.Pedido:
                        return "Pedido";
                    case TipoPedidoType.Requisição:
                        return "Requisição";
                    default:
                        return "";
                }
            }
        }

        [InformacaoExibicao(4, "Tipo", 200)]
        public virtual string TipoExibicao { get { return ((TipoPedidoType)_Tipo).ToString(); } }

        private TipoNota _TipoNota;
        public virtual TipoNota TipoNota { get { return _TipoNota; } set { _TipoNota = value; } }

        private Impressora _Impressora;
        public virtual Impressora Impressora { get { return _Impressora; } set { _Impressora = value; } }

        private Int32 _TipoPreco;
        public virtual Int32 TipoPreco { get { return _TipoPreco; } set { _TipoPreco = value; } }

        private Nullable<int> _bRecalcula;
        public virtual Nullable<int> bRecalcula { get { return _bRecalcula; } set { _bRecalcula = value; } }

        private Nullable<int> _bRecalculaPMudTabPreco;
        public virtual Nullable<int> bRecalculaPMudTabPreco { get { return _bRecalculaPMudTabPreco; } set { _bRecalculaPMudTabPreco = value; } }

        public virtual string Observacao { get; set; }

        public virtual BaseCalculoParcela BaseCalculoParcelas { get; set; }

        public virtual bool CalcularST { get; set; }

        public virtual bool Truncar { get; set; }

        public virtual string NomeRelatorio { get; set; }

        public virtual LocalEstoque LocalEstoque { get; set; }

        public virtual bool BLimiteCredito { get; set; }

        public virtual int Status { get; set; }

        public virtual int StatusPosAprovacao { get; set; }

        public virtual bool bShlDistribuicao { get; set; }

        public virtual int CodShl { get; set; }
        public virtual int Categoria { get; set; }

        public virtual string Referencias{ get; set; }

    }
}
