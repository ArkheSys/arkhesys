using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Text.RegularExpressions;
using System.Security.Permissions;
using cwkGestao.Negocio.Comercial;
using System.Reflection;
using System.Globalization;
using System.Diagnostics;
using Microsoft.Win32;
using System.Threading;
using System.Management;

namespace cwkGestao.Negocio.Utils
{
    public class NFSEUtils
    {
        #region sanitizador de entradas
        public static string Sanitizador(string str)
        {
            Regex.Replace(str, @"[\r\n\x00\x1a\\'""]", @"\$0");
            int counter = 0;
            string strLower = str.ToLower();
            if (strLower.Contains("drop"))
            {
                strLower = strLower.Replace("drop", "");
                counter++;
            }
            if (strLower.Contains("--"))
            {
                strLower = strLower.Replace("--", "");
                counter++;
            }
            if (strLower.Contains("alter"))
            {
                strLower = strLower.Replace("alter", "");
                counter++;
            }
            if (strLower.Contains("update"))
            {
                strLower = strLower.Replace("update", "");
                counter++;
            }
            if (strLower.Contains("select"))
            {
                strLower = strLower.Replace("select", "");
                counter++;
            }
            if (strLower.Contains("delete"))
            {
                strLower = strLower.Replace("delete", "");
                counter++;
            }
            if (counter >0)
            {
                return strLower;
            }
                
            return str;
        }
        #endregion

        #region tratador de excecoes I/O
        public static Exception Tratador(Exception e)
        {
            string padraoErro = "Erro de ";
            IOException io = new IOException(padraoErro+="IO: \r\n" + e.Message, e);
            UnauthorizedAccessException uae = new UnauthorizedAccessException(padraoErro += "Permissões: \r\n" + e.Message, e);
            ArgumentException ae = new ArgumentException(padraoErro += "Caminho de Arquivo Vazio: \r\n" + e.Message, e);
            ArgumentNullException ane = new ArgumentNullException(padraoErro += "Caminho de Arquivo Vazio: \r\n" + e.Message, e);
            PathTooLongException ptle = new PathTooLongException(padraoErro += "Caminho de Arquivo Muito Longo: \r\n" + e.Message, e);
            DirectoryNotFoundException dnfe = new DirectoryNotFoundException(padraoErro += "Diretório Não Encontrado: \r\n" + e.Message, e);
            NotSupportedException nse = new NotSupportedException(padraoErro += "Operação Não Suportada: \r\n" + e.Message, e);


            if (e.GetType() == io.GetType())
            {
                return io;
            }
            if (e.GetType() == uae.GetType())
            {
                return uae;
            }
            if (e.GetType() == ae.GetType())
            {
                return ae;
            }
            if (e.GetType() == ane.GetType())
            {
                return ane;
            }
            if (ptle.GetType() == e.GetType())
            {
                return ptle;
            }
            if (dnfe.GetType() == e.GetType())
            {
                return dnfe;
            }
            if (nse.GetType() == e.GetType())
            {
                return nse;
            }
            return e;
        }
        #endregion

        [PermissionSet(SecurityAction.Demand, Name = "FullTrust")]
        public static void Monitor(string path)
        {
            FileSystemWatcher monitor = new FileSystemWatcher();
            monitor.Path = path;
            monitor.NotifyFilter = NotifyFilters.LastAccess | NotifyFilters.LastWrite
           | NotifyFilters.FileName | NotifyFilters.DirectoryName;
            monitor.Filter = "*.txt";
            monitor.Created += new FileSystemEventHandler(NFSEManager.Lancador);
            monitor.EnableRaisingEvents = true;

        }

