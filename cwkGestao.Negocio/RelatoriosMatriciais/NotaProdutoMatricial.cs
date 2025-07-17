using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using MatrixReporter;

namespace cwkGestao.Negocio.RelatoriosMatriciais
{
    public class NotaProdutoMatricial
    {
        private int itensPorPagina = 35;
        private int count;
        private int pag;
        private EpsonCodes lCodes;
        private Reporter lPrinter;
        private Nota nota;
        private bool verificarFaixa;
        private bool impFaixaSimples = false;
        private string textoLei = "";
        private decimal perc = 0;
        private decimal vlr = 0;

        public NotaProdutoMatricial(Nota nota, bool verificarFaixa)
        {
            this.nota = nota;
            this.verificarFaixa = verificarFaixa;
            PreparaImpressao();
        }

        private void PreparaImpressao()
        {
            lCodes = new EpsonCodes();
            lPrinter = new Reporter();

            lPrinter.Output = "LPT1";
            lPrinter.NomeArquivo = "n" + nota.Codigo.ToString() + ".txt";

            if (verificarFaixa)
            {
                textoLei = "PERMITE O APROVEITAMENTO DO CREDITO DE ICMS SOB A ALIQUOTA DE {0}%, NO VALOR DE {1}, CONFORME ARTIGO 23 DA LC 123/2006";
                var filial = FilialController.Instancia.LoadObjectById(nota.Filial.ID);
                if (filial.TabelaFaixaFatSimples != null)
                {
                    var tabela = filial.TabelaFaixaFatSimples;

                    if (tabela.PercReducaoBC > 0)
                        perc = (decimal)tabela.ICMS - (((decimal)tabela.ICMS * (decimal)tabela.PercReducaoBC) / 100);
                    else
                        perc = (decimal)tabela.ICMS;

                    perc = System.Math.Round(perc, 2);
                    vlr = (System.Math.Round((decimal)nota.TotalNota, 2) * (perc / 100));
                    impFaixaSimples = true;

                    itensPorPagina--;
                }
            }

            lPrinter.StartJob();
            count = 0;
            pag = 1;

            lPrinter.PrintText(count, 0, lCodes.Reset + lCodes.LinesInch8 + lCodes.CondensedOn);

            ImprimeCabeca();

            int cItens = 0;
            int qtCFOP = nota.NotaICMSs.Count;

            foreach (NotaItem p in nota.NotaItems)
            {
                if (qtCFOP > 1)
                {
                    lPrinter.PrintText(count, 0, String.Format("{0,-4} ", p.CFOP.CFOP));
                    lPrinter.PrintText(count, 6, String.Format("{0,-7} ", p.Produto.Codigo));
                }
                else
                {
                    lPrinter.PrintText(count, 0, String.Format("{0,-12} ", p.Produto.Codigo));
                }

                if (p.ProdutoDescReduzida != null && p.ProdutoDescReduzida.Length > 0)
                {
                    lPrinter.PrintText(count, 14, String.Format("{0,-60} ", p.ProdutoNome + "-" + p.ProdutoDescReduzida));
                }
                else
                {
                    lPrinter.PrintText(count, 14, String.Format("{0,-60} ", p.ProdutoNome));
                }

                lPrinter.PrintText(count, 76, String.Format("{0,2} ", p.SitTrib));
                lPrinter.PrintText(count, 82, String.Format("{0,4} ", p.Unidade));
                lPrinter.PrintText(count, 89, String.Format("{0,9} ", p.Quantidade));
                lPrinter.PrintText(count, 100, String.Format("{0,11} ", String.Format("{0:n}", p.Valor)));
                lPrinter.PrintText(count, 115, String.Format("{0,15} ", p.Total));
                lPrinter.PrintText(count, 133, String.Format("{0,2} ", Math.Truncate(p.AliqICMS)));

                count++;
                cItens++;

                if (cItens == itensPorPagina && (cItens * pag) < nota.NotaItems.Count)
                {
                    if (impFaixaSimples)
                    {
                        lPrinter.PrintText(count, 0, String.Format(textoLei, String.Format("{0:n2}", perc), String.Format("{0:c2}", vlr)));
                        impFaixaSimples = false;
                    }

                    pag++;
                    ImprimeRoda(false);
                    count += 6;
                    ImprimeCabeca();
                    cItens = 0;
                }
            }

            if (qtCFOP > 1)
            {
                if ((pag - 1) > 1)
                {
                    count = 84 + (59 * (pag - 2));
                }
                else
                {
                    count = 55;
                }

                var totalPag = (from n in nota.NotaICMSs
                                group n by new { n.CFOP.CFOP } into g
                                select new
                                {
                                    CFOP = g.Key.CFOP,
                                    ValorTotalProd = g.Sum(p => p.ValorTotalProdutos)
                                });

                int x, x2, t, t2, auxCount;
                x = 1;
                t = 7;
                x2 = t + 1;
                t2 = 19;
                auxCount = 1;
                foreach (var tot in totalPag)
                {
                    lPrinter.PrintText(count, x, String.Format("{0," + t.ToString() + "}", tot.CFOP));
                    lPrinter.PrintText(count, x2, String.Format("{0," + t2.ToString() + "}", tot.ValorTotalProd));
                    x += 20;

                    if (auxCount != totalPag.Count())
                    {
                        lPrinter.PrintText(count, x, "/");
                    }

                    x += 2;
                    t = x + 6;
                    x2 = t + 1;
                    t2 = x2 + 11;
                    auxCount++;
                }
            }

            if (!String.IsNullOrEmpty(nota.Observacao1))
            {
                count = 52;
                lPrinter.PrintText(count, 0, "OBS: " + nota.Observacao1);
            }

            count = 53;
            if (impFaixaSimples)
            {
                lPrinter.PrintText(count, 0, String.Format(textoLei, String.Format("{0:n2}", perc), String.Format("{0:c2}", vlr)));
                impFaixaSimples = false;
            }

            ++count;
            ++count;
            ++count;

            pag++;

            ImprimeRoda(true);

            count = 86;
            lPrinter.PrintText(count, 0, " " + lCodes.Reset);

            lPrinter.EndJob();
        }

