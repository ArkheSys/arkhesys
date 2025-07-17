using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MatrixReporter;

namespace BLL
{
    public class Gabarito
    {
        public static void ImprimeGabarito(int quantidade, bool condensado, int lpi, string caminho)
        {
            EpsonCodes lCodes = new EpsonCodes();
            Reporter lPrinter = null;
            int count = 0;
            lPrinter = new Reporter();
            lPrinter.Output = caminho;
            lPrinter.NomeArquivo = "gab.txt";
            lPrinter.StartJob();

            string inicio = lCodes.Reset;
            string fim = String.Empty;
            if (condensado)
            {
                inicio += lCodes.CondensedOn;
                fim += lCodes.CondensedOff;
            }
            if (lpi == 6)
            {
                inicio += lCodes.LinesInch6;
            }
            else if (lpi == 8)
            {
                inicio += lCodes.LinesInch8;
            }
            inicio += lCodes.SetPageSize(quantidade);
            //fim += lCodes.Eject;

            //lPrinter.PrintText(0, 0, inicio);
            //for (int i = 1; i < 18; i++)
            //{
            //    lPrinter.PrintText(i, 0, ".");
            //}
            //lPrinter.PrintText(18, 1, fim);

            lPrinter.PrintText(count++, 0, inicio + "12345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
            int aux = 2;
            while (count < quantidade)
            {
                if (count % 2 == 0)
                    lPrinter.PrintText(count, 0, aux + "2345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012345678901234567890123456789012456789012345");
                else
                    lPrinter.PrintText(count, 0, aux + "        1         2         3         4         5         6         7         8         9         0         1         2        3    .");

                count++;
                aux++;
                if (aux == 10)
                    aux = 0;
            }
            lPrinter.PrintText(count, 0, fim);

            lPrinter.EndJob();
            lPrinter.PreviewJob();
            lPrinter.PrintJob();
        }
    }
}
