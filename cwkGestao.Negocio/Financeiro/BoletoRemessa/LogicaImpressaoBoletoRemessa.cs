using System;
using System.Collections.Generic;
using System.Linq;
using cwkGestao.Integracao.ACBr.Boleto;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Modelo.Util;
using Remessa = cwkGestao.Modelo.Remessa;

namespace cwkGestao.Negocio.Financeiro.BoletoRemessa
{
    public class LogicaImpressaoBoletoRemessa
    {
        #region Propriedades

        #endregion

        #region Construtores

        #endregion

        #region Métodos

        #endregion

        #region Constantes

        #endregion

        #region Métodos Estáticos

        #endregion

        private readonly DocumentoController documentoController = DocumentoController.Instancia;
        private readonly RemessaController remessaController = RemessaController.Instancia;
        private Boleto boletoController;
        private List<string> NossosNumeros = new List<string>();
        private IList<Tel_Fatura> faturasComDocumentos = new List<Tel_Fatura>();
        private int numRemessaSicredi;
#pragma warning disable CS0414 // O campo "LogicaImpressaoBoletoRemessa.existeFatura" é atribuído, mas seu valor nunca é usado
        private bool existeFatura = false;
#pragma warning restore CS0414 // O campo "LogicaImpressaoBoletoRemessa.existeFatura" é atribuído, mas seu valor nunca é usado
        private Filial _filial;
#pragma warning disable CS0169 // O campo "LogicaImpressaoBoletoRemessa._portador" nunca é usado
        private Portador _portador;
#pragma warning restore CS0169 // O campo "LogicaImpressaoBoletoRemessa._portador" nunca é usado
        private Remessa _remessa;

