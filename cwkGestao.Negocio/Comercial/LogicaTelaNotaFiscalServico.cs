using System.Collections.Generic;
using System.Linq;
using cwkGestao.Modelo;
using cwkGestao.Negocio.Tributacao;
using System;

namespace cwkGestao.Negocio
{
    public partial class LogicaTelaNotaFiscal
    {
        private readonly TributacaoServico tributacaoServico;

        #region NotaItemServico

        public void NovoNotaItemServico()
        {
            if (nota.NotaItemsServicos.Where(nis => nis.Servico == null).Count() == 0)
            {
                NovoNotaItemServico(new NotaItemServico
                                        {
                                            Nota = nota,
                                            Quantidade = 1
                                        });
            }
        }

        public void NovoNotaItemServico(NotaItemServico notaItemServico)
        {
            if (!nota.NotaItemsServicos.Contains(notaItemServico))
            {
                nota.NotaItemsServicos.Add(notaItemServico);
                notaItemServico.Nota = nota;
            }
        }

        public void AtualizaImpostosServico()
        {
            AtualizaTotalServicos();
            AtualizaImpostosServicos();
            AtualizaTotalNotaFiscal();
        }

        private void AtualizaImpostosServicos()
        {
            tributacaoServico.AtualizarImpostosServico();
        }

        private void AtualizaTotalServicos()
        {
            foreach (NotaItemServico notaItemsServico in nota.NotaItemsServicos)
            {
                decimal total = notaItemsServico.Valor * notaItemsServico.Quantidade;
                notaItemsServico.SubTotal = Math.Round(total - (total * Convert.ToDecimal(notaItemsServico.PercDesconto) / 100), 2);
            }
            nota.TotalServico = nota.NotaItemsServicos.Sum(nis => nis.SubTotal);
        }

        #endregion
    }
}