        private void ImprimeCabeca()
        {
            var pagStr = Utilitarios.StrNumeroPagina(pag, nota.NotaItems.Count, itensPorPagina);
            int colTipo = 0;
            if (nota.Ent_Sai == InOutType.Saída)
                colTipo = 86;
            else
                colTipo = 96;

            //Marca o tipo de nota: Entrada ou Saida
            lPrinter.PrintText(++count, colTipo, "XX");
            //Imprime o numero da nota
            lPrinter.PrintText(count, 112, lCodes.ExpandedOn + lCodes.BoldOn + String.Format("{0:000000}", nota.Numero) + lCodes.BoldOff + lCodes.ExpandedOff);
            //Numero da pagina
            ++count;
            lPrinter.PrintText(++count, 112, pagStr);

            ++count;
            ++count;
            ++count;
            ++count;
            ++count;

            var nICMS = (from n in nota.NotaICMSs
                         select new
                         {
                             CFOP = n.CFOP.CFOP,
                             Nome = n.CFOP.Nome
                         }).First();

            lPrinter.PrintText(count, 0, String.Format("{0,-33}", nICMS.Nome));
            lPrinter.PrintText(count, 35, String.Format("{0,-4}", nICMS.CFOP));

            ++count;
            ++count;
            ++count;

            lPrinter.PrintText(count, 0, String.Format("{0,-83}", lPrinter.CleanText(nota.Pessoa.Nome) + " (" + nota.Pessoa.Codigo.ToString() + ")"));
            lPrinter.PrintText(count, 87, String.Format("{0,-27}", nota.Pessoa.CNPJ_CPF));
            lPrinter.PrintText(count, 119, nota.Dt.ToShortDateString());

            ++count;
            ++count;

            lPrinter.PrintText(count, 0, String.Format("{0,-58}", lPrinter.CleanText(nota.PessoaEndereco + (String.IsNullOrEmpty(nota.Complemento) ? "" : ", " + nota.Complemento))));
            lPrinter.PrintText(count, 61, String.Format("{0,-35}", lPrinter.CleanText(nota.PessoaBairro)));
            lPrinter.PrintText(count, 101, nota.PessoaCEP == null ? "" : nota.PessoaCEP.TrimEnd()); //cep

            ++count;
            ++count;

            lPrinter.PrintText(count, 0, String.Format("{0,-53}", lPrinter.CleanText(nota.PessoaCidade)));
            lPrinter.PrintText(count, 56, String.Format("{0,-20}", nota.PessoaTelefone));
            lPrinter.PrintText(count, 83, String.Format("{0,-2}", nota.PessoaUF));
            lPrinter.PrintText(count, 89, String.Format("{0,-26}", nota.PessoaInscRG));

            ++count;
            ++count;
            ++count;
            ++count;
        }

