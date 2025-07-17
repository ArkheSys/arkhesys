using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao ("Arquivo Obra")]
    public class Ofd_ObraArquivos : ModeloBase
    {
        public override int ID { get; set; }
        public virtual Ofd_Obra Obra { get; set; }
        [InformacaoExibicao(0, "Seq", 20)]
        public virtual int Sequencia { get; set; }
        [InformacaoExibicao(1, "Descrição", 200)]
        public virtual string Descricao { get; set; }
        [InformacaoExibicao(1, "Tipo", 80)]
        public virtual TipoArquivoObra TipoArquivo { get; set; }
        [InformacaoExibicao(1, "Versão", 80)]
        public virtual int VersaoArquivo { get; set; }
        public virtual string CaminhoArquivo { get; set; }
    }
}
