using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo.Util;

namespace cwkGestao.Modelo
{    
    [InformacaoExibicao ("Medi��o Servi�os")]
    public class Ofd_MedicaoServicos : ModeloBase
	{
        public override int ID { get; set; }

		public virtual Ofd_Medicao Ofd_Medicao { get; set; }
		public virtual TipoDiametro Diametro { get; set; }
        [InformacaoExibicao(1, "Qtd", 90)]
		public virtual decimal Quantidade { get; set; }
		public virtual decimal Valor { get; set; }
        [InformacaoExibicao(2, "Metros", 90)]
		public virtual decimal Metro { get; set; }
        [InformacaoExibicao(3, "Total", 100)]
		public virtual decimal Total { get; set; }
        [InformacaoExibicao(0, "Di�metro", 80)]
        public virtual string DiametroStr { get { return "Perfura��o " + Convert.ToInt32((Convert.ToDecimal(Diametro.TipoDiametroToString()) * 100m)) + " (mL)"; } }
    }
}
