using System;
using System.Net.Http;
using System.Threading.Tasks;
using cwkGestao.Modelo.Util;
using Newtonsoft.Json;

namespace CworkGestaoPlus.ControleAcesso
{
    public class ApiHelper
    {
        #region Propriedades

        private readonly string _uriBase;

        #endregion

        #region Construtores

        public ApiHelper(string uriBase)
        {
            _uriBase = uriBase;
        }

        #endregion

        #region Métodos

        public async Task<bool> VerificarStatus(string cnpj)
        {
            try
            {
                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_uriBase);

                    var uriRequest = $"v1/clientes/{cnpj.OnlyNumbers()}/status";
                    var response = client.GetAsync(uriRequest);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var content = await response.Result.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ResultViewModel>(content);

                        return result.Data.ToString() == "LIBERADO";
                    }

                    return false;
                }
            }
            catch
            {
                return false;
            }
        }

        public async Task<ResultadoControleAcesso> ObterDataLiberacao(string cnpj)
        {
            var acesso = new ResultadoControleAcesso();
            acesso.Valido = false;

            try
            {
                var data = new
                {
                    Cnpj = cnpj.OnlyNumbers(),
                    NomeComputador = Environment.MachineName
                };

                using (var client = new HttpClient())
                {
                    client.BaseAddress = new Uri(_uriBase);
                    var uriRequest = $"v1/clientes/liberacao";
                    var response = client.PostAsJsonAsync(uriRequest, data);
                    if (response.Result.IsSuccessStatusCode)
                    {
                        var content = await response.Result.Content.ReadAsStringAsync();
                        var result = JsonConvert.DeserializeObject<ResultViewModel>(content);

                        if (result.Success)
                        {
                            acesso.Valido = true;
                            acesso.DataLiberacao = Convert.ToDateTime(result.Data.ToString());
                        }

                        return acesso;
                    }

                    return acesso;
                }
            }
            catch
            {
                return acesso;
            }
        }

        #endregion
    }
}
