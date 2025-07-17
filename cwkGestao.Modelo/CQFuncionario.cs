using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class CQFuncionario : ModeloBase
    {
        public override int ID { get; set; }
        public virtual int Codigo { get; set; }
        public virtual ControleQualidade ControleQualidade { get; set; }
        public virtual Pessoa Funcionario1 { get; set; }
        public virtual Pessoa Funcionario2 { get; set; }
        public virtual Cor Cor { get; set; }
        public virtual decimal QtdDefeito { get; set; }
    }
}
