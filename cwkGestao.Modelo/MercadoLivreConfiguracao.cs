using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class MercadoLivreConfiguracao : ModeloBase
	{
        public override int ID { get; set; }
		public virtual Pessoa Fornecedor { get; set; }
		public virtual TabelaPreco TabelaPreco { get; set; }
		public virtual Condicao Condicao { get; set; }
		public virtual Pessoa Vendedor { get; set; }
		public virtual LocalEstoque LocalEstoque { get; set; }
		public virtual long Aplicacao { get; set; }
		public virtual string ChaveSecreta { get; set; }
		public virtual Filial Filial { get; set; }
		public virtual TipoPedido TipoOrcamento { get; set; }
        public virtual string Token { get; set; }
        public virtual string RefreshToken { get; set; }
        public virtual string URL { get; set; }
        public virtual int TempoSinc { get; set; }
        public virtual int IDUsuarioMercadoLivre { get; set; }
        public virtual DateTime DtInicioImportacao { get; set; }
    }
}
