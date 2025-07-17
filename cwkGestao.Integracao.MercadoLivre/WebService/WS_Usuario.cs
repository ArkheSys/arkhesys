using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using MercadoLibre.SDK;
using RestSharp;
using cwkGestao.Modelo;
using Newtonsoft.Json;
using Modelo;
using cwkGestao.Negocio;

namespace cwkGestao.Integracao.MercadoLivre.WebService
{
    public class WS_Usuario
    {
        WS_Autorizacao aut = new WS_Autorizacao();
        public void GetIdUserLogado(MercadoLivreConfiguracao mConf)
        {
            Meli m = aut.InstanciaMeli();
            IRestResponse response = m.Get("/users/me?access_token=" + mConf.Token);

            if (response.Content == "")
            {
                aut.refreshToken(mConf);
                response = m.Get("/users/me?access_token=" + mConf.Token);
            }

            var user = JsonConvert.DeserializeAnonymousType(response.Content, new { id = "" });

            mConf.IDUsuarioMercadoLivre = Int32.Parse(user.id);

            MercadoLivreConfiguracaoController.Instancia.Salvar(mConf, Acao.Alterar);
        }

        public string GetNomeCidade(string id)
        {
            Meli m = aut.InstanciaMeli();
            IRestResponse response = m.Get("/cities/"+id);
            CddML resposta = JsonConvert.DeserializeObject<CddML>(response.Content);
            return resposta.Name;
        }
    }
}
