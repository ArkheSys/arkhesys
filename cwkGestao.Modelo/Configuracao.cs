using System;
using System.Xml;

namespace cwkGestao.Modelo
{
    public class Configuracao : ModeloBase
    {
        private int _ID;
        public override int ID { get { return _ID; } set { _ID = value; } }

        private int _Codigo;
        public virtual int Codigo { get { return _Codigo; } set { _Codigo = value; } }

        private Filial _Filial;
        public virtual Filial Filial { get { return _Filial; } set { _Filial = value; } }

        private int _IDPlanoContaJuro;
        public virtual int IDPlanoContaJuro { get { return _IDPlanoContaJuro; } set { _IDPlanoContaJuro = value; } }
        public virtual PlanoConta PlanoContaDesconto { get; set; }
        public virtual PlanoConta PlanoContaTroco { get; set; }
        public virtual PlanoConta PlanoContaRenegociacao { get; set; }
        public virtual int TempoEsperaNFe { get; set; }
        public virtual bool EditarAssuntoEmailPedido { get; set; }

        private int _IDPlanoContaMulta;
        public virtual int IDPlanoContaMulta { get { return _IDPlanoContaMulta; } set { _IDPlanoContaMulta = value; } }

        private TabelaPreco _TabelaPreco;
        public virtual TabelaPreco TabelaPreco { get { return _TabelaPreco; } set { _TabelaPreco = value; } }

        private bool _SimplesNacional;
        public virtual bool SimplesNacional { get { return _SimplesNacional; } set { _SimplesNacional = value; } }

        private decimal _AliqSimplesSubst;
        public virtual decimal AliqSimplesSubst { get { return _AliqSimplesSubst; } set { _AliqSimplesSubst = value; } }

        private int _TipoOrcamento;
        public virtual int TipoOrcamento { get { return _TipoOrcamento; } set { _TipoOrcamento = value; } }

        private string _ServidorSMTP;
        public virtual string ServidorSMTP { get { return _ServidorSMTP; } set { _ServidorSMTP = value; } }

        private string _UsuarioEmail;
        public virtual string UsuarioEmail { get { return _UsuarioEmail; } set { _UsuarioEmail = value; } }

        private string _SenhaEmail;
        public virtual string SenhaEmail { get { return _SenhaEmail; } set { _SenhaEmail = value; } }

        private int _ModeloDanfe;
        public virtual int ModeloDanfe { get { return _ModeloDanfe; } set { _ModeloDanfe = value; } }

        private string _Certificado;
        public virtual string Certificado { get { return _Certificado; } set { _Certificado = value; } }

        private int _TipoST;
        public virtual int TipoST { get { return _TipoST; } set { _TipoST = value; } }

        private int _FormaEmissao;
        public virtual int FormaEmissao { get { return _FormaEmissao; } set { _FormaEmissao = value; } }

        private int _Ambiente;
        public virtual int Ambiente { get { return _Ambiente; } set { _Ambiente = value; } }

        private int _IdPlanoContaRenegociacao;
        public virtual int IdPlanoContaRenegociacao { get { return _IdPlanoContaRenegociacao; } set { _IdPlanoContaRenegociacao = value; } }

        private int _IdTabelaFaixaFatSimples;
        public virtual int IdTabelaFaixaFatSimples { get { return _IdTabelaFaixaFatSimples; } set { _IdTabelaFaixaFatSimples = value; } }

        private int _IDTipoPedidoCupomFiscal;
        public virtual int IDTipoPedidoCupomFiscal { get { return _IDTipoPedidoCupomFiscal; } set { _IDTipoPedidoCupomFiscal = value; } }

        private ModeloFiltro _ListagemAbertos;
        public virtual ModeloFiltro ListagemAbertos { get { return _ListagemAbertos; } set { _ListagemAbertos = value; } }

        private ModeloFiltro _ListagemBaixados;
        public virtual ModeloFiltro ListagemBaixados { get { return _ListagemBaixados; } set { _ListagemBaixados = value; } }
        public virtual ModeloFiltro NotaFiltroListagem { get; set; }
        public virtual ModeloFiltro ComprasFiltroListagem { get; set; }

