using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class NotaInutilizada : ModeloBase
	{
        public override int ID { get; set; }
		public virtual Filial Filial { get; set; }
        [InformacaoExibicao(2, "Série", 100)]
		public virtual int Serie { get; set; }
        [InformacaoExibicao(3, "Número Início", 100)]
		public virtual int NumeroInicio { get; set; }
        [InformacaoExibicao(4, "Número Fim", 100)]
		public virtual int NumeroFim { get; set; }
        [InformacaoExibicao(5, "Justificativa", 170)]
		public virtual string Justificativa { get; set; }
        [InformacaoExibicao(6, "Data", 100)]
		public virtual DateTime Data { get; set; }
        public virtual string Ano { get; set; }
        public virtual int ModeloNota { get; set; }

        [InformacaoExibicao(1, "Modelo", 60, InformacaoExibicao.HAlinhamento.Centro)]
        public virtual string ModeloNotaStr
        {
            get
            {
                switch (this.ModeloNota)
                {
                    case 55:
                        return "NFe";
                    case 65:
                        return "NFCe";
                    default:
                        return this.ModeloNota.ToString();

                }
            }
        }

        


    }
}
