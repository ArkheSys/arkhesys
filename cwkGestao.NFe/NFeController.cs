using System;
using System.Collections.Generic;
using System.Linq;
using cwkGestao.Modelo.Util;
using Modelo;
using MO = Modelo;
using cwkNotaFiscalEletronica;
using cwkGestao.NFe.Adapters;
using cwkNotaFiscalEletronica.Erros;
using System.Reflection;
using System.Threading;
using System.IO;
using cwkGestao.Negocio;
using System.Windows.Forms;
using cwkGestao.Modelo;
using NFe.Classes.Informacoes.Identificacao.Tipos;
using NFe.Classes.Servicos.Tipos;
using NFe.Servicos.Retorno;

namespace cwkGestao.NFe
{
    public class NFeController
    {
        cwkNotaFiscalEletronica.Modelo.ConfiguracaoManager configManager;
        IObserverNFe observer;
        AdapterNota nota;
        static string tipoFrete = "";
        public bool bEnviadoEmailNFE;
        private String emailsDestinario = String.Empty;
        public bool Duplicidade = false;

        #region Factory
        public static NFeController ProduceFromLinq(MO.Nota _nota, bool utilizaLayout4, cwkGestao.NFe.Enums.TipoCertificado tipoCertificado, IObserverNFe _observer, Modelo.ConfiguracaoManager gestaoConfigManager)
        {
            VersaoXML versaoXml = utilizaLayout4 == true ? VersaoXML.v4 : VersaoXML.v3;

            TipoEmissao tipoEmissao;
            switch (_nota.Filial.FormaEmissao)
            {
                case MO.FormaEmissaoNFe.Normal: tipoEmissao = TipoEmissao.teNormal; break;
                default: tipoEmissao = TipoEmissao.teSCAN; break;
            }

            cwkAmbiente ambiente;
            switch (_nota.Filial.Ambiente)
            {
                case 0: ambiente = cwkAmbiente.Producao; break;
                case 1: ambiente = cwkAmbiente.Homologacao; break;
                default: ambiente = cwkAmbiente.Homologacao; break;
            }

            TipoDoCertificado tipoDoCertificado = TipoDoCertificado.ckFile;
            switch (tipoCertificado)
            {
                case Enums.TipoCertificado.ckFile:
                    tipoDoCertificado = TipoDoCertificado.ckFile;
                    break;
                case Enums.TipoCertificado.ckSmartCard:
                    tipoDoCertificado = TipoDoCertificado.ckSmartCard;
                    break;
                case Enums.TipoCertificado.ckMemory:
                    tipoDoCertificado = TipoDoCertificado.ckMemory;
                    break;
                case Enums.TipoCertificado.ckLocalMachine:
                    tipoDoCertificado = TipoDoCertificado.ckLocalMachine;
                    break;
                case Enums.TipoCertificado.ckActiveDirectory:
                    tipoDoCertificado = TipoDoCertificado.ckActiveDirectory;
                    break;
            }

            Danfe danfe;
            switch (_nota.Filial.ModeloDanfe)
            {
                case 1: danfe = Danfe.Retrato; break;
                default: danfe = Danfe.Paisagem; break;
            }

            DanfeNFCe danfeNFCe;
            switch (_nota.Filial.ModeloDanfeNFCe)
            {
                case 5: danfeNFCe = DanfeNFCe.msgEletronica; break;
                default: danfeNFCe = DanfeNFCe.DANFE; break;
            }
            tipoFrete = _nota.TipoFrete.ToString();

            var notaItemTpDevolucao = _nota.NotaItems.Where(i => i.TabelaCFOP.bDevolucao != null && i.TabelaCFOP.bDevolucao == true).FirstOrDefault();
            bool tpDevolucao = notaItemTpDevolucao == null ? false : (bool)notaItemTpDevolucao.TabelaCFOP.bDevolucao;
            return new NFeController(versaoXml, tipoEmissao, ambiente, tipoDoCertificado, danfe, AdapterNota.Produce(_nota), _observer, 1, IndPres.OperacaoPresencial, IndInter.OperacaoSemIntermediador, tpDevolucao, gestaoConfigManager, danfeNFCe);
        }

