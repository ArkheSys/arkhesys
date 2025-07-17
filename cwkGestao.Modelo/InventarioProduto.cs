using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Inventário Produto")]
    public class InventarioProduto : ModeloBase
	{
        public override int ID { get; set; }
		public virtual Inventario Inventario { get; set; }
        public virtual Produto Produto { get; set; }
        [InformacaoExibicao(1, "Produto", 250, InformacaoExibicao.HAlinhamento.Esquerda)]
        public virtual string ProdutoNome { get { return Produto != null ? Produto.Nome : ""; } }
        [InformacaoExibicao(2, "Quantidade", 120, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual decimal Quantidade { get; set; }
        [InformacaoExibicao(3, "Valor", 120, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal Valor { get; set; }
        [InformacaoExibicao(4, "Total", 120, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal Total { get; set; }
        public virtual string ProdutoNomeOrdenado { get { return Produto != null ? Produto.NomeOrdenado : ""; } }
    }
}
