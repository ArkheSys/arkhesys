using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.NFe.Adapters.NH
{
    internal class NHNotaReferencia : AdapterNotaReferencia
    {
        Modelo.NotaReferencia notaReferencia;

        public NHNotaReferencia(Modelo.NotaReferencia _notaReferencia)
        {
            notaReferencia = _notaReferencia;
        }

        public override int ID
        {
            get { return notaReferencia.ID; }
        }

        public override string Chave
        {
            get { return notaReferencia.Chave; }
        } 
    }
}
