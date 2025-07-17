using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class OSAgendamento : ModeloBase
    {
        public override int ID { get; set; }
        public virtual PessoaFuncionario Funcionario { get; set; }
        public virtual OSOrdemServico OSOrdemServico { get; set; }
        public virtual DateTime Inicio { get; set; }
        public virtual DateTime Fim { get; set; }
      
    }
}