        public static NFeController ProduceFromNHibernate(Modelo.Nota _nota, int LayoutNFE, cwkGestao.NFe.Enums.TipoCertificado tipoCertificado, IObserverNFe _observer, Modelo.ConfiguracaoManager gestaoConfigManager)
        {
            //VersaoXML versaoXml = utilizaLayout4 == true ? VersaoXML.v4 : VersaoXML.v3;
            VersaoXML versaoXml;
            switch (LayoutNFE)
            {
                case 0:
                    versaoXml = VersaoXML.v3;
                    break;
                case 2:
                    versaoXml = VersaoXML.v5a;
                    break;
                case 3:
                    versaoXml = VersaoXML.v6;
                    break;
                default:
                    versaoXml = VersaoXML.v4;
                    break;
            }

            TipoEmissao tipoEmissao;
            switch (_nota.Filial.FormaEmissao)
            {
                case 1:
                    tipoEmissao = TipoEmissao.teNormal;
                    break;
                default:
                    tipoEmissao = TipoEmissao.teEPEC;
                    break;
            }

            cwkAmbiente ambiente;
            switch (_nota.Filial.Ambiente)
            {
                case 0: ambiente = cwkAmbiente.Producao; break;
                case 1: ambiente = cwkAmbiente.Homologacao; break;
                default: ambiente = cwkAmbiente.Homologacao; break;
            }

            TipoDoCertificado tipoDoCertificado = TipoDoCertificado.ckFile;
            switch (tipoCertificado)
            {
                case Enums.TipoCertificado.ckFile:
                    tipoDoCertificado = TipoDoCertificado.ckFile;
                    break;
                case Enums.TipoCertificado.ckSmartCard:
                    tipoDoCertificado = TipoDoCertificado.ckSmartCard;
                    break;
                case Enums.TipoCertificado.ckMemory:
                    tipoDoCertificado = TipoDoCertificado.ckMemory;
                    break;
                case Enums.TipoCertificado.ckLocalMachine:
                    tipoDoCertificado = TipoDoCertificado.ckLocalMachine;
                    break;
                case Enums.TipoCertificado.ckActiveDirectory:
                    tipoDoCertificado = TipoDoCertificado.ckActiveDirectory;
                    break;
            }

            Danfe danfe;
            switch (_nota.Filial.ModeloDanfe)
            {
                case 1: danfe = Danfe.Retrato; break;
                default: danfe = Danfe.Paisagem; break;
            }

            DanfeNFCe danfeNFCe;
            switch (_nota.Filial.ModeloDanfeNFCe)
            {
                case 5: danfeNFCe = DanfeNFCe.msgEletronica; break;
                default: danfeNFCe = DanfeNFCe.DANFE; break;
            }
            tipoFrete = _nota.TipoFrete.ToString();

            return new NFeController(versaoXml,
                tipoEmissao,
                ambiente,
                tipoDoCertificado,
                danfe,
                AdapterNota.Produce(_nota),
                _observer,
                _nota.Pessoa?.IntConsumidorFinal ?? 1,
                _nota.TipoNota == null ? IndPres.OperacaoPresencial : (IndPres)_nota.TipoNota.IdentificadorDestinatario,
                _nota.TipoNota == null ? IndInter.OperacaoSemIntermediador : (IndInter)_nota.TipoNota.IndIntermed,
                _nota.TipoNota?.BDevolucao ?? false,
                gestaoConfigManager,
                danfeNFCe);
        }
        #endregion

