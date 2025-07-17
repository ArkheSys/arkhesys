using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class FechamentoCaixa : ModeloBase
	{
        public override int ID { get; set; }
		public virtual Banco Banco { get; set; }
		public virtual DateTime DtFechamento { get; set; }
		public virtual string UsuarioFechamento { get; set; }
        public virtual DateTime DtConciliacao { get; set; }
        public virtual bool bFechado { get; set; }
        public virtual decimal ValorEntrada { get; set; }
        public virtual decimal ValorSaida { get; set; }
        public virtual decimal Saldo { get; set; }
        public virtual decimal SaldoInicial { get; set; }
    }
}
