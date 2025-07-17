using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class EstoqueIdentificadorExpedicao : ModeloBase
    {
        public override int ID { get; set; }
        private Identificador _Identificador;
        public virtual Identificador IDIdentificador { get { return _Identificador; } set { _Identificador = value; } }
        public virtual decimal Saldo { get; set; }
        public virtual decimal CustoUnitario { get; set; }
        public virtual decimal QuantidadeBaixada { get; set; }
        public virtual decimal SaldoDisponivel { get; set; }
        public virtual bool bGravado { get; set; }
    }
}