        INotaFiscalEletronica nfe;
        private NFeController(VersaoXML _versao, TipoEmissao _tipoEmissao, cwkAmbiente _ambiente, TipoDoCertificado _tipoCertificado, Danfe _danfe, AdapterNota _nota, IObserverNFe _observer, Int16 indFinal, IndPres indPres, IndInter indInter, bool bDevolucao, Modelo.ConfiguracaoManager gestaoConfigManager, DanfeNFCe _danfeNFCe)
        {
            observer = _observer;
            configManager = new cwkNotaFiscalEletronica.Modelo.ConfiguracaoManager();

            if (gestaoConfigManager != null)
            {
                configManager.grupo = gestaoConfigManager.grupo;
                configManager.host = gestaoConfigManager.host;
                configManager.cnpj = _nota.Empresa.Cnpj;
                configManager.usuario = gestaoConfigManager.usuario;
                configManager.senha = gestaoConfigManager.senha;
            }

            string IEST = null;
            var Filial = FilialController.Instancia.GetAll().FirstOrDefault(o => o.CNPJ.OnlyNumbers() == _nota.Empresa.Cnpj.OnlyNumbers());
            if (Filial != null)
            {
                var dadosSubstitutos = DadosSubstitutoController.Instancia.GetByIDFilial(Filial.ID);
                var IEEncontrada = dadosSubstitutos.FirstOrDefault(o => o.UF_ST.ToUpper() == _nota.Cliente.UF.ToUpper());
                if (!string.IsNullOrEmpty(IEEncontrada?.IE_ST))
                    IEST = IEEncontrada.IE_ST;
            }

            nfe = INotaFiscalEletronica.NotaFiscalEletronicaFactory(_versao, _tipoEmissao, _ambiente, _tipoCertificado, _nota.Empresa.DiretorioPadrao, indFinal, indPres, indInter, bDevolucao, _nota.ModeloDocto, _nota.Empresa.ComponenteDfe, IEST, configManager);
            nfe.Danfe = _danfe;
            nfe.DanfeNFCe = _danfeNFCe;
            nfe.Empresa = _nota.Empresa;
            _nota.TipoFrete = tipoFrete;

            nfe.Nota = _nota;
            nota = _nota;

            nfe.Iniciar();
        }

        private List<string> TransfomarDicionario(IDictionary<string, string> dicionario)
        {
            List<string> retornar = new List<string>();
            foreach (var item in dicionario)
            {
                retornar.Add(item.Key + " | " + item.Value);
            }

            return retornar;
        }

        public void GerarNFe(string pEmailsDestinatario, int filialFormaEmissao, bool nfceOffline, string CaminhoLogo)
        {
            IList<string> andamentoEnvio = new List<string>();
            emailsDestinario = pEmailsDestinatario;

            try
            {
                andamentoEnvio.Add("Enviando nota...");
                NotificarObserver(andamentoEnvio);

                if (nota.ModeloDocto == 65)
                {
                    nfe.AlterarFormaDeEmissao();
                }

                if (nfceOffline)
                {
                    // Validação se modelo do documento for diferente de NFCe ou componente for diferente do Zeus
                    if (nota.ModeloDocto != 65 || nota.Empresa.ComponenteDfe != 1)
                        throw new Exception("Contingência OffLine não disponível");

                    andamentoEnvio.Add("Gerando NFCe OFFLine...");
                    NotificarObserver(andamentoEnvio);
                    var retornoOffline = nfe.GerarNFCeOffline();
                }
                else
                {
                    var retornoNFe = nfe.GerarNFe(CaminhoLogo);

                    // Paulo: Tratamento do Recibo/Duplicidade
                    if (15 == (nfe.Nota.NumeroProtocolo?.Length ?? 0))
                    {
                        var Recibo = nfe.ConsultarProtocolo();
                        bool vbDuplicidade = "204" == Recibo["retConsSitNFe protNFe Status"]; // retConsReciNFe
                        if (vbDuplicidade)
                        {
                            var NfeConsut = nfe.ConsultarNFe();
                            if ("100" == NfeConsut["retConsSitNFe protNFe Status"])
                            {
                                Duplicidade = true;
                                nota.NumeroProtocolo = NfeConsut["retConsSitNFe protNFe Protocolo"];

                                andamentoEnvio.Clear();
                                andamentoEnvio.Add("NFe em Duplicidade, solicitando o Cancelamento.");
                                NotificarObserver(andamentoEnvio);
                                return;
                            }
                        }
                    }
                    else
                    {
                        if (retornoNFe.ContainsKey("retEnviNFe protNFe Status"))
                            if (retornoNFe["retEnviNFe protNFe Status"] != "100")
                            {
                                andamentoEnvio.Clear();
                                andamentoEnvio.Add(retornoNFe["retEnviNFe protNFe Razão"]);
                                NotificarObserver(andamentoEnvio);
                                return;
                            }
                    }
                }

                if (filialFormaEmissao == 1 && !nfceOffline)
                    TrataEnvioNotaNormal(andamentoEnvio);
                else
                    TrataEnvioNotaContingencia(andamentoEnvio);
            }
            catch (XmlMalFormatadoException exc)
            {
                andamentoEnvio.Add(exc.Message);
                andamentoEnvio.Add("Detalhes:");

                object statusMotivo = nota.GetType().GetProperty("StatusMotivo").GetValue(nota, null);
                if (statusMotivo != null)
                    andamentoEnvio.Add(statusMotivo.ToString());

                AdicionarDetalhes(andamentoEnvio, nota);
                NotificarObserver(andamentoEnvio);
            }
        }

