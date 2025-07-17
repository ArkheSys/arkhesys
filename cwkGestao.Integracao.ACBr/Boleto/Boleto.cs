using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Windows.Documents;

using cwkGestao.Integracao.ACBr.Core;
using cwkGestao.Integracao.ACBr.Core.Ini;
using cwkGestao.Modelo.Util;

namespace cwkGestao.Integracao.ACBr.Boleto
{
    public class Boleto : IDisposable
    {
        #region Propriedades

        private readonly IList<DocumentoBoleto> _documentos;
        private readonly StringBuilder _docs;
#pragma warning disable CS0169 // O campo "Boleto._stream" nunca é usado
        private StreamWriter _stream;
#pragma warning restore CS0169 // O campo "Boleto._stream" nunca é usado
        private ACBrBoleto _boleto;

        public Remessa Remessa { get; set; }
        public List<DocumentoBoleto> ListaDocumento { get; set; }
        public string CaminhoArquivoRemessa { get; private set; }

        // Verificar
#pragma warning disable CS0169 // O campo "Boleto.cc" nunca é usado
        private string cc;
#pragma warning restore CS0169 // O campo "Boleto.cc" nunca é usado
#pragma warning disable CS0169 // O campo "Boleto.Opcao" nunca é usado
        private string Opcao;
#pragma warning restore CS0169 // O campo "Boleto.Opcao" nunca é usado
#pragma warning disable CS0649 // Campo "Boleto.Email" nunca é atribuído e sempre terá seu valor padrão null
        private string Email;
#pragma warning restore CS0649 // Campo "Boleto.Email" nunca é atribuído e sempre terá seu valor padrão null
#pragma warning disable CS0169 // O campo "Boleto.identificador" nunca é usado
        private string identificador;
#pragma warning restore CS0169 // O campo "Boleto.identificador" nunca é usado
#pragma warning disable CS0169 // O campo "Boleto.htmlBoleto" nunca é usado
        private string htmlBoleto;
#pragma warning restore CS0169 // O campo "Boleto.htmlBoleto" nunca é usado
#pragma warning disable CS0169 // O campo "Boleto.caminhoArquivo" nunca é usado
        private string caminhoArquivo;
#pragma warning restore CS0169 // O campo "Boleto.caminhoArquivo" nunca é usado
        private string CaminhoRemessa;
#pragma warning disable CS0414 // O campo "Boleto.OpcaoBkp" é atribuído, mas seu valor nunca é usado
        private readonly string OpcaoBkp = "";
#pragma warning restore CS0414 // O campo "Boleto.OpcaoBkp" é atribuído, mas seu valor nunca é usado
#pragma warning disable CS0169 // O campo "Boleto.tamanho" nunca é usado
        private int tamanho;
#pragma warning restore CS0169 // O campo "Boleto.tamanho" nunca é usado
        private int NumRemessaSicredi;
        public int contador;
#pragma warning disable CS0414 // O campo "Boleto.valorjuro" é atribuído, mas seu valor nunca é usado
        private decimal? valorjuro = 0;
#pragma warning restore CS0414 // O campo "Boleto.valorjuro" é atribuído, mas seu valor nunca é usado
#pragma warning disable CS0414 // O campo "Boleto.valormulta" é atribuído, mas seu valor nunca é usado
        private decimal? valormulta = 0;
#pragma warning restore CS0414 // O campo "Boleto.valormulta" é atribuído, mas seu valor nunca é usado

        #endregion

        #region Construtores

        public Boleto()
        {
            _boleto = new ACBrBoleto();
            _docs = new StringBuilder();
            Remessa = new Remessa();
            ListaDocumento = new List<DocumentoBoleto>();
            _documentos = new List<DocumentoBoleto>();
        }

        #endregion

        #region Métodos

        public void LerRetorno(string dir, string arq)
        {
            _boleto.LerRetorno(dir, arq);
        }

        private void ConfigurarComponente(int numeroArquivo)
        {
            _boleto.ConfigGravarValor(ACBrSessao.BoletoBancoFCFortesConfig, "NomeArquivo", Remessa.CaminhoPDF);
            _boleto.ConfigGravar();
        }

