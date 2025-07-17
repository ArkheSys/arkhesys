using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class OrdemProducaoExecucao : ModeloBase
    {
        public override int ID { get; set; }
        public virtual int Sequencia { get; set; }
        public virtual string Descricao { get; set; }
        public virtual decimal Quantidade { get; set; }
        public virtual OrdemProducao OrdemProducao { get; set; }
        public virtual Servico Servico { get; set; }
        public virtual PessoaFuncionario Funcionario { get; set; }
        public virtual DateTime DataInicio { get; set; }
        public virtual DateTime DataTermino { get; set; }
        public virtual IList<OrdemProducaoExecucaoProduto> OrdemProducaoExecucaoProdutos { get; set; }

    }
}
