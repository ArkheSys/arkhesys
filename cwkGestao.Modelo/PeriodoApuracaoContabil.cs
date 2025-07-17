using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Negocio.Utils;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Período de Apuração")]
    public class PeriodoApuracaoContabil : ModeloBase
	{
        public override int ID { get; set; }        

		public virtual Filial Filial { get; set; }
        [InformacaoExibicao(1, "Filial", 350)]
        public virtual String FilialNome { get { return Filial.Nome; } }
        
		public virtual DateTime Inicio { get; set; }
        [InformacaoExibicao(2, "Início", 100)]
        public virtual String InicioExibicao { get { return DatasUtil.GetLiteralMes(Inicio.Month) + "/" + Inicio.Year; } }
        
        public virtual DateTime Fim { get; set; }
        [InformacaoExibicao(3, "Fim", 100)]
        public virtual String FimExibicao { get { return DatasUtil.GetLiteralMes(Fim.Month) + "/" + Fim.Year; } }

        [InformacaoExibicao(4, "Contabilista", 200)]
		public virtual Pessoa Pessoa { get; set; }
    }
}
