using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Cotação de Transportadora - Pedido")]
    public class CotacaoTransportadoraPedido : ModeloBase
    {
        public override int ID { get; set; }

        [InformacaoExibicao(1, "Código", 100)]
        public virtual int Codigo { get; set; }

        [InformacaoExibicao(2, "Cód. Pedido", 100)]
        public virtual int CodigoPedido { get; set; }
        public virtual int CodigoTransportadora { get; set; }

        [InformacaoExibicao(3, "Observação", 100)]
        public virtual string Observacao { get; set; }

        [InformacaoExibicao(4, "Prazo de Entrega", 100)]
        public virtual DateTime PrazoEntrega { get; set; }
        
        [InformacaoExibicao(5, "Valor", 100)]
        public virtual Decimal Valor { get; set; }

        private string _TransNome;
        [InformacaoExibicao(6, "Tranportadora", 90, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual string TransNome { get { return _TransNome; } set { _TransNome = value; } }

        //public virtual Pessoa PessoaTransportadora { get; set; }

    }
}
