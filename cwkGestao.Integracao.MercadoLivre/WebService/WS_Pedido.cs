using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using RestSharp;
using MercadoLibre.SDK;
using Modelo;
using Newtonsoft.Json;
using Modelo.VendaAtributos;

namespace cwkGestao.Integracao.MercadoLivre.WebService
{
    public class WS_Pedido
    {
        WS_Autorizacao ws_autorizacao = new WS_Autorizacao();

        public Venda GetAllPedidos(MercadoLivreConfiguracao mConf, Meli meli)
        {
            IRestResponse response = meli.Get("/orders/search/recent?seller=" + mConf.IDUsuarioMercadoLivre
                + "&sort=date_desc" + "&access_token=" + mConf.Token);

            if ((response.Content.Contains("expired"))||(response.Content.Contains("invalid_token"))||(response.Content.Contains("Error converting access token")))
            {
                ws_autorizacao.refreshToken(mConf);
                response = meli.Get("/orders/search/recent?seller=" + mConf.IDUsuarioMercadoLivre
                + "&sort=date_desc" + "&access_token=" + mConf.Token);
            }

            Venda resposta = JsonConvert.DeserializeObject<Venda>(response.Content);

            return resposta;
        }

        public Result GetPedidoById(MercadoLivreConfiguracao mConf, Meli meli, string id)
        {
            IRestResponse response = meli.Get("/orders/"+id+"?" + "access_token=" + mConf.Token);
            if (response.Content.Contains("expired") || response.Content.Contains(""))
            {
                ws_autorizacao.refreshToken(mConf);
                response = meli.Get("/orders/" + id + "?" + "access_token=" + mConf.Token);
            }

            return JsonConvert.DeserializeObject<Result>(response.Content);
        }
    }
}
