using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using cwkGestao.Modelo;

namespace cwkGestao.Negocio.Comercial
{
    /// <summary>
    /// Classe Responsável pelo POST com cookie.
    /// </summary>
    public class CookieAwareWebClient : WebClient
    {

        private CookieContainer m_container = new CookieContainer();

        /// <summary>
        /// Método Responsável Por, criar um cookie que servirá para autenticação com o Manager EDoc.
        /// </summary>
        /// <param name="Cnpj">Cnpj da filial selecionada na NFSe</param>
        /// <param name="configManager">Objeto com as configurações do Manager.</param>
        /// <returns></returns>
        public void cookie(string Cnpj, ConfiguracaoManager configManager, string Grupo)
        {
            Cookie cookie = new Cookie("ManagerAPIWeb", "ManagerAPIWeb");
            string str = this.RetirarPorta(configManager);
            cookie.Domain = str;
            cookie.Value = string.Format("&CNPJ={0}&usuario={1}&senha={2}&grupo={3}", new object[] { Cnpj.Replace(".", "").Replace("/", "").Replace("-", ""), configManager.usuario, configManager.senha, Grupo });
            cookie.Path = "/";
            this.m_container.Add(cookie);
        }

        protected override WebRequest GetWebRequest(Uri address)
        {
            WebRequest request = base.GetWebRequest(address);
            if (request is HttpWebRequest)
            {
                (request as HttpWebRequest).CookieContainer = m_container;
            }
            return request;
        }

        private string RetirarPorta(ConfiguracaoManager configManager)
        {
            string str = configManager.host;
            int index = str.IndexOf(":");
            return str.Substring(0, index);
        }
    }
}
