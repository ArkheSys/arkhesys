using System;
using MatrixReporter;

namespace cwkGestao.Negocio.RelatoriosMatriciais
{
    public static class Utilitarios
    {
        public static string RetiraAcentos(string texto)
        {
            string comAcentos = "ÄÅÁÂÀÃäáâàãÉÊËÈéêëèÍÎÏÌíîïìÖÓÔÒÕöóôòõÜÚÛüúûùÇç";
            string semAcentos = "AAAAAAaaaaaEEEEeeeeIIIIiiiiOOOOOoooooUUUuuuuCc";
            for (int i = 0; i < comAcentos.Length; i++)
            {
                texto = texto.Replace(comAcentos[i].ToString(), semAcentos[i].ToString());
            }
            return texto;
        }

        /// <summary>
        /// Método que calcula a quantidade de páginas do pedido
        /// </summary>
        /// <param name="pag">Número da página atual</param>
        /// <param name="qtItens">Quantidade de itens do pedido</param>
        /// <param name="itensPag">Quantidade de itens por página</param>
        /// <returns>Quantidade de páginas</returns>
        public static int CalculaQtPag(int pag, int qtItens, int itensPag)
        {
            if (qtItens < itensPag)
            {
                return 1;
            }

            if (qtItens % itensPag > 0)
            {
                return Convert.ToInt32(Math.Truncate((decimal)qtItens / itensPag) + 1);
            }
            else
            {
                return Convert.ToInt32(qtItens / itensPag);
            }
        }

        /// <summary>
        /// Método que retorna uma string com o número da página
        /// </summary>
        /// <param name="pag">Número da página atual</param>
        /// <param name="qtItens">Quantidade de itens do pedido</param>
        /// <param name="itensPag">Quantidade de itens por página</param>
        /// <returns>string no formato "nº/qt"</returns>
        public static string StrNumeroPagina(int pag, int qtItens, int itensPag)
        {
            return pag.ToString() + "/" + CalculaQtPag(pag, qtItens, itensPag).ToString();
        }

        public static void ImprimeGabaritoNota(int qtdLinhas)
        {
            EpsonCodes lCodes = new EpsonCodes();

            Reporter lPrinter = new Reporter();

            lPrinter.Output = "LPT1";

            lPrinter.StartJob();

            lPrinter.PrintText(0, 0, lCodes.Reset);

            lCodes.SetPageSize(50);
            lPrinter.PrintText(0, 1, lCodes.LinesInch8 + lCodes.CondensedOn);

            string linha1 = "1234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456";
            string linha2 = "         1         2         3         4         5         6         7         8         9         0         1         2         3      ";

            for (int i = 1; i < (qtdLinhas - 3); i++)
            {
                if ((i % 2) == 0)
                    lPrinter.PrintText(i, 0, linha1);
                else
                    lPrinter.PrintText(i, 0, linha2);
            }

            lPrinter.PrintText((qtdLinhas - 2), 133, lCodes.CondensedOff + lCodes.Reset);
            //lPrinter.PrintText(qtdLinhas, 135, lCodes.Reset);
            //lPrinter.PrintText(qtdLinhas, 136, lCodes.Eject);

            lPrinter.EndJob();

            //lPrinter.PreviewJob();
            lPrinter.PrintJob();
        }

    }
}