        public void ValidadorNFSE(NFSE Nfse)
        {
            HeaderControleRPS controle = Nfse.Controle;
            HeaderRPS header = Nfse.Cabecalho;
            IList<DadosRPS> dados = Nfse.Dados;
            
            PropertyInfo[] propControle = controle.GetType().GetProperties();
            PropertyInfo[] propHeader = header.GetType().GetProperties();
            

            foreach (PropertyInfo prop in propControle)
            {
                if (prop.GetValue(controle, null) == null)
                {
                    //do sth
                }
            }

            foreach (PropertyInfo prop in propHeader)
            {
                if (prop.GetValue(header, null) == null)
                {
                    //do sth
                }
            }

            foreach (DadosRPS RPS in dados)
            {
                PropertyInfo[] propDados = RPS.GetType().GetProperties();
                foreach (PropertyInfo prop in propDados)
                {
                    if (prop.GetValue(RPS, null) == null)
                    {
                        //do sth
                    }
                }
            }
        }

        public static StringBuilder EncodeAndAddItem(StringBuilder baseRequest, string key, string dataItem)
        {
            if (baseRequest == null)
            {
                baseRequest = new StringBuilder();
            }

            baseRequest.Append("=");
            baseRequest.Append(key);

            return baseRequest.AppendLine(System.Web.HttpUtility.UrlEncode(dataItem));
        }

        public static string RemoveAcentos(string pString)
        {
            //Remove acentos dos nomes das cidades.
            string aux = "";

            if ((pString == "") || (pString == null))
                return "";

            string normalizedString = pString.Normalize(NormalizationForm.FormD);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < normalizedString.Length; i++)
            {
                UnicodeCategory uc = CharUnicodeInfo.GetUnicodeCategory(normalizedString[i]);
                if (uc != UnicodeCategory.NonSpacingMark)
                {
                    sb.Append(normalizedString[i]);
                }
            }
            aux = (sb.ToString().Normalize(NormalizationForm.FormC));

            return aux;
        }

        public static void ImprimirNFSELote(IList<string> caminhosNotas)
        {
            IList<string> filePaths = new List<string>();
            foreach (string file in caminhosNotas)
            {
                if (File.Exists(file))
                {
                    filePaths.Add(file);
                }
            }
            try
            {
                foreach (string pdf in filePaths)
                {
                    ImprimirPDF(pdf, GetImpressoraPadrao());
                    Thread.Sleep(500);
                }
                KillProc("AcroRd32");
            }
            catch (Exception ex)
            {
                throw new Exception("Houve um problema ao imprimir. Instale o Adobe Reader", ex);
            }

        }
        public static string GetImpressoraPadrao()
        {
            ObjectQuery query = new ObjectQuery(
                                    "Select * From Win32_Printer " +
                                    "Where Default = True");

            ManagementObjectSearcher searcher =
                new ManagementObjectSearcher(query);
            ManagementObjectCollection moc = searcher.Get();
            string printerPadrao = "";
            foreach (ManagementObject mo in moc)
            {
                Console.WriteLine(mo["Name"] + "\n");
                if (Convert.ToBoolean(mo.GetPropertyValue("Default")) == true)
                {
                    printerPadrao = mo["Name"].ToString();
                    break;
                }
            }
            return printerPadrao;
        }

        public static void KillProc(string nomeProc)
        {
            Thread.Sleep(2000);
            Process[] ps = Process.GetProcessesByName(nomeProc);


            foreach (Process p in ps)
                try
                {
                    if (!p.HasExited)
                    {
                        p.Kill();
                    }
                }
                catch (Exception)
                {
                }
        }

        public static void ImprimirPDF(string file, string printer)
        {
            try
            {
                ProcessStartInfo psi = new ProcessStartInfo(Registry.LocalMachine.OpenSubKey(
                        @"SOFTWARE\Microsoft\Windows\CurrentVersion" +
                        @"\App Paths\AcroRd32.exe").GetValue("").ToString(),
                        string.Format("/t /h \"{0}\" \"{1}\"", file, printer));
                psi.UseShellExecute = false;
                psi.WindowStyle = System.Diagnostics.ProcessWindowStyle.Minimized;
                psi.RedirectStandardInput = true;
                psi.RedirectStandardOutput = false;
                psi.CreateNoWindow = true;
                Process.Start(psi);
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
