using System;

namespace cwkGestao.Modelo
{    
    public class LogExportacaoNota : ModeloBase
	{
        public override int ID { get; set; }
		public virtual DateTime DataCadastro { get; set; }

        public virtual string Periodo { get; set; }

        public virtual int QuantidadeXML { get; set; }
        public virtual int QuantidadePDF { get; set; }


    }
}
