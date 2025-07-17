using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
   [InformacaoExibicao("Tabela de Logs de Retorno")] 
    public class LogRetorno : ModeloBase
	{
        [InformacaoExibicao(0, "Código", 75)]
        public override int ID { get; set; }

        [InformacaoExibicao(1, "Nome Arquivo", 250)]
		public virtual string NomeArquivo { get; set; }

		public virtual Remessa remessa { get; set; }

        [InformacaoExibicao(2, "Remessa", 200)]
        public virtual string nomeRemessa
        {
            get { return remessa != null ? remessa.Descricao : String.Empty; }
        }

        [InformacaoExibicao(3, "Data Importação", 125, InformacaoExibicao.HAlinhamento.Centro,InformacaoExibicao.Mascaras.DataHora)]
		public override DateTime? IncData 
        {
            get
            {
                return base.IncData;
            }
            set
            {
                base.IncData = value;
            }
        }

        [InformacaoExibicao(4, "Usuário", 100)]
        public override String IncUsuario
        {
            get
            {
                return base.IncUsuario;
            }
            set
            {
                base.IncUsuario = value;
            }
        }

        public virtual IList<LogRetornoDetalhe> LogRetornoDetalhe { get; set; }
    }
}


   