        public void ConfigurarComponente()
        {
            _boleto.ConfigGravarValor(ACBrSessao.BoletoBancoConfig, "TipoCobranca", Remessa.Banco);

            //_boleto.ConfigGravarValor(ACBrSessao.BoletoCedenteConfig, "TipoCarteira", "1"); // Remessa.TipoCarteira);
            _boleto.ConfigGravarValor(ACBrSessao.BoletoCedenteConfig, "TipoDocumento", 0);                                                  // TipoDocumento = Documento Tradicional
            _boleto.ConfigGravarValor(ACBrSessao.BoletoCedenteConfig, "TipoInscricao", Remessa.CpfCnpj.OnlyNumbers().Length == 11 ? 0 : 1); // 0 = Fisica, 1 = Juridica
            _boleto.ConfigGravarValor(ACBrSessao.BoletoCedenteConfig, "Agencia", Remessa.CodigoAgencia);
            _boleto.ConfigGravarValor(ACBrSessao.BoletoCedenteConfig, "AgenciaDigito", Remessa.DigitoAgencia);
            _boleto.ConfigGravarValor(ACBrSessao.BoletoCedenteConfig, "Conta", Remessa.ContaCorrente);
            _boleto.ConfigGravarValor(ACBrSessao.BoletoCedenteConfig, "ContaDigito", Remessa.DigitoConta);
            _boleto.ConfigGravarValor(ACBrSessao.BoletoCedenteConfig, "CodigoCedente", Remessa.CodigoCedente);
            _boleto.ConfigGravarValor(ACBrSessao.BoletoCedenteConfig, "DigitoVerificadorAgenciaConta", Remessa.DigitoAgenciaConta);
            _boleto.ConfigGravarValor(ACBrSessao.BoletoCedenteConfig, "ResponEmissao", 0); // 0 = Cliente
            _boleto.ConfigGravarValor(ACBrSessao.BoletoCedenteConfig, "Operacao", Remessa.NumeroContrato); 

            // Estas opções não existem por hora
            //_boleto.ConfigGravarValor(ACBrSessao.BoletoCedenteConfig, "CodigoTransmissao", txtCodTransmissao.Text);
            _boleto.ConfigGravarValor(ACBrSessao.BoletoCedenteConfig, "Convenio", Remessa.Convenio); // Teste de Convênio...
            //_boleto.ConfigGravarValor(ACBrSessao.BoletoCedenteConfig, "Modalidade", txtModalidade.Text);

            _boleto.ConfigGravarValor(ACBrSessao.BoletoCedenteConfig, "Bairro", Remessa.Bairro?.RemoverAcentos());
            _boleto.ConfigGravarValor(ACBrSessao.BoletoCedenteConfig, "CEP", Remessa.Cep);
            _boleto.ConfigGravarValor(ACBrSessao.BoletoCedenteConfig, "Cidade", Remessa.Cidade?.RemoverAcentos());
            _boleto.ConfigGravarValor(ACBrSessao.BoletoCedenteConfig, "CNPJCPF", Remessa.CpfCnpj.OnlyNumbers());
            _boleto.ConfigGravarValor(ACBrSessao.BoletoCedenteConfig, "Complemento", Remessa.Complemento?.RemoverAcentos());
            _boleto.ConfigGravarValor(ACBrSessao.BoletoCedenteConfig, "Logradouro", Remessa.Logradouro?.RemoverAcentos());
            _boleto.ConfigGravarValor(ACBrSessao.BoletoCedenteConfig, "Nome", Remessa.Nome?.RemoverAcentos());
            _boleto.ConfigGravarValor(ACBrSessao.BoletoCedenteConfig, "NumeroRes", Remessa.NumeroResidencia);
            _boleto.ConfigGravarValor(ACBrSessao.BoletoCedenteConfig, "Telefone", Remessa.Telefone);
            _boleto.ConfigGravarValor(ACBrSessao.BoletoCedenteConfig, "UF", Remessa.UF);

            _boleto.ConfigGravarValor(ACBrSessao.BoletoDiretorioConfig, "LayoutRemessa", Remessa.LayoutRemessa.ToInt());
            _boleto.ConfigGravarValor(ACBrSessao.BoletoDiretorioConfig, "DirArqRemessa", Remessa.CaminhoRemessa);
            _boleto.ConfigGravarValor(ACBrSessao.BoletoDiretorioConfig, "NomeArqRemessa", Remessa.NomeRemessa);
            _boleto.ConfigGravarValor(ACBrSessao.BoletoDiretorioConfig, "DirArqRetorno", Remessa.CaminhoRetorno);
            _boleto.ConfigGravarValor(ACBrSessao.BoletoDiretorioConfig, "NomeArqRetorno", Remessa.NomeRetorno);
            _boleto.ConfigGravarValor(ACBrSessao.BoletoDiretorioConfig, "LeCedenteRetorno", 0);

            _boleto.ConfigGravarValor(ACBrSessao.Email, "Nome", Remessa.NomeEmail);
            _boleto.ConfigGravarValor(ACBrSessao.Email, "Conta", Remessa.EmailRemetente);
            _boleto.ConfigGravarValor(ACBrSessao.Email, "Usuario", Remessa.UsuarioEmail);
            _boleto.ConfigGravarValor(ACBrSessao.Email, "Senha", Remessa.SenhaEmail);
            _boleto.ConfigGravarValor(ACBrSessao.Email, "Servidor", Remessa.ServidorSMTP);
            _boleto.ConfigGravarValor(ACBrSessao.Email, "Porta", Remessa.PortaSMTP);
            _boleto.ConfigGravarValor(ACBrSessao.Email, "SSL", 0);
            _boleto.ConfigGravarValor(ACBrSessao.Email, "TLS", 0);

            _boleto.ConfigGravarValor(ACBrSessao.BoletoBancoFCFortesConfig, "Layout", Remessa.LayoutBoleto);
            _boleto.ConfigGravarValor(ACBrSessao.BoletoBancoFCFortesConfig, "MostrarPreview", 1);               // 0 = Não | 1 = Sim
            _boleto.ConfigGravarValor(ACBrSessao.BoletoBancoFCFortesConfig, "MostrarProgresso", 1);             // 0 = Não | 1 = Sim
            _boleto.ConfigGravarValor(ACBrSessao.BoletoBancoFCFortesConfig, "MostrarSetup", 1);                 // 0 = Não | 1 = Sim
            _boleto.ConfigGravarValor(ACBrSessao.BoletoBancoFCFortesConfig, "NomeArquivo", Remessa.CaminhoPDF); // Caminho completo do arquivo PDF
            _boleto.ConfigGravarValor(ACBrSessao.BoletoBancoFCFortesConfig, "NumeroCopias", 1);
            _boleto.ConfigGravarValor(ACBrSessao.BoletoBancoFCFortesConfig, "DirLogo", Remessa.Logotipo);

            _boleto.ConfigGravar();
        }