        private void ImprimeRoda(bool imp)
        {
            if ((pag - 1) > 1)
            {
                count = 84 + (60 * (pag - 2));
            }
            else
            {
                count = 56;
            }
            if (imp)
            {
                decimal? BaseICMS = nota.NotaICMSs.Sum(n => n.BaseICMS);
                decimal? ValorICMS = nota.NotaICMSs.Sum(n => n.ValorICMS);
                decimal? BaseSubst = nota.NotaICMSs.Sum(n => n.BaseSubst);
                decimal? ValorSubst = nota.NotaICMSs.Sum(n => n.ValorSubst);

                lPrinter.PrintText(count, 12, String.Format("{0,-12} ", String.Format("{0:n}", BaseICMS)));
                lPrinter.PrintText(count, 38, String.Format("{0,-12} ", String.Format("{0:n}", ValorICMS)));
                lPrinter.PrintText(count, 66, String.Format("{0,-12} ", String.Format("{0:n}", BaseSubst)));
                lPrinter.PrintText(count, 93, String.Format("{0,-12} ", String.Format("{0:n}", ValorSubst)));
                lPrinter.PrintText(count, 121, String.Format("{0,-12} ", String.Format("{0:n}", nota.TotalProduto)));

                ++count;
                ++count;

                lPrinter.PrintText(count, 121, String.Format("{0,-12} ", String.Format("{0:n}", nota.TotalNota)));

                ++count;
                ++count;
                ++count;

                lPrinter.PrintText(count, 0, String.Format("{0,-56}", lPrinter.CleanText(nota.TransNome)));
                lPrinter.PrintText(count, 77, "1");
                lPrinter.PrintText(count, 83, String.Format("{0,-14}", nota.TransPlaca));
                lPrinter.PrintText(count, 101, String.Format("{0,-2}", nota.TransPlacaUF));
                lPrinter.PrintText(count, 107, String.Format("{0,-26}", nota.TransCNPJCPF));

                ++count;
                ++count;

                lPrinter.PrintText(count, 0, String.Format("{0,-56}", lPrinter.CleanText(nota.TransEndereco)));
                lPrinter.PrintText(count, 58, String.Format("{0,-39}", lPrinter.CleanText(nota.TransCidade)));
                lPrinter.PrintText(count, 101, String.Format("{0,-2}", nota.TransPlacaUF));
                lPrinter.PrintText(count, 107, String.Format("{0,-26}", ""));

                ++count;
                ++count;

#pragma warning disable CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
                string aux = (nota.VolumeQuant == null || nota.VolumeQuant == 0 ? "" : nota.VolumeQuant.ToString());
#pragma warning restore CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'

                lPrinter.PrintText(count, 0, String.Format("{0,-15}", aux));

                lPrinter.PrintText(count, 19, String.Format("{0,-12}", nota.VolumeEspecie));
                lPrinter.PrintText(count, 37, String.Format("{0,-18}", nota.VolumeMarca));
                lPrinter.PrintText(count, 59, String.Format("{0,-22}", nota.VolumeNumero));

#pragma warning disable CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
                aux = (nota.VolumePesoBruto == null || nota.VolumePesoBruto == 0 ? "" : nota.VolumePesoBruto.ToString());
#pragma warning restore CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'

                lPrinter.PrintText(count, 89, String.Format("{0,-12}", aux));

#pragma warning disable CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
                aux = (nota.VolumePesoLiquido == null || nota.VolumePesoLiquido == 0 ? "" : nota.VolumePesoLiquido.ToString());
#pragma warning restore CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'

                lPrinter.PrintText(count, 114, String.Format("{0,-12}", aux));

                ++count;
                ++count;

                for (int i = 0; i < nota.NotaParcelas.Count; i++)
                {
                    if (i == 0)
                    {
                        lPrinter.PrintText(count, 0, String.Format("{0,-12}", "Vencimento: "));
                    }
                    else
                    {
                        lPrinter.PrintText(count, 0, String.Format("{0,-12}", "            "));
                    }
                    lPrinter.PrintText(count, 12, String.Format("{0,-13}", nota.Numero + " - " + nota.NotaParcelas[i].Parcela + "/" + nota.NotaParcelas.Count));
                    lPrinter.PrintText(count, 25, String.Format("{0,-10}", nota.NotaParcelas[i].Vencimento.ToShortDateString()));
                    lPrinter.PrintText(count, 37, String.Format("{0,-12}", nota.NotaParcelas[i].Valor));
                    count++;
                }
            }
            else
            {
                lPrinter.PrintText(count, 12, String.Format("{0,-12} ", String.Format("{0:n}", "****")));
                lPrinter.PrintText(count, 38, String.Format("{0,-12} ", String.Format("{0:n}", "****")));
                lPrinter.PrintText(count, 66, String.Format("{0,-12} ", String.Format("{0:n}", "****")));
                lPrinter.PrintText(count, 93, String.Format("{0,-12} ", String.Format("{0:n}", "****")));
                lPrinter.PrintText(count, 121, String.Format("{0,-12} ", String.Format("{0:n}", "****")));

                count += 2;

                lPrinter.PrintText(count, 121, String.Format("{0,-12} ", String.Format("{0:n}", "****")));
            }

            if ((pag - 1) > 1)
            {
                count = 84 + (85 * (pag - 2));
            }
            else
            {
                count = 81;
            }
            lPrinter.PrintText(count, 122, lCodes.BoldOn + String.Format("{0:000000}", nota.Numero) + lCodes.BoldOff);
        }

