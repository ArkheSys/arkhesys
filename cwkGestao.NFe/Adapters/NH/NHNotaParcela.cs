using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.NFe.Adapters.NH
{
    internal class NHNotaParcela : AdapterNotaParcela
    {
        Modelo.NotaParcela notaParcela;

        public NHNotaParcela(Modelo.NotaParcela _notaParcela)
        {
            notaParcela = _notaParcela;
        }

        public override int Sequencia
        {//NÚMERO DA DUPLICATA
            get { return notaParcela.Parcela; }
        }

        public override decimal Valor
        {
            get { return notaParcela.Valor; }
        }

        public override DateTime Vencimento
        {
            get { return notaParcela.Vencimento; }
        }

        public override string FaturaNumero
        {
            get { return notaParcela.Nota.Numero.ToString(); }
        }

        public override decimal FaturaValorLiquido
        {
            get { return notaParcela.Nota.TotalNota; }
        }

        public override decimal FaturaValorOriginal
        {
            get { return notaParcela.Nota.TotalNota; }
        }

        public override String FormaPagamento
        {
            get { return notaParcela.TipoFormaPagamento.ToString(); }
        }

        public override int? BandeiraCartao
        {
            get { return notaParcela.BandeiraCartao; }
        }

        public override string Operadora
        {
            get { return notaParcela.Operadora; }
        }

        public override string CAut
        {
            get { return notaParcela.CAut; }
        }

        public override decimal ValorTroco
        {
            get
            {
                return notaParcela.ValorTroco;
            }
        }
    }
}