        public string ObterDadosCedente()
        {
            var cedente = new ACBrIniFile();

            cedente["Cedente"]["Nome"] = Remessa.Nome?.RemoverAcentos();
            cedente["Cedente"]["CNPJCPF"] = Remessa.CpfCnpj;
            cedente["Cedente"]["Logradouro"] = Remessa.Logradouro?.RemoverAcentos();
            cedente["Cedente"]["Numero"] = Remessa.NumeroResidencia;
            cedente["Cedente"]["Bairro"] = Remessa.Bairro?.RemoverAcentos();
            cedente["Cedente"]["Cidade"] = Remessa.Cidade?.RemoverAcentos();
            cedente["Cedente"]["CEP"] = Remessa.Cep;
            cedente["Cedente"]["Complemento"] = Remessa.Complemento?.RemoverAcentos();
            cedente["Cedente"]["UF"] = Remessa.UF;
            cedente["Cedente"]["RespEmis"] = "0";
            cedente["Cedente"]["TipoPessoa"] = Remessa.CpfCnpj.OnlyNumbers().Length == 11 ? "0" : "1";
            cedente["Cedente"]["CodigoCedente"] = Remessa.CodigoCedente;
            cedente["Cedente"]["LayoutBol"] = Remessa.LayoutBoleto;
            cedente["Cedente"]["CaracTitulo"] = "0"; // 0-Cobranca Simples, 1-Cobranca vinculada, 2-Cobranca Caucionada, 3-Cobranca Descontada, 4-Cobranca Vendor, 5-Cobranca Simples (Rapida com registro), 6- Cobranca Caucionada (rapida com registro)
            cedente["Cedente"]["TipoCarteira"] = Remessa.TipoCarteira.ToString();
            cedente["Cedente"]["TipoDocumento"] = "0"; // 0 = Documento Tradicional 1 = Documento Escritural
            cedente["Cedente"]["Modalidade"] = Remessa.OutroDadoConfiguracao1;
            //cedente["Cedente"]["CodTransmissao"] = "10";
            cedente["Cedente"]["Convenio"] = Remessa.Convenio;

            cedente["Conta"]["Conta"] = Remessa.ContaCorrente;
            cedente["Conta"]["DigitoConta"] = Remessa.DigitoConta;
            cedente["Conta"]["Agencia"] = Remessa.CodigoAgencia;
            cedente["Conta"]["DigitoAgencia"] = Remessa.DigitoAgencia;
            cedente["Conta"]["DigitoVerificadorAgenciaConta"] = Remessa.DigitoAgenciaConta;

            cedente["Banco"]["Numero"] = "0"; // Gerado automaticamente
            cedente["Banco"]["CNAB"] = Remessa.LayoutRemessa;
            cedente["Banco"]["TipoCobranca"] = Remessa.Banco.ToString();
            cedente["Banco"]["VersaoArquivo"] = Remessa.VersaoLayoutArquivo;
            cedente["Banco"]["VersaoLote"] = Remessa.VersaoLayoutLote;
            //cedente["Banco"]["NumeroCorrespondente"] = "0"; 

            return cedente.ToString();
        }

        public List<string> GeraBoletoRemessa(bool remessa, bool impressao, bool pdf, bool email, int sequencia, string pEmail, int? pBanco, int pNumRemessaSicredi, bool pJuro, int seqNossoNumero, int? codigoBaixaDevolucao, bool BExportaJurosMulta)
        {
            NumRemessaSicredi = pNumRemessaSicredi;
            ConfigurarComponente(sequencia);
            _boleto.LimparLista();

            IncluirTitulos(ListaDocumento, pBanco, pJuro, seqNossoNumero, codigoBaixaDevolucao, BExportaJurosMulta, pEmail);

            #region GerarDocumentos

            if (remessa)
            {

                string nomeRemessa = "";
                
                
                Remessa.NomeRemessa.Replace("{0}", sequencia.ToString());

                if (pBanco != 8) //sicredi
                {
                    nomeRemessa = Remessa.NomeRemessa.Replace("{0}", sequencia.ToString());
                }
                else
                {
                    string mes;
                    DateTime DataAtual = DateTime.Now.Date;
                    string CodCedente = Remessa.CodigoCedente.Replace(".", "").Replace(",", "").Replace("-", "").Replace("_", "").Replace("|", "").Replace("/", "").Replace("\\", "");
                    switch (DataAtual.Month)
                    {
                        case 10:
                            mes = "o";
                            break;
                        case 11:
                            mes = "n";
                            break;
                        case 12:
                            mes = "d";
                            break;
                        default:
                            mes = DataAtual.Month.ToString();
                            break;
                    }

                    try
                    {
                        if (NumRemessaSicredi == 1)
                            nomeRemessa = string.Format("{0:00000}", Convert.ToInt64(CodCedente)) + mes + string.Format("{0:00}", DataAtual.Day) + ".crm";
                        else
                            nomeRemessa = string.Format("{0:00000}", Convert.ToInt64(CodCedente)) + mes + string.Format("{0:00}", DataAtual.Day) + ".rms";// + NumRemessaSicredi.ToString().PadLeft(3, '0') ;
                    }
                    catch (OverflowException)
                    {
                        _docs.AppendLine("Código do cedente muito grande, por favor cadastre um código de cedente correto.");
                        nomeRemessa = string.Empty;
                    }
                }
                _boleto.GerarRemessa(Remessa.CaminhoRemessa, sequencia, nomeRemessa);
            }

            if (pdf)
            {
                var numeroDocumento = ListaDocumento.FirstOrDefault()?.NumDocumento;
                var caminhoBoleto = Path.Combine(Remessa.CaminhoPDF, $"boleto.pdf");
                if (!Directory.Exists(Remessa.CaminhoPDF))
                    Directory.CreateDirectory(Remessa.CaminhoPDF);

                if (File.Exists(caminhoBoleto))
                    File.Delete(caminhoBoleto);

                _boleto.GerarPDF();

                var caminhoBoletoGerado = Path.Combine(Remessa.CaminhoPDF, "Boleto_" + (numeroDocumento.IsNullOrEmpty() ? sequencia.ToString() : numeroDocumento) + ".pdf");

                if (File.Exists(caminhoBoletoGerado))
                    File.Delete(caminhoBoletoGerado);

                if (File.Exists(caminhoBoleto))
                    File.Copy(caminhoBoleto, caminhoBoletoGerado, true);
            }

            if (email) _boleto.EnviarEmail(pEmail, Remessa.AssuntoEmail, Remessa.MensagemEmail, "");

            if (impressao) _boleto.Imprimir();

            #endregion

            return ObterListaNossoNumero(pBanco);
        }