        private void TrataEnvioNotaNormal(IList<string> andamentoEnvio)
        {
            andamentoEnvio.Add("Nota enviada.");
            NotificarObserver(andamentoEnvio);

            // Para nfc-e não precisa pedir recibo e envio já envia e retorna o recibo pois utiliza o Manager e-doc (tecnospeed)
            if (nota.ModeloDocto != 65 && nota.Empresa.ComponenteDfe == 0)  // Componente 0 = Tecnospeed / 1 = Zeus
            {
                andamentoEnvio.Add("Aguardando recibo...");
                NotificarObserver(andamentoEnvio);

                int tempoEnvio = ConfiguracaoController.Instancia.GetConfiguracao().TempoEsperaNFe;
                if (tempoEnvio > 0)
                    Aguardar(andamentoEnvio, tempoEnvio);
                else
                    Aguardar(andamentoEnvio, 15);

                /* Verificar */
                nfe.ConsultarRecibo();
                andamentoEnvio.Add("Recibo recebido.");

            }
            else if (nota.Empresa.ComponenteDfe == 1)
            {
                andamentoEnvio.Add("Aguardando recibo...");
                NotificarObserver(andamentoEnvio);

                int tempoEnvio = ConfiguracaoController.Instancia.GetConfiguracao().TempoEsperaNFe;
                if (tempoEnvio > 0)
                    Aguardar(andamentoEnvio, tempoEnvio);
                else
                    Aguardar(andamentoEnvio, 15);
                nfe.ConsultarProtocolo();

                andamentoEnvio.Add("Recibo recebido.");
            }

            andamentoEnvio.Add("Nota enviada com sucesso.");
            NotificarObserver(andamentoEnvio);

            andamentoEnvio.Add("Detalhes:");

            AdicionarDetalhes(andamentoEnvio, nota);
            NotificarObserver(andamentoEnvio);

            if (nota.Status == "7")
            {
                var documentosnota = DocumentoController.Instancia.GetDocumentosPorNota(nota.Id);
                foreach (var item in documentosnota)
                {
                    if (item.Situacao != "Canc")
                    {
                        try
                        {
                            DocumentoController.Instancia.CancelarDocumento(item, item.Historico, "Cancelamento por Nota Denegada", item.Saldo);
                        }
#pragma warning disable CS0168 // A variável "e" está declarada, mas nunca é usada
                        catch (Exception e)
#pragma warning restore CS0168 // A variável "e" está declarada, mas nunca é usada
                        {
                            throw new Exception("Não foi possível cancelar os documentos relacionados a nota Denegada. Verifique se os documentos encontram-se baixados ou se já estão cancelados.");
                        }
                    }
                }

            }
            if (ConfiguracaoController.Instancia.GetConfiguracao().EnvioAutomaticoNfe && nota.Status == "2")
            {
                try
                {
                    if (!String.IsNullOrEmpty(emailsDestinario))
                    {
                        andamentoEnvio.Add("Enviando email...");
                        NotificarObserver(andamentoEnvio);

                        String[] arrayEmailDestinatario = emailsDestinario.Split(',');
                        String texto = String.Empty;
                        String assunto = ConfiguracaoController.Instancia.GetConfiguracao().AssuntoEmailNfe;

                        //Não retirar os campos comentados pois futuramente serão usados com o novo componente NFe.
                        //if (StringUtil.VerificaSeEHtml(ConfiguracaoController.Instancia.GetConfiguracao().TextoEmailNfe))
                        texto = ConfiguracaoController.Instancia.GetConfiguracao().TextoEmailNfe;
                        //else
                        //texto = StringUtil.ConvertRtfToHtml(ConfiguracaoController.Instancia.GetConfiguracao().TextoEmailNfe);

                        var nEnvio = NotaController.Instancia.LoadObjectById(nota.Id);

                        this.EnviarDanfeEmail(nEnvio, emailsDestinario,
                            assunto,
                            texto);

                        foreach (var email in arrayEmailDestinatario)
                        {
                            andamentoEnvio.Add("Envio para o e-mail " + email);
                            bEnviadoEmailNFE = true;

                            NotificarObserver(andamentoEnvio);
                        }

                        NotificarObserver(andamentoEnvio);
                    }
                }
                catch
                {
                    andamentoEnvio.Add("Falha no envio do email.");
                    bEnviadoEmailNFE = false;
                    NotificarObserver(andamentoEnvio);
                }
            }
        }

