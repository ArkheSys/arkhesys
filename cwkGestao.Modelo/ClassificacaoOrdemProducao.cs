using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Classificação Ordem Produção")]
    public class ClassificacaoOrdemProducao : ModeloBase
    {
        public override int ID { get; set; }
        [InformacaoExibicao(1, "Código", 250)]
        public virtual int Codigo { get; set; }
        [InformacaoExibicao(2, "Nome", 450)]
        public virtual string Nome { get; set; }
        public virtual int Status { get; set; }
        public virtual IList<OrdemProducao> OrdemProducaos { get; set; }

        public virtual IList<OrdemProducaoHistorico> OrdemProducaoHistoricos { get; set; }

    }
}
