using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace cwkGestao.Modelo
{
    [InformacaoExibicao("Usuário")]
    public class Cw_usuario : ModeloBase
    {
        public override int ID { get; set; }
        [InformacaoExibicao(1,"Código", 75)]
        public virtual int Codigo { get; set; }
        [InformacaoExibicao(2,"Login",125)]
        public virtual string Login { get; set; }
        public virtual string Senha { get; set; }
        [InformacaoExibicao(3,"Nome do Usuário", 150)]
        public virtual string Nome { get; set; }
        [InformacaoExibicao(4,"Tipo",100)]
        public virtual TipoUsuario Tipo { get; set; }
        [InformacaoExibicao(5,"Grupo", 100)]
        public virtual Cw_grupo Grupo { get; set; }
        public virtual DateTime Incdata { get; set; }
        public virtual DateTime Inchora { get; set; }
        public virtual string Incusuario { get; set; }
        public virtual DateTime Altdata { get; set; }
        public virtual DateTime Althora { get; set; }
        public virtual string Altusuario { get; set; }
        public virtual IList<AcompanhamentoOS> AcompanhamentoOSs { get; set; }
        [InformacaoExibicao(6,"E-mail", 150)]
        public virtual string Email { get; set; }
        public virtual string SenhaEmail { get; set; }
        public virtual string Smtp { get; set; }
        public virtual string Porta { get; set; }
        public virtual bool SSL { get; set; }

        #region Menus //
        
        //Menu Cadastro
        public virtual bool UtilizaCadastro { get; set; }
        public virtual bool UtilizaCadastroBasicos { get; set; }
        public virtual bool UtilizaCadastroClassificacao { get; set; }
        public virtual bool UtilizaCadastroCondicaoDePagamento { get; set; }
        public virtual bool UtilizaCadastroStatusOS { get; set; }
        public virtual bool UtilizaCadastroGrupo { get; set; }
        public virtual bool UtilizaCadastroConfiguracaoManager { get; set; }
        public virtual bool UtilizaCadastroEmpresa { get; set; }
        public virtual bool UtilizaCadastroPeriodoApuracaoContabil { get; set; }
        public virtual bool UtilizaCadastroPessoa { get; set; }
        public virtual bool UtilizaCadastroFuncionario { get; set; }
        public virtual bool UtilizaCadastroGrupoEstoque { get; set; }
        public virtual bool UtilizaCadastroUnidade { get; set; }
        public virtual bool UtilizaCadastroTabelaPreco { get; set; }
        public virtual bool UtilizaCadastroCategoria { get; set; }
        public virtual bool UtilizaCadastroModelo { get; set; }
        public virtual bool UtilizaCadastroTipo { get; set; }
        public virtual bool UtilizaCadastroProduto { get; set; }
        public virtual bool UtilizaCadastroConversaoDeUnidades { get; set; }
        public virtual bool UtilizaCadastroProdutosInativos { get; set; }
        public virtual bool UtilizaCadastroNCM { get; set; }
        public virtual bool UtilizaCadastroSubProduto { get; set; }
        public virtual bool UtilizaCadastroMateriaPrima { get; set; }
        public virtual bool UtilizaCadastroImobilizado { get; set; }
        public virtual bool UtilizaCadastroServico { get; set; }
        public virtual bool UtilizaCadastroParametros { get; set; }
        public virtual bool UtilizaCadastroRelatorios { get; set; }
        public virtual bool UtilizaCadastroRelatoriosDeProdutos { get; set; }

        //Menu Comercial
        public virtual bool UtilizaComercial { get; set; }
        public virtual bool UtilizaComercialTransportadora { get; set; }
        public virtual bool UtilizaComercialCliente { get; set; }
        public virtual bool UtilizaComercialVendedor { get; set; }
        public virtual bool UtilizaComercialOrcamento { get; set; }
        public virtual bool UtilizaComercialAprovacaoOrcamento { get; set; }
        public virtual bool UtilizaComercialHistoricoDeVendaPorProduto { get; set; }
        public virtual bool UtilizaComercialHistoricoDeVendaPorCliente { get; set; }
        public virtual bool UtilizaComercialContatos { get; set; }
        public virtual bool UtilizaComercialControleDeRotas { get; set; }
        public virtual bool UtilizaComercialControleDeEntregas { get; set; }
        public virtual bool UtilizaComercialNotaDeSaida { get; set; }
        public virtual bool UtilizaComercialNotaComplementarDeSaida { get; set; }
        public virtual bool UtilizaComercialNotaCancelada { get; set; }
        public virtual bool UtilizaComercialFechamentoFatura { get; set; }
        public virtual bool UtilizaComercialLoteOrcamento { get; set; }
        public virtual bool UtilizaComercialCuponsFiscais { get; set; }
        public virtual bool UtilizaComercialTipoServicoTransporte { get; set; }
        public virtual bool UtilizaComercialRelatorios { get; set; }

        // Menu Compra
        public virtual bool UtilizaCompra { get; set; }
        public virtual bool UtilizaCompraFornecedor { get; set; }
        public virtual bool UtilizaCompraCotacoes { get; set; }
        public virtual bool UtilizaCompraOrcamento { get; set; }
        public virtual bool UtilizaCompraPedidos { get; set; }
        public virtual bool UtilizaCompraNotaDeEntrada { get; set; }
        public virtual bool UtilizaCompraNotaComplementarDeEntrada { get; set; }
        public virtual bool UtilizaCompraCompraCancelada { get; set; }
        public virtual bool UtilizaCompraImportacaoXMLFornecedor { get; set; }
        public virtual bool UtilizaCompraIdentificadores { get; set; }
        public virtual bool UtilizaCompraRelatorios { get; set; }

        // Menu Estoque
        public virtual bool UtilizaEstoque { get; set; }
        public virtual bool UtilizaEstoqueRequisicaoDeSaida { get; set; }
        public virtual bool UtilizaEstoqueRequisicaoBaixaSaida { get; set; }
        public virtual bool UtilizaEstoqueRequisicaoDeEntrada { get; set; }
        public virtual bool UtilizaEstoqueRequisicaoBaixadaEntrada { get; set; }
        public virtual bool UtilizaEstoqueBaixarRequisicoesEmLote { get; set; }
        public virtual bool UtilizaEstoqueConsultaPorEmpresa { get; set; }
        public virtual bool UtilizaEstoqueConsultaPorProduto { get; set; }
        public virtual bool UtilizaEstoqueABC { get; set; }
        public virtual bool UtilizaEstoqueBalanco { get; set; }
        public virtual bool UtilizaEstoqueTipoDeServicoDeTransporte { get; set; }
        public virtual bool UtilizaEstoqueInventario { get; set; }
        public virtual bool UtilizaEstoqueLocalEstoque { get; set; }
        public virtual bool UtilizaEstoqueRelatorios { get; set; }

        // Menu Financeiro
        public virtual bool UtilizaFinanceiro { get; set; }
        public virtual bool UtilizaFinanceiroCadastros { get; set; }
        public virtual bool UtilizaFinanceiroDocumentos { get; set; }
        public virtual bool UtilizaFinanceiroRelatorios { get; set; }

        // Menu Caixa
        public virtual bool UtilizaCaixa { get; set; }
        public virtual bool UtilizaCaixaLancamento { get; set; }
        public virtual bool UtilizaCaixaFechamentoDeCaixa { get; set; }
        public virtual bool UtilizaCaixaCheque { get; set; }
        public virtual bool UtilizaCaixaTransferenciaBancaria { get; set; }
        public virtual bool UtilizaCaixaRelatorios { get; set; }

        // Menu Delivery
        public virtual bool UtilizaDelivery { get; set; }
        public virtual bool UtilizaDeliveryDelivery { get; set; }
        public virtual bool UtilizaDeliveryMonitoramento { get; set; }

        // Menu PDV
        public virtual bool UtilizaPDV { get; set; }
        public virtual bool UtilizaPDVFrenteDeCaixa { get; set; }
        public virtual bool UtilizaPDVLancamentoFluxoDeCaixa { get; set; }
        public virtual bool UtilizaPDVLancamentoDeSangria { get; set; }
        public virtual bool UtilizaPDVLancamentoDeSuprimento { get; set; }

        // Menu Utilitarios
        public virtual bool UtilizaUtilitario { get; set; }
        public virtual bool UtilizaUtilitarioExportacao { get; set; }
        public virtual bool UtilizaUtilitarioAtualizacaoDeNotas { get; set; }
        public virtual bool UtilizaUtilitarioEnvioXMLsDestinatarios { get; set; }
        public virtual bool UtilizaUtilitarioAlteracaoDePreco { get; set; }
        public virtual bool UtilizaUtilitarioAtualizarCSTNCMCSOSN { get; set; }

        // Menu Configuracao
        public virtual bool UtilizaConfiguracao { get; set; }
        public virtual bool UtilizaConfiguracaoSistema { get; set; }
        public virtual bool UtilizaConfiguracaoImpressora { get; set; }
        public virtual bool UtilizaConfiguracaoBackup { get; set; }

        // Menu Orcamnento de Servico
        public virtual bool UtilizaOrcamentoDeServico { get; set; }
        public virtual bool UtilizaOrcamentoDeServicoEquipamento { get; set; }
        public virtual bool UtilizaOrcamentoDeServicoTipoEquipamento { get; set; }
        public virtual bool UtilizaOrcamentoDeServicoImposto { get; set; }
        public virtual bool UtilizaOrcamentoDeServicoOrdemDeServico { get; set; }
        public virtual bool UtilizaOrcamentoDeServicoNotasDeServico { get; set; }
        public virtual bool UtilizaOrcamentoDeServicoFechamentoFatura { get; set; }
        public virtual bool UtilizaOrcamentoDeServicoAgendamento { get; set; }
        public virtual bool UtilizaOrcamentoDeServicoAtendimentos { get; set; }
        public virtual bool UtilizaOrcamentoDeServicoRelatorios { get; set; }

        // Menu Seguranca
        public virtual bool UtilizaSeguranca { get; set; }
        public virtual bool UtilizaSegurancaCadastroUsuario { get; set; }
        public virtual bool UtilizaSegurancaTrocaUsuario { get; set; }

        // Menu Contratos
        public virtual bool UtilizaContratos { get; set; }
        public virtual bool UtilizaContratosContratos { get; set; }
        public virtual bool UtilizaContratosProdutos { get; set; }
        public virtual bool UtilizaContratosServicos { get; set; }
        public virtual bool UtilizaContratosPesquisarContratos { get; set; }
        public virtual bool UtilizaContratosRelatorios { get; set; }

        // Menu Dashboard
        public virtual bool UtilizaDashboard { get; set; }
        public virtual bool UtilizaDashboardGeral { get; set; }

        #endregion

        public virtual IList<Cw_usuario> Vendedor { get; set; }
        public virtual IList<OrdemServico> OrdemServicos { get; set; }
        public virtual IList<ServicoOS> ServicoOSs { get; set; }
        public virtual IList<Atendimento> Atendimentos { get; set; }
    }
}
