using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{    
    public class FormatoPacoteTransporte : ModeloBase, ICloneable
	{
        public override int ID { get; set; }
		public virtual int Codigo { get; set; }
		public virtual string Descricao { get; set; }
        public virtual IList<CorreiosPrecoPrazo> CorreiosPrecosPrazos { get; set; }

        public virtual int ComprimentoMinimo { get; set; }
        public virtual int ComprimentoMaximo { get; set; }
        public virtual int LarguraMinima { get; set; }
        public virtual int LarguraMaxima { get; set; }
        public virtual int AlturaMinima { get; set; }
        public virtual int AlturaMaxima { get; set; }
        public virtual int SomaMaximaDimensoes { get; set; }
        public virtual int DiametroMinimo { get; set; }
        public virtual int DiametroMaximo { get; set; }
                   
        public FormatoPacoteTransporte()
        {
        }
        public override string ToString()
        {
            return this.Codigo + " | " + this.Descricao;
        }

        #region ICloneable Members

        public virtual object Clone()
        {
            return this.MemberwiseClone() as FormatoPacoteTransporte;
        }

        #endregion
    }
}
