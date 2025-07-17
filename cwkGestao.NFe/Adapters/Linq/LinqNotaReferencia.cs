using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MO = Modelo;

namespace cwkGestao.NFe.Adapters.Linq
{
    internal class LinqNotaReferencia : AdapterNotaReferencia
    {
        MO.NotaReferencia notaReferencia;

        public LinqNotaReferencia(MO.NotaReferencia _notaReferencia)
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
