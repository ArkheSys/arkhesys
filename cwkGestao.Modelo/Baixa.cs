using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    public class Baixa : ModeloBase
    {
        public override int ID { get; set; }
        public virtual int Codigo { get; set; }
        public virtual int Tipo { get; set; }
        public virtual DateTime DtBaixa { get; set; }
        public virtual Banco Banco { get; set; }
        public virtual int CodBanco { get; set; }
        public virtual string Banco_Str { get; set; }
        public virtual string Agencia { get; set; }
        public virtual string Conta { get; set; }
        public virtual string Cheque { get; set; }
        public virtual string Emitente { get; set; }
        public virtual decimal Total { get; set; }
        public virtual IList<BaixaDocumento> BaixaDocumentos { get; set; }

    }

   
}