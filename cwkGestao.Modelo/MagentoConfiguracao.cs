using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Configuração Magento")]
	public class MagentoConfiguracao : ModeloBase
	{
        public override int ID { get; set; }

        public virtual string Usuario { get; set; }
        public virtual string Senha { get; set; }
        public virtual string Uri { get; set; }
        public virtual Unidade Unidade { get; set; }
        public virtual Pessoa Fornecedor { get; set; }
        public virtual GrupoEstoque GrupoEstoque { get; set; }
        public virtual TabelaPreco TabelaPreco { get; set; }

        public virtual Condicao Condicao { get; set; }
        public virtual Pessoa Vendedor { get; set; }
        public virtual TipoPedido TipoPedido { get; set; }
        public virtual int IDCategoriaPadrao { get; set; }
        public virtual LocalEstoque LocalEstoque { get; set; }
        
        public virtual TipoDocumento TipoDocumento { get; set; }
        public virtual TipoDocumento TipoDocumentoReg { get; set; }
        public virtual Portador Portador { get; set; } 
        public virtual Portador PortadorReg { get; set; }
        public virtual Banco Banco { get; set; }
        public virtual Acrescimo Acrescimo { get; set; }
        public virtual Historico Historico { get; set; }
        public virtual PlanoConta PlanoConta { get; set; }
        public virtual Remessa Remessa { get; set; }
        public virtual Remessa RemessaReg { get; set; }
        public virtual string AssuntoEmailBoleto { get; set; }
        public virtual string MensagemEmailBoleto { get; set; }
        public virtual string UrlBoletos { get; set; }
        public virtual bool BAgruparProdutos { get; set; }
        public virtual bool BAtualizarInfAdicional { get; set; }
        public virtual string StatusPedido { get; set; }
        public virtual bool BEnviaEstoqueMagento { get; set; }
        public virtual bool BEnviaPrecoMagento { get; set; }
	}
}
		