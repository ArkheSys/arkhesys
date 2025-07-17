using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkNotaFiscalEletronica.Interfaces;
using MO = Modelo;
using cwkGestao.NFe.Adapters.NH;
using cwkGestao.NFe.Adapters.Linq;

namespace cwkGestao.NFe.Adapters
{
    abstract class AdapterAdicaoNotaItem : IAdicaoNotaItem
    {
        public static AdapterAdicaoNotaItem Produce(object adicaoNotaItem)
        {
            if (adicaoNotaItem.GetType() == typeof(MO.AdicaoNotaItem))
            {
                return new LinqAdicaoNotaItem((MO.AdicaoNotaItem)adicaoNotaItem);
            }
            else if (adicaoNotaItem.GetType() == typeof(Modelo.AdicaoNotaItem))
            {
                return new NHAdicaoNotaItem((Modelo.AdicaoNotaItem)adicaoNotaItem);
            }

            throw new ArgumentException("Parâmetro passado não é do tipo Modelo.AdicaoNotaItem ou cwkGestao.Modelo.AdicaoNotaItem");
        }

        public abstract decimal I26_nAdicao
        {
            get;
        }

        public abstract decimal I27_nSeqAdic
        {
            get;
        }

        public abstract string I28_cFabricante
        {
            get;
        }

        public abstract decimal I29_vDescDI
        {
            get;
        }

        public abstract string I30_xPed
        {
            get;
        }

        public abstract decimal I31_nItemPed
        {
            get;
        }
    }
}
