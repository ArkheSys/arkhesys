using System;
using System.Collections.Generic;

namespace cwkGestao.Modelo
{
    public class Promocao : ModeloBase
    {
        public override int ID { get; set; }
        public virtual TabelaPreco TabelaPreco { get; set; }
        public virtual string Nome { get; set; }
        public virtual DateTime? DataInicio { get; set; }
        public virtual DateTime? DataFim { get; set; }
        public virtual decimal Percentual { get; set; }
        public virtual int Tipo { get; set; }

        public virtual string TabelaPrecoFormatada { get { return TabelaPreco?.Nome; } }

        public virtual IList<PromocaoProduto> Itens { get; set; }

        public Promocao()
        {
        }
    }
}