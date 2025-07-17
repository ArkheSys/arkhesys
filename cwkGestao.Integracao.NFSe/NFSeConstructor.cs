using ACBrLib.NFSe;

using System.IO;
using System.Reflection;

namespace cwkGestao.Integracao.NFSe
{
    public class NFSeConstructor
    {
        private ACBrNFSe ACBrNFSe;

        public NFSeConstructor(bool AmbienteProdutivo, int IDFilial)
        {
            ACBrNFSe = new ACBrNFSe(); // Inicializando a classe e carregando a dll
            LoadConfig($"ACBrLib_NFSe_{IDFilial}.ini");

            ACBrNFSe.Config.Ambiente = ACBrLib.Core.DFe.TipoAmbiente.taHomologacao;
            if (AmbienteProdutivo)
                ACBrNFSe.Config.Ambiente = ACBrLib.Core.DFe.TipoAmbiente.taProducao;
        }

        private void LoadConfig(string file)
        {
            ACBrNFSe.ConfigLer(file);
        }

        public string EmitirNota(string arquivoIni, string numeroLote)
        {
            try
            {
                ACBrNFSe.LimparLista();
                ACBrNFSe.CarregarINI(arquivoIni);

                var ret = ACBrNFSe.Emitir(numeroLote, 0, false);
                return ret;
            }
            catch // (Exception exception)
            {
                return null;
            }
        }

        public void ImprimirPorArquivoXml(string arquivoXml)
        {
            try
            {
                ACBrNFSe.LimparLista();
                ACBrNFSe.CarregarXML(arquivoXml);
                ACBrNFSe.Imprimir(bMostrarPreview: true);
            }
            catch
            {
            }
        }

        public string ConsultarLoteRPS(string arquivoIni, string numeroProtocolo, string numeroLote)
        {
            try
            {
                ACBrNFSe.LimparLista();
                ACBrNFSe.CarregarINI(arquivoIni);

                var ret = ACBrNFSe.ConsultarLoteRps(numeroProtocolo, numeroLote);
                return ret;
            }
            catch
            {
                return null;
            }
        }

        //public void ImprimirDANFSe(string xml)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(xml)) return;

        //        ACBrNFSe.LimparLista();
        //        ACBrNFSe.CarregarXML(xml);
        //        ACBrNFSe.ImprimirPDF();
        //    }
        //    catch // (Exception exception)
        //    {
        //    }
        //}

        //public void ImprimirNFSe(string xml)
        //{
        //    try
        //    {
        //        if (string.IsNullOrEmpty(xml)) return;

        //        ACBrNFSe.LimparLista();
        //        ACBrNFSe.CarregarXML(xml);
        //        ACBrNFSe.Imprimir(bMostrarPreview: true);
        //    }
        //    catch // (Exception exception)
        //    {
        //    }
        //}

        //public void ImprimirNFSeMaringa(bool ambienteProdutivo, string numero, string cpfCnpj, string chave)
        //{
        //    using (var client = new System.Net.WebClient())
        //    {
        //        var arquivoExe = Assembly.GetEntryAssembly().Location;
        //        var caminho = Path.Combine(Path.GetDirectoryName(arquivoExe), "");
        //        var caminhoArquivo = caminho + $@"\PdfNFSe\NFSe{numero}.pdf";

        //        if (ambienteProdutivo)
        //            client.DownloadFile($"https://nfse.ecity.maringa.pr.gov.br/ImprimirNfse/{numero}/{cpfCnpj}/{chave}", caminhoArquivo);
        //        else
        //            client.DownloadFile($"https://nfse.hom-ecity.maringa.pr.gov.br/ImprimirNfse/{numero}/{cpfCnpj}/{chave}", caminhoArquivo);

        //        if (File.Exists(caminhoArquivo))
        //            System.Diagnostics.Process.Start(caminhoArquivo);
        //    }
        //}

        public string CancelarNFSe(string arquivoINI)
        {
            //try
            //{
                if (string.IsNullOrEmpty(arquivoINI)) return null;

                ACBrNFSe.LimparLista();
                ACBrNFSe.CarregarINI(arquivoINI);
                return ACBrNFSe.Cancelar(arquivoINI);
            //}
            //catch (Exception exception)
            //{
            //}
        }

        //public T DeserializeObject<T>(string xml) where T : class
        //{
        //    if (string.IsNullOrEmpty(xml))
        //        throw new InvalidOperationException("Empty XML ERROR");

        //    using (var stringReader = new StringReader(xml))
        //    {
        //        var serializer = new XmlSerializer(typeof(T));
        //        return (T)serializer.Deserialize(stringReader);
        //    }
        //}
    }
}
