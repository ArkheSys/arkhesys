using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using cwkGestao.Modelo;
using Aplicacao.Base;
using cwkGestao.Negocio;
using MO = Modelo;
using cwkGestao.Negocio.Comercial;
using cwkGestao.Negocio.Padroes;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Xml.Linq;
using cwkGestao.Modelo.Util;
using Aplicacao.Util;
using System.Reflection;
using System.Diagnostics;
using DBUtils.Classes;
using cwkGestao.Integracao.NFSe;
using System.Text.RegularExpressions;
using Newtonsoft.Json;

namespace Aplicacao.Modulos.Comercial
{
    public partial class FormNFSe : Form
    {
        private Nota NFSe;
        private HttpManager httpManager = new HttpManager();
        private ConfiguracaoManager configManager = ConfiguracaoManagerController.Instancia.GetAll().FirstOrDefault();

        public FormNFSe(Nota _NFSe)
        {
            InitializeComponent();
            NFSe = _NFSe;
        }

        private void FormNFSe_Shown(object sender, EventArgs e)
        {
            IList<string> frasesNFSe = new List<string>();
            frasesNFSe.Add(NFSe.FrasesNFSe);

            txtSerie.EditValue = NFSe.Serie;
            txtNumero.EditValue = NFSe.Numero;
            txtPessoaNome.EditValue = NFSe.Pessoa.Nome;
            txtData.EditValue = NFSe.Dt;
            txtTotalNota.EditValue = NFSe.TotalNota;
            txtNumeroNFSe.EditValue = NFSe.NumeroNFSe;
            txtSituacao.EditValue = NFSe.StatusLegivel;
            txtdtAutorizacao.Text = String.Format("{0:dd/MM/yyyy HH:mm}", NFSe.DtHoraAuto);
            txtdtCancelamento.Text = String.Format("{0:dd/MM/yyyy HH:mm}", NFSe.DtHoraCanc);
            txtEmail.Text = NFSe.Pessoa.PessoaEmails.FirstOrDefault(i => i.BNFSe)?.Email;
            txt2CNPJ_CPF.Text = NFSe.Pessoa.CNPJ_CPF;
            gridControl1.DataSource = frasesNFSe;

            MostrarCamposErrados(NFSe);
            HabilitaBotoes();

            if (string.IsNullOrEmpty(txtEmail.Text))
                txtEmail.Focus();
        }

