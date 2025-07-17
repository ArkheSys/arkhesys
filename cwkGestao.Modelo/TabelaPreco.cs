using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Preço")]
    public class TabelaPreco : ModeloBase
    {
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        [InformacaoExibicao(1, "Codigo", 100)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private string _Nome;
        [InformacaoExibicao(2, "Nome", 275)]
        public virtual string Nome { get { return _Nome; } set { _Nome = value; } }

        public virtual decimal SugestaoMargemLucro { get; set; }

        public virtual decimal SugestaoPDesconto { get; set; }

        public virtual decimal SugestaoPAcrescimo { get; set; }

        //private byte? _BAtiva;
        //public virtual byte? BAtiva { get { return _BAtiva; } set { _BAtiva = value; } }
        private bool _BAtiva;
        [InformacaoExibicao(3, "Ativa", 260)]
        public virtual bool BAtiva { get { return _BAtiva; } set { _BAtiva = value; } }

        private IList<TabelaPrecoProduto> _TabelaPrecoProdutos;
        public virtual IList<TabelaPrecoProduto> TabelaPrecoProdutos { get { return _TabelaPrecoProdutos; } set { _TabelaPrecoProdutos = value; } }

        public virtual bool Promocional { get; set; }
    }
}