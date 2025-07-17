using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    [InformacaoExibicao ("C�digo de Barra")]
    public class ProdutoCodBarra : ModeloBase
	{
        public ProdutoCodBarra()
        {
        }

        public ProdutoCodBarra(int Id, Produto produto, bool controleIdentificacao, string codigoBarra)
        {
            ID = Id;
            Produto = produto;
            BControleIdentificacao = controleIdentificacao;
            CodigoBarra = codigoBarra;
        }
        public override int ID { get; set; }
        [InformacaoExibicao(3, "Produto", 300)]
		public virtual Produto Produto { get; set; }
        [InformacaoExibicao (1, "Controle Identifica��o", 100)]
		public virtual bool BControleIdentificacao { get; set; }
        [InformacaoExibicao (2, "C�digo de Barra", 100)]
		public virtual string CodigoBarra { get; set; }
    }
}
