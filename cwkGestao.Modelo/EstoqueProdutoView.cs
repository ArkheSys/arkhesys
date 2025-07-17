using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class EstoqueProdutoView
    {

        public EstoqueProdutoView(Estoque estoque)
        {
            this.Estoque = estoque;
        }

        public virtual Estoque Estoque { get; set; }

        [InformacaoExibicao(8, "Compra", 75)]
        public virtual decimal Qtd_Compra_Pedido
        {
            get
            {
                return Estoque.Qtd_Compra_Pedido;
            }
        }

        [InformacaoExibicao(5, "Físico", 75)]
        public virtual decimal Qtd_Estoque_Fisico
        {
            get
            {
                return Estoque.Qtd_Estoque_Fisico;
            }
        }

        [InformacaoExibicao(9, "Qtd. Mínima", 75, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Decimal)]
        public virtual decimal Qtd_Estoque_Min
        {
            get
            {
                return Estoque.Qtd_Estoque_Min;
            }
        }

        [InformacaoExibicao(6, "Reserva", 75)]
        public virtual decimal Qtd_Estoque_Pedido
        {
            get
            {
                return Estoque.Qtd_Estoque_Pedido;
            }
        }

        [InformacaoExibicao(7, "Efetivo", 75)]
        public virtual decimal Efetivo
        {
            get
            {
                return Estoque.Efetivo;
            }
        }

        [InformacaoExibicao(10, "Localidade", 100)]
        public virtual string Localidade { get { return Estoque.Localidade; } }

        [InformacaoExibicao(1, "Código", 95)]
        public virtual string CodigoProduto { get { return Estoque.Produto.Codigo; } }

        [InformacaoExibicao(11, "Barra", 100)]
        public virtual string Barra { get { return Estoque.Produto.Barra; } }

        [InformacaoExibicao(2, "Nome", 460)]
        public virtual string NomeProduto { get { return Estoque.Produto.Nome; } }

        public virtual string NomeOrdenado { get { return Estoque.Produto.NomeOrdenado; } }
        [InformacaoExibicao(14, "Desc. Reduzida", 210)]
        public virtual string DescReduzidaProduto { get { return Estoque.Produto.DescReduzida; } }

        [InformacaoExibicao(3, "Und", 45)]
        public virtual Unidade UnidadeProduto { get { return Estoque.Produto.Unidade; } }

        [InformacaoExibicao(13, "Grupo", 170)]
        public virtual GrupoEstoque GrupoEstoqueProduto { get { return Estoque.Produto.GrupoEstoque; } }

        [InformacaoExibicao(12, "Barra Fornecedor", 100)]
        public virtual string BarraFornecedorProduto { get { return Estoque.Produto.BarraFornecedor; } }

        [InformacaoExibicao(15, "NCM", 80)]
        public virtual string NCMProduto { get { return Estoque.Produto.NCM; } }

        [InformacaoExibicao(16, "Grupo1", 100)]
        public virtual Grupo1 Grupo1Produto { get { return Estoque.Produto.Grupo1; } }
        [InformacaoExibicao(17, "Grupo2", 100)]
        public virtual Grupo2 Grupo2Produto { get { return Estoque.Produto.Grupo2; } }
        [InformacaoExibicao(18, "Grupo3", 100)]
        public virtual Grupo3 Grupo3Produto { get { return Estoque.Produto.Grupo3; } }
        [InformacaoExibicao(19, "Situacao", 80)]
        public virtual SituacaoEstoque Situacao
        {
            get
            {
                if (Efetivo < 0)
                    return SituacaoEstoque.AbaixoZero;
                if (Efetivo < Qtd_Estoque_Min)
                    return SituacaoEstoque.AbaixoMinimo;
                return SituacaoEstoque.Normal;
            }
        }

        public enum SituacaoEstoque
        {
            AbaixoZero = 0,
            AbaixoMinimo = 1,
            Normal = 2
        }
    }
}
