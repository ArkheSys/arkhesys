using System.Collections.Generic;
using cwkGestao.Modelo;
using System.IO;
using MatrixReporter;

namespace cwkGestao.Negocio.ImpOrdemServico
{
    public class ImpressaoMatricial
    {
        private OSOrdemServico osOrdemServico;
#pragma warning disable CS0169 // O campo "ImpressaoMatricial.impressaoFormatterFactory" nunca é usado
        private ImpressaoFormatterFactory impressaoFormatterFactory;
#pragma warning restore CS0169 // O campo "ImpressaoMatricial.impressaoFormatterFactory" nunca é usado

        public ImpressaoMatricial(OSOrdemServico osOrdemServico)
        {
            this.osOrdemServico = osOrdemServico;
            ImpressaoFormatterFactory formatterFactory = new ImpressaoFormatterFactory();
            IList<string> linhas = formatterFactory.Configure(osOrdemServico);

            IList<string> impressao = formatterFactory.GetImpressao();

            Reporter lPrinter = new Reporter();
            EpsonCodes lCodes = new EpsonCodes();
            lPrinter.Output = @"\\dev03\EPSON";
            lPrinter.NomeArquivo = "osservico" + osOrdemServico.Codigo +".txt";

            lPrinter.StartJob();
            lPrinter.PrintText(0,0,lCodes.Reset);

            for (int i = 0; i < impressao.Count; i++)
            {
                string linha = impressao[i];
                lPrinter.PrintText(i, 0, linha);
            }

            lPrinter.EndJob();
            lPrinter.PrintJob();
            lPrinter.PreviewJob();


        }
    }
}