        private void TrataEnvioNotaContingencia(IList<string> andamentoEnvio)
        {
            andamentoEnvio.Add("Nota enviada em modo de contingência.");
            NotificarObserver(andamentoEnvio);

            andamentoEnvio.Add("Detalhes:");

            AdicionarDetalhes(andamentoEnvio, nota);
            NotificarObserver(andamentoEnvio);
        }

        private void Aguardar(IList<string> andamentoEnvio, int segundos)
        {
            andamentoEnvio.Add("Pedindo recibo em " + segundos + "...");
            NotificarObserver(andamentoEnvio);
            Thread.Sleep(50);

            for (int i = segundos - 1; i > 0; i--)
            {
                andamentoEnvio[andamentoEnvio.Count - 1] = "Pedindo recibo em " + i + "...";
                NotificarObserver(andamentoEnvio);
                Thread.Sleep(50);
            }
        }

        private void AdicionarDetalhes(IList<string> lista, object nota)
        {
            PropertyInfo propriedadeUltimoXmlRecebido = nota.GetType().GetProperty("UltimoXmlRecebido");

            try
            {
                IDictionary<string, string> retorno = cwkNotaFiscalEletronica.Funcoes.DesmembrarXml(propriedadeUltimoXmlRecebido.GetValue(nota, null).ToString());
                var ret = "";
                foreach (var item in retorno)
                {
                    if (item.Key.Contains("Status"))
                        ret += item.Value;

                    if (item.Key.Contains("Razão"))
                        ret += " - " + item.Value;
                }

                if (!string.IsNullOrEmpty(ret))
                    lista.Add(ret);

                if (lista.Count == 0)
                {
                    foreach (var item in retorno)
                    {
                        lista.Add("- " + item.Value);
                    }
                }

                if (lista.Count > 0)
                {
                    foreach (string item in lista[lista.Count - 1].SepararAspas())
                    {
                        lista.Add("- " + item);
                    }
                }
            }
#pragma warning disable CS0168 // A variável "e" está declarada, mas nunca é usada
            catch (Exception e)
#pragma warning restore CS0168 // A variável "e" está declarada, mas nunca é usada
            {

            }
        }

