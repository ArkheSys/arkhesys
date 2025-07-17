using System;
using System.Linq;
using System.IO;
using System.Net;
using cwkGestao.Modelo.Util;
using cwkGestao.Modelo;
using System.Net.Mime;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace cwkGestao.Negocio.Utils
{
    public class AnexoEmail
    {
        public string NomeAnexo { get; set; }
        public Stream Anexo { get; set; }

        public AnexoEmail() { }
    }

    public class EmailUtil
    {
        /// <summary>
        /// clase de envio de email genérica.
        /// </summary>
        /// <param name="destino"></param>
        /// <param name="assunto"></param>
        /// <param name="texto"></param>
        /// <param name="files"></param>
        /// <param name="filial"></param>
        /// <param name="remessa">esse parametro não é obrigatório caso não venha de uma rotina que utilize a remessa como configuração.
        /// passar null nesse parametro caso não for utilizá-lo</param>
        /// <returns></returns>
        public static bool EnviarEmail(String destino, String assunto, String texto, FileInfo[] files, cwkGestao.Modelo.Filial filial, Remessa remessa)
        {
            return EnviarEmail(destino, assunto, texto, files, filial, remessa, true);
        }
        public static bool EnviarEmail(String destino, String assunto, String texto, FileInfo[] files, cwkGestao.Modelo.Filial filial, Remessa remessa, bool tratarErro, AnexoEmail[] Streams = null)
        {
            try
            {
                string ServidorSMTP = "", Usuario = "", Senha = "", textoEnvio = "";
                string html = "<div align=\"center\">Trial version can convert up to 30000 symbols.<br><a href=\"http://www.sautinsoft.com/convert-rtf-to-html/order.php\">Get the full featured version!</a></div>";
                int porta = 0;
                bool ssl = false;

                if (!StringUtil.VerificaSeEHtml(texto))
                {
                    if (!texto.Contains(html))
                        textoEnvio = texto;
                    else
                        textoEnvio = StringUtil.ConvertRtfToHtml(texto).Replace(html, "");
                }
                else
                {
                    textoEnvio = texto.Replace(html, "");
                }

                var userLogado = cwkControleUsuario.Facade.getUsuarioLogado;

                if (!String.IsNullOrEmpty(userLogado.Email))
                {
                    ServidorSMTP = userLogado.SMTP;
                    Usuario = userLogado.Email;
                    Senha = userLogado.SenhaEmail;
                    porta = Convert.ToInt32(userLogado.Porta);
                    ssl = userLogado.SSL;
                }
                else
                {
                    ServidorSMTP = filial.ServidorSMTP;
                    Usuario = filial.UsuarioEmail;
                    Senha = filial.SenhaEmail;
                    porta = Convert.ToInt32(filial.PortaSmtp);
                    ssl = filial.GMailAutenticacao;
                }

                System.Net.Mail.MailMessage message = new System.Net.Mail.MailMessage(Usuario, destino, assunto, textoEnvio);
                if (ConfiguracaoController.Instancia.GetAll().FirstOrDefault().SolicitaRecebimentoEmail == true)
                {
                    message.DeliveryNotificationOptions = System.Net.Mail.DeliveryNotificationOptions.OnSuccess;
                }

                if (remessa != null && remessa.SolicitaConfirmaEmailBoleto)
                    message.Headers.Add("Disposition-Notification-To", Usuario);
                else
                {
                    var configTel = Tel_ConfiguracaoController.Instancia.GetAll().FirstOrDefault();
                    if (configTel != null)
                    {
                        if (configTel.SolicitaConfirmaEmailTelefonia)
                        {
                            message.Headers.Add("Disposition-Notification-To", Usuario);
                        }
                    }
                }
                message.IsBodyHtml = true;

                if (Streams != null) /* Necessário para os relatórios da devExpress */
                {
                    System.Net.Mail.Attachment[] anexos = new System.Net.Mail.Attachment[Streams.Length];
                    for (int i = 0; i < Streams.Length; i++)
                    {
                        Streams[i].Anexo.Position = 0;
                        anexos[i] = new System.Net.Mail.Attachment(Streams[i].Anexo, MediaTypeNames.Application.Pdf);
                        anexos[i].ContentDisposition.FileName = Streams[i].NomeAnexo;
                    }

                    foreach (System.Net.Mail.Attachment anexo in anexos)
                        message.Attachments.Add(anexo);
                }
                else
                {
                    System.Net.Mail.Attachment[] anexos = new System.Net.Mail.Attachment[files.Length];
                    for (int i = 0; i < files.Length; i++)
                        anexos[i] = new System.Net.Mail.Attachment(files[i].FullName);

                    foreach (System.Net.Mail.Attachment anexo in anexos)
                        message.Attachments.Add(anexo);
                }

                ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate;

                System.Net.Mail.SmtpClient client = new System.Net.Mail.SmtpClient(ServidorSMTP, porta);
                client.EnableSsl = ssl;
                System.Net.NetworkCredential credencial = new System.Net.NetworkCredential(Usuario, Senha);
                client.UseDefaultCredentials = false;
                client.Credentials = credencial;
                client.Send(message);
                return true;
            }
            catch (Exception e)
            {
                if (tratarErro)
                {
                    System.Windows.Forms.MessageBox.Show("Erro no envio do email:\n" + e.Message);
                    return false;
                }
                else
                {
                    throw e;
                }
            }
        }

        public static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;

        }
    }
}
