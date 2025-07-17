using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Arquivo Ligação")]
    public class Tel_ArquivoLigacao : ModeloBase
    {
        public override int ID { get; set; }
        [InformacaoExibicao(1, "Código", 85)]
        public virtual int Codigo { get; set; }
        [InformacaoExibicao(2, "Tipo de Arquivo", 105)]
        public virtual TipoArquivoLigacoes TipoArquivo { get; set; }
        [InformacaoExibicao(3, "Data da Importação", 130)]
        public virtual DateTime DataImportacao { get; set; }
        [InformacaoExibicao(4, "Caminho do Arquivo", 350)]
        public virtual string CaminhoArquivo { get; set; }        
        [InformacaoExibicao(5, "Data da Inclusão", 115)]
        public override DateTime? IncData { get; set; }
        [InformacaoExibicao(6, "Usuário de Inclusão", 115)]
        public override string IncUsuario { get; set; }
        [InformacaoExibicao(7, "Data da Alteração", 115)]
        public override DateTime? AltData { get; set; }
        [InformacaoExibicao(9, "Usuário da Alteração", 125)]
        public override string AltUsuario { get; set; }

        public virtual DateTime Referencia { get; set; }

        public virtual IList<Tel_Ligacao> Ligacoes { get; set; }
    }
}
