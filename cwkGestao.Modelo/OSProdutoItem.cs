using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class OSProdutoItem : ModeloBase
    {
        public override int ID { get; set; }
        public virtual OSOrdemServico OSOrdemServico { get; set; }
        public virtual int Seq { get; set; }
        public virtual Produto Produto { get; set; }
        public virtual string Descricao { get; set; }
        public virtual decimal Quantidade { get; set; }
        public virtual string Unidade { get; set; }
        public virtual decimal PrecoOriginal { get; set; }
        public virtual decimal Preco { get; set; }
        public virtual decimal DescontoPerc { get; set; }
        public virtual decimal Total { get; set; }

        public virtual string GuidIdentificacao { get; set; }

        public virtual string CodigoProduto => Produto?.Codigo;
        public virtual string DescricaoProduto => Produto?.Nome;
    }
}