        public void ConsultarNFe()
        {
            IList<string> andamentoConsulta = new List<string>();

            try
            {
                andamentoConsulta.Add("Requisitando consulta de NFe...");
                NotificarObserver(andamentoConsulta);
                nfe.ConsultarNFe();
                andamentoConsulta.Add("Requisição concluída.");
            }
            catch (XmlMalFormatadoException exc)
            { andamentoConsulta.Add(exc.Message); }

            andamentoConsulta.Add("Detalhes:");
            AdicionarDetalhes(andamentoConsulta, nota);
            NotificarObserver(andamentoConsulta);
        }

        public void ConsultarRecibo()
        {
            IList<string> andamentoConsulta = new List<string>();

            try
            {
                andamentoConsulta.Add("Requisitando recibo de NFe...");
                NotificarObserver(andamentoConsulta);
                nfe.ConsultarRecibo();
                andamentoConsulta.Add("Requisição concluída.");
            }
            catch (XmlMalFormatadoException exc)
            { andamentoConsulta.Add(exc.Message); }

            andamentoConsulta.Add("Detalhes:");
            AdicionarDetalhes(andamentoConsulta, nota);
            NotificarObserver(andamentoConsulta);
        }

        /// <summary>
        /// Retorna Lista de Certificado no padrão do Zeus
        /// </summary>
        /// <returns></returns>
        ///
        /*
        public static System.Security.Cryptography.X509Certificates.X509Certificate2 RetornaListaCertificados()
        {
            return INotaFiscalEletronica.ListaCertificado();
        }*/

