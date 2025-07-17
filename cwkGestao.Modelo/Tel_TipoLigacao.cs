using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Tipo Ligação")]
    public class Tel_TipoLigacao : ModeloBase
    {
        public Tel_TipoLigacao()
        {
            TipoLigacaoPrecos = new List<Tel_TipoLigacaoPreco>();
        }

        public override int ID { get; set; }

        [InformacaoExibicao(1, "Código", 90, InformacaoExibicao.HAlinhamento.Direita)]
        public virtual int Codigo { get; set; }

        [InformacaoExibicao(2, "Descrição", 300)]
        public virtual string Descricao { get; set; }

        [InformacaoExibicao(3, "Resumo Fatura", 200)]
        public virtual string ResumoFatura { get; set; }

        [InformacaoExibicao(6, "Data de Inclusão", 120, InformacaoExibicao.HAlinhamento.Centro)]
        public override DateTime? IncData { get; set; }

        [InformacaoExibicao(6, "Usuário de Inclusão", 120)]
        public override string IncUsuario { get; set; }

        [InformacaoExibicao(7, "Data da Alteração", 120, InformacaoExibicao.HAlinhamento.Centro)]
        public override DateTime? AltData { get; set; }

        [InformacaoExibicao(9, "Usuário da Alteração", 120)]
        public override string AltUsuario { get; set; }

        public virtual bool LigacaoLocal { get; set; }

        private IList<Tel_TipoLigacaoPreco> _Tel_TipoLigacaoPrecos;
        public virtual IList<Tel_TipoLigacaoPreco> TipoLigacaoPrecos { get { return _Tel_TipoLigacaoPrecos; } set { _Tel_TipoLigacaoPrecos = value; } }

        public override string ToString()
        {
            return Descricao;
        }
    }
}
