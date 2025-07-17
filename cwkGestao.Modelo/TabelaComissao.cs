using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class TabelaComissao : ModeloBase
    {
        public override int ID { get; set; }
        public virtual int Codigo { get; set; }
        public virtual string Nome { get; set; }
        public virtual DateTime ValidadeInicial { get; set; }
        public virtual DateTime ValidadeFinal { get; set; }
        public virtual IList<TabelaComissaoProduto> TabelaComissaoProdutos { get; set; }

    }
}
