using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Fatura Serviço")]
    public class Tel_FaturaServico : ModeloBase
    {
        public Tel_FaturaServico() 
        {
            Operacao = 1;
            Quantidade = 1;
        }

        public override int ID { get; set; }
        [InformacaoExibicao(1, "Sequência", 100)]
        public virtual int Sequencia { get; set; }
        [InformacaoExibicao(2, "Quantidade", 100)]
        public virtual int Quantidade { get; set; }
        [InformacaoExibicao(3, "Valor Unitário", 170, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal ValorUnitario { get; set; }
        [InformacaoExibicao(4, "Total", 170, InformacaoExibicao.HAlinhamento.Direita, InformacaoExibicao.Mascaras.Dinheiro)]
        public virtual decimal Total { get; set; }
        public virtual Tel_Servico Servico { get; set; }
        public virtual Tel_Fatura Fatura { get; set; }
        public virtual int Operacao { get; set; }
        public virtual Documento Documento { get; set; }
        [InformacaoExibicao(5, "Serviço", 245)]
        public virtual String ServicoDesc { get { return Servico.Descricao; } }
    }
}