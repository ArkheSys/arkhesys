using System;
using System.Text;
using System.Windows.Forms;
using Aplicacao;
using cwkGestao;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Util;
using cwkGestao.Negocio;

namespace CworkGestaoPlus.ControleAcesso
{
    public class ControleAcesso
    {
        #region Propriedades

        private Filial _filial;

        // Incluir aqui o EndPoint da API - CWorkControle
        private const string _urlControleAcesso = "http://localhost:53858/";
        //private const string _urlControleAcesso = "http://64.31.9.201:8081/";

        #endregion

        #region Métodos 

        private bool VerificarAcesso()
        {
            // Busca Filial
            _filial = FilialController.Instancia.GetFilialPrincipal();
            if (_filial == null) return RetornoErro("Filial não encontrada. Por favor, tente novamente");

            var chaveValidade = CriptoString.Decrypt(_filial.ChaveValidade);
            if (chaveValidade.IsNullOrEmpty())
            {
                return GerarNovaChaveAcesso();
            }

            var dataAtual = DateTime.Now.Date;

            // Valida api se status for bloqueado
            if (HealthCheckApi())
            {
                var api = new ApiHelper(_urlControleAcesso);
                var resultadoStatus = api.VerificarStatus(_filial.CNPJ).Result;
                if (!resultadoStatus)
                {
                    LimparChaveValidade();
                    MessageBox.Show("Seu acesso ao sistema expirou.", "Controle - DATA POSTERIOR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                    return false;
                }
            }

            // Valida a data retroativa
            var ultimoAcesso = Convert.ToDateTime(CriptoString.Decrypt(_filial.UltimoAcesso)).Date;
            if (ultimoAcesso > dataAtual)
            {
                LimparChaveValidade();
                MessageBox.Show("Seu acesso ao sistema expirou. Para gerar outra chave de acesso entre em contato.", "Controle - DATA POSTERIOR", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return GerarNovaChaveAcesso();
            }

            string input = "cworkparceirosac";
            byte[] key = Encoding.ASCII.GetBytes(input);

            var EncryptTest = new EncryptDecrypt(key);
            string base64DecryptString = EncryptTest.Decrypt(chaveValidade);
            var chaveValida = base64DecryptString;

            var dataLiberacao = chaveValida.Substring(0, 2) + "/" + chaveValida.Substring(2, 2) + "/" + chaveValida.Substring(4, 4);
            var qtdDiasLiberacao = chaveValida.Substring(8, 3).TrimStart('0');
            var qtdDiasAvisar = chaveValida.Substring(15, 2);
            var chaveAcesso = chaveValida.Substring(11, 4);
            var avisarDiasAVencer = chaveValida.Substring(17, 1) == "0" ? false : true;

            // Valida a data de vencimento
            var dataLimiteComDias = Convert.ToDateTime(dataLiberacao).AddDays(Convert.ToInt32(qtdDiasLiberacao));

            if (avisarDiasAVencer)
            {
                if (((int)dataLimiteComDias.Subtract(DateTime.Today).TotalDays >= 0) && (int)dataLimiteComDias.Subtract(DateTime.Today).TotalDays <= Convert.ToInt32(qtdDiasAvisar))
                {
                    MessageBox.Show("Faltam " + (int)dataLimiteComDias.Subtract(DateTime.Today).TotalDays + " dia(s) para expirar a licença!");
                }
            }

            if (dataLimiteComDias < dataAtual)
            {
                LimparChaveValidade();
                return GerarNovaChaveAcesso();
            }

            _filial.UltimoAcesso = CriptoString.Encrypt(dataAtual.ToString());
            FilialController.Instancia.Salvar(_filial, Acao.Alterar);
            return true;
        }

        private bool GerarNovaChaveAcesso()
        {
            if (HealthCheckApi())
                return GerarNovaChaveAcessoOnline();
            else
                return GerarNovaChaveAcessoOffline();
        }

        private bool GerarNovaChaveAcessoOnline()
        {
            var api = new ApiHelper(_urlControleAcesso);
            var resultadoStatus = api.VerificarStatus(_filial.CNPJ).Result;

            if (!resultadoStatus)
                return RetornoErro("Sem permissão para atualização. Por favor, entre em contato.");

            var resultadoAcesso = api.ObterDataLiberacao(_filial.CNPJ).Result;
            if (!resultadoAcesso.Valido)
                return RetornoErro("Não foi possível atualizar. Por favor, entre em contato.");

            return RetornoSucesso(resultadoAcesso.DataLiberacao);
        }

        private bool GerarNovaChaveAcessoOffline()
        {
            using (var form = new InputBox())
            {
                var numero = GerarNumero();
                var retorno = form.Show("", $"CHAVE DE ACESSO - {numero}", out string resultado);
                return retorno ? ValidarChaveAcessoOffline(numero, resultado) : RetornoErro();
            }
        }

        private bool ValidarChaveAcessoOffline(string numeroRandom, string chave)
        {
            //if (chave.IsNullOrEmpty()) return RetornoErro();

            //// Se chave de acesso digitada contém somente números e possui 8 dígitos
            //chave = chave.OnlyNumbers();
            //if (chave.Length != 9) return RetornoErro();

            //int.TryParse(numeroRandom.Substring(0, 4), out var numero);
            //int.TryParse(numeroRandom.Substring(3, 1), out var ultimoNumero);
            //var numeroValido = numero / ultimoNumero.NotZeroOrLower() + numero + DateTime.Now.Day + DateTime.Now.Month + DateTime.Now.Hour;

            //var numeroChave = chave.Substring(0, 4);
            //return numeroChave.Equals($"{numeroValido:0000}") ? RetornoSucesso(chave) : RetornoErro();

            if (chave.IsNullOrEmpty()) return RetornoErro();

            string input = "cworkparceirosac";
            byte[] key = Encoding.ASCII.GetBytes(input);

            var EncryptTest = new EncryptDecrypt(key);
            string base64DecryptString = EncryptTest.Decrypt(chave);
            var chaveValida = base64DecryptString;



            if ((chaveValida.Length == 20) && (chaveValida.Substring(11, 4) == numeroRandom))
            {
                var dataLiberacao = chaveValida.Substring(0, 2) + "/" + chaveValida.Substring(2, 2) + "/" + chaveValida.Substring(4, 4);
                var qtdDiasLiberacao = chaveValida.Substring(8, 3);
                var qtdDiasAvisar = chaveValida.Substring(15, 2);
                var chaveAcesso = chaveValida.Substring(11, 4);
                var avisarDiasAVencer = chaveValida.Substring(17, 1);
                return RetornoSucesso(chave);
            }
            else
            {
                return RetornoErro();
            }
        }

        //private bool RetornoSucesso(string chave, string message = "")
        //{
        //    int diasAdicionar;
        //    int.TryParse(chave.Substring(4, 3), out diasAdicionar);
        //    var dataLimite = DateTime.Now.AddDays(diasAdicionar).Date;

        //    return RetornoSucesso(dataLimite, message);
        //}

        //private bool RetornoSucesso(DateTime chaveValida, string message = "")
        //{
        //    if (message.IsNullOrEmpty()) message = "Chave de acesso informada com sucesso!";

        //    var dataAcesso = DateTime.Now.Date;

        //    _filial.ChaveValidade = CriptoString.Encrypt(chaveValida.ToString());
        //    _filial.UltimoAcesso = CriptoString.Encrypt(dataAcesso.ToString());
        //    FilialController.Instancia.Salvar(_filial, Acao.Alterar);

        //    MessageBox.Show(message, "Controle", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    return true;
        //}

        private bool RetornoSucesso(DateTime chave, string message = "")
        {

            return RetornoSucesso("", message);
        }

        private bool RetornoSucesso(string chaveValida, string message = "")
        {
            if (message.IsNullOrEmpty()) message = "Chave de acesso informada com sucesso!";

            var dataAcesso = DateTime.Now.Date;

            _filial.ChaveValidade = CriptoString.Encrypt(chaveValida.ToString());
            _filial.UltimoAcesso = CriptoString.Encrypt(dataAcesso.ToString());
            FilialController.Instancia.Salvar(_filial, Acao.Alterar);

            MessageBox.Show(message, "Controle", MessageBoxButtons.OK, MessageBoxIcon.Information);
            return true;
        }
        private bool RetornoErro(string message = "")
        {
            if (message.IsNullOrEmpty()) message = "Chave inválida. Por favor, tente novamente";
            MessageBox.Show(message, "Controle", MessageBoxButtons.OK, MessageBoxIcon.Error);
            return false;
        }

        private string GerarNumero()
        {
            var randon = new Random();
            var numero = randon.Next(3999);
            return $"{numero:0000}";
        }

        private void LimparChaveValidade()
        {
            _filial.ChaveValidade = "";
            _filial.UltimoAcesso = "";
            FilialController.Instancia.Salvar(_filial, Acao.Alterar);
        }

        private bool HealthCheckApi()
        {
            try
            {
                using (var webClient = new System.Net.WebClient())
                using (webClient.OpenRead($"{_urlControleAcesso}v1/healthcheck"))
                    return true;
            }
            catch
            {
                return false;
            }
        }

        #endregion

        #region Métodos Estáticos

        public static bool Validar()
        {
            return new ControleAcesso().VerificarAcesso();
        }

        #endregion
    }
}