        //TODO: a adaptar estes métodos
        public void VisualizarDANFE(bool ExportarPDF, string CaminhoLogo)
        {
            switch (nota.Status)
            {
                case "2": //Autorizada
                    nfe.VisualizarDANFE(true, nota.Empresa.ComponenteDfe == 0, CaminhoLogo);
                    break;
                default:
                    MessageBox.Show("A nota não selecionada ainda não foi enviada foi enviada.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        public void ImprimirNFCe(string NomeImpressora, bool ExibirCaixaDialogo, string CaminhoLogo)
        {
            switch (nota.Status)
            {
                case "2": //Autorizada
                case "9": //Contingência OFFLine
                    nfe.ImprimirDanfe(NomeImpressora, ExibirCaixaDialogo, true, CaminhoLogo);
                    break;
                case "8": //Contingência
                    break;
                default:
                    MessageBox.Show("A nota não selecionada ainda não foi enviada foi enviada.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }
        }

        public void ImprimirNota(Modelo.Nota nota, bool ImprimirDireto, string NomeImpressora)
        {
            switch (nota.StatusNota)
            {
                case Modelo.Nota.StatusTipo.Autorizada:
                    Dictionary<string, string> notas = new Dictionary<string, string>();
                    notas.Add(nota.ChaveNota, nota.XmlDestinatarioNFe.ToString());
                    nfe.ImprimirDANFE(notas, ImprimirDireto, NomeImpressora, nota.Filial.CaminhoLogoNfe);
                    break;
                case Modelo.Nota.StatusTipo.Contingencia:
                    break;
                default:
                    MessageBox.Show("A nota selecionada ainda não foi enviada.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    break;
            }

            // Gerar modelo offline aqui
        }

        public void ExportarDanfe(Modelo.Nota nota)
        {
            nfe.ExportarDanfe(nota.XmlDestinatarioNFe.ToString(), nota.ChaveNota, nota.Filial.CaminhoLogoNfe);
        }

        public void SalvarXmlArquivo(Modelo.Nota nota)
        {
            string caminho = FilialController.Instancia.GetFilialPrincipal().DiretorioPadraoNFe;
            StreamWriter streamWriter;
            if (nota.Filial.Ambiente == 1)
            {
                streamWriter = File.CreateText(caminho + "\\XMLDestinatarioHom\\" + nota.ChaveNota + ".xml");
            }
            else
            {
                streamWriter = File.CreateText(caminho + "\\XMLDestinatario\\" + nota.ChaveNota + ".xml");
            }

            streamWriter.Write(nota.XmlDestinatarioNFe.ToString());
            streamWriter.Close();
        }

        public bool CancelarNFe(string motivo, string usuario, bool Duplicidade)
        {
            IList<string> andamentoConsulta = new List<string>();
            bool ret = false;

            try
            {
                if (Duplicidade)
                    andamentoConsulta.Add("NFe em duplicidade...");

                andamentoConsulta.Add("Requisitando cancelamento de NFe...");
                NotificarObserver(andamentoConsulta);

                PropertyInfo propriedadeIdNota = nota.GetType().GetProperty("Id");
                cwkGestao.Negocio.DocumentoController documentosController = cwkGestao.Negocio.DocumentoController.Instancia;
                if (documentosController.NotaPossuiDocumentosBaixados((int)propriedadeIdNota.GetValue(nota, null)))
                {
                    NotificarExisteDocumentoBaixado(andamentoConsulta);
                    return false;
                }

                nfe.CancelarNFe(motivo, usuario);
                andamentoConsulta.Add("Requisição concluída.");
                CancelarDocumentos();
                ret = true;
            }
            catch (XmlMalFormatadoException exc)
            { andamentoConsulta.Add(exc.Message); }

            andamentoConsulta.Add("Detalhes:");
            AdicionarDetalhes(andamentoConsulta, nota);
            NotificarObserver(andamentoConsulta);
            return ret;
        }

        private void CancelarDocumentos()
        {
            if (nota.Status == "3")
            {
                var documentosNota = DocumentoController.Instancia.GetDocumentosPorNota(nota.Id);
                DocumentoController.Instancia.CancelarDocumentos(documentosNota);
                int idnota = nota.Id;
                cwkGestao.Modelo.Nota objNota = NotaController.Instancia.LoadObjectById(idnota);

                IList<cwkGestao.Modelo.NotaItemPedidoItem> pedidosItens = NotaItemPedidoItemController.Instancia.GetAllNiPiPorIDNota(idnota);

                if ((pedidosItens != null) && (pedidosItens.Count > 0))
                {
                    if (MessageBox.Show("Deseja alterar o Status dos Pedidos relacionados para Aberto?", "Atenção",
                    MessageBoxButtons.YesNo, MessageBoxIcon.Exclamation) == DialogResult.Yes)
                    {
                        AbrirPedido(pedidosItens.Select(pi => pi.PedidoItem).ToList());
                    }
                }
            }
        }

        public void ResolveNfce()
        {
            List<string> andamentoEnvio = new List<string>();
            try
            {
                nfe.ResolveNfce();

                andamentoEnvio.Add("Detalhes:");

                AdicionarDetalhes(andamentoEnvio, nota);
                NotificarObserver(andamentoEnvio);

                CancelarDocumentos();
            }
            catch (XmlMalFormatadoException exc)
            {
                andamentoEnvio.Add(exc.Message);

                andamentoEnvio.Add("Detalhes:");

                object statusMotivo = nota.GetType().GetProperty("StatusMotivo").GetValue(nota, null);
                if (statusMotivo != null)
                    andamentoEnvio.Add(statusMotivo.ToString());

                AdicionarDetalhes(andamentoEnvio, nota);
                NotificarObserver(andamentoEnvio);
            }

        }

        private void NotificarExisteDocumentoBaixado(IList<string> andamentoConsulta)
        {
            andamentoConsulta.Add("Nota não pode ser cancelada.");
            andamentoConsulta.Add("Existem documentos baixados no financeiro associados com essa nota.");
            andamentoConsulta.Add("Para cancelar essa nota, desfaça todas as baixas dos documentos associados com ela.");
        }

        public void InutilizarNFe(out List<string> retorno, string pAno, string pSerie, string pNumIni, string pNumFim, string pJustificativa)
        {
            retorno = new List<string>();
            string ret = nfe.InutilizarNFe(pAno, pSerie, pNumIni, pNumFim, pJustificativa);
            retorno.Add(ret);
        }

        public void EnviarDanfeEmail(Modelo.Nota _nota, string emailDestinatario, string assuntoEmail, string corpoEmail)
        {
                nfe.EnviarDanfeEmail(emailDestinatario, assuntoEmail, corpoEmail);
        }

        public RetornoRecepcaoEvento ManifestarNFe(string CNPJ, string ChaveNFe, NFeTipoEvento tpEvento)
        {
            return nfe.ManifestarNFe(CNPJ, ChaveNFe, tpEvento);
        }

        public RetornoNfeDistDFeInt ConsultarNotaPelaChave(string CNPJ, string ChaveNFe)
        {
            return nfe.ConsultarNotaPelaChave(CNPJ, ChaveNFe);
        }

        public Dictionary<string, byte[]> ConsultarNotasPeloNsu(string CNPJ, string Nsu = "0")
        {
            return nfe.GetNotasContraCNPJPorNsu(CNPJ, Nsu);
        }

        public void VisualizarDanfeZeus(string xml)
        {
            nfe.VisualizarDanfeZeus(xml);
        }

        public long GetUltimoNsuPorCNPJ(string CNPJ)
        {
            return nfe.GetUltimoNsuPorCNPJ(CNPJ);
        }

        public RetornoNfeDistDFeInt GetNotasPorUltimoNsu(string Cnpj, string ultimoNsu)
        {
            return nfe.GetNotasPorUltimoNsu(Cnpj, ultimoNsu);
        }

        public void NotificarObserver(IList<string> lista)
        {
            if (observer != null)
            {
                observer.AtualizarGrid(lista);
            }
        }

        public IList<string> DesmembrarXml()
        {
            PropertyInfo propriedadeUltimoXml = nota.GetType().GetProperty("UltimoXmlRecebido");
            object valorUltimoXml = propriedadeUltimoXml.GetValue(nota, null);
            if (valorUltimoXml != null && !String.IsNullOrEmpty(valorUltimoXml.ToString()))
            {
                IList<string> retorno = new List<string>();
                AdicionarDetalhes(retorno, nota);
                return retorno;
            }
            return new List<string>();
        }

        public string GeraXmlNota()
        {
            return nfe.GeraXmlNota();
        }

        public void GerarXmlPreDanfe(string CaminhoLogo)
        {
            nfe.GerarXmlPreDanfe(CaminhoLogo);
        }

        public void AbrirPedido(IList<cwkGestao.Modelo.PedidoItem> listaPedidoItem)
        {
            try
            {
                int qtdeItensPedido = 0;
                IList<cwkGestao.Modelo.Pedido> listaDePedidos = listaPedidoItem.Select(pi => pi.Pedido).GroupBy(p => p.ID).Select(p => p.First()).ToList();

                foreach (var pedidoItem in listaPedidoItem)
                    pedidoItem.BFaturado = false;

                foreach (cwkGestao.Modelo.Pedido pedido in listaDePedidos)
                {
                    qtdeItensPedido = pedido.Items.Count;

                    if (qtdeItensPedido > 0)
                    {
                        IList<cwkGestao.Modelo.PedidoItem> itensPedidoAbertos = pedido.Items.Where(pi => pi.BFaturado == false).ToList();
                        pedido.Status = itensPedidoAbertos.Count == qtdeItensPedido ? cwkGestao.Modelo.StatusOrcamento.Aberto : cwkGestao.Modelo.StatusOrcamento.FaturadoParcial;
                    }
                    PedidoController.Instancia.RegisterNewIntoTransaction(pedido);
                }
            }
#pragma warning disable CS0168 // A variável "e" está declarada, mas nunca é usada
            catch (Exception e)
#pragma warning restore CS0168 // A variável "e" está declarada, mas nunca é usada
            {
            }
        }
    }
}
