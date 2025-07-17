using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FileHelpers;

namespace cwkGestao.Integracao.Sped.Fiscal.Interfaces
{
    [DelimitedRecord("|")]
    public abstract class Registro
    {
#pragma warning disable CS0618 // "FieldIgnoredAttribute" é obsoleto: "You must use [FieldNotInFile] instead"
        [FieldIgnored]
#pragma warning restore CS0618 // "FieldIgnoredAttribute" é obsoleto: "You must use [FieldNotInFile] instead"
        private Dictionary<string, object> _Parametros;
        public Dictionary<string, object> Parametros
        {
            get
            {
                return _Parametros;
            }
            set
            {
                _Parametros = value;
            }
        }
    }
}