        private List<string> ObterListaNossoNumero(int? pBanco)
        {
            var lista = new List<string>();

            var indice = _boleto.TotalTitulosLista();
            for (var i = 0; i < indice; i++)
            {
                var nossoNumero = _boleto.MontarNossoNumero(i);
                if (pBanco == 8)
                    nossoNumero = nossoNumero.Replace("-0", "-9");

                lista.Add(nossoNumero);
            }

            return lista;
        }

        private void IncluirTitulos(IEnumerable<DocumentoBoleto> listaDocumento, int? pBanco, bool pJuro, int seq, int? codigoBaixaDevolucao, bool bExportaJurosMulta, string email)
        {
            var titulo = new ACBrIniFile();
            var contadorTitulo = 1;

            foreach (var doc in listaDocumento)
            {
                titulo[$"Titulo{contadorTitulo}"]["Sacado.CNPJCPF"] = doc.CNPJ.OnlyNumbers().IsNotNullOrEmpty() ? doc.CNPJ.OnlyNumbers().TrimCk() : doc.CPF.OnlyNumbers().TrimCk();
                //titulo[$"Titulo{contadorTitulo}"]["Sacado.Pessoa"] = doc.CNPJ.OnlyNumbers().IsNotNullOrEmpty() ? "1" : "0";
                titulo[$"Titulo{contadorTitulo}"]["Sacado.Pessoa"] = titulo[$"Titulo{contadorTitulo}"]["Sacado.CNPJCPF"].Length == 11 ? "0" : "1";

                titulo[$"Titulo{contadorTitulo}"]["Sacado.NomeSacado"] = doc.NomeSacado?.RemoverAcentos();
                titulo[$"Titulo{contadorTitulo}"]["Sacado.Logradouro"] = doc.Rua?.RemoverAcentos();
                titulo[$"Titulo{contadorTitulo}"]["Sacado.Numero"] = doc.NumeroResidencia;
                titulo[$"Titulo{contadorTitulo}"]["Sacado.Bairro"] = doc.Bairro?.RemoverAcentos();
                titulo[$"Titulo{contadorTitulo}"]["Sacado.Cidade"] = doc.Cidade?.RemoverAcentos();
                titulo[$"Titulo{contadorTitulo}"]["Sacado.UF"] = doc.Estado;
                titulo[$"Titulo{contadorTitulo}"]["Sacado.CEP"] = string.IsNullOrEmpty(doc.CEP) ? "" : doc.CEP.Replace("-", "");
                titulo[$"Titulo{contadorTitulo}"]["Sacado.Complemento"] = string.Empty;

                titulo[$"Titulo{contadorTitulo}"]["DiasDeProtesto"] = doc.DiaProtesto == null ? "0" : doc.DiaProtesto.ToString();
                titulo[$"Titulo{contadorTitulo}"]["Especie"] = Remessa.TipoDocumentoCobranca;
                titulo[$"Titulo{contadorTitulo}"]["EspecieMod"] = "R$";
                titulo[$"Titulo{contadorTitulo}"]["DataDocumento"] = $"{doc.Data:dd/MM/yyyy}";
                titulo[$"Titulo{contadorTitulo}"]["DataProcessamento"] = $"{doc.Data:dd/MM/yyyy}";
                titulo[$"Titulo{contadorTitulo}"]["Vencimento"] = $"{doc.Vencimento:dd/MM/yyyy}";

                //titulo[$"Titulo{contadorTitulo}"]["DataLimitePagto"] = Remessa.DataLimitePagamento;
                titulo[$"Titulo{contadorTitulo}"]["DataLimitePagto"] = Remessa.DataLimitePagamento;

                if (pJuro)
                {
                    titulo[$"Titulo{contadorTitulo}"]["CodigoMora"] = "1";
                    titulo[$"Titulo{contadorTitulo}"]["PercentualMulta"] = $"{doc.PercMulta:##,00}";
                    titulo[$"Titulo{contadorTitulo}"]["ValorMoraJuros"] = $"{doc.Saldo * (doc.Juro / 100) / 30:N}";

                    if (doc.Juro != 0 && doc.DiaJuro != 0)
                    {
                        var dataJuros = doc.Vencimento.GetValueOrDefault().AddDays(doc.DiaJuro.GetValueOrDefault());
                        titulo[$"Titulo{contadorTitulo}"]["DataMoraJuros"] = $"{dataJuros:dd/MM/yyyy}";
                    }

                    if (doc.PercMulta != 0 && doc.DiaMulta != 0)
                    {
                        var dataMulta = doc.Vencimento.GetValueOrDefault().AddDays(doc.DiaMulta.GetValueOrDefault());
                        titulo[$"Titulo{contadorTitulo}"]["DataMulta"] = $"{dataMulta:dd/MM/yyyy}";
                    }
                }

                titulo[$"Titulo{contadorTitulo}"]["Carteira"] = Remessa.Carteira;
                titulo[$"Titulo{contadorTitulo}"]["NossoNumero"] = (doc.Codigo + seq).ToString();
                titulo[$"Titulo{contadorTitulo}"]["NumeroDocumento"] = doc.NumDocumento;
                titulo[$"Titulo{contadorTitulo}"]["ValorDocumento"] = $"{doc.Saldo}";
                titulo[$"Titulo{contadorTitulo}"]["LocalPagamento"] = "Pagável em qualquer agência bancária mesmo após o vencimento";
                if (string.IsNullOrEmpty(Remessa.CodigoGeracao))
                {
                    titulo[$"Titulo{contadorTitulo}"]["CodigoGeracao"] = "0";
                }
                else
                {
                    titulo[$"Titulo{contadorTitulo}"]["CodigoGeracao"] = Remessa.CodigoGeracao.ToString();
                }
                

                titulo[$"Titulo{contadorTitulo}"]["Instrucao1"] = "10";
                titulo[$"Titulo{contadorTitulo}"]["Instrucao2"] = "00";

                #region Mensagem

                //_instrucoes.Append($"{doc.Rua}, {doc.NumeroResidencia} - {doc.Bairro} - {doc.Cidade} - {doc.Estado} - {doc.CEP}");
                //_instrucoes.Append($"\r\n{doc.Telefone} - Código {doc.CodigoSacado}");
                //var msg3 = codigoBaixaDevolucao == null ? "1" : codigoBaixaDevolucao.Value.ToString();

                var mensagem = string.Empty;
                if (!string.IsNullOrEmpty(doc.NumeroNota))
                    mensagem = "NF: " + doc.NumeroNota + " // ";

                mensagem += "Parcela " + doc.Parcela + "/" + doc.QtdParcela;
                titulo[$"Titulo{contadorTitulo}"]["Mensagem"] = mensagem;

                #endregion

                contadorTitulo++;
            }

            _boleto.ConfigurarDados(ObterDadosCedente());
            _boleto.IncluirTitulos(titulo.ToString());
        }

