using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo.Objeto
{
    public class PxyPessoaDoc
    {
        public int ID                            { get; set; }
        public int Codigo                        { get; set; }
        public Modelo.TipoDocumentoType Tipo     { get; set; }
        public Decimal? valor                    { get; set; }
        public DateTime? Vencimento              { get; set; }
        public string NumDoc                     { get; set; }
    }
}