        private bool _BListarBaixados;
        public virtual bool BListarBaixados { get { return _BListarBaixados; } set { _BListarBaixados = value; } }

        private string _Observacao;
        public virtual string Observacao { get { return _Observacao; } set { _Observacao = value; } }

        private bool _UtilizarProjeto;
        public virtual bool UtilizarProjeto { get { return _UtilizarProjeto; } set { _UtilizarProjeto = value; } }

        private bool _CodigoProdutoStr;
        public virtual bool CodigoProdutoStr { get { return _CodigoProdutoStr; } set { _CodigoProdutoStr = value; } }

        private bool _CincoFornec;
        public virtual bool CincoFornec { get { return _CincoFornec; } set { _CincoFornec = value; } }

        private bool _BloqAltPedImpresso;
        public virtual bool BloqAltPedImpresso { get { return _BloqAltPedImpresso; } set { _BloqAltPedImpresso = value; } }

        private bool _PermitirFatClienteNaoAtivo;
        public virtual bool PermitirFatClienteNaoAtivo { get { return _PermitirFatClienteNaoAtivo; } set { _PermitirFatClienteNaoAtivo = value; } }

        private bool _ImprimirOrcClienteNaoAtivo;
        public virtual bool ImprimirOrcClienteNaoAtivo { get { return _ImprimirOrcClienteNaoAtivo; } set { _ImprimirOrcClienteNaoAtivo = value; } }

        private TipoNota _TipoNotaNF;
        public virtual TipoNota TipoNotaNF { get { return _TipoNotaNF; } set { _TipoNotaNF = value; } }

        private TipoNota _TipoNotaCupom;
        public virtual TipoNota TipoNotaCupom { get { return _TipoNotaCupom; } set { _TipoNotaCupom = value; } }

        private TipoPedido _TipoRequisicaoProduto;
        public virtual TipoPedido TipoRequisicaoProduto { get { return _TipoRequisicaoProduto; } set { _TipoRequisicaoProduto = value; } }

        private TipoPedido _TipoRequisicaoMaterial;
        public virtual TipoPedido TipoRequisicaoMaterial { get { return _TipoRequisicaoMaterial; } set { _TipoRequisicaoMaterial = value; } }

        private bool _AlteracaoDescricaoProduto;
        public virtual bool AlteracaoDescricaoProduto { get { return _AlteracaoDescricaoProduto; } set { _AlteracaoDescricaoProduto = value; } }

        private bool _BSalvarFiltroGrid;
        public virtual bool BSalvarFiltroGrid { get { return _BSalvarFiltroGrid; } set { _BSalvarFiltroGrid = value; } }

        private bool _UtilizarFichaTecnica;
        public virtual bool UtilizarFichaTecnica { get { return _UtilizarFichaTecnica; } set { _UtilizarFichaTecnica = value; } }

        private TipoNota _TipoNotaBalancoEnt;
        public virtual TipoNota TipoNotaBalancoEnt { get { return _TipoNotaBalancoEnt; } set { _TipoNotaBalancoEnt = value; } }

        private TipoNota _TipoNotaBalancoSai;
        public virtual TipoNota TipoNotaBalancoSai { get { return _TipoNotaBalancoSai; } set { _TipoNotaBalancoSai = value; } }

        private int _TipoCertificadoNFe;
        public virtual int TipoCertificadoNFe { get { return _TipoCertificadoNFe; } set { _TipoCertificadoNFe = value; } }

        private bool _PrecisaoPrecoProd;
        public virtual bool PrecisaoPrecoProd { get { return _PrecisaoPrecoProd; } set { _PrecisaoPrecoProd = value; } }

        private int _NaoSomarIpiTotalNota;
        public virtual int NaoSomarIpiTotalNota { get { return _NaoSomarIpiTotalNota; } set { _NaoSomarIpiTotalNota = value; } }

        private int _NaoSomarIcmsDesonTotalNota;
        public virtual int NaoSomarIcmsDesonTotalNota { get { return _NaoSomarIcmsDesonTotalNota; } set { _NaoSomarIcmsDesonTotalNota = value; } }

        private int _ReducaoMagento;
        public virtual int ReducaoMagento { get { return _ReducaoMagento; } set { _ReducaoMagento = value; } }

