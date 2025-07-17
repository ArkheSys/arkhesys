using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MercadoLibre.SDK;
using cwkGestao.Negocio;
using cwkGestao.Modelo;
using RestSharp;
using System.Net;
using Newtonsoft.Json;

namespace cwkGestao.Integracao.MercadoLivre.WebService
{
    public class WS_Autorizacao
    {
        MercadoLivreConfiguracao conf = MercadoLivreConfiguracaoController.Instancia.GetAll().First();

        public Meli InstanciaMeli()
        {
            Meli m = new Meli(conf.Aplicacao, conf.ChaveSecreta);
            return m;
        }

        public void Autorizar(string auth_code)
        {
            Meli meli = InstanciaMeli();
            meli.Authorize(auth_code, conf.URL);
            conf.Token = meli.AccessToken;
            conf.RefreshToken = meli.RefreshToken;
            MercadoLivreConfiguracaoController.Instancia.Salvar(conf, Acao.Alterar);
        }

        public void refreshToken(MercadoLivreConfiguracao m)
        {
            Meli meli = InstanciaMeli();

            var request = new RestRequest("/oauth/token?grant_type=refresh_token&client_id={client_id}&client_secret={client_secret}&refresh_token={refresh_token}", Method.POST);
            request.AddParameter("client_id", m.Aplicacao, ParameterType.UrlSegment);
            request.AddParameter("client_secret", m.ChaveSecreta, ParameterType.UrlSegment);
            request.AddParameter("refresh_token", m.RefreshToken, ParameterType.UrlSegment);

            request.AddHeader("Accept", "application/json");

            var response = meli.ExecuteRequest(request);

            if (response.StatusCode.Equals(HttpStatusCode.OK))
            {
                var token = JsonConvert.DeserializeAnonymousType(response.Content, new { refresh_token = "", access_token = "" });
                m.RefreshToken = token.refresh_token;
                m.Token = token.access_token;
                MercadoLivreConfiguracaoController.Instancia.Salvar(m, Acao.Alterar);
            }
            else
            {
                throw new Exception(response.Content.ToString());
            }
        }
    }
}