        public void Dispose()
        {
            _boleto.Dispose();
            _boleto = null;
        }

        #endregion

        #region Desativados

        public List<string> GeraBoletoRemessaEmLote(string Caminho, int sequencia, string pOpcao, int? pBanco, int pNumRemessaSicredi, DocumentoBoleto documento, bool pJuro, int seqNossoNumero, bool Naogerarmsgprotesto, int? codigoBaixaDevolucao, bool BExportaJurosMulta)
        {
            //Documentos.Add(documento);
            //ContaCorrente objContaCorrente = new ContaCorrenteClass();
            //Opcao = pOpcao;
            //NumRemessaSicredi = pNumRemessaSicredi;
            //ParametrosBancos(objContaCorrente, pBanco);
            //switch (Opcao)
            //{
            //    case "Impressao": //Boleto
            //        ConfigurarBoleto(objContaCorrente);
            //        break;
            //    case "HTML_EmLote":
            //        ConfigurarBoleto(objContaCorrente);
            //        objContaCorrente.PadroesBoleto.PadroesBoletoEmail.URLLogotipo = Remessa.URLLogotipo;
            //        objContaCorrente.PadroesBoleto.PadroesBoletoEmail.URLImagensCodigoBarras = Remessa.URLCodigodeBarras;
            //        objContaCorrente.PadroesBoleto.PadroesBoletoEmail.LayoutBoletoEmail = "PadraoReciboPersonalizadoImpressao";
            //        break;
            //    case "PDF_EmLote":
            //        ConfigurarBoleto(objContaCorrente);
            //        objContaCorrente.PadroesBoleto.PadroesBoletoEmail.URLLogotipo = Remessa.URLLogotipo;
            //        objContaCorrente.PadroesBoleto.PadroesBoletoEmail.URLImagensCodigoBarras = Remessa.URLCodigodeBarras;
            //        objContaCorrente.PadroesBoleto.PadroesBoletoEmail.LayoutBoletoEmail = "PadraoReciboPersonalizadoImpressao";
            //        break;
            //    default: //Débito em Conta Corrente
            //        ConfigurarRemessa(Caminho, sequencia, pBanco, objContaCorrente);
            //        break;
            //}
            //foreach (DocumentoBoleto doc in Documentos)
            //{
            //    instrucoes.Remove(0, instrucoes.Length);
            //    instrucoesCaixa.Remove(0, instrucoesCaixa.Length);
            //    MontarDocumento(objContaCorrente, doc, pBanco, pJuro, seqNossoNumero, Naogerarmsgprotesto, codigoBaixaDevolucao, BExportaJurosMulta);
            //}
            //GerarDocumentos(objContaCorrente);
            //foreach (var itemLista in ListaDocumento) 
            //    listaRetornoNossoNumero.Add(objContaCorrente.CodigoCedente + string.Format("{0:0000000000}", itemLista.Codigo));
            //return listaRetornoNossoNumero;
            return null;
        }

        // VERIFICAR
        private void ConfigurarRemessa(string Caminho, int sequencia, int? pBanco, /*ContaCorrente objContaCorrente*/ dynamic objContaCorrente)
        {
            string arquivo;
            CaminhoRemessa = Caminho;

            if (pBanco != 8) //sicredi
            {
                DateTime dataAtual = DateTime.Now;
                objContaCorrente.ArquivoRemessa.DataGravacao = dataAtual.Date.ToString();

                if (pBanco == 0) //Nomenclatura padrão da Caixa
                    arquivo = "E" + string.Format("{0:00}", dataAtual.Day) + "000" + sequencia + ".rem";
                else
                    arquivo = "R" + sequencia + ".rem";

                CaminhoArquivoRemessa = arquivo;
                objContaCorrente.ArquivoRemessa.Arquivo = arquivo;

                if (!Directory.Exists(Caminho))
                    Directory.CreateDirectory(Caminho);

                objContaCorrente.ArquivoRemessa.Diretorio = Caminho;
                objContaCorrente.ArquivoRemessa.Layout = Remessa.LayoutRemessa;
                objContaCorrente.ArquivoRemessa.Sequencia = sequencia; //nao deveria ser a sequencia????
            }
            else
            {
                string mes;
                DateTime DataAtual = DateTime.Now.Date;
                string CodCedente = Remessa.CodigoCedente.Replace(".", "").Replace(",", "").Replace("-", "").Replace("_", "").Replace("|", "").Replace("/", "").Replace("\\", "");
                objContaCorrente.ArquivoRemessa.DataGravacao = DataAtual.ToString();
                switch (DataAtual.Month)
                {
                    case 10:
                        mes = "o";
                        break;
                    case 11:
                        mes = "n";
                        break;
                    case 12:
                        mes = "d";
                        break;
                    default:
                        mes = DataAtual.Month.ToString();
                        break;
                }

                try
                {
                    if (NumRemessaSicredi == 1)
                        arquivo = string.Format("{0:00000}", Convert.ToInt64(CodCedente)) + mes + string.Format("{0:00}", DataAtual.Day) + ".crm";
                    else
                        arquivo = string.Format("{0:00000}", Convert.ToInt64(CodCedente)) + mes + string.Format("{0:00}", DataAtual.Day) + ".rm" + NumRemessaSicredi;
                }
                catch (OverflowException)
                {
                    _docs.AppendLine("Código do cedente muito grande, por favor cadastre um código de cedente correto.");
                    arquivo = string.Empty;
                }

                objContaCorrente.ArquivoRemessa.Arquivo = arquivo;

                if (!Directory.Exists(Caminho))
                    Directory.CreateDirectory(Caminho);

                objContaCorrente.ArquivoRemessa.Diretorio = Caminho;
                objContaCorrente.ArquivoRemessa.Layout = Remessa.LayoutRemessa;
                objContaCorrente.ArquivoRemessa.Sequencia = sequencia; //nao deveria ser a sequencia????
            }
        }