        private int _ComissaoPedido;
        public virtual int ComissaoPedido { get { return _ComissaoPedido; } set { _ComissaoPedido = value; } }

        private string _HoraEnvioEmailCorreios;
        public virtual string HoraEnvioEmailCorreios { get { return _HoraEnvioEmailCorreios; } set { _HoraEnvioEmailCorreios = value; } }

        private int _UtilizaTelefonia;
        public virtual int UtilizaTelefonia { get { return _UtilizaTelefonia; } set { _UtilizaTelefonia = value; } }

        private int _UtilizaOrcamentoDeServico;
        public virtual int UtilizaOrcamentoDeServico { get { return _UtilizaOrcamentoDeServico; } set { _UtilizaOrcamentoDeServico = value; } }

        private int _UtilizaControleDaRevenda;
        public virtual int UtilizaControleDaRevenda { get { return _UtilizaControleDaRevenda; } set { _UtilizaControleDaRevenda = value; } }

        private int _UtilizaObrasEFundacoes;
        public virtual int UtilizaObrasEFundacoes { get { return _UtilizaObrasEFundacoes; } set { _UtilizaObrasEFundacoes = value; } }

        private int _UtilizaContratos;
        public virtual int UtilizaContratos { get { return _UtilizaContratos; } set { _UtilizaContratos = value; } }

        private int _UtilizaControleAvisos;
        public virtual int UtilizaControleAvisos { get { return _UtilizaControleAvisos; } set { _UtilizaControleAvisos = value; } }

        private bool _UtilizaControleIMEI;
        public virtual bool UtilizaControleIMEI { get { return _UtilizaControleIMEI; } set { _UtilizaControleIMEI = value; } }

        private int _LayoutControleVenda;
        public virtual int LayoutControleVenda { get { return _LayoutControleVenda; } set { _LayoutControleVenda = value; } }

        private string _AssuntoEmail;
        public virtual string AssuntoEmail { get { return _AssuntoEmail; } set { _AssuntoEmail = value; } }

        private string _TextoEmail;
        public virtual string TextoEmail { get { return _TextoEmail; } set { _TextoEmail = value; } }

        private int? _IDRmessa;
        public virtual int? IDRemessa { get { return _IDRmessa; } set { _IDRmessa = value; } }

        private int? _ServidorSHL;
        public virtual int? ServidorSHL { get { return _ServidorSHL; } set { _ServidorSHL = value; } }

        private int? _IDRmessaPadrao;
        public virtual int? IDRemessaPadrao { get { return _IDRmessaPadrao; } set { _IDRmessaPadrao = value; } }

        private int _ModeloImpressaoOS;
        public virtual int ModeloImpressaoOS { get { return _ModeloImpressaoOS; } set { _ModeloImpressaoOS = value; } }

