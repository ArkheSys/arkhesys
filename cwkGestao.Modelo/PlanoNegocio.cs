using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Plano de Negócio")]
    public class PlanoNegocio : ModeloBase
    {
        [InformacaoExibicao(1, "Código", 100)]
        public virtual int Codigo { get; set; }
        [InformacaoExibicao(2, "Descrição", 100)]
        public virtual string Descricao { get; set; }
        public virtual IList<PlanoNegocioOperacao> PlanoNegocioOperacaos { get; set; }
        public override int ID { get; set; }
        public virtual IList<Produto> Produtos { get; set; }

    }
}
