using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Produto Cotação")]
    public class CotacaoProduto : ModeloBase
    {
        public override int ID { get; set; }
        [InformacaoExibicao(0, "Sequencia", 70)]
        public virtual int Codigo { get; set; }
        public virtual Cotacao Cotacao { get; set; }
        public virtual Produto Produto { get; set; }
        [InformacaoExibicao(1, "Produto", 90, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual string ProdutoCodigo { get { try { return Produto.Codigo; } catch{return "";} } }
        [InformacaoExibicao(2, "Descrição", 180)]
        public virtual string DescricaoProduto { get; set; }
        [InformacaoExibicao(3, "Quantidade", 70, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Decimal)]
        public virtual decimal Quantidade { get; set; }
        public virtual decimal Preco_01 { get; set; }
        public virtual decimal Preco_02 { get; set; }
        public virtual decimal Preco_03 { get; set; }
        public virtual decimal Preco_04 { get; set; }
        public virtual decimal Preco_05 { get; set; }
        public virtual decimal Preco_06 { get; set; }
        public virtual decimal PrecoMultiplicado_01 { get { return Preco_01*Quantidade; } }
        public virtual decimal PrecoMultiplicado_02 { get { return Preco_02 * Quantidade; } }
        public virtual decimal PrecoMultiplicado_03 { get { return Preco_03 * Quantidade; } }
        public virtual decimal PrecoMultiplicado_04 { get { return Preco_04 * Quantidade; } }
        public virtual decimal PrecoMultiplicado_05 { get { return Preco_05 * Quantidade; } }
        public virtual decimal PrecoMultiplicado_06 { get { return Preco_06 * Quantidade; } }
        public virtual IList<CotacaoProdutoPedidoItem> CotacaoProdutoPedidoItems { get; set; }

    }
}