        // VERIFICAR (OK)
        private void MontarDocumento(DocumentoBoleto doc, int contador, int? pBanco, bool pJuro, int seq, bool Naogerarmsgprotesto, int? codigoBaixaDevolucao, bool BExportaJurosMulta)
        {
            var titulo = new ACBrIniFile();

            titulo[$"Titulo{contador}"]["Sacado.CNPJCPF"] = doc.CNPJ.OnlyNumbers().IsNotNullOrEmpty() ? doc.CNPJ.OnlyNumbers().TrimCk() : doc.CPF.OnlyNumbers().TrimCk(); // Fopi alterado esta linha tb -BB - Pessoa Fisica - código 1
            //titulo[$"Titulo{contador}"]["Sacado.Pessoa"] = doc.CNPJ.OnlyNumbers().IsNotNullOrEmpty() ? "1" : "0";
            titulo[$"Titulo{contador}"]["Sacado.Pessoa"] = titulo[$"Titulo{contador}"]["Sacado.CNPJCPF"].Length == 11 ? "0" : "1"; // Alteração para gerar boleto com código 1 BB se for Pessoa Fisica - 26/01/2022

            titulo[$"Titulo{contador}"]["Sacado.NomeSacado"] = doc.NomeSacado?.RemoverAcentos();
            titulo[$"Titulo{contador}"]["Sacado.Logradouro"] = doc.Rua?.RemoverAcentos();
            titulo[$"Titulo{contador}"]["Sacado.Numero"] = doc.NumeroResidencia;
            titulo[$"Titulo{contador}"]["Sacado.Bairro"] = doc.Bairro?.RemoverAcentos();
            titulo[$"Titulo{contador}"]["Sacado.Cidade"] = doc.Cidade?.RemoverAcentos();
            titulo[$"Titulo{contador}"]["Sacado.UF"] = doc.Estado;
            titulo[$"Titulo{contador}"]["Sacado.CEP"] = string.IsNullOrEmpty(doc.CEP) ? "" : doc.CEP.Replace("-", "");
            titulo[$"Titulo{contador}"]["Sacado.Complemento"] = string.Empty;
            titulo[$"Titulo{contador}"]["Sacado.Email"] = Email.IsNotNullOrEmpty() ? Email.TrimCk() : string.Empty;

            titulo[$"Titulo{contador}"]["DiasDeProtesto"] = doc.DiaProtesto == null ? "0" : doc.DiaProtesto.ToString();
            titulo[$"Titulo{contador}"]["Especie"] = Remessa.TipoDocumentoCobranca;
            titulo[$"Titulo{contador}"]["EspecieMod"] = "R$";
            titulo[$"Titulo{contador}"]["DataDocumento"] = $"{doc.Data:dd/MM/yyyy}";
            titulo[$"Titulo{contador}"]["DataProcessamento"] = $"{doc.Data:dd/MM/yyyy}";
            titulo[$"Titulo{contador}"]["Vencimento"] = $"{doc.Vencimento:dd/MM/yyyy}";
            titulo[$"Titulo{contador}"]["PercentualMulta"] = $"{doc.PercMulta:##,00}";
            titulo[$"Titulo{contador}"]["ValorMoraJuros"] = $"{doc.Saldo * (doc.Juro / 100) / 30:N}";
            titulo[$"Titulo{contador}"]["CodigoMora"] = "1";

            //titulo[$"Titulo{contador}"]["DataMoraJuros"] = "1";
            //titulo[$"Titulo{contador}"]["DataMulta"] = "1";

            titulo[$"Titulo{contador}"]["NossoNumero"] = (doc.Codigo + seq).ToString();
            titulo[$"Titulo{contador}"]["NumeroDocumento"] = doc.NumDocumento;
            titulo[$"Titulo{contador}"]["ValorDocumento"] = $"{doc.Saldo.GetValueOrDefault(0):N}";
            titulo[$"Titulo{contador}"]["LocalPagamento"] = "Pagável em qualquer agência bancária mesmo após o vencimento";

            titulo[$"Titulo{contador}"]["Instrucao1"] = "10";
            titulo[$"Titulo{contador}"]["Instrucao2"] = "11";

            // ############### INICIO INSTRUÇÃO / MSG
            //valorjuro = doc.Saldo.Value * doc.Juro / 100 / 30;
            //valormulta = doc.Saldo.Value * doc.PercMulta / 100;

            ////_instrucoesCaixa.Append(doc.InstrucoesCaixa);
            ////boleto.PadroesBoleto.InstrucoesCaixa = _instrucoesCaixa + "<br>";

            //_instrucoes.Append(boleto.EnderecoSacado + ", " + boleto.BairroSacado + ", " + boleto.CidadeSacado + " - " + boleto.EstadoSacado + "  " + boleto.CepSacado);
            //_instrucoes.Append("<br>" + doc.Telefone + ", Código " + doc.CodigoSacado);

            //if (pBanco != 3)
            //    //instrução1 aparecerá no comprovante de entrega (primeiro)
            //    boleto.InstrucaoCobranca1 = _instrucoes.ToString();

            ////instrução2 aparecerá no recibo do sacado (segundo)
            //if (!string.IsNullOrEmpty(doc.NumeroNota))
            //    boleto.InstrucaoCobranca2 = "NF: " + doc.NumeroNota + " // ";

            //boleto.InstrucaoCobranca2 += "Parcela " + doc.Parcela + "/" + doc.QtdParcela;

            //boleto.InstrucaoCobranca3 = codigoBaixaDevolucao == null ? "1" : codigoBaixaDevolucao.Value.ToString();
            //boleto.ValorInstrucaoCobranca3 = doc.ValorInstrucaoCobranca3;

            //if (!string.IsNullOrEmpty(boleto.PadroesBoleto.InstrucoesCaixa) && boleto.PadroesBoleto.InstrucoesCaixa.IndexOf("*vlrjuro") != -1)
            //    boleto.PadroesBoleto.InstrucoesCaixa = boleto.PadroesBoleto.InstrucoesCaixa.Replace("*vlrjuro", string.Format("{0:c2}", valorjuro));

            //if (!string.IsNullOrEmpty(boleto.PadroesBoleto.InstrucoesCaixa) && boleto.PadroesBoleto.InstrucoesCaixa.IndexOf("*vlrmulta") != -1)
            //    boleto.PadroesBoleto.InstrucoesCaixa = boleto.PadroesBoleto.InstrucoesCaixa.Replace("*vlrmulta", string.Format("{0:c2}", valormulta));

            //if (Naogerarmsgprotesto)
            //    boleto.NaoGerarMensagemProtesto = true;
            // ###################### FINAL INSTRUÇÃO MSG

            //if (BExportaJurosMulta)
            //{
            //    /*IMeuDado*/
            //    dynamic MDado = boleto.MeusDados.Add;
            //    MDado.Nome = "DataJurosMora";
            //    MDado.Valor = string.Format("{0:dd/MM/yyyy}", doc.Vencimento.Value.AddDays(doc.DiaJuro.Value == 0 ? 1 : doc.DiaJuro.Value));

            //    /*IMeuDado*/
            //    dynamic MDado1 = boleto.MeusDados.Add;
            //    MDado1.Nome = "FormaCadastramento";
            //    MDado1.Valor = "7"; // "1";

            //    boleto.ValorJurosDiaAtraso = Convert.ToDouble(valorjuro);
            //    boleto.ValorMultaAtraso = Convert.ToDouble(valormulta);
            //    boleto.ValorDesconto = 0;
            //}

            doc.ConteudoTitulo = titulo.ToString();

            // ## INICIO BKP

            //boleto.NomeSacado = doc.NomeSacado;
            //boleto.EnderecoSacado = doc.Rua;
            //boleto.BairroSacado = doc.Bairro;
            //boleto.CidadeSacado = doc.Cidade;
            //boleto.EstadoSacado = doc.Estado;
            //boleto.CepSacado = string.IsNullOrEmpty(doc.CEP) ? "" : doc.CEP.Replace("-", "");

            // ################################## NAO UTILIZADO AINDA
            //switch (pBanco)
            //{
            //    case 1:
            //    case 3: //SANTANDER
            //        boleto.AcaoCobrancaRemessa = "01";
            //        boleto.InstrucaoCobranca1 = "2";
            //        break;
            //    case 2: //Sicredi
            //        boleto.NumeroParcelaCarnet = doc.Parcela.Value;
            //        boleto.AcaoCobrancaRemessa = "02";
            //        boleto.InstrucaoCobranca1 = "2";
            //        break;
            //    case 4: //Sicoob
            //        boleto.NumeroParcelaCarnet = 1; // doc.Parcela.Value;
            //        boleto.AcaoCobrancaRemessa = "01";
            //        boleto.InstrucaoCobranca1 = "02"; // "2";
            //        boleto.DataLimiteDesconto = string.Format("{0:dd/MM/yyyy}", doc.Vencimento);

            //        break;
            //    default:
            //        boleto.AcaoCobrancaRemessa = "02";
            //        break;
            //}

            //boleto.BancoEmiteBoleto = doc.BancoEmiteBoleto == null ? false : (bool) doc.BancoEmiteBoleto;
            // #######################################################

            //boleto.DiasProtesto = doc.DiaProtesto == null ? 0 : (int) doc.DiaProtesto;
            //boleto.TipoDocumentoCobranca = Remessa.TipoDocumentoCobranca;

            //if (doc.CNPJ != string.Empty)
            //    boleto.CNPJSacado = doc.CNPJ;

            //else if (doc.CPF != string.Empty)
            //    boleto.CPFSacado = doc.CPF;

            //boleto.DataDocumento = string.Format("{0:dd/MM/yyyy}", doc.Data);
            // boleto.DataVencimento = string.Format("{0:dd/MM/yyyy}", doc.Vencimento);

            //boleto.PercentualMultaAtraso = Convert.ToDouble(doc.PercMulta);
            //boleto.PercentualJurosDiaAtraso = Convert.ToDouble(doc.Juro / 30);

            //boleto.NossoNumero = Convert.ToString(doc.Codigo + seq);

            // ############ VERIFICAR DEPOIS
            //if (pBanco == 0) //Caixa Economica
            //{
            //    if (Opcao == "Remessa")
            //    {
            //        if (Remessa.LayoutRemessa == "CNAB400")
            //            boleto.NumeroDocumento = string.Format("{0:0000000000}", Convert.ToInt64(doc.NumDocumento));
            //        else
            //            boleto.NumeroDocumento = string.Format("{0:00000000000}", Convert.ToInt64(doc.NumDocumento));
            //    }
            //    else // BOLETO IMPRESSAO
            //    {
            //        boleto.NumeroDocumento = string.Format("{0:00000000000}", Convert.ToInt64(doc.NumDocumento)) + " / " + doc.Parcela;
            //    }
            //}
            //else
            //{
            //    if (Opcao == "Remessa")
            //    {
            //        if (Remessa.LayoutRemessa == "CNAB400")
            //            boleto.NumeroDocumento = string.Format("{0:0000000000}", Convert.ToInt64(doc.NumDocumento));
            //        else
            //            boleto.NumeroDocumento = string.Format("{0:00000000000000}", Convert.ToInt64(doc.NumDocumento));
            //    }
            //    else // BOLETO IMPRESSAO
            //    {
            //        boleto.NumeroDocumento = string.Format("{0:0000000000000}", Convert.ToInt64(doc.NumDocumento));
            //    }
            //}
            // #################################

            //boleto.ValorDocumento = Convert.ToDouble(doc.Saldo);
            //boleto.BancoEmiteBoleto = Remessa.BancoEmiteBoleto;

            //switch (Remessa.LayoutRemessa)
            //{
            //    case "DebitoCC": //Débito em conta
            //        try
            //        {
            //            doc.AgenciaDebito = string.Format("{0:0000-0}", Convert.ToInt64(doc.AgenciaDebito.Replace("-", "").Replace(".", "").Replace("/", "")));

            //            cc = doc.ContaCorrenteDebito.Replace("-", "").Replace(".", "").Replace("/", "");
            //            tamanho = cc.Length;
            //            //doc.ContaCorrenteDebito = String.Format("{0:00000000}", Convert.ToInt64(cc.Substring(0, tamanho - 1))) + "-" + cc.Substring((tamanho - 1), 1);
            //            doc.ContaCorrenteDebito = string.Format("{0:00000000}", Convert.ToInt64(cc.Substring(0, tamanho - 1)));

            //            identificador = doc.NumDocumento.Replace("-", "").Replace(".", "").Replace("/", "");
            //            boleto.NumeroDocumento = Convert.ToDouble(identificador).ToString("000000000000000000");
            //            boleto.NossoNumero = Convert.ToDouble(identificador).ToString("000000000000000000");

            //            boleto.ContaCorrenteDebito = doc.ContaCorrenteDebito;
            //            boleto.AgenciaDebito = doc.AgenciaDebito.Replace("-", "").Replace(".", "").Replace(",", "").Replace("/", "");

            //            tamanho = 0;
            //        }
            //        catch
            //        {
            //            _docs.AppendLine("A pessoa " + doc.NomeSacado + " deste docuemnto " + doc.NumDocumento + " possui numeros de agência de debito ou conta corrente inválidos.");
            //        }

            //        break;
            //    default: //Boleto

            //valorjuro = doc.Saldo.Value * doc.Juro / 100 / 30;
            //valormulta = doc.Saldo.Value * doc.PercMulta / 100;

            ////if (valor.Trim() != String.Empty)
            //_instrucoesCaixa.Append(doc.InstrucoesCaixa);

            ////boleto.PadroesBoleto.InstrucoesCaixa = objBanco.InstrucoesCaixa;
            //boleto.PadroesBoleto.InstrucoesCaixa = _instrucoesCaixa + "<br>";

            //_instrucoes.Append(boleto.EnderecoSacado + ", " + boleto.BairroSacado + ", " + boleto.CidadeSacado + " - " + boleto.EstadoSacado + "  " + boleto.CepSacado);
            //_instrucoes.Append("<br>" + doc.Telefone + ", Código " + doc.CodigoSacado);

            //if (pBanco != 3)
            //    //instrução1 aparecerá no comprovante de entrega (primeiro)
            //    boleto.InstrucaoCobranca1 = _instrucoes.ToString();

            ////instrução2 aparecerá no recibo do sacado (segundo)
            //if (!string.IsNullOrEmpty(doc.NumeroNota))
            //    boleto.InstrucaoCobranca2 = "NF: " + doc.NumeroNota + " // ";

            //boleto.InstrucaoCobranca2 += "Parcela " + doc.Parcela + "/" + doc.QtdParcela;

            //boleto.InstrucaoCobranca3 = codigoBaixaDevolucao == null ? "1" : codigoBaixaDevolucao.Value.ToString();
            //boleto.ValorInstrucaoCobranca3 = doc.ValorInstrucaoCobranca3;

            //if (!string.IsNullOrEmpty(boleto.PadroesBoleto.InstrucoesCaixa) && boleto.PadroesBoleto.InstrucoesCaixa.IndexOf("*vlrjuro") != -1)
            //    boleto.PadroesBoleto.InstrucoesCaixa = boleto.PadroesBoleto.InstrucoesCaixa.Replace("*vlrjuro", string.Format("{0:c2}", valorjuro));

            //if (!string.IsNullOrEmpty(boleto.PadroesBoleto.InstrucoesCaixa) && boleto.PadroesBoleto.InstrucoesCaixa.IndexOf("*vlrmulta") != -1)
            //    boleto.PadroesBoleto.InstrucoesCaixa = boleto.PadroesBoleto.InstrucoesCaixa.Replace("*vlrmulta", string.Format("{0:c2}", valormulta));

            //if (BExportaJurosMulta)
            //{
            //    /*IMeuDado*/
            //    dynamic MDado = boleto.MeusDados.Add;
            //    MDado.Nome = "DataJurosMora";
            //    MDado.Valor = string.Format("{0:dd/MM/yyyy}", doc.Vencimento.Value.AddDays(doc.DiaJuro.Value == 0 ? 1 : doc.DiaJuro.Value));

            //    /*IMeuDado*/
            //    dynamic MDado1 = boleto.MeusDados.Add;
            //    MDado1.Nome = "FormaCadastramento";
            //    MDado1.Valor = "7"; // "1";

            //    boleto.ValorJurosDiaAtraso = Convert.ToDouble(valorjuro);
            //    boleto.ValorMultaAtraso = Convert.ToDouble(valormulta);
            //    boleto.ValorDesconto = 0;
            //}
        }

        #endregion
    }
}