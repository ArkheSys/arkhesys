using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.NFe.Adapters.NH
{
    internal class NHAdicaoNotaItem : AdapterAdicaoNotaItem
    {
        Modelo.AdicaoNotaItem adicaoNotaItem;

        public NHAdicaoNotaItem(Modelo.AdicaoNotaItem _adicaoNotaItem)
        {
            adicaoNotaItem = _adicaoNotaItem;
        }

        public override decimal I26_nAdicao
        {
            get { return adicaoNotaItem.I26_nAdicao; }
        }

        public override decimal I27_nSeqAdic
        {
            get { return adicaoNotaItem.I27_nSeqAdic; }
        }

        public override string I28_cFabricante
        {
            get { return adicaoNotaItem.I28_cFabricante; }
        }

        public override decimal I29_vDescDI
        {
            get { return adicaoNotaItem.I29_vDescDI; }
        }

        public override string I30_xPed
        {
            get { return adicaoNotaItem.I30_xPed; }
        }

        public override decimal I31_nItemPed
        {
            get { return adicaoNotaItem.I31_nItemPed; }
        }
    }
}
