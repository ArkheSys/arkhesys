using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class ImpressaoEtiquetaProdutoView
    {

        public ImpressaoEtiquetaProdutoView(Produto produto)
        {
            this.Produto = produto;
        }

        public ImpressaoEtiquetaProdutoView(int id, Decimal qtd_Compra_Pedido, Decimal qtd_Estoque_Fisico, Decimal qtd_Estoque_Min,
            Decimal qtd_Estoque_Pedido, Decimal efetivo, String codigo, String barra, String nome, String descReduzida,
            String unidade, String GE, String barraForn, String ncm, String grupo1, String grupo2, String grupo3, Decimal precoBase, int _QuantidadeImprimir)
        {
            ID = id;
            Qtd_Compra_Pedido = qtd_Compra_Pedido;
            Qtd_Estoque_Fisico = qtd_Estoque_Fisico;
            Qtd_Estoque_Min = qtd_Estoque_Min;
            Qtd_Estoque_Pedido = qtd_Estoque_Pedido;
            Efetivo = efetivo;
            CodigoProduto = codigo;
            Barra = barra;
            NomeProduto = nome;
            DescReduzidaProduto = descReduzida;
            UnidadeProduto = new Unidade() { Nome = unidade };
            GrupoEstoqueProduto = new GrupoEstoque() { Nome = GE};
            BarraFornecedorProduto = barraForn;
            NCMProduto = ncm;
            Grupo1Produto = new Grupo1 { Nome = grupo1 };
            Grupo2Produto = new Grupo2 { Nome = grupo2 };
            Grupo3Produto = new Grupo3 { Nome = grupo3 };
            PrecoBase = precoBase;
            QuantidadeImprimir = _QuantidadeImprimir;
        }

        public virtual int ID { get; set; }

        [InformacaoExibicao(1, "Imprimir", 75)]
        public virtual bool Imprimir { get; set; }

        [InformacaoExibicao(2, "Qtd.", 75)]
        public virtual decimal QuantidadeImprimir { get; set; }

        public virtual Produto Produto { get; set; }

        [InformacaoExibicao(10, "Compra", 75)]
        public virtual decimal Qtd_Compra_Pedido{ get; set; }

        [InformacaoExibicao(7, "Físico", 75)]
        public virtual decimal Qtd_Estoque_Fisico{ get; set; }

        [InformacaoExibicao(11, "Qtd. Mínima", 75, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Decimal)]
        public virtual decimal Qtd_Estoque_Min{ get; set; }

        [InformacaoExibicao(8, "Reserva", 75)]
        public virtual decimal Qtd_Estoque_Pedido { get; set; }

        [InformacaoExibicao(9, "Efetivo", 75)]
        public virtual decimal Efetivo{get; set; }

        [InformacaoExibicao(3, "Código", 95)]
        public virtual string CodigoProduto { get; set; }

        [InformacaoExibicao(13, "Barra", 100)]
        public virtual string Barra { get; set; }

        [InformacaoExibicao(4, "Nome", 460)]
        public virtual string NomeProduto { get; set; }

        [InformacaoExibicao(16, "Desc. Reduzida", 210)]
        public virtual string DescReduzidaProduto { get; set; }

        [InformacaoExibicao(5, "Und", 45)]
        public virtual Unidade UnidadeProduto { get; set; }

        [InformacaoExibicao(15, "Grupo", 170)]
        public virtual GrupoEstoque GrupoEstoqueProduto { get; set; }

        [InformacaoExibicao(14, "Barra Fornecedor", 100)]
        public virtual string BarraFornecedorProduto { get; set; }

        [InformacaoExibicao(17, "NCM", 80)]
        public virtual string NCMProduto { get; set; }

        [InformacaoExibicao(18, "Grupo1", 100)]
        public virtual Grupo1 Grupo1Produto { get; set; }

        [InformacaoExibicao(19, "Grupo2", 100)]
        public virtual Grupo2 Grupo2Produto { get; set; }

        [InformacaoExibicao(20, "Grupo3", 100)]
        public virtual Grupo3 Grupo3Produto { get; set; }

        public virtual Decimal PrecoBase { get; set; }
    }
}
