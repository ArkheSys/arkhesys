using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
   [InformacaoExibicao("Parâmetros do Contrato de Produto")]
    public class ParametrosContratoProduto : ModeloBase
	{
        public override int ID { get; set; }
        [InformacaoExibicao(1, "Código", 80)]
        public virtual int Codigo { get; set; }

        [InformacaoExibicao(2, "Descrição", 180)]
        public virtual String Descricao { get; set; }

		public virtual Filial Filial { get; set; }

		public virtual TipoNota TipoNota { get; set; }
		public virtual Condicao Condicao { get; set; }
		public virtual TipoPedido TipoPedido { get; set; }
		public virtual TabelaPreco TabelaPreco { get; set; }

        public virtual TipoNota TipoNotaServico { get; set; }
        public virtual Pessoa Vendedor { get; set; }
        public virtual OSEquipamento Equipamento { get; set; }

    }
}
