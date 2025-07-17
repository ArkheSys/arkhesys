using System;
using System.Collections.Generic;


namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Plano Contas")]
    public class FinanceiroPlanoContas : ModeloBase
    {
        public FinanceiroPlanoContas()
        {
            RateioProjeto = new List<Rateio_Mov>();
        }

        public override int ID { get; set; }

        [InformacaoExibicao(0, "Código", 90)]
        public int PlanoContaCodigo { get { return PlanoConta.Codigo; } }

        [InformacaoExibicao(1, "Descrição", 90)]
        public String PlanoContaDescricao { get { return PlanoConta.Classificacao + " " + PlanoConta.Nome; } }

        public PlanoConta PlanoConta { get; set; }

        public string Descricao { get; set; }

        [InformacaoExibicao(2, "Valor", 90, InformacaoExibicao.HAlinhamento.Direita,InformacaoExibicao.Mascaras.Dinheiro)]
        public decimal Valor { get; set; }

        public List<Rateio_Mov> RateioProjeto { get; set; }
        public bool planocontaparcelaclientecartao { get; set; }
    }
}
