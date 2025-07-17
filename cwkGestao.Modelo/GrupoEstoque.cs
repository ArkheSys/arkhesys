using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Grupo Estoque")]
    public class GrupoEstoque : ModeloBase
    {
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        [InformacaoExibicao(1, "Código", 100)]
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private string _Nome;
        [InformacaoExibicao(2, "Nome", 397)]
        public virtual string Nome { get { return _Nome; } set { _Nome = value; } }

        private string _NomeReduzido;
        public virtual string NomeReduzido { get { return _NomeReduzido; } set { _NomeReduzido = value; } }

        private string _Classificacao;
        [InformacaoExibicao(3, "Classificação", 150)]
        public virtual string Classificacao { get { return _Classificacao; } set { _Classificacao = value; } }

        private GrupoEstoque _GrupoEstoquePai;
        public virtual GrupoEstoque GrupoEstoquePai { get { return _GrupoEstoquePai; } set { _GrupoEstoquePai = value; } }

        private bool _BTitulo;
        [InformacaoExibicao(4, "Título", 80)]
        public virtual bool BTitulo { get { return _BTitulo; } set { _BTitulo = value; } }

        private ClassificacaoProduto _ClassificacaoProduto;
        public virtual ClassificacaoProduto ClassificacaoProduto { get { return _ClassificacaoProduto; } set { _ClassificacaoProduto = value; } }

        private IList<Produto> _Produtos;
        public virtual IList<Produto> Produtos { get { return _Produtos; } set { _Produtos = value; } }

        public virtual int IDIntegracao { get; set; }

        public virtual bool bShlGrupoEstoqueCampanha { get; set; }

        public virtual int CodShl { get; set; }

        public virtual string CodClassProduto { get; set; }


        public override string ToString()
        {
            return Nome;
        }
    }
}
