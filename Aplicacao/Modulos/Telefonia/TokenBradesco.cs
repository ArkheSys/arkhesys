using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;

namespace Aplicacao.Modulos.Telefonia
{

    public class CobrancaPIXBradescoCalendario
    {
        [JsonProperty("dataDeVencimento")]
        public string DataVencimento { get; set; }

        [JsonProperty("validadeAposVencimento")]
        public int ValidadeAposVencimento { get; set; }
    }

    public class CobrancaPIXBradescoDevedor
    {
        [JsonProperty("cpf")]
        public string CPF { get; set; }

        [JsonProperty("cnpj")]
        public string CNPJ { get; set; }

        [JsonProperty("nome")]
        public string Nome { get; set; }
    }

    public class CobrancaPIXBradescoValor
    {
        [JsonProperty("original")]
        public string Original { get; set; }
    }

    public class CobrancaPIXBradesco
    {
        [JsonProperty("valor")]
        public CobrancaPIXBradescoValor Valor { get; set; }

        [JsonProperty("chave")]
        public string Chave { get; set; }

        [JsonProperty("calendario")]
        public CobrancaPIXBradescoCalendario Calendario { get; set; }

        [JsonProperty("devedor")]
        public CobrancaPIXBradescoDevedor Devedor { get; set; }

    }

    public class TokenBradesco
    {
        [JsonProperty("access_token")]
        public string AccessToken { get; set; }

        [JsonProperty("token_type")]
        public string TokenType { get; set; }

        [JsonProperty("expires_in")]
        public int ExpiresIn { get; set; }

        [JsonProperty("refresh_token")]
        public string RefreshToken { get; set; }
    }

    public class ResultadoCriacaoPixValor
    {
        [JsonProperty("original")]
        public string Original { get; set; }

        [JsonProperty("modalidadeAlteracao")]
        public int ModalidadeAlteracao { get; set; }
    }

    public class Loc
    {
        public string location { get; set; }
    }

    public class Cobv
    {
        public Loc loc { get; set; }
    }

    public class ResultadoCriacaoPix
    {
        public Cobv cobv { get; set; }
        public string emv { get; set; }
        public string base64 { get; set; }
    }

    public static class TokenBracescoUtils
    {
        public static TokenBradesco GetToken(string URL, string arquivoCert, string clientID, string clientSecret)
        {
            bool validateServerCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return sslPolicyErrors == SslPolicyErrors.None; }

            WebRequestHandler handler = new WebRequestHandler();
            X509Certificate certificate = X509Certificate.CreateFromCertFile(arquivoCert);
            handler.ClientCertificates.Add(certificate);
            handler.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(validateServerCertificate);
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;

            using (var client = new HttpClient(handler))
            {
                client.Timeout = new TimeSpan(0, 0, 0, 30, 0);
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Basic", Convert.ToBase64String(Encoding.UTF8.GetBytes($"{clientID}:{clientSecret}")));
                var requestBody = new FormUrlEncodedContent(new List<KeyValuePair<string, string>> { new KeyValuePair<string, string>("grant_type", "client_credentials") });
                var request = client.PostAsync(URL, requestBody).Result;
                var response = request.Content.ReadAsStringAsync().Result;
                return JsonConvert.DeserializeObject<TokenBradesco>(response);
            }
        }

        public static ResultadoCriacaoPix GerarCobrancaPIX(string URL, string arquivoCert, TokenBradesco token, CobrancaPIXBradesco cobranca)
        {
            bool validateServerCertificate(object sender, X509Certificate cert, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return sslPolicyErrors == SslPolicyErrors.None; }

            WebRequestHandler handler = new WebRequestHandler();
            X509Certificate certificate = X509Certificate.CreateFromCertFile(arquivoCert);
            handler.ClientCertificates.Add(certificate);
            handler.ServerCertificateValidationCallback = new RemoteCertificateValidationCallback(validateServerCertificate);
            handler.ClientCertificateOptions = ClientCertificateOption.Manual;

            using (var client = new HttpClient(handler))
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(token.TokenType, token.AccessToken);

                var myContent = JsonConvert.SerializeObject(cobranca);
                var buffer = Encoding.UTF8.GetBytes(myContent);
                var byteContent = new ByteArrayContent(buffer);
                byteContent.Headers.ContentType = new MediaTypeHeaderValue("application/json");
                var request = client.PutAsync(URL, byteContent).Result;
                var response = request.Content.ReadAsStringAsync().Result;
                if (request.StatusCode == System.Net.HttpStatusCode.PreconditionFailed)
                    throw new Exception(response);

                return JsonConvert.DeserializeObject<ResultadoCriacaoPix>(response);
            }
        }
    }
}
