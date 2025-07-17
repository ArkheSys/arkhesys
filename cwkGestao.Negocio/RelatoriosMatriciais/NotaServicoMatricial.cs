using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixReporter;

namespace cwkGestao.Negocio.RelatoriosMatriciais
{
    public class NotaServicoMatricial
    {
        private const int itensPorPagina = 12;
        private int count;
        private int pag;
        private EpsonCodes lCodes;
        private Reporter lPrinter;
        Modelo.Nota nota;

        [Obsolete()]
        public NotaServicoMatricial(Modelo.NotaServico notaServico)
        {
            SetaDadosNotaServico(notaServico);
            PreparaImpressao();
        }

        [Obsolete()]
        private void SetaDadosNotaServico(Modelo.NotaServico notaServico)
        {
            nota = new Modelo.Nota();
            nota.Observacao1 = notaServico.Observacao;
            nota.TotalNota = notaServico.TotalNota;
            nota.PessoaInscRG = notaServico.ClienteInscricao;
            nota.PessoaCNPJCPF = notaServico.ClienteCnpjCpf;
            nota.PessoaTelefone = notaServico.ClienteFone;
            nota.PessoaCEP = notaServico.ClienteCep;
            nota.PessoaUF = notaServico.ClienteUF;
            nota.PessoaCidade = notaServico.ClienteCidade;
            nota.PessoaBairro = notaServico.ClienteBairro;
            nota.PessoaEndereco = notaServico.ClienteEndereco;
            nota.PessoaNome = notaServico.ClienteNome;
            nota.Dt = notaServico.Data;
            nota.Codigo = notaServico.Codigo;
            nota.NotaItemsServicos = new List<Modelo.NotaItemServico>();
            foreach (var item in notaServico.NotaServicoItems)
            {
                nota.NotaItemsServicos.Add(new Modelo.NotaItemServico()
                {
                    Quantidade = item.Quantidade,
                    NomeServico = item.ServicoDescricao,
                    Valor = item.ValorUnitario,
                    SubTotal = item.Total
                });
            }
        }

        public NotaServicoMatricial(Modelo.Nota nota)
        {
            this.nota = nota;
            PreparaImpressao();
        }

        private void PreparaImpressao()
        {
            lCodes = new EpsonCodes();
            lPrinter = new Reporter();

            count = 0;
            pag = 1;

            lPrinter.Output = "LPT1";
            lPrinter.NomeArquivo = "n" + nota.Codigo.ToString() + ".txt";
            lPrinter.StartJob();
            lPrinter.PrintText(count, 0, lCodes.Reset);
            lPrinter.PrintText(count, 0, lCodes.Normal);

            //Imprime o Cabeçalho da Nota Fiscal
            ImprimeCabecalho(lCodes, lPrinter, Utilitarios.StrNumeroPagina(pag, nota.NotaItemsServicos.Count, itensPorPagina));

            int cItens = 0;
            count = 14;
            foreach (var item in nota.NotaItemsServicos)
            {
                //Imprime a Quantidade
                lPrinter.PrintText(count, 0, String.Format("{0,4} ", String.Format("{0:n0}", item.Quantidade)));
                string descricao = Utilitarios.RetiraAcentos(item.NomeServico);
                if (descricao.Length > 80)
                    descricao = descricao.Substring(0, 80);

                lPrinter.PrintText(count, 8, String.Format("{0,-80} ", descricao));
                //Imprime o Preço Unitário
                lPrinter.PrintText(count, 98, String.Format("{0,13} ", String.Format("{0:n}", item.Valor)));
                lPrinter.PrintText(count, 120, String.Format("{0,13} ", String.Format("{0:n}", item.SubTotal)));

                count++;
                cItens++;
            }

            pag++;

            //Imprime o rodapé
            ImprimeRodape(lCodes, lPrinter, true);

            count = 34;
            lPrinter.PrintText(++count, 0, lCodes.CondensedOff + lCodes.Normal + lCodes.Reset);

            lPrinter.EndJob();
        }