        private void MostrarCamposErrados(Nota nota)
        {
            bool ok = false;
            StringBuilder erros = new StringBuilder();
            erros.Append("Por favor, verifique os seguintes campos: \n");

            if (nota.Pessoa.TelefonePrincipal == null || nota.Pessoa.TelefonePrincipal == "")
            {
                erros.Append("- Telefone Principal \n");
                ok = true;
            }

            if (ok)
                MessageBox.Show(erros.ToString(), "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
        }

        private void btFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void HabilitaBotoes()
        {
            switch (NFSe.Status)
            {
                case "2":
                    btEnviarNFSe.Enabled = false;
                    sbResolve.Enabled = true;
                    btCancelarNFSe.Enabled = true;
                    btImprimirNFSe.Enabled = true;
                    btEmail.Enabled = true;
                    txtSerie.Enabled = false;
                    txtNumero.Enabled = false;
                    break;
                case "0":
                    sbResolve.Enabled = false;
                    btCancelarNFSe.Enabled = true;
                    btImprimirNFSe.Enabled = true;
                    btEmail.Enabled = false;
                    btEnviarNFSe.Enabled = true;
                    break;
                case "3":
                    btEnviarNFSe.Enabled = false;
                    sbResolve.Enabled = true;
                    btEmail.Enabled = true;
                    btImprimirNFSe.Enabled = true;
                    btCancelarNFSe.Enabled = false;
                    txtSerie.Enabled = false;
                    txtNumero.Enabled = false;
                    break;
                case "1":
                    btEnviarNFSe.Enabled = true;
                    sbResolve.Enabled = true;
                    btCancelarNFSe.Enabled = true;
                    btImprimirNFSe.Enabled = true;
                    btEmail.Enabled = true;
                    break;
                default:
                    btEnviarNFSe.Enabled = true;
                    sbResolve.Enabled = true;
                    btCancelarNFSe.Enabled = true;
                    btImprimirNFSe.Enabled = true;
                    btEmail.Enabled = true;
                    break;
            }

            Refresh();
        }

        private void DesabilitaBotoes()
        {
            btCancelarNFSe.Enabled = false;
            btEmail.Enabled = false;
            btImprimirNFSe.Enabled = false;
            sbResolve.Enabled = false;
            btEnviarNFSe.Enabled = false;

            Refresh();
        }

        private string SalvarArquivoIni(NFSE dadosNFSe)
        {
            var filial = FilialController.Instancia.GetFilialPrincipal();

            string caminhoIni = Debugger.IsAttached
                ? $"{Path.GetFullPath(".").Substring(0, Path.GetFullPath(".").IndexOf("cwkGestao"))}cwkGestao\\App_Data\\NFSe.ini"
                : $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\App_Data\\NFSe.ini";

            var iniNFSe = new IniFile(caminhoIni);
            iniNFSe.SetValue("IdentificacaoNFSe", "Numero", NFSe.NumeroNFSe.ToString());
            foreach (var item in dadosNFSe.Dados)
            {
                iniNFSe.SetValue("IdentificacaoRps", "DataEmissao", DateTime.Now.ToString("dd/MM/yyyy"));
                iniNFSe.SetValue("IdentificacaoRps", "Numero", item.NumeroRps.ToString());
                iniNFSe.SetValue("IdentificacaoRps", "Serie", item.SerieRps);
                iniNFSe.SetValue("IdentificacaoRps", "Tipo", item.TipoRps); // VErificar 

                iniNFSe.SetValue("RpsSubstituido", "Numero", item.NumeroRpsSubstituido.ToString());
                iniNFSe.SetValue("RpsSubstituido", "Serie", item.SerieRpsSubstituido);
                iniNFSe.SetValue("RpsSubstituido", "Tipo", item.TipoRpsSubstituido);

                iniNFSe.SetValue("Prestador", "Regime", "0");
                iniNFSe.SetValue("Prestador", "OptanteSN", item.OptanteSimplesNacional.ToString());
                iniNFSe.SetValue("Prestador", "IncentivadorCultural", "2");
                iniNFSe.SetValue("Prestador", "CNPJ", item.CpfCnpjPrestador?.OnlyNumbers());
                iniNFSe.SetValue("Prestador", "InscricaoMunicipal", item.InscricaoMunicipalPrestador?.OnlyNumbers());
                iniNFSe.SetValue("Prestador", "RazaoSocial", item.RazaoSocialPrestador);
                iniNFSe.SetValue("Prestador", "NomeFantasia", item.RazaoSocialPrestador);
                iniNFSe.SetValue("Prestador", "Logradouro", item.EnderecoPrestador);
                iniNFSe.SetValue("Prestador", "Numero", item.NumeroEnderecoPrestador);
                iniNFSe.SetValue("Prestador", "Bairro", item.BairroEnderecoPrestador);
                iniNFSe.SetValue("Prestador", "CodigoMunicipio", item.CodigoCidadePrestacao);
                iniNFSe.SetValue("Prestador", "UF", filial.Cidade?.UF?.Sigla ?? ""); // Verificar para adicionar posteriormente
                iniNFSe.SetValue("Prestador", "CodigoPais", "1058");
                iniNFSe.SetValue("Prestador", "xPais", "BRASIL");
                iniNFSe.SetValue("Prestador", "CEP", "00000000");
                iniNFSe.SetValue("Prestador", "Telefone", item.TelefonePrestador);
                iniNFSe.SetValue("Prestador", "Email", "");

                iniNFSe.SetValue("Tomador", "Tipo", "1"); // Verificar
                iniNFSe.SetValue("Tomador", "CNPJCPF", item.CpfCnpjTomador?.OnlyNumbers());
                iniNFSe.SetValue("Tomador", "InscricaoMunicipal", item.InscricaoMunicipalTomador?.OnlyNumbers());
                iniNFSe.SetValue("Tomador", "InscricaoEstadual", item.InscricaoEstadualTomador?.OnlyNumbers());
                //; Exigido pelo provedor Equiplano
                iniNFSe.SetValue("Tomador", "RazaoSocial", item.RazaoSocialTomador);
                iniNFSe.SetValue("Tomador", "TipoLogradouro", item.TipoLogradouroTomador);
                iniNFSe.SetValue("Tomador", "Logradouro", item.EnderecoTomador);
                iniNFSe.SetValue("Tomador", "Numero", item.NumeroTomador.ToString());
                iniNFSe.SetValue("Tomador", "Complemento", item.ComplementoTomador);
                iniNFSe.SetValue("Tomador", "Bairro", item.BairroTomador);
                iniNFSe.SetValue("Tomador", "CodigoMunicipio", item.CodigoCidadeTomador.ToString());
                iniNFSe.SetValue("Tomador", "UF", item.UfTomador);
                iniNFSe.SetValue("Tomador", "CodigoPais", "1058");
                iniNFSe.SetValue("Tomador", "CEP", item.CepTomador);
                iniNFSe.SetValue("Tomador", "xPais", "BRASIL");
                iniNFSe.SetValue("Tomador", "Telefone", item.TelefoneTomador);
                iniNFSe.SetValue("Tomador", "Email", item.EmailTomador);
                iniNFSe.SetValue("Tomador", "AtualizaTomador", "");
                iniNFSe.SetValue("Tomador", "TomadorExterior", "");

                iniNFSe.SetValue("Intermediario", "CNPJCPF", item.CpfCnpjIntermediario?.OnlyNumbers());
                iniNFSe.SetValue("Intermediario", "InscricaoMunicipal", item.InscricaoMunicipalIntermediario);
                iniNFSe.SetValue("Intermediario", "RazaoSocial", item.RazaoSocialIntermediario);

                iniNFSe.SetValue("Servico", "ItemListaServico", item.CodigoItemListaServico);
                iniNFSe.SetValue("Servico", "CodigoCnae", item.CodigoCnae.ToString());
                iniNFSe.SetValue("Servico", "CodigoTributacaoMunicipio", item.CodigoTributacaoMunicipio);
                iniNFSe.SetValue("Servico", "Discriminacao", item.DiscriminacaoServico.RemoverAcentos());
                iniNFSe.SetValue("Servico", "CodigoMunicipio", filial.Cidade.IBGE); // "4115200");
                iniNFSe.SetValue("Servico", "CodigoPais", "1058");
                iniNFSe.SetValue("Servico", "ExigibilidadeISS", item.ExigibilidadeISS);
                iniNFSe.SetValue("Servico", "MunicipioIncidencia", "");
                iniNFSe.SetValue("Servico", "UFPrestacao", filial.Cidade?.UF?.Sigla ?? ""); // "PR");
                iniNFSe.SetValue("Servico", "ResponsavelRetencao", item.ResponsavelRetencao?.ToString());

                iniNFSe.SetValue("Valores", "ValorServicos", item.ValorServicos?.ToString("n2"));
                iniNFSe.SetValue("Valores", "ValorDeducoes", item.ValorDeducoes?.ToString("n2"));
                iniNFSe.SetValue("Valores", "ValorPis", item.ValorPis?.ToString("n2"));
                iniNFSe.SetValue("Valores", "AliquotaPis", item.AliquotaPIS?.ToString("n2"));
                iniNFSe.SetValue("Valores", "ValorCofins", item.ValorCofins?.ToString("n2"));
                iniNFSe.SetValue("Valores", "AliquotaCofins", item.AliquotaCOFINS?.ToString("n2"));
                iniNFSe.SetValue("Valores", "ValorInss", item.ValorInss?.ToString("n2"));
                iniNFSe.SetValue("Valores", "ValorIr", item.ValorIr?.ToString("n2"));
                iniNFSe.SetValue("Valores", "ValorCsll", item.ValorCsll?.ToString("n2"));
                iniNFSe.SetValue("Valores", "ISSRetido", item.IssRetido);
                iniNFSe.SetValue("Valores", "OutrasRetencoes", "0");
                iniNFSe.SetValue("Valores", "DescontoIncondicionado", item.DescontoIncondicionado?.ToString("n2"));
                iniNFSe.SetValue("Valores", "DescontoCondicionado", item.DescontoCondicionado?.ToString("n2"));
                iniNFSe.SetValue("Valores", "BaseCalculo", item.BaseCalculo?.ToString("n2"));
                iniNFSe.SetValue("Valores", "Aliquota", item.AliquotaISS?.ToString("n2"));
                iniNFSe.SetValue("Valores", "ValorIss", item.ValorIss?.ToString("n2"));
                iniNFSe.SetValue("Valores", "ValorIssRetido", item.ValorIssRetido?.ToString("n2"));
                iniNFSe.SetValue("Valores", "ValorLiquidoNfse", item.ValorLiquidoNfse?.ToString("n2"));

                iniNFSe.SetValue("CondicaoPagamento", "QtdParcela", "0");
                iniNFSe.SetValue("CondicaoPagamento", "Condicao", "A_VISTA");
            }

            iniNFSe.Save(caminhoIni);
            return caminhoIni;
        }

        private string EnviarNFSeAcbr(NFSE dadosNFSe)
        {
            var caminhoIni = SalvarArquivoIni(dadosNFSe);
            return new NFSeConstructor(NFSe.Filial.AmbienteNFSe == 1, NFSe.Filial.ID).EmitirNota(caminhoIni, NFSe.ID.ToString());
        }

        private string CancelarNFSeAcbr(NFSE dadosNFSe)
        {
            string caminhoIni = Debugger.IsAttached
                ? $"{Path.GetFullPath(".").Substring(0, Path.GetFullPath(".").IndexOf("cwkGestao"))}cwkGestao\\App_Data\\NFSeCancelamento.ini"
                : $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\App_Data\\NFSeCancelamento.ini";

            var iniNFSe = new IniFile(caminhoIni);

            var dados = dadosNFSe.Dados.FirstOrDefault();
            iniNFSe.SetValue("CancelarNFSe", "NumeroNFSe", NFSe.NumeroNFSe.ToString());
            iniNFSe.SetValue("CancelarNFSe", "SerieNFSe", NFSe.Serie.ToString());
            iniNFSe.SetValue("CancelarNFSe", "ChaveNFSe", NFSe.NumeroRecibo.ToString());
            iniNFSe.SetValue("CancelarNFSe", "DataEmissaoNFSe", dados.DataEmissao.ToString("dd/MM/yyyy"));
            iniNFSe.SetValue("CancelarNFSe", "CodCancelamento", "1");
            iniNFSe.SetValue("CancelarNFSe", "MotCancelamento", "'Cancelamento Padrao NFSe'");
            iniNFSe.SetValue("CancelarNFSe", "NumeroLote", NFSe.ID.ToString());
            iniNFSe.SetValue("CancelarNFSe", "NumeroRps", dados.NumeroRps.ToString());
            iniNFSe.SetValue("CancelarNFSe", "SerieRps", dados.SerieRps.ToString());
            //iniNFSe.SetValue("CancelarNFSe", "ValorNFSe", NFSe.TotalNota.ToString("n2"));
            iniNFSe.SetValue("CancelarNFSe", "CodVerificacao", NFSe.NumeroRecibo);
            //iniNFSe.SetValue("CancelarNFSe", "email", "");
            //iniNFSe.SetValue("CancelarNFSe", "NumeroNFSeSubst", dados.NumeroNfseSubstituida.ToString());
            //iniNFSe.SetValue("CancelarNFSe", "SerieNFSeSubst", dados.SerieRps.ToString());
            iniNFSe.SetValue("CancelarNFSe", "CodServ", dados.CodigoItemListaServico);
            iniNFSe.Save(caminhoIni);

            return new NFSeConstructor(NFSe.Filial.AmbienteNFSe == 1, NFSe.Filial.ID).CancelarNFSe(caminhoIni);
        }

        //private void Imprimir(NFSE dadosNFSe, int Numero, bool AmbienteProdutivo)
        //{
        //    var Filial = FilialController.Instancia.GetFilialPrincipal();
        //    foreach (var item in dadosNFSe.Dados)
        //    {
        //        var NFSeGen = new NFSeGenerator(Application.StartupPath, new PrestadorNFSe
        //        {
        //            CPFCNPJ = item.CpfCnpjPrestador,
        //            IM = item.InscricaoMunicipalPrestador,
        //            RazaoSocial = item.RazaoSocialPrestador,
        //            Fantasia = item.RazaoSocialPrestador,
        //            Fone = item.TelefonePrestador,
        //            Municipio = Convert.ToInt32(item.CodigoCidadePrestacao),
        //            Ambiente = (OpenAC.Net.DFe.Core.Common.DFeTipoAmbiente)(Filial.AmbienteNFSe ?? 2),

        //            // Certificado
        //            NumeroSerie = Filial.Certificado,
        //            //Senha = "",

        //            ArquivoCidades = Path.Combine(Application.StartupPath, "Municipios.nfse"),
        //            DiretorioSchemas = Application.StartupPath + @"\ACBrNFSe\Schemas",

        //            SalvarArquivos = true,
        //            PathXml = Application.StartupPath + @"\NFSe",
        //        });

        //        var Dir = new DirectoryInfo(Application.StartupPath + @"\NFSe");
        //        var Files = Dir.GetFiles("*.xml", SearchOption.AllDirectories);
        //        var FileInfo = Files.FirstOrDefault(o => o.Name.StartsWith($"NFSe-{Numero}"));
        //        if (FileInfo == null)
        //        {
        //            MessageBox.Show("Arquivo .xml não encontrado.", "ERRO!");
        //            return;
        //        }
        //        NFSeGen.Imprimir(FileInfo.FullName, dadosNFSe.Dados.FirstOrDefault()?.DescricaoCidadeTomador, AmbienteProdutivo);
        //    }
        //}

        public IDictionary<string, string> DesmembrarXml(string xml)
        {
            return DesmembrarXml(XElement.Load(new StreamReader(new MemoryStream(ASCIIEncoding.UTF8.GetBytes(xml)))));
        }

        public IDictionary<string, string> DesmembrarXml(XElement elemento)
        {
            IDictionary<string, string> retorno = new Dictionary<string, string>();
            foreach (var item in elemento.Elements())
            {
                if (item.HasElements)
                    foreach (var nosInternos in DesmembrarXml(item.ToString()))
                    {
                        try
                        {
                            retorno.Add(item.Parent.Name.LocalName + " " + nosInternos.Key, nosInternos.Value);
                        }
                        catch
                        {
                            retorno[item.Parent.Name.LocalName + " " + nosInternos.Key] = nosInternos.Value;
                        }
                    }
                else
                    retorno.Add(item.Name.LocalName, item.Value);
            }

            return retorno;
        }

        private int GetNumeroNFSe(string XML)
        {
            try
            {
                var xmlRoot = XDocument.Parse(XML).Root.Elements().FirstOrDefault();
                var xmlRet = XDocument.Parse(xmlRoot.Value);

                var numero = xmlRet.GetElementAny("EnviarLoteRpsSincronoResposta")?
                    .GetElementAny("ListaNfse")?
                    .GetElementAny("CompNfse")?
                    .GetElementAny("Nfse")?
                    .GetElementAny("InfNfse")?
                    .GetElementAny("Numero");

                return Convert.ToInt32(numero.Value);
            }
            catch (Exception e)
            {
                return 0;
            }
        }

        private string GetCodigoVerificacao(string XML)
        {
            try
            {
                var xmlRoot = XDocument.Parse(XML).Root.Elements().FirstOrDefault();
                var xmlRet = XDocument.Parse(xmlRoot.Value);

                var numero = xmlRet.GetElementAny("EnviarLoteRpsSincronoResposta")?
                    .GetElementAny("ListaNfse")?
                    .GetElementAny("CompNfse")?
                    .GetElementAny("Nfse")?
                    .GetElementAny("InfNfse")?
                    .GetElementAny("CodigoVerificacao");

                return numero.Value;
            }
            catch (Exception e)
            {
                return "";
            }
        }

        private void Resolve(NFSE dadosNFSe)
        {
            if (NFSe.Filial.Cidade.IBGE == "4115200")
            {
                var caminhoIni = SalvarArquivoIni(dadosNFSe);
                var retorno = new NFSeConstructor(NFSe.Filial.AmbienteNFSe == 1, NFSe.Filial.ID).ConsultarLoteRPS(caminhoIni, NFSe.NumeroProtocolo, NFSe.ID.ToString());
                var dic = ConvertLinesToDictionary(retorno);

                /* Código de situação de lote de RPS
                   1 – Não Recebido
                   2 – Não Processado
                   3 – Processado com Erro
                   4 – Processado com Sucesso
                 */
                var situacao = dic["ConsultaLoteRps"]["Situacao"];
                if (situacao == "4") // Processado com Sucesso
                {
                    IList<string> auxRetornoEnvia = new List<string>();
                    auxRetornoEnvia.Add("NFSe Autorizada com sucesso.");

                    NFSe.FrasesNFSe = "NFSe Autorizada com sucesso.";
                    NFSe.Status = "2"; // Autorizada
                    NFSe.ModeloDocto = 3; // Troca o modelo de RPS para NFSe
                    NFSe.BImpressa = true;
                    NFSe.NumeroProtocolo = dic["ConsultaLoteRps"]["Protocolo"];
                    NFSe.NumeroRecibo = dic["ConsultaLoteRps"]["CodVerificacao"];
                    NotaController.Instancia.Salvar(NFSe, Acao.Alterar);
                    gridControl1.DataSource = auxRetornoEnvia;
                }
            }
        }

        private void btEnviarNFSe_Click(object sender, EventArgs e)
        {
            if (NFSe.Status == "2")
            {
                MessageBox.Show("NFSe já está autorizada, não é possível enviar novamente!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            if (string.IsNullOrEmpty(txtEmail.Text) && MessageBox.Show("Deseja informar o e-mail? Caso contrário o e-mail não será enviado", "Informar E-mail", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                txtEmail.Focus();
                return;
            }

            NFSe.Serie = txtSerie.Text;
            NFSe.Numero = Convert.ToInt32(txtNumero.Text);
            NFSe.NumeroNFSe = Convert.ToInt32(txtNumeroNFSe.Text);

            var notaBD = NotaController.Instancia.VerificaSeExisteNFSePeloNumeroBD(NFSe.Numero).FirstOrDefault();

            if (notaBD != null && notaBD.ID != NFSe.ID && notaBD.Serie == NFSe.Serie)
            {
                MessageBox.Show("Já existe uma NFSe cadastrada com esse número e serie no Gestão !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }
            NotaController.Instancia.Salvar(NFSe, Acao.Alterar);
            IList<string> auxRetornoEnvia = new List<string>();

            // Rotina Acbr
            var Retorno = EnviarNFSeAcbr(NFSEManager.ConstrutorNFSE(NFSe));

            var dic = ConvertLinesToDictionary(Retorno);
            var sucesso = dic["Envio"].ContainsKey("Sucesso") ? dic["Envio"]["Sucesso"] : null;
            var codigoVerificacao = dic["Envio"].ContainsKey("CodigoVerificacao") ? dic["Envio"]["CodigoVerificacao"] : null;
            var protocolo = dic["Envio"].ContainsKey("Protocolo") ? dic["Envio"]["Protocolo"] : null;
            var numeroNota = dic["Envio"].ContainsKey("NumeroNota") ? dic["Envio"]["NumeroNota"] : "0";
            var xmlEnvio = dic["Envio"].ContainsKey("XmlEnvio") ? dic["Envio"]["XmlEnvio"] : null;
            var xmlRetorno = dic["Envio"].ContainsKey("XmlRetorno") ? dic["Envio"]["XmlRetorno"] : null;

            List<string> erros = new List<string>();
            for (int i = 1; i <= 10; i++)
                if (dic.ContainsKey($"Erro{i}"))
                {
                    var strErro = $"Código: {dic[$"Erro{i}"]["Codigo"]}{Environment.NewLine}Erro: {dic[$"Erro{i}"]["Descricao"]}";
                    if (dic[$"Erro{i}"].ContainsKey("Correcao"))
                        strErro += $"{Environment.NewLine}Correção: {dic[$"Erro{i}"]["Correcao"]}";

                    erros.Add(strErro);
                }

            auxRetornoEnvia.Clear();
            if ("1" == sucesso && !string.IsNullOrEmpty(codigoVerificacao) && erros?.Count() == 0) // Verificar tratativa de NFSe autorizada.
            {
                auxRetornoEnvia.Add("NFSe Autorizada com sucesso.");
                NFSe.FrasesNFSe = "NFSe Autorizada com sucesso.";
                NFSe.Status = "2"; // Autorizada
                NFSe.ModeloDocto = 3; // Troca o modelo de RPS para NFSe
                NFSe.NumeroNFSe = Convert.ToInt32(numeroNota);
                NFSe.BImpressa = true;
                NFSe.XmlLogEnvNFe = xmlEnvio;
                NFSe.UltimoXmlRecebido = xmlRetorno;
                NFSe.NumeroProtocolo = protocolo;
                NFSe.NumeroRecibo = codigoVerificacao;

                var filial = FilialController.Instancia.GetFilialPrincipal();

                var fileXML = filial.CaminhoNFSeXML + $@"\NFSe{NFSe.NumeroNFSe}.xml";
                File.Create(fileXML).Dispose();
                File.WriteAllText(fileXML, xmlRetorno);

                if (NFSe.Filial.Cidade.IBGE == "4115200") // Maringá
                {
                    using (var client = new WebClient())
                    {
                        var caminho = filial.CaminhoNFSePDF;
                        var caminhoArquivo = caminho + $@"\NFSe{NFSe.NumeroNFSe}.pdf";

                        if (NFSe.Filial.AmbienteNFSe == 1)
                            client.DownloadFile($"https://nfse.ecity.maringa.pr.gov.br/ImprimirNfse/{NFSe.NumeroNFSe}/{NFSe.Filial.CNPJ.OnlyNumbers()}/{codigoVerificacao}", caminhoArquivo);
                        else
                            client.DownloadFile($"https://nfse.hom-ecity.maringa.pr.gov.br/ImprimirNfse/{NFSe.NumeroNFSe}/{NFSe.Filial.CNPJ.OnlyNumbers()}/{codigoVerificacao}", caminhoArquivo);
                        //if (File.Exists(caminhoArquivo))
                        //    System.Diagnostics.Process.Start(caminhoArquivo);
                    }
                }
                else // Tratar pra fazer somente para MG
                {
                    var xml = XElement.Parse(NFSe.UltimoXmlRecebido);
                    var linkImpressao = xml.Elements().First(o => o.Name == "RetornoNota").Elements().First(o => o.Name == "LinkImpressao").Value;
                    linkImpressao = linkImpressao.Replace("amp;", "");
                    using (var client = new WebClient())
                    {
                        var caminhoArquivo = FilialController.Instancia.GetFilialPrincipal().CaminhoNFSePDF + $@"\NFSe{NFSe.NumeroNFSe}.pdf";
                        client.DownloadFile(linkImpressao, caminhoArquivo);
                    }
                }

                if (!string.IsNullOrEmpty(txtEmail.Text))
                    SendEmailNFSe();
            }
            else
                erros?.ForEach(err =>
                {
                    auxRetornoEnvia.Add($"{err}");
                    NFSe.FrasesNFSe += $"{err}{Environment.NewLine}";
                });


            NotaController.Instancia.Salvar(NFSe, Acao.Alterar);
            gridControl1.DataSource = auxRetornoEnvia;
        }

        private void ConsultaNFSe()
        {
            IList<string> ConsultaNFSe = new List<string>();
            IList<string> ParametrosConsulta = new List<string>();

            //parametros para consulta da NFSe no Manager.
            ParametrosConsulta.Add("CNPJ=" + NFSe.Filial.CNPJ.Replace(".", "").Replace("/", "").Replace("-", ""));
            ParametrosConsulta.Add("grupo=" + NFSe.Filial.GrupoManager);
            ParametrosConsulta.Add("Filtro=nrps=" + NFSe.Numero.ToString());
            ParametrosConsulta.Add("Campos=handle,nnfse,situacao,dtautorizacao,dtcancelamento");
            ConsultaNFSe = httpManager.GetManager(configManager, NFSe, "nfse", "consulta", ParametrosConsulta);

            string[] ret = ConsultaNFSe.FirstOrDefault().Split(new char[] { ',' });

            if (ConsultaNFSe.FirstOrDefault().ToLower().Contains("nenhum"))
            {
                NFSe.Status = "-1";
                NFSe.ModeloDocto = 2;
                return;
            }

            NFSe.Handle = Convert.ToInt32(ret[0]);

            if (ret[1] == "")
                ret[1] = "0";

            NFSe.NumeroNFSe = Convert.ToInt32(ret[1]);
            switch (ret[2].ToLower())
            {
                case "autorizada":
                    NFSe.Status = "2";
                    NFSe.ModeloDocto = 3;
                    NFSe.BImpressa = true;
                    NFSe.DtHoraAuto = NFSe.DtHoraAuto != null ? NFSe.DtHoraAuto : DateTime.Now;
                    txtNumeroNFSe.EditValue = NFSe.NumeroNFSe;
                    txtdtAutorizacao.Text = String.Format("{0:dd/MM/yyyy HH:mm}", NFSe.DtHoraAuto);
                    //SalvarXmlDestinatarioComPdf();
                    break;
                case "rejeitada":
                    NFSe.Status = "0";
                    NFSe.ModeloDocto = 2;
                    NFSe.LoteNFSe = NotaController.Instancia.GetNovoLoteNFSe() + 1;
                    //DescartaNFSe();
                    break;
                case "cancelada":
                    NFSe.Status = "3";
                    NFSe.ModeloDocto = 3;
                    NFSe.CancDt = DateTime.Today;
                    NFSe.CancUsuario = MO.cwkGlobal.objUsuarioLogado.Login;
                    NFSe.DtHoraCanc = NFSe.DtHoraCanc != null ? NFSe.DtHoraCanc : DateTime.Now;
                    txtdtCancelamento.Text = String.Format("{0:dd/MM/yyyy HH:mm}", NFSe.DtHoraCanc);
                    //SalvarXmlDestinatarioComPdf();
                    break;
                case "registrada":
                    NFSe.Status = "6";
                    NFSe.ModeloDocto = 2;
                    break;
                case "enviada":
                    NFSe.Status = "5";
                    NFSe.ModeloDocto = 2;
                    break;
                case "pendente":
                    NFSe.Status = "4";
                    NFSe.ModeloDocto = 2;
                    break;

                default:
                    NFSe.Status = "4";
                    NFSe.ModeloDocto = 2;
                    break;
            }

            txtSituacao.EditValue = NFSe.StatusLegivel;
        }

        private void btCancelarNFSe_Click(object sender, EventArgs e)
        {
            try
            {
                DesabilitaBotoes();

                if (NFSe.Status != "2")
                {
                    MessageBox.Show("Não é possível cancelar uma NFSe que não esteja Autorizada !", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    HabilitaBotoes();
                    return;
                }
                var documentosNota = DocumentoController.Instancia.GetDocumentosPorNota(NFSe.ID);
                if (DocumentoController.Instancia.ExisteDocumentoBaixado(documentosNota))
                {
                    throw new Exception("Nota não pode ser cancelada porque possui documentos no financeiro já baixados.");
                }

                if ((MessageBox.Show("Deseja cancelar a NFSe ?", "Atenção", MessageBoxButtons.YesNo, MessageBoxIcon.Question)) == DialogResult.Yes)
                {
                    // Alteração da rotina de cancelamento para ACBr
                    var RetornoCancelamento = CancelarNFSeAcbr(NFSEManager.ConstrutorNFSE(NFSe));
                    var dic = ConvertLinesToDictionary(RetornoCancelamento);

                    List<string> erros = new List<string>();
                    for (int i = 1; i <= 10; i++)
                        if (dic.ContainsKey($"Erro{i}"))
                        {
                            var strErro = $"Código: {dic[$"Erro{i}"]["Codigo"]}{Environment.NewLine}Erro: {dic[$"Erro{i}"]["Descricao"]}";
                            if (dic[$"Erro{i}"].ContainsKey("Correcao"))
                                strErro += $"{Environment.NewLine}Correção: {dic[$"Erro{i}"]["Correcao"]}";

                            NFSe.FrasesNFSe += strErro;
                            erros.Add(strErro);
                        }

                    if (erros.Count == 0)
                    {
                        NFSe.Status = "3";
                        NFSe.ModeloDocto = 3;
                        NFSe.CancDt = DateTime.Today;
                        NFSe.CancUsuario = MO.cwkGlobal.objUsuarioLogado.Login;
                        NFSe.DtHoraCanc = NFSe.DtHoraCanc != null ? NFSe.DtHoraCanc : DateTime.Now;
                        txtdtCancelamento.Text = String.Format("{0:dd/MM/yyyy HH:mm}", NFSe.DtHoraCanc);

                        var docsnota = DocumentoController.Instancia.GetDocumentosPorNota(NFSe.ID);
                        DocumentoController.Instancia.CancelarDocumentosNFSe(docsnota);
                        NotaController.Instancia.Salvar(NFSe, Acao.Alterar);
                        LogicaTelaNotaFiscal logicaTelaNotaFiscal = new LogicaTelaNotaFiscal(NFSe);
                        logicaTelaNotaFiscal.AlteraStatusOSOrdemServico(logicaTelaNotaFiscal.PegaIDOrdemServico());

                        erros.Add("Evento de Cancelamento Vinculado a NFSe.");
                    }

                    gridControl1.DataSource = erros;
                }

                HabilitaBotoes();
            }
            catch (Exception exc)
            {
                HabilitaBotoes();
                MessageBox.Show(exc.Message, "Erro", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void btEmail_Click(object sender, EventArgs e)
        {
            DesabilitaBotoes();

            try
            {
                //chama tela de envio de email da NFSe.
                if (NFSe.Status == "2" || NFSe.Status == "3")
                {
                    FormEnviaEmailNFSe form = new FormEnviaEmailNFSe(NFSe, txtEmail.Text);
                    form.ShowDialog();
                }
                else
                    MessageBox.Show("Somente Notas Autorizadas ou Canceladas pode-se enviar email.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                HabilitaBotoes();
            }
            catch (Exception exc)
            {
                HabilitaBotoes();
                new FormErro(exc).ShowDialog();
            }
        }

        private void btImprimirNFSe_Click(object sender, EventArgs e)
        {
            DesabilitaBotoes();

            try
            {
                if (NFSe.Status == "2" || NFSe.Status == "3")
                {
                    if (NFSe.Filial.Cidade.IBGE == "4115200") // Maringá
                    {
                        using (var client = new WebClient())
                        {
                            var caminhoArquivo = FilialController.Instancia.GetFilialPrincipal().CaminhoNFSePDF + $@"\NFSe{NFSe.NumeroNFSe}.pdf";
                            if (File.Exists(caminhoArquivo))
                                Process.Start(caminhoArquivo);
                            else
                            {
                                if (NFSe.Filial.AmbienteNFSe == 1)
                                    client.DownloadFile($"https://nfse.ecity.maringa.pr.gov.br/ImprimirNfse/{NFSe.NumeroNFSe}/{NFSe.Filial.CNPJ.OnlyNumbers()}/{NFSe.NumeroRecibo}", caminhoArquivo);
                                else
                                    client.DownloadFile($"https://nfse.hom-ecity.maringa.pr.gov.br/ImprimirNfse/{NFSe.NumeroNFSe}/{NFSe.Filial.CNPJ.OnlyNumbers()}/{NFSe.NumeroRecibo}", caminhoArquivo);
                            }

                            if (File.Exists(caminhoArquivo))
                                Process.Start(caminhoArquivo);
                        }
                    }
                    else // Tratar pra fazer somente para MG
                    {
                        var xml = XElement.Parse(NFSe.UltimoXmlRecebido);
                        var linkImpressao = xml.Elements().First(o => o.Name == "RetornoNota").Elements().First(o => o.Name == "LinkImpressao").Value;
                        linkImpressao = linkImpressao.Replace("amp;", "");
                        using (var client = new WebClient())
                        {
                            var caminhoArquivo = FilialController.Instancia.GetFilialPrincipal().CaminhoNFSePDF + $@"\NFSe{NFSe.NumeroNFSe}.pdf";
                            client.DownloadFile(linkImpressao, caminhoArquivo);
                            if (File.Exists(caminhoArquivo))
                                Process.Start(caminhoArquivo);
                        }
                    }
                }
                else
                    MessageBox.Show("Somente Notas Autorizadas ou Canceladas pode-se imprimir.", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);

                HabilitaBotoes();
            }
            catch (Exception exc)
            {
                HabilitaBotoes();
                new FormErro(exc).ShowDialog();
            }
        }

        //private void DescartaNFSe()
        //{
        //    NameValueCollection reqparm = new NameValueCollection();
        //    reqparm.Add("NomeCidade", cwkGestao.Negocio.Utils.NFSEUtils.RemoveAcentos(NFSe.Filial.Cidade.Nome));
        //    reqparm.Add("Handle", NFSe.Handle.ToString());

        //    httpManager.PostManager(configManager, NFSe, "nfse", "descarta", reqparm);
        //}

        //private void SalvarXmlDestinatarioComPdf()
        //{
        //    IList<string> parametros = new List<string>();
        //    parametros.Add("NomeCidade=" + cwkGestao.Negocio.Utils.NFSEUtils.RemoveAcentos(NFSe.Filial.Cidade.Nome));
        //    parametros.Add("handle=" + NFSe.Handle);

        //    var xml = httpManager.GetManager(configManager, NFSe, "nfse", "xml", parametros);

        //    if (Directory.Exists(configManager.CaminhoXMLNFSE))
        //    {
        //        File.WriteAllText(configManager.CaminhoXMLNFSE + @"\" + NFSe.Handle + ".xml", xml.FirstOrDefault());
        //    }
        //    else
        //    {
        //        MessageBox.Show("Diretório de xml's não foi informado ou não existe em Configuração Manager. \r\nNão foi possível gerar o xml !",
        //            "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
        //    }

        //    GeraPdfAutomatico();
        //}

        //private void GeraPdfAutomatico()
        //{
        //    IList<string> ParametrosImprimir = new List<string>();

        //    ParametrosImprimir.Add("NomeCidade=" + cwkGestao.Negocio.Utils.NFSEUtils.RemoveAcentos(NFSe.Filial.Cidade.Nome));
        //    ParametrosImprimir.Add("NumNFSe=" + NFSe.NumeroNFSe.ToString());
        //    ParametrosImprimir.Add("ModoImpressao=" + "PDF");
        //    ParametrosImprimir.Add("Url=1");
        //    ParametrosImprimir.Add("Quantidade=1");

        //    httpManager.GetManager(configManager, NFSe, "nfse", "imprime", ParametrosImprimir).FirstOrDefault();
        //    this.WindowState = FormWindowState.Normal;
        //}

        private void sbResolve_Click(object sender, EventArgs e)
        {
            try
            {
                DesabilitaBotoes();
                if (NFSe.Status == "2")
                {
                    MessageBox.Show("NFSe já está autorizada, não é possível enviar novamente!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                    return;
                }

                NFSe.Serie = txtSerie.Text;
                NFSe.Numero = Convert.ToInt32(txtNumero.Text);
                NFSe.NumeroNFSe = Convert.ToInt32(txtNumeroNFSe.Text);

                Resolve(NFSEManager.ConstrutorNFSE(NFSe));
                HabilitaBotoes();
            }
            catch (Exception ex)
            {
                HabilitaBotoes();
                new FormErro(ex).ShowDialog();
            }
        }

        private void SendEmailNFSe()
        {
            try
            {
                var filial = FilialController.Instancia.GetFilialPrincipal();
                var caminhoPdf = filial.CaminhoNFSePDF + $@"\NFSe{NFSe.NumeroNFSe}.pdf";
                //var Dir = new DirectoryInfo(Application.StartupPath + @"\NFSe");
                var Dir = new DirectoryInfo(filial.CaminhoNFSeXML);
                var Files = Dir.GetFiles("*.xml", SearchOption.AllDirectories);
                var FileInfo = Files.FirstOrDefault(o => o.Name.StartsWith($"NFSe{NFSe.NumeroNFSe}"));
                if (FileInfo == null)
                {
                    MessageBox.Show("Arquivo .xml da NFSe não encontrado.", "ERRO!");
                    return;
                }

                var config = ConfiguracaoController.Instancia.GetConfiguracao();
                EnviarEmail(config, filial.ServidorSMTP, filial.UsuarioEmail, filial.SenhaEmail, filial.PortaSmtp.ToString(), filial.GMailAutenticacao,
                    new List<string>
                    {
                        txtEmail.Text,
                    }, $"XML e PDF da NFSe: {NFSe.NumeroNFSe}", $"XML e PDF da NFSe: {NFSe.NumeroNFSe}",
                    FileInfo.FullName, caminhoPdf);
            }
            catch (Exception exc)
            {
                new FormErro(exc).ShowDialog();
            }
        }

        private static void EnviarEmail(Configuracao config,
            string SMTP, string UsuarioEmail, string SenhaEmail, string PortaSMTP, bool SSL,
            IEnumerable<string> pEmailsCliente, string Corpo, string Assunto,
            string caminhoXml,
            string caminhoPdf)
        {
            var cliente = new SmtpClient(SMTP, Convert.ToInt32(PortaSMTP) /* TLS */);
            var mensagem = new MailMessage { From = new MailAddress(UsuarioEmail) };

            foreach (var pEmail in pEmailsCliente)
                if (!string.IsNullOrEmpty(pEmail))
                    mensagem.To.Add(new MailAddress(pEmail));
            try
            {
                cliente.EnableSsl = SSL;
                cliente.Credentials = new NetworkCredential(UsuarioEmail, SenhaEmail);

                mensagem.Subject = Assunto;
                mensagem.IsBodyHtml = true;

                mensagem.Attachments.Add(new Attachment(caminhoXml));
                mensagem.Attachments.Add(new Attachment(caminhoPdf));

                if (config.SolicitaConfirmaEmailPedido)
                    mensagem.Headers.Add("Disposition-Notification-To", UsuarioEmail);

                mensagem.Body = StringUtil.VerificaSeEHtml(Corpo) ? StringUtil.ConvertRtfToHtml(Corpo) : StringUtil.ConvertRtfToHtml(Corpo).Replace("<div align=\"center\">Trial version can convert up to 30000 symbols.<br><a href=\"http://www.sautinsoft.com/convert-rtf-to-html/order.php\">Get the full featured version!</a></div>", "");

                cliente.Send(mensagem);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
            finally
            {
                mensagem.Dispose();
            }
        }

        private readonly Regex _sectionRegex = new Regex(@"(?<=\[)(?<SectionName>[^\]]+)(?=\])");
        private readonly Regex _keyValueRegex = new Regex(@"(?<Key>[^=]+)=(?<Value>.+)");
        public Dictionary<string, Dictionary<string, string>> ConvertLinesToDictionary(string Body)
        {
            try
            {
                var _iniFileContent = new Dictionary<string, Dictionary<string, string>>();
                var currentSectionName = string.Empty;
                foreach (var Line in GetLines(Body))
                {
                    Match M = _sectionRegex.Match(Line);
                    if (M.Success)
                        currentSectionName = M.Groups["SectionName"].Value;
                    else
                    {
                        M = _keyValueRegex.Match(Line);
                        if (M.Success)
                        {
                            var Key = M.Groups["Key"].Value;
                            var value = M.Groups["Value"].Value;

                            Dictionary<string, string> lista = _iniFileContent.ContainsKey(currentSectionName) ? _iniFileContent[currentSectionName] : new Dictionary<string, string>();
                            lista[Key] = value;
                            _iniFileContent[currentSectionName] = lista;
                        }
                    }
                }
                return _iniFileContent;
            }
            catch
            {
                return null;
            }
        }

        public string GetValue(Dictionary<string, Dictionary<string, string>> iniFileContent, string sectionName, string key, string DefaultValue)
        {
            return (iniFileContent.ContainsKey(sectionName) && iniFileContent[sectionName].ContainsKey(key) ? iniFileContent[sectionName][key] : DefaultValue).Trim();
        }

        public IEnumerable<string> GetLines(string str, bool removeEmptyLines = false)
        {
            return str.Split(new[] { "\r\n", "\r", "\n" }, removeEmptyLines ? StringSplitOptions.RemoveEmptyEntries : StringSplitOptions.None);
        }
    }
}