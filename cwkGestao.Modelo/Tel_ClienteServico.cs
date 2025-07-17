using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Cliente Serviço")]
    public class Tel_ClienteServico : ModeloBase
    {
        public Tel_ClienteServico()
        {
            Operacao = 1;
        }

        public override int ID { get; set; }
        public virtual Tel_Cliente Cliente { get; set; }
        public virtual Tel_Servico Servico { get; set; }
        [InformacaoExibicao(1, "Data Ativação", 120, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual DateTime DataAtivacao { get; set; }
        [InformacaoExibicao(3, "Data Canc.", 120, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual DateTime? DataDesligamento { get; set; }
        [InformacaoExibicao(4, "Motivo Canc.", 200)]
        public virtual string MotivoDesligamento { get; set; }
        [InformacaoExibicao(5, "Contato Canc.", 200)]
        public virtual string ContatoPediuDesligamento { get; set; }
        [InformacaoExibicao(2, "Valor", 90, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal Valor { get; set; }
        /// <summary>
        /// 0 - Crédito
        /// 1 - Débito
        /// </summary>
        public virtual byte Operacao { get; set; }

        [InformacaoExibicao(0, "Serviço", 100)]
        public virtual string ServicoStr { get { return Servico != null ? Servico.Descricao : String.Empty; } }
    }
}
