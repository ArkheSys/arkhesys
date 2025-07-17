using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class FinanceiroPContasSort: IComparer<Modelo.Objeto.FinanceiroPlanoContas>
    {
            public enum SortType
            {
                Conta,
                Valor
            }

            private SortType _sortType;

            public FinanceiroPContasSort(SortType sortType)
            {
                this._sortType = sortType;
            }

            public int Compare(Modelo.Objeto.FinanceiroPlanoContas x, Modelo.Objeto.FinanceiroPlanoContas y)
            {
                switch (this._sortType)
                {
                    case SortType.Valor:
                        return x.Valor.CompareTo(y.Valor);
                    case SortType.Conta:
                        return x.Conta.CompareTo(y.Conta);
                }

                return 0;
            }
    }
}
