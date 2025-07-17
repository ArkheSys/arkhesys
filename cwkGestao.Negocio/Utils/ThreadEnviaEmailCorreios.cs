using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Negocio;
using cwkGestao.Repositorio;
using System.Threading;
using cwkGestao.Modelo.Util;
using System.Net.Mail;
using System.Net;

namespace cwkGestao.Negocio.Utils
{
    public class ThreadEnviaEmailCorreios
    {
        public bool Executar { get; set; }

        public ThreadEnviaEmailCorreios()
        {
            Executar = true;
        }

        public void ExecutarThread()
        {
            while (Executar)
            {
                var config = ConfiguracaoController.Instancia.GetAll().FirstOrDefault();
                var horaAtual = String.Format("{0:HH:mm}", DateTime.Now);

                if (horaAtual == config.HoraEnvioEmail)
                {
                    CarregaDados();
                }

                Thread.Sleep(60000);
            }

        }

        public void CarregaDados()
        {
            var ExpedicaoHistorico = ExpedicaoHistoricoController.Instancia.GetAll();
            var Filial = FilialController.Instancia.GetFilialPrincipal();
            int cont = 0;

            foreach (var item in ExpedicaoHistorico)
            {
                var _pedido = PedidoController.Instancia.LoadObjectById(item.ExpedicaoRastreamento.Pedido.ID);
                var _StatusEntEventoCorreio = StatusEntEventoCorreioController.Instancia.LoadObjectById(_pedido.StatusEntEventoCorreio.ID);

                foreach (var PessoaEmail in _pedido.Pessoa.PessoaEmails)
                {
                    switch (item.ExpedicaoRastreamento.Tipo)
                    {
                        case "BDE":
                            if (!_pedido.bEnviadoEmail && item.ExpedicaoRastreamento.Status == "01")
                            {
                                EnviarEmail(PessoaEmail.Email, "BringIT", _StatusEntEventoCorreio.StatusEntrega.Desc_HTML, Filial);
                                cont++;

                                if (cont == _pedido.Pessoa.PessoaEmails.Count)
                                {
                                    _pedido.bEnviadoEmail = true;
                                }
                            }
                            else if (!_pedido.bEnviadoEmail)
                            {
                                EnviarEmail(PessoaEmail.Email, "BringIT", _StatusEntEventoCorreio.StatusEntrega.Desc_HTML, Filial);
                            }
                            break;
                        case "BDI": goto case "BDE";
                        case "BDR": goto case "BDE";
                        default:
                            EnviarEmail(PessoaEmail.Email, "BringIT", _StatusEntEventoCorreio.StatusEntrega.Desc_HTML, Filial);
                            break;
                    }
                }

                PedidoController.Instancia.Salvar(_pedido, Acao.Alterar);
            }  
        }

        private bool EnviarEmail(String destino, String assunto, String texto, cwkGestao.Modelo.Filial filial)
        {

            SmtpClient cliente = new SmtpClient(filial.ServidorSMTP, filial.PortaSmtp /* TLS */);
            MailMessage mensagem = new MailMessage(new MailAddress(filial.UsuarioEmail), new MailAddress(destino));

            try
            {
                cliente.EnableSsl = filial.GMailAutenticacao;
                cliente.Credentials = new NetworkCredential(filial.UsuarioEmail, filial.SenhaEmail);
                mensagem.Subject = assunto;
                mensagem.IsBodyHtml = true;

                if (StringUtil.VerificaSeEHtml(texto))
                    mensagem.Body = texto;
                else
                    mensagem.Body = StringUtil.ConvertRtfToHtml(texto).Replace("<div align=\"center\">Trial version can convert up to 30000 symbols.<br><a href=\"http://www.sautinsoft.com/convert-rtf-to-html/order.php\">Get the full featured version!</a></div>", "");

                cliente.Send(mensagem);

                return true;
            }
            catch (Exception)
            {
                return false;
            }
            finally
            {
                mensagem.Dispose();
            }
        }
    }
}