        private int? _IDFilialOS;
        public virtual int? IDFilialOS { get { return _IDFilialOS; } set { _IDFilialOS = value; } }
        public virtual string HoraEnvioEmail { get; set; }
        public virtual bool SolicitaConfirmaEmailNota { get; set; }
        public virtual bool SolicitaRecebimentoEmail { get; set; }
        public virtual bool SolicitaConfirmaEmailContrato { get; set; }
        public virtual bool SolicitaConfirmaEmailPedido { get; set; }
        public virtual ModoBaixaEstoqueType ModoBaixaEstoque { get; set; }
        public virtual bool UtilizaIdentificadorEstoque { get; set; }
        public virtual bool NaoSomarFrete { get; set; }
        public virtual bool PermitirAlteracaoVendedorPedido { get; set; }
        public virtual PlanoConta PlanoContaDescontoPagar { get; set; }
        public virtual PlanoConta PlanoContaDescontoReceber { get; set; }
        public virtual string OSTermoCompromisso { get; set; }
        public virtual string OSAssistenciaTecnica { get; set; }
        public virtual string NomeGrupo1 { get; set; }
        public virtual string NomeGrupo2 { get; set; }
        public virtual string NomeGrupo3 { get; set; }
        public virtual string NomeImpressora { get; set; }
        public virtual string EmailPDV { get; set; }
        public virtual int EnviarEmailPDV { get; set; }
        public virtual int VisualizarNFCe { get; set; }
        public virtual bool NotaConjugada { get; set; }
        public virtual bool BExcluirOrcamentoFaturado { get; set; }
        public virtual ModeloDuplicata ModeloDuplicata { get; set; }
        public virtual ModeloImpressaoGraficaType ModeloImpressaoGrafica { get; set; }
        public virtual Filial FilialFrenteCaixa { get; set; }
        public virtual TabelaPreco TabelaPrecoFrenteCaixa { get; set; }
        public virtual Condicao CondicaoFrenteCaixa { get; set; }
        public virtual Pessoa ClienteFrenteCaixa { get; set; }
        public virtual TipoPedido TipoOrcamentoFrenteCaixa { get; set; }
        public virtual TipoDocumento TipoDocumentoFrenteCaixa { get; set; }
        public virtual Pessoa VendedorFrenteCaixa { get; set; }
        public virtual decimal PercentualMaxDesconto { get; set; }
        public virtual TipoNota TipoNotaObrasFundacoes { get; set; }
        public virtual PessoaVendedor VendedorObrasFundacoes { get; set; }
        public virtual Condicao CondicaoObrasFundacoes { get; set; }
        public virtual bool UtilizaLayout4 { get; set; }
        public virtual bool RotinaCorreios { get; set; }
        public virtual bool ExportarCodigoDeRastreamento { get; set; }
        public virtual bool UtilizaTrocaProdutoPedido { get; set; }
        public virtual string Assunto { get; set; }
        public virtual string TextoEmailPedido { get; set; }
        public virtual int UtilizaProducao { get; set; }
        public virtual int UtilizaPCP { get; set; }
        public virtual bool EnvioAutomaticoNfe { get; set; }
        public virtual bool UtilizaCustoNota { get; set; }
        public virtual bool UtilizaRotinaCartao { get; set; }
        public virtual string AssuntoEmailNfe { get; set; }
        public virtual string TextoEmailNfe { get; set; }
        public virtual bool Temp_EnviaTotalImpostos { get; set; }
        public virtual string DiretorioSalc { get; set; }
        public virtual bool UtilizaPafECF { get; set; }
        public virtual bool UtilizaNumeroEmpenhoNota { get; set; }
        public virtual ModeloImpressaoPedidoCompraType ModeloImpressaoPedidoCompra { get; set; }
        public virtual int GetModuloOrcamentoVenda(XmlDocument documentoXml)
        {
            int ret = -1;
            foreach (XmlElement item in documentoXml.DocumentElement)
            {
                if (item.Name == "OrcamentoVenda")
                {
                    ret = Convert.ToInt32(item.InnerText);
                }
            }

            return ret;
        }
        public virtual int TelaProduto { get; set; }
        public virtual bool MostraProdutosInativos { get; set; }
        public virtual PlanoConta IDPlanoContaJurosRecebidos { get; set; }
        public virtual PlanoConta IDPlanoContaJurosPagos { get; set; }
        public virtual PlanoConta IDPlanoContaMultasRecebidas { get; set; }
        public virtual PlanoConta IDPlanoContaMultasPagas { get; set; }
        public virtual bool bUtilizaWsCorreios { get; set; }
        public virtual bool UtilizaAtendimento { get; set; }
        public virtual bool UtilizaServicoPorExtenso { get; set; }
        public virtual bool BImprimeRelatorioControleVenda { get; set; }
        public virtual string CondicaoRelatorioControleVenda { get; set; }
        public virtual string ObservacaoTroca { get; set; }
        public virtual bool bExibeMargemLucroPedido { get; set; }
        public virtual int? DiasInativacaoCliente { get; set; }
        public virtual DateTime? DtUltimaInativacao { get; set; }
        public virtual string MotivoInativacaoAutomatica { get; set; }
        public virtual int LayoutNFe { get; set; }
        public virtual bool PermiteFatDtRetroativa { get; set; }
        public virtual bool bBloquearStatus { get; set; }
        public virtual bool CarregarVendedor { get; set; }
        public virtual bool GerarPedidoCompra { get; set; }
        public virtual bool EnviarEmailAutomaticoOS { get; set; }
        public virtual bool ReiniciarNumeracaoPedido { get; set; }
        public virtual PlanoConta IdPlanoContaBaixaCartao { get; set; }
        public virtual PlanoConta IdPlanoContaBaixaTaxaCartao { get; set; }
        public virtual int UtilizaCadastro { get; set; }
        public virtual int UtilizaComercial { get; set; }
        public virtual int UtilizaCompra { get; set; }
        public virtual int UtilizaEstoque { get; set; }
        public virtual int UtilizaFinanceiro { get; set; }
        public virtual int UtilizaCaixa { get; set; }
        public virtual int UtilizaUtilitarios { get; set; }
        public virtual int UtilizaConfiguracao { get; set; }
        public virtual int UtilizaSeguranca { get; set; }
        public virtual int UtilizaDashboard { get; set; }
        public virtual int UtilizaPDV { get; set; }
        public virtual int UtilizaDelivery { get; set; }
        public virtual decimal? TaxaEntregaDelivery { get; set; }
        public virtual int? IDTipoPedidoDelivery { get; set; }
        public virtual string ObservacaoDelivery { get; set; }
        public virtual int BPedidoAberto { get; set; }
        public virtual int BPedidoEmPreparacao { get; set; }
        public virtual int BPedidoCancelado { get; set; }
        public virtual int BPedidoPendente { get; set; }
        public virtual int BPedidoEmEntrega { get; set; }
        public virtual int BPedidoFinalizado { get; set; }
        public virtual decimal? IsencaoTaxaEntrega { get; set; }
        public virtual decimal? ValorMinimoDelivery { get; set; }
        public virtual int? ImprimeSangriaPDV { get; set; }
        public virtual int? UltimoNSU { get; set; }
        public virtual int? ImprimeSurpimentoPDV { get; set; }
        public virtual int? HabilitaBandeira { get; set; }
        public virtual int? EnviaNfceTabelaPedido { get; set; }
        public virtual int? EnviaEmailSangria { get; set; }
        public virtual int? EnviaEmailSuprimento { get; set; }
        public virtual int? IDCidadePadraoNFCe { get; set; }
        public virtual int? HabilitaFechamentoCego { get; set; }
        public virtual int? EnviarMensagemLimiteSangria { get; set; }
        public virtual decimal? ValorLimiteSangria { get; set; }
        public virtual int? AbrirTelaDadosClienteDelivery { get; set; }
        public virtual int? HabilitaFechamentoPdvAposViradaDia { get; set; }
        public virtual int? UtilizaSenhaGerente { get; set; }
        public virtual int? FormaPesquisaClienteDelivery { get; set; }
        public virtual int? GravarEnviarNfe { get; set; }
        public virtual int? GravarEnviarNfce { get; set; }
        public virtual int? GravarEnviarNfse { get; set; }
        public virtual int? TipoMovimentacaoCompras { get; set; }
        public virtual int? CondicaoPagamentoCompras { get; set; }
        public virtual int? TabelaPrecoCompras { get; set; }
        public virtual int? TrazerComoPadraoNoLancamentoNFe { get; set; }
        public virtual int? ClienteEmiteNFSe { get; set; }
        public virtual int? IDUnidadeMedidaEntradaProduto { get; set; }
        public virtual int? IDUnidadeMedidaSaidaProduto { get; set; }
        public virtual int? IDGrupoEstoqueProduto { get; set; }
        public virtual int? IDTabelaPrecoProduto { get; set; }
        public virtual int? CodigoEnquadramentoIPI { get; set; }
        public virtual int? IDFornecedorProduto { get; set; }
        public virtual int? IDNcmProduto { get; set; }
        public virtual int? IDUnidadeEstoqueSaidaProduto { get; set; }
        public virtual int? GerarCodigoBarrasProduto { get; set; }
        public virtual int? GerarCodigoBarrasSemGetin { get; set; }
        public virtual string UrlEcommerce { get; set; }
        public virtual string UsuarioEcommerce { get; set; }
        public virtual string SenhaEcommerce { get; set; }
        public virtual string CaminhoImagens { get; set; }
        public virtual int? DesejaImprimirCupom { get; set; }
        public virtual int? BotaoPdv1 { get; set; }
        public virtual int? BotaoPdv2 { get; set; }
        public virtual int? BotaoPdv3 { get; set; }
        public virtual int? BotaoPdv4 { get; set; }
        public virtual int? BotaoPdv5 { get; set; }
        public virtual int? BotaoPdv6 { get; set; }
        public virtual int? BotaoPDVFormaPagamento1 { get; set; }
        public virtual int? BotaoPDVFormaPagamento2 { get; set; }
        public virtual int? BotaoPDVFormaPagamento3 { get; set; }
        public virtual int? BotaoPDVFormaPagamento4 { get; set; }
        public virtual int? BotaoPDVFormaPagamento5 { get; set; }
        public virtual int? BotaoPDVFormaPagamento6 { get; set; }
        public virtual int? ImprimirNomeClienteCupom { get; set; }
        public virtual int? ImprimirEnderecoClienteCupom { get; set; }
        public virtual int? ImprimirTelefoneClienteCupom { get; set; }
        public virtual int? ImprimirCnpjCpfClienteCupom { get; set; }
        public virtual int? ImprimirAssinaturaNaoDinheiro { get; set; }
        public virtual int? SenhaGerenteCancelarItemPDV { get; set; }
        public virtual int? SenhaGerenteSairPDV { get; set; }
        public virtual string ObservacaoPDV { get; set; }
        public virtual int? ImprimirItensCanceladosPDV { get; set; }
        public virtual int? ImprimirFechamentoCaixaDireto { get; set; }
        public virtual int? SenhaGerenteDescontoItemPDV { get; set; }
        public virtual int? SenhaGerenteLimiteDescontoPDV { get; set; }
        public virtual int? FocarQuantidadeDescontoPDV { get; set; }
        public virtual int? SenhaAoEntrarNoPDV { get; set; }
        public virtual int? ObservacaoNoItemPedido { get; set; }
        public virtual int? BuscarProdutoPesquisaDinamica { get; set; }
        public virtual int? PedirSenhaCancelarVendaPDV { get; set; }
        public virtual int? IDClienteFrenteCaixaDelivery { get; set; }
        public virtual int? IncluirProdutosDiretoListaDelivery { get; set; }
        public virtual int? ImprimirOutrasBaixas { get; set; }
        public virtual int? Habilitar6CondicoesPagamento { get; set; }
        public virtual int? IdentificacaoTipoClienteNFCe { get; set; }
        public virtual int? IDTipoClienteNFCeIdentificado { get; set; }
        public virtual int? IDTipoPedidoDevolucaoPDV { get; set; }
        public virtual int? VincularNumeroTransacaoPos { get; set; }
        public virtual int? HabilitaIdentificacaoVendas { get; set; }        
        public virtual int? EmitirSinalSangriaPorCor { get; set; }
        public virtual decimal? ValorLimiteSangriaPorCor { get; set; }
        public virtual int? SenhaGerentePorCodigoBarras { get; set; }
        public virtual int? IDPessoaOrcamentoPadrao { get; set; }
        public virtual int? IDTipoPedidoOrcamentoPadrao { get; set; }
        public virtual int? IDVendedorPedidoOrcamentoPadrao { get; set; }
        public virtual int? IDTabelaPrecoPadraoPedido { get; set; }
        public virtual int? IDCondicaoPadraoPedido { get; set; }
        public virtual bool ExibirTrocoPDV { get; set; }
        public virtual bool FocoDescontoPDV { get; set; }
        public virtual bool SenhaTrocaTabelaPDV { get; set; }
        public virtual bool FechamentoDetalhadoPDV { get; set; }
        public virtual bool BuscaProdutoDetalhada { get; set; }
        public virtual bool ImpressaoPreVendaOrcamento { get; set; }
        public virtual string NomeImpressoraPedido { get; set; }
        public virtual bool PesagemAutomaticaPDV { get; set; }
        public virtual bool UsaListaReferenciaNota { get; set; }
        public virtual int? IDCondicaoNota { get; set; }
        public virtual int? IDTabelaPrecoNota { get; set; }
        public virtual int? IDTipoNotaNota { get; set; }
        public virtual int? IDFormaPagamentoCreditoPDV { get; set; }
        public virtual bool ZerarSaldoCliente { get; set; }
        public virtual int TipoEnvioFechamentoPDV { get; set; }
        public virtual bool PermiteDescontoGridPDV { get; set; }
    }
}