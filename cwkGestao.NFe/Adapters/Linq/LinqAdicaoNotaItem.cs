using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MO = Modelo;

namespace cwkGestao.NFe.Adapters.Linq
{
    internal class LinqAdicaoNotaItem : AdapterAdicaoNotaItem
    {
        MO.AdicaoNotaItem adicaoNotaItem;

        public LinqAdicaoNotaItem(MO.AdicaoNotaItem _adicaoNotaItem)
        {
            adicaoNotaItem = _adicaoNotaItem;
        }

        public override decimal I26_nAdicao
        {
            get { return adicaoNotaItem.I26_nAdicao.HasValue ? adicaoNotaItem.I26_nAdicao.Value : 0; }
        }

        public override decimal I27_nSeqAdic
        {
            get { return adicaoNotaItem.I27_nSeqAdic.HasValue ? adicaoNotaItem.I27_nSeqAdic.Value : 0; }
        }

        public override string I28_cFabricante
        {
            get { return adicaoNotaItem.I28_cFabricante; }
        }

        public override decimal I29_vDescDI
        {
            get { return adicaoNotaItem.I29_vDescDI.HasValue ? adicaoNotaItem.I29_vDescDI.Value : 0; }
        }

        public override string I30_xPed
        {
            get { return adicaoNotaItem.I30_xPed; }
        }

        public override decimal I31_nItemPed
        {
            get { return adicaoNotaItem.I31_nItemPed.HasValue ? adicaoNotaItem.I31_nItemPed.Value : 0; }
        }
    }
}