        private void ImprimeCabecalho(EpsonCodes lCodes, Reporter lPrinter, string pag)
        {
            //Imprime o numero da nota
            count = 3;
            lPrinter.PrintText(count, 60, lCodes.ExpandedOn + lCodes.BoldOn + String.Format("{0:D6}", nota.Numero) + lCodes.BoldOff + lCodes.ExpandedOff);

            //Numero da pagina
            //lPrinter.PrintText(count, 7, lCodes.CondensedOn);
            lPrinter.PrintText(count, 70, lCodes.CondensedOn);

            //Imprime a Data
            count = 5;
            lPrinter.PrintText(count, 105, lCodes.BoldOn + nota.Dt.ToShortDateString() + lCodes.BoldOff);

            //Imprime o nome do Pessoa
            count = 7;
            lPrinter.PrintText(count, 12, String.Format("{0,-83}", Utilitarios.RetiraAcentos(nota.PessoaNome == null ? "" : nota.PessoaNome.TrimEnd())));

            //Imprime o Endereço/Bairro
            count++;
            string endereco = Utilitarios.RetiraAcentos(nota.PessoaEndereco == null ? "" : nota.PessoaEndereco.TrimEnd());
            if (!String.IsNullOrEmpty(nota.Complemento))
                endereco += ", " + nota.Complemento;
            lPrinter.PrintText(count, 12, String.Format("{0,-76}", endereco));
            lPrinter.PrintText(count, 98, String.Format("{0,-30}", Utilitarios.RetiraAcentos(nota.PessoaBairro == null ? "" : nota.PessoaBairro.TrimEnd())));

            //Imprime o Cidade/UF/CEP/Fone
            count++;
            lPrinter.PrintText(count, 12, String.Format("{0,-43}", Utilitarios.RetiraAcentos(nota.PessoaCidade == null ? "" : nota.PessoaCidade.TrimEnd())));
            lPrinter.PrintText(count, 63, String.Format("{0,-2}", nota.PessoaUF));
            lPrinter.PrintText(count, 83, String.Format("{0,-15}", nota.PessoaCEP));
            lPrinter.PrintText(count, 113, String.Format("{0,-20}", nota.PessoaTelefone));

            //Imprime CNPJ e Inscrição
            count++;
            lPrinter.PrintText(count, 12, String.Format("{0,-20}", nota.PessoaCNPJCPF));
            lPrinter.PrintText(count, 83, String.Format("{0,-20}", nota.PessoaInscRG));

            //Imprime Condição
            count++;
            lPrinter.PrintText(count, 12, String.Format("{0,-20}", Utilitarios.RetiraAcentos(nota.Condicao.Nome)));
            lPrinter.PrintText(count, 83, String.Format("{0,-20}", nota.NotaParcelas.OrderBy(n=> n.Vencimento).First().Vencimento.ToShortDateString()));
        }

        private void ImprimeRodape(EpsonCodes lCodes, Reporter lPrinter, bool imp)
        {
            //Imprime o Total da Nota Fiscal
            count = 27;
            lPrinter.PrintText(count, 115, lCodes.BoldOn + String.Format("{0,13} ", String.Format("{0:n}", nota.TotalNota)) + lCodes.BoldOff);

            //Imprime a Observação
            count = 29;
            int tam = 0;
            if (nota.Observacao1 != null)
                tam = nota.Observacao1.Length;

            if (tam != 0 && tam <= 100)
            {
                lPrinter.PrintText(count, 0, String.Format("{0,-100} ", Utilitarios.RetiraAcentos(nota.Observacao1.Substring(0, tam))));
            }
            else if (tam > 100 && tam <= 200)
            {
                lPrinter.PrintText(count, 0, String.Format("{0,-100} ", Utilitarios.RetiraAcentos(nota.Observacao1.Substring(0, 100))));
                count++;
                lPrinter.PrintText(count, 0, String.Format("{0,-100} ", Utilitarios.RetiraAcentos(nota.Observacao1.Substring(100, 100))));
            }
            else if (tam >= 200)
            {
                lPrinter.PrintText(count, 0, String.Format("{0,-100} ", Utilitarios.RetiraAcentos(nota.Observacao1.Substring(0, 100))));
                count++;
                lPrinter.PrintText(count, 0, String.Format("{0,-100} ", Utilitarios.RetiraAcentos(nota.Observacao1.Substring(100, 100))));
                count++;
                lPrinter.PrintText(count, 0, String.Format("{0,-100} ", Utilitarios.RetiraAcentos(nota.Observacao1.Substring(200, 100))));
            }
        }

        public void ImprimeNota(bool ImpressaoEmVideo)
        {
            if (ImpressaoEmVideo)
                lPrinter.PreviewJob();
            else
                lPrinter.PrintJob();
            //nota.BImpressa = true;
            //Negocio.NotaController.Instancia.Salvar(nota, cwkGestao.Modelo.Acao.Alterar);
        }
    }


}