        public List<pxyRemessa> Documentos { get; set; }
        public Nota Nota { get; set; }
        public int Filtro { get; set; }
        public TipoDocumentoType Tipo { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public bool Impresso { get; set; }
        public bool GeradoRemessa { get; set; }
        public bool GeradoPDF { get; set; }
        public bool EnviadoEmail { get; set; }
        public bool TodosDocumentos { get; set; }
        public string Email { get; set; }
        public string EmailsDestino { get; set; }
        public bool EmLote = false;

        public Filial Filial
        {
            get => _filial;
            set
            {
                //_filial = value;
                _filial = Tel_ConfiguracaoController.Instancia.GetConfiguracao().Filial;
                if (_filial != null) SetDadosFilial();
            }
        }

        public Remessa Remessa
        {
            get => _remessa;
            set
            {
                _remessa = value;
                if (_remessa != null) SetDadosRemessa();
            }
        }

        private void SetDadosFilial()
        {
            if (boletoController == null)
                boletoController = new Boleto();

            boletoController.Remessa.Nome = _filial.Nome;
            boletoController.Remessa.Bairro = _filial.Bairro;
            boletoController.Remessa.Cep = _filial.CEP;
            boletoController.Remessa.Cidade = _filial.Cidade.Nome;
            boletoController.Remessa.CpfCnpj = _filial.CNPJ.OnlyNumbers();
            boletoController.Remessa.Complemento = _filial.Complemento;
            boletoController.Remessa.Logradouro = _filial.Endereco;
            boletoController.Remessa.NumeroResidencia = _filial.Numero;
            boletoController.Remessa.Telefone = _filial.Telefone.OnlyNumbers();
            boletoController.Remessa.UF = _filial.Cidade.UF.Sigla;

            boletoController.Remessa.EmailRemetente = _filial.UsuarioEmail;
            boletoController.Remessa.ServidorSMTP = _filial.ServidorSMTP;
            boletoController.Remessa.PortaSMTP = _filial.PortaSmtp;
            boletoController.Remessa.UsuarioEmail = _filial.UsuarioEmail;
            boletoController.Remessa.SenhaEmail = _filial.SenhaEmail;
            boletoController.Remessa.EmailRemetente = _filial.UsuarioEmail;
            boletoController.Remessa.ProximoNossoNumero = "1";
        }

        private void SetDadosRemessa()
        {
            if (boletoController == null)
                boletoController = new Boleto();

            boletoController.Remessa.Banco = _remessa.Banco;
            boletoController.Remessa.CodigoAgencia = _remessa.CodigoAgencia;
            boletoController.Remessa.DigitoAgencia = _remessa.DigitoAgencia;
            boletoController.Remessa.ContaCorrente = _remessa.ContaCorrente;
            boletoController.Remessa.DigitoConta = _remessa.DigitoConta;
            boletoController.Remessa.CodigoCedente = _remessa.CodigoCedente;
            boletoController.Remessa.DigitoAgenciaConta = _remessa.DigitoAgenciaConta;
            boletoController.Remessa.TipoCarteira = Convert.ToInt32(_remessa.SicrediTipoCobranca);
            boletoController.Remessa.Carteira = _remessa.VariacaoCarteira;
            boletoController.Remessa.LayoutBoleto = _remessa.LayoutBoleto;
            boletoController.Remessa.LayoutRemessa = _remessa.LayoutRemessa;
            boletoController.Remessa.CaminhoRemessa = _remessa.CaminhoRemessa;
            boletoController.Remessa.NomeRemessa = _remessa.NomeRemessa;
            boletoController.Remessa.CaminhoRetorno = _remessa.CaminhoRetorno;
            boletoController.Remessa.NomeRetorno = _remessa.NomeRetorno;
            boletoController.Remessa.VersaoLayoutArquivo = _remessa.VersaoLayoutArquivo;
            boletoController.Remessa.VersaoLayoutLote = _remessa.VersaoLayoutLote;
            boletoController.Remessa.Convenio = _remessa.Convenio;
            boletoController.Remessa.NumeroContrato = _remessa.NumeroContrato;
            boletoController.Remessa.Carteira = _remessa.Carteira;
            boletoController.Remessa.CodigoGeracao = _remessa.CodigoGeracao;
            boletoController.Remessa.DataLimitePagamento = _remessa.DataLimitePagamento;


            boletoController.Remessa.CpfCnpj = _filial.CNPJ.OnlyNumbers();
            boletoController.Remessa.Nome = _filial.Nome;
            boletoController.Remessa.Logradouro = _filial.Endereco;
            boletoController.Remessa.NumeroResidencia = _filial.Numero;
            boletoController.Remessa.Bairro = _filial.Bairro;
            boletoController.Remessa.Cidade = _filial.Cidade.Nome;
            boletoController.Remessa.UF = _filial.Cidade.UF.Sigla;
            boletoController.Remessa.Cep = _filial.CEP.OnlyNumbers();

            boletoController.Remessa.Logotipo = _remessa.Logotipo;
            boletoController.Remessa.InstrucoesCaixa = _remessa.InstrucoesCaixa;
            boletoController.Remessa.InicioNossoNumero = "00001";
            boletoController.Remessa.FimNossoNumero = "99999";
            boletoController.Remessa.MensagemEmail = _remessa.MensagemEmail;
            boletoController.Remessa.AssuntoEmail = _remessa.AssuntoEmail;
            boletoController.Remessa.NomeEmail = _remessa.NomeDoEmail;
            boletoController.Remessa.BancoEmiteBoleto = _remessa.BancoEmiteBoleto;
            boletoController.Remessa.CaminhoPDF = _remessa.CaminhoPDF;
            boletoController.Remessa.ImpressoraPDF = _remessa.ImpressoraNome;
            boletoController.Remessa.CaminhoHTML = _remessa.CaminhoHTML;

            // Variação Carteira x Modalidade
            boletoController.Remessa.OutroDadoConfiguracao1 = _remessa.VariacaoCarteira;
            boletoController.Remessa.TipoDocumentoCobranca = "DM";

            // Não utilizado
            //boletoController.Remessa.CaminhoImagem = _remessa.CaminhoImagem;
            //boletoController.Remessa.CaminhoLayoutBoleto = _remessa.CaminhoLayoutBoleto;
            //boletoController.Remessa.MargemSuperior = 15;
            //boletoController.Remessa.LayoutBoletoEmail = _remessa.LayoutBoletoEmail;
            //boletoController.Remessa.URLLogotipo = _remessa.URLLogotipo;
            //boletoController.Remessa.URLCodigodeBarras = _remessa.URLCodigodeBarras;
            //if (_remessa.Banco == 8)
            //{
            //    boletoController.Remessa.TipoDocumentoCobranca = _remessa.EspecieTituloSicredi;
            //    boletoController.Remessa.OutroDadoConfiguracao1 = _remessa.Sequencia.ToString().Substring(0, 1);
            //}
            //else
            //{
            //    boletoController.Remessa.TipoDocumentoCobranca = "DM";
            //    boletoController.Remessa.OutroDadoConfiguracao1 = _remessa.VariacaoCarteira;
            //}
            boletoController.ConfigurarComponente();
        }

        public LogicaImpressaoBoletoRemessa()
        {
            Documentos = new List<pxyRemessa>();
            boletoController = new Boleto();

            // remover
            _filial = Tel_ConfiguracaoController.Instancia.GetConfiguracao().Filial;
            SetDadosFilial();

        }

        public LogicaImpressaoBoletoRemessa(Nota nota) : this() { }

        public void AtualizarListaDocumentos()
        {
            Documentos.Clear();
            boletoController.ListaDocumento.Clear();
            if (Nota != null)
                Documentos.AddRange(documentoController.GetRemessaPorNota(Nota));
            else
                Documentos.AddRange(documentoController.GetRemessa(Filial, DataInicial, DataFinal, Filtro == 0, Tipo, Impresso, GeradoRemessa, GeradoPDF, EnviadoEmail, TodosDocumentos));
        }

        public IList<Documento> DevolveListaDocumento()
        {
            IList<Documento> listaDeDocumentos = new List<Documento>();

            var proxyRemessa = Documentos.Where(d => d.Selecionado).ToList();

            foreach (var item in proxyRemessa) listaDeDocumentos.Add(DocumentoController.Instancia.GetDocumentoPorCodigo(item.Codigo).FirstOrDefault());
            return listaDeDocumentos;
        }

        public IList<pxyRemessa> DevolveListaPxyRemessaParaGerarArquivoRemessa(int idArquivoRemessa)
        {
            Documentos.Clear();
            boletoController.ListaDocumento.Clear();

            IList<pxyRemessa> pxy = DocumentoController.Instancia.GetDocumentosPorArquivoRemessa(idArquivoRemessa);

            Documentos.AddRange(pxy);

            return pxy;
        }

        public bool ExistemDocumentosSelecionados
        {
            get
            {
                int contador = Documentos.Where(d => d.Selecionado).Count();
                if (contador > 0)
                    return true;
                return false;
            }
        }

        private IEnumerable<DocumentoBoleto> GetDocumentosProcessar(bool consideraValorTotalDocumento)
        {
            IList<Documento> docs = documentoController.GetGerarBoletoRemessa(Documentos.Where(d => d.Selecionado).ToList());

            foreach (var doc in docs.Distinct())
            {
                doc.Saldo = consideraValorTotalDocumento ? doc.Valor : doc.Saldo;
                yield return new DocumentoBoletoImpl(_remessa, doc);
            }
        }

        public void GerarBoletosRemessaEmLote(Documento documento, bool consideraValorTotalDocumento, int sequencia)
        {
            ChamarRotinaGerarEmLote("PDF_EmLote", documento, consideraValorTotalDocumento, sequencia, true, false, false);
        }

        public void GerarBoletosPDFEmLote(Documento documento, bool consideraValorTotalDocumento, int sequencia)
        {
            ChamarRotinaGerarEmLote("PDF_EmLote", documento, consideraValorTotalDocumento, sequencia, false, true, true);
        }

        //public void GerarBoletosEmLote(Documento documento, bool consideraValorTotalDocumento)
        //{
        //    ChamarRotinaGerarEmLote("HTML_EmLote", documento, consideraValorTotalDocumento);
        //}

        public void GerarHTML(bool consideraValorTotalDocumento)
        {
            //ChamarRotinaGerar("HTML", consideraValorTotalDocumento);
        }

        public void SalvarBoleto(DocumentoBoleto item, int sequencia)
        {
            var doc = ((DocumentoBoletoImpl) item).Documento;
            doc.BGeradoRemessa = true;
            doc.NossoNumero = NossosNumeros.Count() > 0 ? NossosNumeros[sequencia] : "";

            var TelFatura = Tel_FaturaController.Instancia.GetFaturaByDocumento(doc.ID);
            if (TelFatura != null)
            {
                TelFatura.NossoNumero = doc.NossoNumero;
                Tel_FaturaController.Instancia.Salvar(TelFatura, Acao.Alterar);
            }
        }

        public void AtualizaRemessa()
        {
            _remessa.Sequencia++;
        }

        private void ChamarRotinaGerarEmLote(string tipo, Documento documento, bool consideraValorTotalDocumento, int sequencia, bool remessa, bool impressao, bool pdf)
        {
            DocumentoBoletoImpl docPerametro = new DocumentoBoletoImpl(_remessa, documento);

            if (_remessa.SomaNossoNumero < 0) _remessa.SomaNossoNumero = 0;

            if (boletoController.ListaDocumento.Count == 0)
                boletoController.ListaDocumento.AddRange(GetDocumentosProcessar(consideraValorTotalDocumento));

            //if (_remessa.Banco == 2) SicrediNumeroRemessa(tipo);
            try
            {
                NossosNumeros = boletoController.GeraBoletoRemessa(remessa, impressao, pdf, false, _remessa.Sequencia, string.Empty, _remessa.Banco, numRemessaSicredi, _remessa.PadraoJuroBoleto, _remessa.SomaNossoNumero, _remessa.CodigoBaixaDevolucao, _remessa.BExportaJurosMulta);

                sequencia = 0;
                foreach (var item in boletoController.ListaDocumento)
                {
                    Documento doc = ((DocumentoBoletoImpl) item).Documento;
                    doc.BImpresso = true;
                    doc.NossoNumero = NossosNumeros[sequencia];
                    sequencia++;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public Boleto retornaListaBoleto() =>
            //boletoController.ListaDocumento.AddRange(GetDocumentosProcessar(false));
            //NossosNumeros = AtribuiNossosNumeros();
            //return boletoController;
            null;

        private List<string> AtribuiNossosNumeros() =>
            //var retorno = boletoController.GeraBoletoRemessa(_remessa.CaminhoRemessa, _remessa.Sequencia, "Remessa", Email == null ? "" : Email,
            //    _remessa.Banco, numRemessaSicredi, _remessa.PadraoJuroBoleto, Remessa.SomaNossoNumero, _remessa.BNaoGeraMsgProtesto, _remessa.CodigoBaixaDevolucao, _remessa.BExportaJurosMulta);
            //return retorno;
            null;

        private void SicrediNumeroRemessa(bool seRemessa)
        {
            DateTime DataAtual = DateTime.Now.Date;
            string NumSicredi = DataAtual.Year + string.Format("{0:00}", DataAtual.Month) + string.Format("{0:00}", DataAtual.Day) + "1";

            if (_remessa.SicrediNumeroRemessa <= 0 || _remessa.SicrediNumeroRemessa < Convert.ToInt32(NumSicredi))
            {
                numRemessaSicredi = 1;
                _remessa.SicrediNumeroRemessa = Convert.ToInt32(NumSicredi);
            }
            else
            {
                int aux = Convert.ToInt32(_remessa.SicrediNumeroRemessa.ToString().Substring(8));
                if (aux < 10)
                {
                    numRemessaSicredi = ++aux;
                    string NovoNumeroSicredi = _remessa.SicrediNumeroRemessa.ToString().Substring(0, 8) + aux;
                    _remessa.SicrediNumeroRemessa = Convert.ToInt32(NovoNumeroSicredi);
                }
                else if (seRemessa)
                {
                    throw new Exception("O numero limite de remessas enviadas hoje foi alcançado, não é possivel enviar mais remessas hoje.");
                }
            }
        }

        public Documento CarregarDocumentoOriginal(int id) => DocumentoController.Instancia.LoadObjectById(id);

        //Caso ele tenha impresso com o valor total do documento no saldo, ele recarrega novamente o valor do saldo.
        public void PersistirDados(bool valorTotalDocumento)
        {
            remessaController.RegisterNewIntoTransaction(_remessa);
            foreach (var item in boletoController.ListaDocumento)
            {
                Documento doc;
                if (valorTotalDocumento)
                {
                    ((DocumentoBoletoImpl) item).Documento.Saldo = CarregarDocumentoOriginal(((DocumentoBoletoImpl) item).Documento.ID).Saldo;
                    doc = ((DocumentoBoletoImpl) item).Documento;
                }
                else
                {
                    doc = ((DocumentoBoletoImpl) item).Documento;
                }

                documentoController.RegisterNewIntoTransaction(doc);
            }

            documentoController.CommitUnitOfWorkTransaction();
            Documentos.Clear();
        }

        public void LimparDados()
        {
            boletoController.ListaDocumento.Clear();
        }

        public void GerarBoleto(bool valorTotalDocumento, bool remessa, bool impressao, bool pdf, bool email)
        {
            try
            {
                if (boletoController.ListaDocumento.Count == 0) boletoController.ListaDocumento.AddRange(GetDocumentosProcessar(valorTotalDocumento));
                if (_remessa.Banco == 8) SicrediNumeroRemessa(remessa);

                NossosNumeros = boletoController.GeraBoletoRemessa(
                    remessa,
                    impressao,
                    pdf,
                    email,
                    _remessa.Sequencia,
                    EmailsDestino ?? "",
                    _remessa.Banco,
                    numRemessaSicredi,
                    _remessa.PadraoJuroBoleto,
                    _remessa.SomaNossoNumero,
                    _remessa.CodigoBaixaDevolucao,
                    _remessa.BExportaJurosMulta
                );

                var contador = 0;
                foreach (var doc in boletoController.ListaDocumento.Select(item => ((DocumentoBoletoImpl) item).Documento))
                {
                    doc.BImpresso = impressao;
                    doc.BGeradoPDF = pdf;
                    doc.BEnviadoEmail = email;
                    doc.BGeradoRemessa = remessa;

                    if (NossosNumeros.Count > 0) doc.NossoNumero = NossosNumeros[contador];

                    var TelFatura = Tel_FaturaController.Instancia.GetFaturaByDocumento(doc.ID);
                    if (TelFatura != null)
                    {
                        TelFatura.NossoNumero = doc.NossoNumero;
                        Tel_FaturaController.Instancia.Salvar(TelFatura, Acao.Alterar);
                    }

                    contador++;
                }

                if (remessa)
                {
                    boletoController.Remessa.LayoutRemessa = _remessa.LayoutRemessa;
                    _remessa.Sequencia++;
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }
        }

        public void Dispose()
        {
            boletoController?.Dispose();
            boletoController = null;
        }
    }
}