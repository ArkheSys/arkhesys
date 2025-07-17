using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio
{
    public class ParcelasBuilder<T> where T : Modelo.ParcelaBase, new()
    {
        private readonly Condicao condicao;

        public ParcelasBuilder(Condicao condicao)
        {
            this.condicao = condicao;
        }

        public List<T> GerarParcelas(Decimal valor, DateTime inicio)
        {
            if (valor == 0 || condicao == null || inicio == null)
                throw new ArgumentException();

            decimal totalFixo = condicao.CondicaoParcelas.Where(parc => parc.TipoVlr == "Valor").Sum(parc => parc.VlrPerc);
            if (totalFixo > valor)
                throw new InvalidOperationException("Valor das parcelas excede o valor a ser pago.");

            List<T> parcelasGeradas = new List<T>();
            decimal total = Math.Round(valor - totalFixo, 2);
            IList<Feriado> feriados = FeriadoController.Instancia.GetAll();
            int parcelaNumero = 1;
            foreach (CondicaoParcela parcela in condicao.CondicaoParcelas)
            {
                DateTime vencimento = inicio;
                var pedParcela = new T();
                pedParcela.Parcela = parcelaNumero++;
                pedParcela.TipoDocumento = parcela.TipoDocumento;

                if (parcela.TipoDt == "Dia")
                    vencimento = vencimento.AddDays(parcela.DiaMes);
                else
                    vencimento = vencimento.AddMonths(parcela.DiaMes);

                while (condicao.VencimentoFeriado != VencimentoFeriado.Nada && (vencimento.DayOfWeek == DayOfWeek.Saturday || vencimento.DayOfWeek == DayOfWeek.Sunday || feriados.Where(feriado => feriado.Data.Day == vencimento.Day && feriado.Data.Month == vencimento.Month).Count() > 0))
                    switch (condicao.VencimentoFeriado)
                    {
                        case VencimentoFeriado.Adiantar:
                            vencimento = vencimento.AddDays(-1);
                            break;
                        case VencimentoFeriado.Adiar:
                            vencimento = vencimento.AddDays(1);
                            break;
                    }

                pedParcela.Vencimento = vencimento;

                if (condicao.Divide.Value == 1)
                {
                    if (condicao.QtParcela > 0)
                        pedParcela.Valor = Math.Round((total / condicao.QtParcela), 2);
                    else
                        throw new InvalidOperationException("Condição não especifica quantidade de parcelas");
                }
                else
                {
                    if (parcela.TipoVlr == "Percentual")
                        pedParcela.Valor = Math.Round(total * parcela.VlrPerc / 100, 2);
                    else
                        pedParcela.Valor = parcela.VlrPerc;
                }
                pedParcela.TipoDocumento = parcela.TipoDocumento;
                parcelasGeradas.Add(pedParcela);
            }
            parcelasGeradas[0].Valor += valor - parcelasGeradas.Sum(parc => parc.Valor);

            return parcelasGeradas;
        }

        public void AtualizaParcelas(IList<T> parcelas, DateTime dataBase, decimal total)
        {
            List<int> indicesAtualizadosManualmente = new List<int>();

            decimal valorAtualizadoManualmente = 0;
            for (int i = 0; i < parcelas.Count; i++)
            {
                if (parcelas[i].BAlterado)
                {
                    indicesAtualizadosManualmente.Add(i);
                    valorAtualizadoManualmente += parcelas[i].Valor;
                }
            }


            if (condicao.CondicaoParcelas.Count > 1 || (condicao.CondicaoParcelas.Count == 1 && !parcelas[0].BAlterado))
            {
                indicesAtualizadosManualmente.Reverse();
                foreach (var item in indicesAtualizadosManualmente)
                {
                    condicao.QtParcela--;
                    condicao.CondicaoParcelas.RemoveAt(item);
                }

                if (total - valorAtualizadoManualmente > 0)
                {

                    List<T> listaNova = GerarParcelas(total - valorAtualizadoManualmente, dataBase);

                    for (int i = 0; i < parcelas.Count; i++)
                    {
                        if (!parcelas[i].BAlterado)
                        {
                            parcelas[i] = listaNova[0];
                            listaNova.RemoveAt(0);
                        }
                        parcelas[i].Parcela = i + 1;
                    }
                }
            }
        }
    }
}
