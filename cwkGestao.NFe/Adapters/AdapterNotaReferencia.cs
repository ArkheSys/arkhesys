using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MO = Modelo;
using cwkGestao.NFe.Adapters.Linq;
using cwkGestao.NFe.Adapters.NH;
using cwkNotaFiscalEletronica.Interfaces;

namespace cwkGestao.NFe.Adapters
{
    abstract class AdapterNotaReferencia : INotaReferencia
    {
        public static AdapterNotaReferencia Produce(object notaReferencia)
        {
            if (notaReferencia.GetType() == typeof(MO.NotaReferencia))
            {
                return new LinqNotaReferencia((MO.NotaReferencia)notaReferencia);
            }
            else if (notaReferencia.GetType() == typeof(Modelo.NotaReferencia))
            {
                return new NHNotaReferencia((Modelo.NotaReferencia)notaReferencia);
            }
            throw new ArgumentException("Parâmetro passado não é do tipo Modelo.NotaParcela ou cwkGestao.Modelo.NotaParcela");
        }

        public abstract int ID
        {
            get;
        }

        public abstract string Chave
        {
            get;
        }
    }
}
