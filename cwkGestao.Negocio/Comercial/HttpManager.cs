using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Web;
using System.Net;
using System.IO;
using System.Xml;
using cwkGestao.Modelo;
using System.Runtime.InteropServices;
using System.Collections.Specialized;


namespace cwkGestao.Negocio.Comercial
{
    public class HttpManager
    {
        /// <summary>
        /// Método Responsável Por, criar um cookie que servirá para autenticação com o Manager EDoc.
        /// </summary>
        /// <param name="Cnpj">Cnpj da filial selecionada na NFSe</param>
        /// <param name="configManager">Objeto com as configurações do Manager.</param>
        /// <returns></returns>
        private Cookie cookie(string Cnpj, ConfiguracaoManager configManager, string Grupo)
        {
            Cookie cookie = new Cookie("ManagerAPIWeb", "ManagerAPIWeb");
            string str = this.RetirarPorta(configManager);
            cookie.Domain = str;
            cookie.Value = string.Format("&CNPJ={0}&usuario={1}&senha={2}&grupo={3}", new object[] { Cnpj.Replace(".", "").Replace("/", "").Replace("-", ""), configManager.usuario, configManager.senha, Grupo });
            cookie.Path = "/";
            return cookie;
        }

        /// <summary>
        /// Método Responsável Por fazer as requisições HTTP Get para o Manager EDoc.
        /// </summary>
        /// <param name="configManager">Objeto com as configurações do Manager.</param>
        /// <param name="nota">NFSe preenchida com todos os valores.</param>
        /// <param name="nomeDocumento">Se é, NFe, NFSe, CTe</param>
        /// <param name="requisicao">Se é, envia, cancela, imprime, resolve, inutiliza etc.</param>
        /// <param name="Parametros"> Lista de string com o nome dos parâmetros e valores. Ex: "NomeCidade=MARINGA"</param>
        /// <returns></returns>
        public IList<string> GetManager(ConfiguracaoManager configManager, Nota nota, string nomeDocumento, string requisicao, IList<string> Parametros)
        {
            string url = "";
            string auxiliar = "";
            IList<string> retorno = new List<string>();

            try
            {
                foreach (var item in Parametros)
                {
                    auxiliar += item.ToString() + "&";
                }

                var ParametrosUrl = auxiliar.Substring(0, auxiliar.Length - 1);

                url = String.Format(@"http://{0}/ManagerAPIWeb/{1}/{2}?{3}", configManager.host, nomeDocumento, requisicao, ParametrosUrl);

                CookieContainer cookiejar = new CookieContainer();
                cookiejar.Add(cookie(nota.Filial.CNPJ, configManager, nota.Filial.GrupoManager));

                HttpWebRequest webRequest = (HttpWebRequest)HttpWebRequest.Create(url);

                webRequest.CookieContainer = cookiejar;

                webRequest.Method = "GET";
                webRequest.ContentType = "application/x-www-form-urlencoded";

                WebResponse webResponse = (HttpWebResponse)webRequest.GetResponse();

                if (webResponse == null)
                { return null; }

                StreamReader sr = new StreamReader(webResponse.GetResponseStream());

                if (requisicao == "imprime")
                {
                    var wc = new System.Net.WebClient();
                    string uriSR = sr.ReadToEnd().Trim();
                    if (uriSR.Contains("EXCEPTION"))
                    {
                        throw new Exception("Não foi possível gerar os arquivos para impressão da nota nº" + nota.Numero, new Exception(uriSR));
                    }
                    byte[] bytes = wc.DownloadData(uriSR);

                    if (Directory.Exists(configManager.CaminhoPDFNFSE))
                    {
                        System.IO.File.WriteAllBytes(configManager.CaminhoPDFNFSE + "\\" + nota.Handle.ToString() + ".pdf", bytes);
                        retorno.Add(configManager.CaminhoPDFNFSE + "\\" + nota.Handle.ToString() + ".pdf");
                    }
                    else
                    {
                        throw new Exception("Diretório não informado na Configuração do Manager ! \r\n Não foi possível gerar o PDF.");
                    }
                }
                else
                {
                    retorno.Add(sr.ReadToEnd().Trim());
                }

                
                webResponse.Close();
                sr.Close();

                return retorno;
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        /// <summary>
        /// Método Responsável Por fazer as requisições HTTP Post para o Manager EDoc.
        /// </summary>
        /// <param name="configManager">Objeto com as configurações do Manager.</param>
        /// <param name="nota">NFSe preenchida com todos os valores.</param>
        /// <param name="nomeDocumento">Se é, NFe, NFSe, CTe</param>
        /// <param name="requisicao">Se é, envia, cancela, imprime, resolve, inutiliza etc.</param>
        /// <param name="Parametros">Parametros que serão enviados para o Manager.</param>
        /// <returns></returns>
        public IList<string> PostManager(ConfiguracaoManager configManager, Nota nota, string nomeDocumento, string requisicao,NameValueCollection Parametros)
        {
            try
            {
                Uri url = new Uri(String.Format(@"http://{0}/ManagerAPIWeb/{1}/{2}", 
                    configManager.host, nomeDocumento, requisicao), UriKind.RelativeOrAbsolute);

                IList<string> retorno = new List<string>();

                using (CookieAwareWebClient client = new CookieAwareWebClient())
                {
                    client.cookie(nota.Filial.CNPJ, configManager, nota.Filial.GrupoManager);
                    byte[] responsebytes = client.UploadValues(url, "POST", Parametros);
                    retorno.Add(Encoding.Default.GetString(responsebytes));
                    client.Dispose();

                    return retorno;
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private string RetirarPorta(ConfiguracaoManager configManager)
        {
            string str = configManager.host;
            int index = str.IndexOf(":");
            return str.Substring(0, index);
        }
    }
}
