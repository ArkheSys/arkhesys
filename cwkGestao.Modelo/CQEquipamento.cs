using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class CQEquipamento : ModeloBase
    {
        public override int ID { get; set; }
        public virtual int Codigo { get; set; }
        public virtual ControleQualidade ControleQualidade { get; set; }
        public virtual OSEquipamento Equipamento { get; set; }
        public virtual decimal QtdDefeito { get; set; }
    }
}
