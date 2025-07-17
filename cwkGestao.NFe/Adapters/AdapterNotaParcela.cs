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
    abstract class AdapterNotaParcela : INotaParcela
    {
        public static AdapterNotaParcela Produce(object notaParcela)
        {
            if (notaParcela.GetType() == typeof(MO.NotaParcela))
            {
                return new LinqNotaParcela((MO.NotaParcela)notaParcela);
            }
            else if (notaParcela.GetType() == typeof(Modelo.NotaParcela))
            {
                return new NHNotaParcela((Modelo.NotaParcela)notaParcela);
            }
            throw new ArgumentException("Parâmetro passado não é do tipo Modelo.NotaParcela ou cwkGestao.Modelo.NotaParcela");
        }

        public abstract int Sequencia
        {
            get;
        }

        public abstract decimal Valor
        {
            get;
        }

        public abstract DateTime Vencimento
        {
            get;
        }

        public abstract string FaturaNumero
        {
            get;
        }

        public abstract decimal FaturaValorLiquido
        {
            get;
        }

        public abstract decimal FaturaValorOriginal
        {
            get;
        }

        public abstract String FormaPagamento
        {
            get;
        }

        public abstract int? BandeiraCartao { get; }
        public abstract string Operadora { get; }
        public abstract string CAut { get; }

        public abstract decimal ValorTroco { get; }
    }
}