        public void ImprimeNota(bool impressaoEmVideo)
        {
            nota.BImpressa = true;
            NotaController.Instancia.Salvar(nota, Acao.Alterar);

            if (impressaoEmVideo)
                lPrinter.PreviewJob();
            else
                lPrinter.PrintJob();
        }

        public void ImprimeNotaGabarito()
        {
            EpsonCodes lCodes = new EpsonCodes();
            Reporter lPrinter = null;
            int count;
            lPrinter = new Reporter();
            lPrinter.Output = @"lpt1";
            lPrinter.NomeArquivo = "doc.txt";
            lPrinter.StartJob();
            count = 0;

            lCodes.SetPageSize(83);

            lPrinter.PrintText(count, 0, lCodes.Normal + lCodes.LinesInch8 + lCodes.CondensedOn);

            lPrinter.PrintText(count++, 0, "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "2        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "32345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "4        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "52345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "6        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "72345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "8        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "92345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "0        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "2        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "32345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "4        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "52345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "6        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "72345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "8        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "92345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "0        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "2        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "32345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "4        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "52345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "6        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "72345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "8        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "92345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "0        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "2        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "32345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "4        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "52345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "6        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "72345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "8        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "92345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "0        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "2        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "32345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "4        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "52345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "6        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "72345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "8        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "92345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "0        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "2        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "32345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "4        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "52345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "6        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "72345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "8        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "92345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "0        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "2        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "32345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "4        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "52345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "6        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "72345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "8        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "92345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "0        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "2        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "32345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "4        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "52345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "6        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "72345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "8        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "92345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "0        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count++, 0, "2        1         2         3         5         6         7         8         9         0         1         2         3        4    .");
            lPrinter.PrintText(count++, 0, "32345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            lPrinter.PrintText(count, 0, lCodes.CondensedOff + lCodes.LinesInch6 + lCodes.Normal + lCodes.Eject);

            lPrinter.EndJob();

            //lPrinter.PreviewJob();
            lPrinter.PrintJob();
        }
    }
}
