using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Linq;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;

using Aplicacao;
using Aplicacao.Base;
using Aplicacao.DashBoard;
using Aplicacao.Modulos.Cadastro;
using Aplicacao.Modulos.Caixa;
using Aplicacao.Modulos.Comercial;
using Aplicacao.Modulos.Contratos;
using Aplicacao.Modulos.Estoque;
using Aplicacao.Modulos.Financeiro;
using Aplicacao.Modulos.Magento;
using Aplicacao.Modulos.Manifesto;
using Aplicacao.Modulos.MercadoLivre;
using Aplicacao.Modulos.Obras_Fundacoes;
using Aplicacao.Modulos.Obras_Fundacoes.Impressao;
using Aplicacao.Modulos.PDV;
using Aplicacao.Modulos.Rel;
using Aplicacao.Modulos.SHL;
using Aplicacao.Modulos.SincronismoPendenteSisECommerceNMS;
using Aplicacao.Modulos.SisECommerceConfiguracaoNMS;
using Aplicacao.Modulos.Telefonia;
using Aplicacao.Modulos.Telefonia.Impressao;
using Aplicacao.Modulos.Tributacao;
using Aplicacao.Modulos.Utilitarios;
using Aplicacao.Obsoleto;
using Aplicacao.Obsoleto.PCP;
using Aplicacao.PCP;
using Aplicacao.Relatorios;
using Aplicacao.Util.Telas;
using Aplicacao.Utilitarios;

using cwkControleUsuario;

using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Negocio;
using cwkGestao.Negocio.Backup;
using cwkGestao.Negocio.Padroes;

using CworkGestaoPlus;
using CworkGestaoPlus.Properties;

using DBUtils.Classes;

using DevExpress.XtraBars.Docking;

using FormRota = Aplicacao.Relatorios.FormRota; //using Aplicacao.Util;
using MO = Modelo;
using MovCaixa = Aplicacao.Relatorios.MovCaixa;
using OSEquipamento = cwkGestao.Modelo.OSEquipamento;
using OSTipoEquipamento = cwkGestao.Modelo.OSTipoEquipamento;
using Pedido = Aplicacao.Relatorios.Pedido;
using Sistema = LicenceLibrary.Sistema;
using ThreadState = System.Threading.ThreadState;

namespace cwkGestao.Visual.Desktop
{
    public partial class MenuInicial : Form
    {
        #region Atributos

        private readonly int menu;
        private bool mostradocbaixado;
        private readonly MO.DataClassesDataContext db = new MO.DataClassesDataContext();
        private MO.Configuracao objConfiguracao;
        private readonly string versao;
        private List<string> modulosLiberados;
        private pxyAvisosPorPeriodo avisoPorPeriodo;
        private pxyAvisos avisoAnterior;
        private pxyAvisos avisoProximo;
        private Thread m;
        private readonly Dictionary<string, Form> telasAbertas = new Dictionary<string, Form>();

        #endregion

        public MenuInicial(int pMenu, string pVersao)
        {
            InitializeComponent();
            CarregaNomeGruposMenu();
            CarregaLayout();
            Ler_BackupConfig_XML();

            menu = pMenu;
            versao = pVersao;
            Text = "Cwork Gestão Plus (v " + pVersao + ")";
        }

        private void EnvioAutomaticoXml()
        {
            var ds = new DataSet();

            const int diaPrimeiro = 1;
            const string path = "menubase2.xml";

            if (!File.Exists(path))
                return;

            ds.ReadXml(path);

            var drFind = ds.Tables["FILTROS"].Select("KEYWORD = 'KEY' AND CONTROL = 'cbEnvioAutomatico'").FirstOrDefault();
            var AindaNaoEnviou = LogExportacaoNotaController.Instancia.GetAll().Count(o => o.Periodo == $"{DateTime.Now.Month}/{DateTime.Now.Year}");

            if (DateTime.Now.Day >= diaPrimeiro && drFind != null && drFind["VALUE"].ToString().ToLower() == "true" && AindaNaoEnviou == 0) // Se é o primeiro dia do mês e está marcado o envio automático.
            {
                var formEnvio = new FormEnviandoNotasAutomatico();
                formEnvio.ShowDialog(this);
            }
        }

        private void EscondeModulos()
        {
            var verificador = ConfiguracaoController.Instancia.GetConfiguracao();

            if (verificador.UtilizaControleDaRevenda != 1)
                controleRevendasToolStripMenuItem.Visible = false;
            else
                controleRevendasToolStripMenuItem.Visible = true;

            if (verificador.UtilizaObrasEFundacoes != 1)
                obrasFundaçõesToolStripMenuItem.Visible = false;
            else
                obrasFundaçõesToolStripMenuItem.Visible = true;

            if (verificador.UtilizaOrcamentoDeServico != 1)
                ordemServiçoToolStripMenuItem1.Visible = false;
            else
                ordemServiçoToolStripMenuItem1.Visible = true;

            if (verificador.UtilizaTelefonia != 1)
                telefoniaToolStripMenuItem.Visible = false;
            else
                telefoniaToolStripMenuItem.Visible = true;

            if (verificador.UtilizaContratos != 1)
                contratosToolStripMenuItem21.Visible = false;
            else
                contratosToolStripMenuItem21.Visible = true;

            if (verificador.UtilizaPCP != 1)
                pcpToolStripMenuItem21.Visible = false;
            else
                pcpToolStripMenuItem21.Visible = true;

            if (verificador.UtilizaProducao != 1)
                produçãoToolStripMenuItem.Visible = false;
            else
                produçãoToolStripMenuItem.Visible = true;

            /* Permissões dos Menus */
            cadastroToolStripMenuItem.Visible = verificador.UtilizaCadastro == 1;
            comercialToolStripMenuItem.Visible = verificador.UtilizaComercial == 1;
            comprasToolStripMenuItem.Visible = verificador.UtilizaCompra == 1;
            estoqueToolStripMenuItem.Visible = verificador.UtilizaEstoque == 1;
            financeiroToolStripMenuItem.Visible = verificador.UtilizaFinanceiro == 1;
            caixaToolStripMenuItem.Visible = verificador.UtilizaCaixa == 1;
            toolStripButton13.Visible = verificador.UtilizaCaixa == 1;
            utilitáriosToolStripMenuItem.Visible = verificador.UtilizaUtilitarios == 1;
            configuraçõesToolStripMenuItem.Visible = verificador.UtilizaConfiguracao == 1;
            segurançaToolStripMenuItem.Visible = verificador.UtilizaSeguranca == 1;
            toolStripMenuDashBoard.Visible = verificador.UtilizaDashboard == 1;

            /* Permissões dos Icones */

            // Orçamento de Serviço
            tsbOrcamentoServico.Visible = verificador.UtilizaOrcamentoDeServico == 1;

            // Cadastro
            tsbProdutos.Visible = verificador.UtilizaCadastro == 1;
            toolStripButton10.Visible = verificador.UtilizaCadastro == 1;

            // Comercial
            tsbClientes.Visible = verificador.UtilizaComercial == 1;
            toolStripButton5.Visible = verificador.UtilizaComercial == 1;
            tsbVendas.Visible = verificador.UtilizaComercial == 1;
            toolStripButton6.Visible = verificador.UtilizaComercial == 1;
            toolStripButton8.Visible = verificador.UtilizaComercial == 1;

            // Compra
            stbNotaCompra.Visible = verificador.UtilizaCompra == 1;

            // Estoque
            toolStripButton4.Visible = verificador.UtilizaEstoque == 1;

            // Financeiro
            toolStripButton1.Visible = verificador.UtilizaFinanceiro == 1;
            toolStripButton2.Visible = verificador.UtilizaFinanceiro == 1;
            tsbRenegociacao.Visible = verificador.UtilizaFinanceiro == 1;

            // Caixa
            toolStripButton9.Visible = verificador.UtilizaCaixa == 1;

            // Dashboard
            toolStripButton3.Visible = verificador.UtilizaDashboard == 1;

            /* Separadores */
            if (!toolStripButton10.Visible &&
                !tsbClientes.Visible &&
                !toolStripButton5.Visible &&
                !tsbOrcamentoServico.Visible &&
                !tsbVendas.Visible &&
                !stbNotaCompra.Visible &&
                !toolStripButton6.Visible &&
                !toolStripButton8.Visible)
                toolStripSeparator24.Visible = false;

            if (!tsbProdutos.Visible &&
                !toolStripButton4.Visible)
                toolStripSeparator26.Visible = false;

            if (!toolStripButton1.Visible &&
                !toolStripButton2.Visible &&
                !tsbRenegociacao.Visible &&
                !toolStripButton9.Visible &&
                !toolStripButton3.Visible)
                toolStripSeparator27.Visible = false;

            toolStripButton8.Visible = verificador.UtilizaPDV == 1;
            pDVToolStripMenuItem.Visible = verificador.UtilizaPDV == 1;

            toolStripButton11.Visible = verificador.UtilizaDelivery == 1;
            toolStripButton12.Visible = verificador.UtilizaDelivery == 1;
            deliveryToolStripMenuItem.Visible = verificador.UtilizaDelivery == 1;
        }

        // Habilita cada Menu individual e os itens dos Menus por Usuários cadastrados na Tabela de Usuários
        private void HabilitaMenusPorUsuario()
        {
            var verificadorhabilitamenu = ConfiguracaoController.Instancia.GetConfiguracao();

            if (verificadorhabilitamenu.UtilizaCadastro != 1)
                cadastroToolStripMenuItem.Visible = false;
            else
                cadastroToolStripMenuItem.Visible = true;

            if (verificadorhabilitamenu.UtilizaComercial != 1)
                comercialToolStripMenuItem.Visible = false;
            else
                comercialToolStripMenuItem.Visible = true;

            if (verificadorhabilitamenu.UtilizaCompra != 1)
                comprasToolStripMenuItem.Visible = false;
            else
                comprasToolStripMenuItem.Visible = true;

            if (verificadorhabilitamenu.UtilizaEstoque != 1)
                estoqueToolStripMenuItem.Visible = false;
            else
                estoqueToolStripMenuItem.Visible = true;

            if (verificadorhabilitamenu.UtilizaFinanceiro != 1)
                financeiroToolStripMenuItem.Visible = false;
            else
                financeiroToolStripMenuItem.Visible = true;

            if (verificadorhabilitamenu.UtilizaCaixa != 1)
                caixaToolStripMenuItem.Visible = false;
            else
                caixaToolStripMenuItem.Visible = true;

            if (verificadorhabilitamenu.UtilizaDelivery != 1)
                deliveryToolStripMenuItem.Visible = false;
            else
                deliveryToolStripMenuItem.Visible = true;

            if (verificadorhabilitamenu.UtilizaPDV != 1)
                pDVToolStripMenuItem.Visible = false;
            else
                pDVToolStripMenuItem.Visible = true;

            //if (verificadorhabilitamenu.UtilizaUtilitario != 1)
            //    utilitáriosToolStripMenuItem.Visible = false;
            //else
            utilitáriosToolStripMenuItem.Visible = true;

            if (verificadorhabilitamenu.UtilizaConfiguracao != 1)
                configuraçõesToolStripMenuItem.Visible = false;
            else
                configuraçõesToolStripMenuItem.Visible = true;

            if (verificadorhabilitamenu.UtilizaOrcamentoDeServico != 1)
                ordemServiçoToolStripMenuItem1.Visible = false;
            else
                ordemServiçoToolStripMenuItem1.Visible = true;

            if (verificadorhabilitamenu.UtilizaSeguranca != 1)
                segurançaToolStripMenuItem.Visible = false;
            else
                segurançaToolStripMenuItem.Visible = true;

            if (verificadorhabilitamenu.UtilizaContratos != 1)
                contratosToolStripMenuItem21.Visible = false;
            else
                contratosToolStripMenuItem21.Visible = true;

            if (verificadorhabilitamenu.UtilizaDashboard != 1)
                toolStripMenuDashBoard.Visible = false;
            else
                toolStripMenuDashBoard.Visible = true;
        }

        private void CarregaLayout()
        {
            bool mostraProduto = ConfiguracaoController.Instancia.MostrarCamposProduto();
            bool mostraServico = ConfiguracaoController.Instancia.MostrarCamposServico();

            vendasToolStripMenuItem.Visible = mostraProduto;
            notasDeServiçoToolStripMenuItem1.Visible = mostraServico;
        }

        private void CarregaNomeGruposMenu()
        {
            Configuracao conf = ConfiguracaoController.Instancia.GetConfiguracao();
            miGrupo1.Text = conf.NomeGrupo1 ?? "Grupo1";
            miGrupo2.Text = conf.NomeGrupo2 ?? "Grupo2";
            miGrupo3.Text = conf.NomeGrupo3 ?? "Grupo3";
        }

        private void ConfiguraThreadAvisos()
        {
            Configuracao conf = ConfiguracaoController.Instancia.GetConfiguracao();
            if (Convert.ToBoolean(conf.UtilizaControleAvisos)) ThreadAviso();
        }

        protected void chamaGrid(string nome)
        {
            try
            {
                var pGp = new GridPadrao(nome, false, 0);
                ShowForm(pGp);
            }
            catch (Exception ex)
            {
                FormErro.ShowDialog(ex);
            }
        }

        protected MenuInicial RetornaMenuInicial()
        {
            return this;
        }

        private void ShowForm(Form form)
        {
            if (TentarMostrarFormAberto(form))
                return;

            if (Facade.ControleAcesso(form)) AbrirForm(form);
        }

        private void ShowForm(Form form, int registrosExistentes)
        {
            if (TentarMostrarFormAberto(form))
                return;

            if (Facade.ControleAcesso(form, registrosExistentes)) AbrirForm(form);
        }

        private void ShowFormSemValidar(Form form)
        {
            if (TentarMostrarFormAberto(form))
                return;

            AbrirForm(form);
        }

        private void AbrirForm(Form form)
        {
            form.MdiParent = this;
            form.Show();
            if (telasAbertas.ContainsKey(form.Text))
                telasAbertas.Remove(form.Text);
            telasAbertas.Add(form.Text, form);
        }

        private bool TentarMostrarFormAberto(Form form)
        {
            var formAberto = BuscarFormAberto(form);
            if (formAberto != null)
            {
                form.Dispose();
                SetarFocoForm(formAberto);
                return true;
            }

            return false;
        }

        private void SetarFocoForm(Form formAberto)
        {
            if (formAberto.WindowState == FormWindowState.Minimized)
                formAberto.WindowState = FormWindowState.Normal;
            formAberto.Focus();
        }

        private Form BuscarFormAberto(Form form)
        {
            if (telasAbertas.ContainsKey(form.Text))
            {
                var t = telasAbertas[form.Text];
                if (t.IsDisposed)
                    telasAbertas.Remove(form.Text);
                else
                    return t;
            }

            return null;
        }

        private void sistemaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            using (FormManutConfig grid = new FormManutConfig())
                if (Facade.ControleAcesso(grid))
                {
                    grid.ShowDialog();
                    DefinirConfiguracaoMenu();
                    CarregaNomeGruposMenu();
                }
        }

        #region Sair

        private void sairDoSistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Tem certeza que deseja sair?", "Saída", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                bool backupSairSistema = false;
                var diretorios = new List<string>();

                if (!File.Exists("Configuração.xml"))
                    return;

                var xml = XElement.Load("Configuração.xml");
                foreach (var x in xml.Elements())
                    if (x.Name == "config")
                        backupSairSistema = bool.Parse(x.Attribute("BackupSairSistema").Value);
                    else
                        diretorios.Add(x.Attribute("Diretorio")?.Value);

                if (backupSairSistema)
                    new BackupDatabase(BackupDatabase.TipoBackup.AOSAIRDOSISTEMA).ShowDialog(this);
                Application.Exit();
            }
        }

        #endregion

        #region Cadastro

        private void paísToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<Pais> grid = new GridGenerica<Pais>(PaisController.Instancia.GetAll(), new FormPais(), false);
            ShowForm(grid);
        }

        private void estadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<UF> grid = new GridGenerica<UF>(UFController.Instancia.GetAll(), new FormUF(), false);
            ShowForm(grid);
        }

        private void cidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<Cidade> grid = new GridGenerica<Cidade>(CidadeController.Instancia.GetAll(), new FormCidade(), false);
            ShowForm(grid);
        }

        private void pessoaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridCliente grid = new GridCliente(PessoaController.Instancia.GetAll().Distinct().ToList(), null, "Pessoa", false, typeof(FormPessoa));
            ShowForm(grid);
        }

        private void condiçãoDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<Condicao> grid = new GridGenerica<Condicao>(CondicaoController.Instancia.GetAll(), new FormCondicao(), false);
            ShowForm(grid);
        }

        private void empresaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            chamaGrid("Tabela de Grupo");
        }

        private void filialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<Filial> grid = new GridGenerica<Filial>(FilialController.Instancia.GetAll(), new FormFilial(), false);
            ShowForm(grid);
        }

        private void classificaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<Classificacao> grid = new GridGenerica<Classificacao>(ClassificacaoController.Instancia.GetAll(), new FormClassificacao(), false);
            ShowForm(grid);
        }

        private void funcionárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridCliente grid = new GridCliente(PessoaController.Instancia.GetAllFuncionarios(),
                null, "Funcionários", false, typeof(FormPessoa));
            ShowForm(grid);
        }

        private void tabelaDePreçosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<TabelaPreco> grid = new GridGenerica<TabelaPreco>(TabelaPrecoController.Instancia.GetAll(), new FormTabelaPreco(), false);
            ShowForm(grid);
        }

        private void grupoEstoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<GrupoEstoque> grid = new GridGenerica<GrupoEstoque>(GrupoEstoqueController.Instancia.GetAll(), new FormGrupoEstoque(), false);
            ShowForm(grid);
        }

        private void unidadeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<Unidade> grid = new GridGenerica<Unidade>(UnidadeController.Instancia.GetAll(), new FormUnidade(), false);
            ShowForm(grid);
        }

        private void tabelaCFOPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<TabelaCFOP> grid = new GridGenerica<TabelaCFOP>(TabelaCFOPController.Instancia.GetAll(), false, typeof(FormTabelaCFOP));
            ShowForm(grid);
        }

        private void operaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<Operacao> grid = new GridGenerica<Operacao>(OperacaoController.Instancia.GetAll(), false, typeof(FormOperacao));
            ShowForm(grid);
        }

        private void tiposDeOrçamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<TipoPedido> grid = new GridGenerica<TipoPedido>(TipoPedidoController.Instancia.GetAll(), false, typeof(FormTipoPedido));
            ShowForm(grid);
        }

        private void tiposDeMovimentaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<TipoNota> grid = new GridGenerica<TipoNota>(TipoNotaController.Instancia.GetAll(), false, typeof(FormTipoNota));
            ShowForm(grid);
        }

        private void produtosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GridProdutoGen grid = new GridProdutoGen(ProdutoController.Instancia.GetAll().ToList(), false, typeof(FormProduto));
            ShowForm(grid);
        }

        private void toolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GridGenerica<ICMS_Interestadual> grid = new GridGenerica<ICMS_Interestadual>(ICMS_InterestadualController.Instancia.GetAll(), false, typeof(FormInterestadual));
            ShowForm(grid);
        }

        private void pessoasPorStatusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            PessoasPorStatus form = new PessoasPorStatus();
            ShowForm(form);
        }

        private void produtosToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            ProdutosRelatorio form = new ProdutosRelatorio();
            ShowForm(form);
        }

        #endregion

        #region Comercial

        private void vendedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridCliente grid = new GridCliente(PessoaController.Instancia.GetAllVendedores(), null, "Vendedor", false, typeof(FormPessoa));
            ShowForm(grid);
        }

        private void orçamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XElement TagBalcaoPafEcf = XDocument.Load("ConfiguracaoCwork.xml").Element("Cwork").Element("BalcaoPafEcf");
            var configuracao = ConfiguracaoController.Instancia.GetConfiguracao();
            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("inOutType", InOutType.Saída);

            if (TagBalcaoPafEcf != null && Convert.ToInt32(TagBalcaoPafEcf.Value) == 1 &&
                configuracao.UtilizaPafECF)
                parametros.Add("tipoPedido", new[] { TipoPedidoType.Pedido });
            else
                parametros.Add("tipoPedido", new[] { TipoPedidoType.Orçamento, TipoPedidoType.Pedido });

            GridOrcamentoGen grid = new GridOrcamentoGen(null, InOutType.Saída, null, null, true, DateTime.Now.Date.AddMonths(-6), DateTime.Now, parametros, typeof(FormOrcamento), InOutType.Saída);
            ShowForm(grid);
        }

        private void clientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridCliente grid = new GridCliente(PessoaController.Instancia.GetAllClientes(), null, "Cliente", false, typeof(FormPessoa));
            ShowForm(grid);
        }

        private void históricoVendaProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHistoricoVenda form = new FormHistoricoVenda(null, null, InOutType.Saída, FormHistoricoVenda.FiltroPor.Produto);
            ShowForm(form);
        }

        private void históricoVendasProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHistoricoVenda form = new FormHistoricoVenda(null, null, InOutType.Saída, FormHistoricoVenda.FiltroPor.Produto);
            ShowForm(form);
        }

        private void vendasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = GridNotaGen.Get(InOutType.Saída, ProdutoServicoType.ProdutosEServicos, false);
            ShowForm(grid);
        }

        private void statusToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OrcamentoVendaPorStatus form = new OrcamentoVendaPorStatus();
            ShowForm(form);
        }

        private void dataToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            OrcamentoVendaPorVend form = new OrcamentoVendaPorVend(1);
            ShowForm(form);
        }

        private void vendedorToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            OrcamentoVendaPorVend form = new OrcamentoVendaPorVend(0);
            ShowForm(form);
        }

        private void clienteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OrcamentoVendaPorCliente form = new OrcamentoVendaPorCliente();
            ShowForm(form);
        }

        private void faturamentoLoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFaturamentoLoteNova form = new FormFaturamentoLoteNova();
            ShowForm(form);
        }

        private void reduçãoZToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (Convert.ToInt32(XDocument.Load("ConfiguracaoCwork.xml").Element("Cwork").Element("ImpressoraFiscal").Value) == 0)
                MessageBox.Show("Nao é possivel realizar a redução Z, pois não foi encontrada uma impressora fiscal em sua máquina.", "Redução Z", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (MessageBox.Show("Deseja fazer a redução Z?", "Redução Z", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                try
                {
                    var form = new CwkAviso("Por favor aguarde enquanto a redução Z é processada.");
                    ValidaPRTipo60 VPRT60 = new ValidaPRTipo60(form);
                    Thread t = new Thread(VPRT60.ReducaoZ);
                    t.Start();
                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    new FormErro(new Exception("Verifique a conexão com a impressora e o diretório da aplicação, conferindo se o arquivo 'BemaFI32.dll' existe.", ex)).ShowDialog();
                }
        }

        private void leituToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CupomFiscal cupomFiscal = new CupomFiscal(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE"));

            try { cupomFiscal.LeituraX(); }
            catch (Exception ex) { new FormErro(new Exception("Verifique a conexão com a impressora e o diretório da aplicação, conferindo se o arquivo 'BemaFI32.dll' existe.", ex)).ShowDialog(); }
        }

        private void cancelaÚltimoCupomToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CupomFiscal cupomFiscal = new CupomFiscal(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE"));

            try { cupomFiscal.CancelaCupom(); }
            catch (Exception ex) { new FormErro(new Exception(ex.Message, ex)).ShowDialog(); }
        }

        #endregion

        #region Compras

        private void fornecedoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridCliente grid = new GridCliente(PessoaController.Instancia.GetAllFornecedores(), null, "Fornecedor", false, typeof(FormPessoa));
            ShowForm(grid);
        }

        private void statusToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrcamentoCompraPorStatus form = new OrcamentoCompraPorStatus();
            ShowForm(form);
        }

        private void dataToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            OrcamentoCompraPorData form = new OrcamentoCompraPorData();
            ShowForm(form);
        }

        private void fornecedorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OrcamentoCompraPorFornecedor form = new OrcamentoCompraPorFornecedor();
            ShowForm(form);
        }

        private void orçamentosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //Func<IList<cwkGestao.Modelo.Pedido>> atualizador = () => cwkGestao.Negocio.PedidoController.Instancia.GetAll(cwkGestao.Modelo.InOutType.Entrada, null, null);

            Dictionary<string, object> parametros = new Dictionary<string, object>();
            parametros.Add("inOutType", InOutType.Entrada);
            parametros.Add("tipoPedido", new[] { TipoPedidoType.Orçamento, TipoPedidoType.Pedido });

            GridOrcamentoGen grid = new GridOrcamentoGen(null, InOutType.Entrada,
                null, null, true, DateTime.Now.Date.AddYears(-1), DateTime.Now, parametros, typeof(FormOrcamento), InOutType.Entrada);
            ShowForm(grid);
        }

        private void comprasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var grid = GridNotaGen.Get(InOutType.Entrada, ProdutoServicoType.Produtos, false);
            ShowForm(grid);
        }

        #endregion

        #region Financeiro

        private void bancoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<Banco> grid = new GridGenerica<Banco>(BancoController.Instancia.GetAll(), new FormBanco(), false);
            ShowForm(grid);
        }

        private void portadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<Portador> grid = new GridGenerica<Portador>(PortadorController.Instancia.GetAll(), new FormPortador(), false);
            ShowForm(grid);
        }

        private void acrescimoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<Acrescimo> grid = new GridGenerica<Acrescimo>(AcrescimoController.Instancia.GetAll(), new FormAcrescimo(), false);
            ShowForm(grid);
        }

        private void planoContaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<PlanoConta> grid = new GridGenerica<PlanoConta>(PlanoContaController.Instancia.GetAll(), new FormPlanoConta(), false);
            ShowForm(grid);
        }

        private void históricosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<Historico> grid = new GridGenerica<Historico>(HistoricoController.Instancia.GetAll(), new FormHistorico(), false);
            ShowForm(grid);
        }

        private void tipoDeDocumentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<TipoDocumento> grid = new GridGenerica<TipoDocumento>(TipoDocumentoController.Instancia.GetAll(), new FormTipoDocumento(), false);
            ShowForm(grid);
        }

        private void históricoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHistoricoVenda form = new FormHistoricoVenda(null, null, InOutType.Saída, FormHistoricoVenda.FiltroPor.Pessoa);
            ShowForm(form);
        }

        private void FinanceiroDocumentosReceber_Click(object sender, EventArgs e)
        {
            var grid = GridDocumentoGen.Get(null, TipoDocumentoType.Receber, false);
            ShowForm(grid);
        }

        private void FinanceiroDocumentosPagar_Click(object sender, EventArgs e)
        {
            var grid = GridDocumentoGen.Get(null, TipoDocumentoType.Pagar, false);
            ShowForm(grid);
        }

        private void vencimentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CReceberPorVencimento form = new CReceberPorVencimento(0);
            ShowForm(form);
        }

        private void previsãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CReceberPorPrevisao form = new CReceberPorPrevisao();
            ShowForm(form);
        }

        private void dataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CReceberPorData form = new CReceberPorData();
            ShowForm(form);
        }

        private void clienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CReceberPorCliente form = new CReceberPorCliente();
            ShowForm(form);
        }

        private void fornecedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CPagarPorFornecedor form = new CPagarPorFornecedor();
            ShowForm(form);
        }

        private void dataToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CPagarPorData form = new CPagarPorData();
            ShowForm(form);
        }

        private void previsãoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CPagarPorPrevisao form = new CPagarPorPrevisao();
            ShowForm(form);
        }

        private void vencimentoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CReceberPorVencimento form = new CReceberPorVencimento(2);
            ShowForm(form);
        }

        private void tipoDocumentoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CPagarPorTipoDocumento form = new CPagarPorTipoDocumento(0);
            ShowForm(form);
        }

        private void tipoDocumentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CReceberPorTipoDocumento form = new CReceberPorTipoDocumento(0);
            ShowForm(form);
        }

        private void movimentoPorFormaPagtoReceberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MovimentacaoPorFormaDePagamento form = new MovimentacaoPorFormaDePagamento(TipoDocumentoType.Receber);
            ShowForm(form);
        }

        private void movimentoPorFormaPgtoPagarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MovimentacaoPorFormaDePagamento form = new MovimentacaoPorFormaDePagamento(TipoDocumentoType.Pagar);
            ShowForm(form);
        }

        private void fluxoDeCaixaRealizadoDetalhadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MovimentacaoPorPlanoConta form = new MovimentacaoPorPlanoConta(false);
            ShowForm(form);
        }

        #endregion

        private void cadastroUsuárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Facade.ChamaGridUsuario("Tabela de Usuário");
        }

        private void loteOrçamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFaturamentoOrcamentoH form = new FormFaturamentoOrcamentoH();
            ShowForm(form);
        }

        private void extratoClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDocumentoReceberCorrigido form = new FormDocumentoReceberCorrigido();
            ShowForm(form);
        }

        private void alteraçãoDePreçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAlteracaoPreco form = new FormAlteracaoPreco();
            ShowForm(form);
        }

        private void baixaEmLoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GridBaixaDocumento grid = new GridBaixaDocumento();
                ShowForm(grid);
            }
            catch (Exception exx)
            {
                MessageBox.Show(exx.Message + exx.InnerException != null ? exx.InnerException.Message : "");
            }
        }

        private void lançamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new GridMovCaixa(MovCaixaController.Instancia.GetAll(),
                null, false, typeof(FormMovCaixa));
            ShowForm(form);
        }

        private void partilhaSimplesNacionalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<TabelaFaixaFatSimples> grid = new GridGenerica<TabelaFaixaFatSimples>(TabelaFaixaFatSimplesController.Instancia.GetAll(), false, typeof(FormTabelaFaixaFatSimples));
            ShowForm(grid);
        }

        private void saldoBancosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SaldoBanco form = new SaldoBanco();
            ShowForm(form);
        }

        private void movimentoCaixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MovCaixa form = new MovCaixa();
            ShowForm(form);
        }

        private void movimentoCaixaPorDataToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MovCaixaPorData form = new MovCaixaPorData();
            ShowForm(form);
        }

        private void históricoClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHistoricoCliente grid = new FormHistoricoCliente();
            if (Facade.ControleAcesso(grid)) grid.ShowDialog();
        }

        private void MenuInicial_Load(object sender, EventArgs e)
        {
            EscondeModulos();
            objConfiguracao = MO.Configuracao.GetConfiguracao(db); // ConfiguracaoController.Instancia.GetConfiguracao();
            switch (menu)
            {
                case 1: //Menu Balcao
                    menuGestao.Visible = false;
                    menuBalcao.Visible = true;
                    break;
                default:
                    menuGestao.Visible = true;
                    menuBalcao.Visible = false;
                    DefinirConfiguracaoMenu();
                    break;
            }

            var nomeEmpresa = (from emp in db.Filials
                               where emp.Codigo == 1
                               select emp).Single().Nome;
            Text = Text + "  [" + nomeEmpresa + "]";
            mostradocbaixado = objConfiguracao.bListarBaixados;
            if (!objConfiguracao.UtilizaAtendimento)
            {
                atendimentosToolStripMenuItem.Visible = false;
                atendimentosToolStripMenuItem.Enabled = false;
                statusDeAtendimentoToolStripMenuItem.Visible = false;
                statusDeAtendimentoToolStripMenuItem.Enabled = false;
            }

            for (int i = 0; i < menuGestao.Items.Count; i++)
                if (menuGestao.Items[i] is ToolStripMenuItem)
                {
                    var menuTemp = menuGestao.Items[i] as ToolStripMenuItem;
                    menuTemp.DropDownOpening += cadastroToolStripMenuItem_DropDownOpening;
                }

            EscondeModulos();
            AtualizaDadosUsuario(true);
        }

        private void VerificaAberturaComponenteAviso()
        {
            if (dckpAvisos.Visibility == DockVisibility.Hidden ||
                dckpAvisos.Visibility == DockVisibility.AutoHide)
                dckpAvisos.BeginInvoke(new Action(() => dckpAvisos.Visibility = DockVisibility.Visible));
        }

        private void ThreadAviso()
        {
            Action met = () => ThreadRotinaAtualizaAviso();
            m = new Thread(new ThreadStart(met));
            m.Name = "ThreadAvisoContatos";
            m.Start();
        }

        public void ThreadRotinaAtualizaAviso()
        {
            while (true)
                try
                {
                    RotinaAtualizaAviso();
                    Thread.Sleep(30000);
                }
                catch (InvalidOperationException)
                {
                    break;
                }
                catch (Exception e)
                {
                    string s = e.Message;
                    s = s;
                }
        }

        private void RotinaAtualizaAviso()
        {
            var usuarioLogado = MO.cwkGlobal.objUsuarioLogado;
            string eventoAnteriorOld, eventoProximoOld;
            AtribuiValoresEventos(out eventoAnteriorOld, out eventoProximoOld);

            avisoPorPeriodo = ContatoController.Instancia.GetAvisosPorPeriodo(usuarioLogado.Id);

            if (eventoAnteriorOld != avisoPorPeriodo.HoraInicialEventoAnterior ||
                eventoProximoOld != avisoPorPeriodo.HoraInicialEventoProximo)
            {
                avisoAnterior = ContatoController.Instancia.GetAvisosAnteriores(usuarioLogado.Id);
                avisoProximo = ContatoController.Instancia.GetAvisosPosteriores(usuarioLogado.Id);
                VerificaAberturaComponenteAviso();

                PreencheObjetoAvisoAnterior();
                PreencheObjetoAvisoProximo();
            }

            PreencheObjetoStatus();
        }

        private void PreencheObjetoStatus()
        {
            lblNumAgendamentosPassados.BeginInvoke(new Action(() => lblNumAgendamentosPassados.Text = Convert.ToString(avisoPorPeriodo.NumEventoPassado)));
            lblNumAgendamentosOntem.BeginInvoke(new Action(() => lblNumAgendamentosOntem.Text = Convert.ToString(avisoPorPeriodo.NumEventoOntem)));
            lblNumAgendamentosHoje.BeginInvoke(new Action(() => lblNumAgendamentosHoje.Text = Convert.ToString(avisoPorPeriodo.NumEventoHoje)));
            lblNumAgendamentosAmanha.BeginInvoke(new Action(() => lblNumAgendamentosAmanha.Text = Convert.ToString(avisoPorPeriodo.NumEventoAmanha)));
            lblNumAgendamentosProximos.BeginInvoke(new Action(() => lblNumAgendamentosProximos.Text = Convert.ToString(avisoPorPeriodo.NumEventoFuturo)));
        }

        private void PreencheObjetoAvisoProximo()
        {
            if (avisoProximo != null)
            {
                lblNomePessoaProximo.BeginInvoke(new Action(() => lblNomePessoaProximo.Text = avisoProximo.NomePessoa));
                lblContatoNomeProximo.BeginInvoke(new Action(() => lblContatoNomeProximo.Text = avisoProximo.Contato));
                lblAssuntoProximo.BeginInvoke(new Action(() => lblAssuntoProximo.Text = avisoProximo.Assunto));
                lblDataProximo.BeginInvoke(new Action(() => lblDataProximo.Text = Convert.ToString(avisoProximo.Data).Substring(0, 10)));
                lblHoraInicialProximo.BeginInvoke(new Action(() => lblHoraInicialProximo.Text = avisoProximo.HoraInicial));
                lblHoraFinalProximo.BeginInvoke(new Action(() => lblHoraFinalProximo.Text = avisoProximo.HoraFinal));
                lblStatusContatoProximo.BeginInvoke(new Action(() => lblStatusContatoProximo.Text = avisoProximo.StatusContato));
            }
            else
            {
                lblNomePessoaProximo.BeginInvoke(new Action(() => lblNomePessoaProximo.Text = string.Empty));
                lblContatoNomeProximo.BeginInvoke(new Action(() => lblContatoNomeProximo.Text = string.Empty));
                lblAssuntoProximo.BeginInvoke(new Action(() => lblAssuntoProximo.Text = string.Empty));
                lblDataProximo.BeginInvoke(new Action(() => lblDataProximo.Text = string.Empty));
                lblHoraInicialProximo.BeginInvoke(new Action(() => lblHoraInicialProximo.Text = string.Empty));
                lblHoraFinalProximo.BeginInvoke(new Action(() => lblHoraFinalProximo.Text = string.Empty));
                lblStatusContatoProximo.BeginInvoke(new Action(() => lblStatusContatoProximo.Text = string.Empty));
            }
        }

        private void PreencheObjetoAvisoAnterior()
        {
            if (avisoAnterior != null)
            {
                lblNomePessoaAnterior.BeginInvoke(new Action(() => lblNomePessoaAnterior.Text = avisoAnterior.NomePessoa));
                lblContatoNomeAnterior.BeginInvoke(new Action(() => lblContatoNomeAnterior.Text = avisoAnterior.Contato));
                lblAssuntoAnterior.BeginInvoke(new Action(() => lblAssuntoAnterior.Text = avisoAnterior.Assunto));
                lblDataAnterior.BeginInvoke(new Action(() => lblDataAnterior.Text = Convert.ToString(avisoAnterior.Data).Substring(0, 10)));
                lblHoraInicialAnterior.BeginInvoke(new Action(() => lblHoraInicialAnterior.Text = avisoAnterior.HoraInicial));
                lblHoraFinalAnterior.BeginInvoke(new Action(() => lblHoraFinalAnterior.Text = avisoAnterior.HoraFinal));
                lblStatusContatoAnterior.BeginInvoke(new Action(() => lblStatusContatoAnterior.Text = avisoAnterior.StatusContato));
            }
            else
            {
                lblNomePessoaAnterior.BeginInvoke(new Action(() => lblNomePessoaAnterior.Text = string.Empty));
                lblContatoNomeAnterior.BeginInvoke(new Action(() => lblContatoNomeAnterior.Text = string.Empty));
                lblAssuntoAnterior.BeginInvoke(new Action(() => lblAssuntoAnterior.Text = string.Empty));
                lblDataAnterior.BeginInvoke(new Action(() => lblDataAnterior.Text = string.Empty));
                lblHoraInicialAnterior.BeginInvoke(new Action(() => lblHoraInicialAnterior.Text = string.Empty));
                lblHoraFinalAnterior.BeginInvoke(new Action(() => lblHoraFinalAnterior.Text = string.Empty));
                lblStatusContatoAnterior.BeginInvoke(new Action(() => lblStatusContatoAnterior.Text = string.Empty));
            }
        }

        private void AtribuiValoresEventos(out string eventoAnteriorOld, out string eventoProximoOld)
        {
            if (avisoPorPeriodo != null)
            {
                eventoAnteriorOld = avisoPorPeriodo.HoraInicialEventoAnterior;
                eventoProximoOld = avisoPorPeriodo.HoraInicialEventoProximo;
            }
            else
            {
                eventoAnteriorOld = "NULL";
                eventoProximoOld = "NULL";
            }
        }

        private void incluirOrcamentoMenu_Click(object sender, EventArgs e)
        {
            FormOrcamento form = new FormOrcamento(InOutType.Saída);
            form.TipoPedido = InOutType.Saída;
            form.Operacao = Acao.Incluir;
            form.ShowDialog();
        }

        private void impressorasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<Impressora> grid = new GridGenerica<Impressora>(ImpressoraController.Instancia.GetAll(), false, typeof(FormImpressora));
            ShowForm(grid);
        }

        private void movimentaçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormConsultaCustoProjeto form = new FormConsultaCustoProjeto(0);
            if (Facade.ControleAcesso(form)) form.ShowDialog();
        }

        private void cadastroToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GridProjeto grid = new GridProjeto(null, "Tabela de Orçamento", false, 0, 1);
            ShowForm(grid);
        }

        private void imprimeGabaritoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormImprimeGabarito form = new FormImprimeGabarito();
            form.ShowDialog();
        }

        private void baixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridBaixaDocumento grid = new GridBaixaDocumento();
            ShowForm(grid);
        }

        private void FinanceiroDocumentosReceberBaixados_Click(object sender, EventArgs e)
        {
            var grid = GridDocumentoGen.Get(null, TipoDocumentoType.Receber, true);
            ShowForm(grid);
            //GridDocumento grid = new GridDocumento("Tabela de Documento a Receber Baixado", false, 0, MO.TipoDocumentoType.Receber, true);
            //ShowForm(grid);
        }

        private void FinanceiroDocumentosPagarBaixados_Click(object sender, EventArgs e)
        {
            var grid = GridDocumentoGen.Get(null, TipoDocumentoType.Pagar, true);
            ShowForm(grid);
            //GridDocumento grid = new GridDocumento("Tabela de Documento a Pagar Baixado", false, 0, MO.TipoDocumentoType.Pagar, true);
            //ShowForm(grid);
        }

        private void exportaçãoDanfePDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormExportacaoNfe form = new FormExportacaoNfe();
            if (Facade.ControleAcesso(form)) form.ShowDialog();
        }

        private void ordemServiçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridOrdemProducao grid = new GridOrdemProducao();
            grid.Text = "Grid de Ordem de Serviço";
            grid.ShowDialog();
        }

        private void serviçosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                GridGenerica<Servico> grid =
                    new GridGenerica<Servico>(ServicoController.Instancia.GetAll(),
                        false, typeof(FormServico));
                ShowForm(grid);
            }
            catch (Exception ex)
            {
                new FormErro(ex).ShowDialog();
            }
        }

        private void ordemServiçoClassificaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrdemProducaoFiltro ordem = new OrdemProducaoFiltro();
            ordem.ShowDialog();
        }

        private void classificaçãoOrdemDeServiçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<ClassificacaoOrdemProducao> grid = new GridGenerica<ClassificacaoOrdemProducao>(ClassificacaoOrdemProducaoController.Instancia.GetAll(), false, typeof(FormClassificacaoOrdemProducao));
            ShowForm(grid);
        }

        private void projetoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //passando 1 vai pra entrada
            var form = new FormImpDocumentosCC(1);
            ShowForm(form);
        }

        private void projetoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            //passando 2 vai pra saida
            var form = new FormImpDocumentosCC(2);
            ShowForm(form);
        }

        private void criarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Func<IList<Cotacao>> atualizador = () => CotacaoController.Instancia.GetAll();

            var form = new GridCotacao(atualizador(), null, atualizador, false, typeof(FormCotacao));
            ShowForm(form);
        }

        private void montaCotacãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMontaCotacao form = new FormMontaCotacao();
            form.Selecionado = CotacaoController.Instancia.GetCotacaoLista().FirstOrDefault();
            if (form.Selecionado == null)
            {
                new FormErro(new Exception("Não existem cotações do tipo lista.")).Show();
                return;
            }

            ShowForm(form);
        }

        private void statusToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CotacaoStatus form = new CotacaoStatus();
            ShowForm(form);
        }

        private void itemsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAtualizaCotacaoLST grid = new FormAtualizaCotacaoLST();
            grid.Visible = false;
            grid.ShowDialog();
        }

        private void notasCanceladasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = GridNotaGen.Get(InOutType.Saída, ProdutoServicoType.ProdutosEServicos, true);
            ShowForm(grid);
        }

        private void remessaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<Remessa> grid = new GridGenerica<Remessa>(RemessaController.Instancia.GetAll(), false, typeof(FormRemessa));
            ShowForm(grid);
        }

        private void gerarBoletosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormManutImprimeBoletoRemessa form = new FormManutImprimeBoletoRemessa();
            if (Facade.ControleAcesso(form)) form.ShowDialog();
        }

        private void trocaUsuárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Facade.ChamaAutenticacao(Sistema.Gestão, versao, false);
            //Cwork.Utilitarios.Componentes.Global.Usuario = MO.cwkGlobal.objUsuarioLogado.Login;
            EscondeModulos();
            AtualizaDadosUsuario(true);
            VerificarCertificado();
        }

        //private void AlteraCor()
        //{
        //    for (int i = 0; i < menuStrip1.Items.Count; i++)
        //    {
        //        string[] Nome = menuStrip1.Items[i].Text.Split(' ');

        //        if (Nome.Length == 3 && Nome[1] == "-")
        //        {
        //            if (Nome[2] == "True")
        //                menuStrip1.Items[i].ForeColor = tbTrue.BackColor;
        //            else if (Nome[2] == "False")
        //                menuStrip1.Items[i].ForeColor = tbFalse.BackColor;
        //        }
        //    }
        //}

        private void AtualizaDadosUsuario(bool ChamarPDV)
        {
            // Verificando se só tem acesso ao PDV
            var TemAcessoAOutraFuncionalidade = false;
            var TemAcessoAoPDV = false;
            var dicFuncionalidadesExsitentes = Facade.getUsuarioLogado.ToDictionary();
            var listFuncionalidadesPDV = new List<string>
            {
                "UtilizaPDV",
                "UtilizaPDVFrenteDeCaixa",
                "UtilizaPDVLancamentosFluxoDeCaixa",
                "UtilizaPDVLancamentosDeSangria",
                "UtilizaPDVLancamentosDeSuprimento"
            };

            TemAcessoAOutraFuncionalidade = dicFuncionalidadesExsitentes
                .Where(o => o.Key.StartsWith("Utiliza") &&
                            !listFuncionalidadesPDV.Contains(o.Key) &&
                            Convert.ToBoolean(o.Value))
                .Select(o => Convert.ToBoolean(o.Value)).Any();

            TemAcessoAoPDV = dicFuncionalidadesExsitentes
                .Where(o => listFuncionalidadesPDV.Contains(o.Key))
                .Select(o => Convert.ToBoolean(o.Value)).Count() == 5;

            var ArquivoIni = new IniFile(Debugger.IsAttached
                ? $"{Path.GetFullPath(".").Substring(0, Path.GetFullPath(".").IndexOf("cwkGestao"))}cwkGestao\\App_Data\\Start.ini"
                : $"{Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)}\\App_Data\\Start.ini");

            //if (!TemAcessoAOutraFuncionalidade && TemAcessoAoPDV && "1" == ArquivoIni.GetValue("Configuracoes_PDV", "ABRIR_PDV_EXECUTAR", "0"))
            if (TemAcessoAoPDV && "1" == ArquivoIni.GetValue("Configuracoes_PDV", "ABRIR_PDV_EXECUTAR", "0"))
            {
                if (ConfiguracaoController.Instancia.GetConfiguracao().SenhaAoEntrarNoPDV == 1)
                {
                    var chamarTelaLogin = Facade.ChamaAutenticacao(Sistema.Gestão, versao, false);
                    EscondeModulos();
                    if (chamarTelaLogin && ChamarPDV)
                        AbrirPdv();
                }
                else
                {
                    AbrirPdv();
                }
            }

#if DEBUG
            // Fluxo para não alterar visibilidade dos menus
            lblUsuario.Text = "Usuário: " + Facade.getUsuarioLogado.Login;
            return;
#endif

            var usuario = Facade.getUsuarioLogado;
            lblUsuario.Text = "Usuário: " + usuario.Login;

            // Método para ocultar Menus, ícones e Barras de separação entre os itens dos Menus Pelo Cadastro de Usuário
            // Cadastro
            cadastroToolStripMenuItem.Visible = usuario.UtilizaCadastro;
            básicosToolStripMenuItem.Visible = usuario.UtilizaCadastroBasicos;
            classificaçãoToolStripMenuItem.Visible = usuario.UtilizaCadastroClassificacao;
            condiçãoDePagamentoToolStripMenuItem.Visible = usuario.UtilizaCadastroCondicaoDePagamento;
            //statusOSToolStripMenuItem.Visible = usuario.UtilizaCadastroStatusOS;
            empresaToolStripMenuItem.Visible = usuario.UtilizaCadastroGrupo;
            configuraçãoManagerToolStripMenuItem.Visible = usuario.UtilizaCadastroConfiguracaoManager;
            filialToolStripMenuItem.Visible = usuario.UtilizaCadastroEmpresa;
            toolStripMenuItem23.Visible = usuario.UtilizaCadastroPeriodoApuracaoContabil;
            pessoaToolStripMenuItem.Visible = usuario.UtilizaCadastroPessoa;
            funcionárioToolStripMenuItem.Visible = usuario.UtilizaCadastroFuncionario;
            toolStripMenuItem11.Visible = usuario.UtilizaCadastroGrupoEstoque;
            toolStripMenuItem12.Visible = usuario.UtilizaCadastroUnidade;
            toolStripMenuItem10.Visible = usuario.UtilizaCadastroTabelaPreco;
            miGrupo1.Visible = usuario.UtilizaCadastroCategoria;
            miGrupo2.Visible = usuario.UtilizaCadastroModelo;
            miGrupo3.Visible = usuario.UtilizaCadastroTipo;
            toolStripMenuItem6.Visible = usuario.UtilizaCadastroProduto;
            ConversaoUnidadeMenuItem.Visible = usuario.UtilizaCadastroConversaoDeUnidades;
            ProdutosInativos.Visible = usuario.UtilizaCadastroProdutosInativos;
            nCMToolStripMenuItem.Visible = usuario.UtilizaCadastroNCM;
            tsSubproduto.Visible = usuario.UtilizaCadastroSubProduto;
            tsMateriaPrima.Visible = usuario.UtilizaCadastroMateriaPrima;
            tsImobilizado.Visible = usuario.UtilizaCadastroImobilizado;
            serviçosToolStripMenuItem.Visible = usuario.UtilizaCadastroServico;
            parâmetrosToolStripMenuItem.Visible = usuario.UtilizaCadastroParametros;
            relatóriosToolStripMenuItem5.Visible = usuario.UtilizaCadastroRelatorios;
            //toolStripMenuItem30.Visible = usuario.UtilizaCadastroRelatoriosDeProdutos;
            // Cadastro Ícones
            tsbProdutos.Visible = usuario.UtilizaCadastro;
            toolStripButton10.Visible = usuario.UtilizaCadastro;
            // Linhas de Separação Cadastro
            toolStripSeparator1.Visible = usuario.UtilizaCadastroStatusOS;
            toolStripSeparator1.Visible = usuario.UtilizaCadastroGrupo;
            toolStripSeparator2.Visible = usuario.UtilizaCadastroPeriodoApuracaoContabil;
            toolStripSeparator2.Visible = usuario.UtilizaCadastroPessoa;
            toolStripSeparator31.Visible = usuario.UtilizaCadastroFuncionario;
            toolStripSeparator31.Visible = usuario.UtilizaCadastroGrupo;
            toolStripSeparator5.Visible = usuario.UtilizaCadastroTipo;
            toolStripSeparator5.Visible = usuario.UtilizaCadastroProduto;
            toolStripSeparator6.Visible = usuario.UtilizaCadastroServico;
            toolStripSeparator6.Visible = usuario.UtilizaCadastroParametros;
            toolStripSeparator3.Visible = usuario.UtilizaCadastroParametros;
            toolStripSeparator3.Visible = usuario.UtilizaCadastroRelatorios;

            // Comercial
            comercialToolStripMenuItem.Visible = usuario.UtilizaComercial;
            toolStripTransportadora.Visible = usuario.UtilizaComercialTransportadora;
            clientesToolStripMenuItem.Visible = usuario.UtilizaComercialCliente;
            vendedoresToolStripMenuItem.Visible = usuario.UtilizaComercialVendedor;
            orcamentoVenda.Visible = usuario.UtilizaComercialOrcamento;
            aprovacaoPedidoToolStripMenuItem21.Visible = usuario.UtilizaComercialAprovacaoOrcamento;
            históricoVendasProdutoToolStripMenuItem.Visible = usuario.UtilizaComercialHistoricoDeVendaPorProduto;
            históricoToolStripMenuItem.Visible = usuario.UtilizaComercialHistoricoDeVendaPorCliente;
            contatosToolStripMenuItem.Visible = usuario.UtilizaComercialContatos;
            toolStripMenuItem25.Visible = usuario.UtilizaComercialControleDeRotas;
            controleDeEntregasToolStripMenuItem.Visible = usuario.UtilizaComercialControleDeEntregas;
            vendasToolStripMenuItem.Visible = usuario.UtilizaComercialNotaDeSaida;
            notaComplementarDeSaidaToolStripMenuItem.Visible = usuario.UtilizaComercialNotaComplementarDeSaida;
            notasCanceladasToolStripMenuItem.Visible = usuario.UtilizaComercialNotaCancelada;
            faturamentoLoteToolStripMenuItem.Visible = usuario.UtilizaComercialFechamentoFatura;
            loteOrçamentoToolStripMenuItem.Visible = usuario.UtilizaComercialLoteOrcamento;
            cupomFiscalToolStripMenuItem.Visible = usuario.UtilizaComercialCuponsFiscais;
            tipoServiçoTransporteToolStripMenuItem.Visible = usuario.UtilizaComercialTipoServicoTransporte;
            relatóriosToolStripMenuItem.Visible = usuario.UtilizaComercialRelatorios;
            // Comercial // Icones
            tsbClientes.Visible = usuario.UtilizaComercial;
            toolStripButton5.Visible = usuario.UtilizaComercial;
            tsbVendas.Visible = usuario.UtilizaComercial;
            toolStripButton6.Visible = usuario.UtilizaComercial;
            // Linhas de Separação Comercial
            toolStripSeparator8.Visible = usuario.UtilizaComercialCliente;
            toolStripSeparator8.Visible = usuario.UtilizaComercialVendedor;
            // toolStripSeparator9.Visible = usuario.UtilizaComercialVendedor;
            // toolStripSeparator9.Visible = usuario.UtilizaComercialOrcamento;
            toolStripSeparator10.Visible = usuario.UtilizaComercialControleDeEntregas;
            toolStripSeparator10.Visible = usuario.UtilizaComercialNotaDeSaida;
            toolStripMenuItem2.Visible = usuario.UtilizaComercialNotaCancelada;
            toolStripMenuItem2.Visible = usuario.UtilizaComercialFechamentoFatura;
            toolStripSeparator11.Visible = usuario.UtilizaComercialCuponsFiscais;
            toolStripSeparator11.Visible = usuario.UtilizaComercialTipoServicoTransporte;
            toolStripSeparator37.Visible = usuario.UtilizaComercialTipoServicoTransporte;
            toolStripSeparator37.Visible = usuario.UtilizaComercialRelatorios;

            // Compra
            comprasToolStripMenuItem.Visible = usuario.UtilizaCompra;
            fornecedoresToolStripMenuItem.Visible = usuario.UtilizaCompraFornecedor;
            cotaçõesToolStripMenuItem.Visible = usuario.UtilizaCompraCotacoes;
            orçamentosToolStripMenuItem1.Visible = usuario.UtilizaCompraOrcamento;
            pedidosToolStripMenuItem.Visible = usuario.UtilizaCompraPedidos;
            comprasToolStripMenuItem1.Visible = usuario.UtilizaCompraNotaDeEntrada;
            notaComplementarDeEntradaToolStripMenuItem1.Visible = usuario.UtilizaCompraNotaComplementarDeEntrada;
            comprasCanceladasToolStripMenuItem.Visible = usuario.UtilizaCompraCompraCancelada;
            importaçãoXMLFornecedorToolStripMenuItem.Visible = usuario.UtilizaCompraImportacaoXMLFornecedor;
            identificadoresToolStripMenuItem.Visible = usuario.UtilizaCompraIdentificadores;
            relatóriosToolStripMenuItem1.Visible = usuario.UtilizaCompraRelatorios;
            //Compra / Ícones
            stbNotaCompra.Visible = usuario.UtilizaCompra;

            // Estoque
            estoqueToolStripMenuItem.Visible = usuario.UtilizaEstoque;
            requisiçãoDeSaídaNHibernateToolStripMenuItem.Visible = usuario.UtilizaEstoqueRequisicaoDeSaida;
            requisiçãoBaixadaSaídaNHibernateToolStripMenuItem.Visible = usuario.UtilizaEstoqueRequisicaoBaixadaSaida;
            requisiçãoEntradaNHibernateToolStripMenuItem.Visible = usuario.UtilizaEstoqueRequisicaoDeEntrada;
            requisiçãoBaixadaEntradaNHibernateToolStripMenuItem.Visible = usuario.UtilizaEstoqueRequisicaoBaixadaEntrada;
            toolStripMenuItem4.Visible = usuario.UtilizaEstoqueBaixarRequisicaoEmLote;
            tsConsultaEstoquePorFilial.Visible = usuario.UtilizaEstoqueConsultaPorEmpresa;
            estoqueToolStripMenuItem1.Visible = usuario.UtilizaEstoqueConsultaPorProduto;
            aBCToolStripMenuItem.Visible = usuario.UtilizaEstoqueABC;
            balançoToolStripMenuItem.Visible = usuario.UtilizaEstoqueBalanco;
            tipoDeServiçoDeTransporteToolStripMenuItem.Visible = usuario.UtilizaEstoqueTipoDeServicoDeTransporte;
            inventárioToolStripMenuItem.Visible = usuario.UtilizaEstoqueInventario;
            localEstoqueToolStripMenuItem.Visible = usuario.UtilizaEstoqueLocalEstoque;
            relatóriosToolStripMenuItem2.Visible = usuario.UtilizaEstoqueRelatorios;
            // Estoque / Ícones
            toolStripButton4.Visible = usuario.UtilizaEstoque;

            // Financeiro
            financeiroToolStripMenuItem.Visible = usuario.UtilizaFinanceiro;
            cadastrosToolStripMenuItem.Visible = usuario.UtilizaFinanceiroCadastros;
            documentosToolStripMenuItem.Visible = usuario.UtilizaFinanceiroDocumentos;
            relatóriosToolStripMenuItem3.Visible = usuario.UtilizaFinanceiroRelatorios;
            // Financeiro / Ícones
            toolStripButton4.Visible = usuario.UtilizaFinanceiro;
            toolStripButton2.Visible = usuario.UtilizaFinanceiro;
            tsbRenegociacao.Visible = usuario.UtilizaFinanceiro;
            toolStripButton1.Visible = usuario.UtilizaFinanceiro;

            // Caixa
            caixaToolStripMenuItem.Visible = usuario.UtilizaCaixa;
            lançamentosToolStripMenuItem.Visible = usuario.UtilizaCaixaLancamento;
            fechamentoDeCaixaToolStripMenuItem.Visible = usuario.UtilizaCaixaFechamentoDeCaixa;
            chequeToolStripMenuItem.Visible = usuario.UtilizaCaixaCheque;
            transferênciaBancáriaToolStripMenuItem.Visible = usuario.UtilizaCaixaTransferenciaBancaria;
            relatóiosToolStripMenuItem.Visible = usuario.UtilizaCaixaRelatorios;
            // Caixa / ìcones
            toolStripButton9.Visible = usuario.UtilizaCaixa;
            toolStripButton13.Visible = usuario.UtilizaCaixa;

            // Delivery
            deliveryToolStripMenuItem.Visible = usuario.UtilizaDelivery;
            deliveryToolStripMenuItem1.Visible = usuario.UtilizaDeliveryDelivery;
            monitoramentoToolStripMenuItem.Visible = usuario.UtilizaDeliveryMonitoramento;
            // Delivery / Ìcones
            toolStripButton11.Visible = usuario.UtilizaDelivery;
            toolStripButton12.Visible = usuario.UtilizaDelivery;

            // PDV                                                                                  
            pDVToolStripMenuItem.Visible = usuario.UtilizaPDV;
            frenteDeCaixaToolStripMenuItem1.Visible = usuario.UtilizaPDVFrenteDeCaixa;
            lançamentosFrenteCaixaToolStripMenuItem.Visible = usuario.UtilizaPDVLancamentosFluxoDeCaixa;
            sangriaToolStripMenuItem.Visible = usuario.UtilizaPDVLancamentosDeSangria;
            suprimentoToolStripMenuItem.Visible = usuario.UtilizaPDVLancamentosDeSuprimento;
            // PDV / Ìcones
            toolStripButton8.Visible = usuario.UtilizaPDV;

            // Utilitários
            utilitáriosToolStripMenuItem.Visible = usuario.UtilizaUtilitario;
            exportaçãoReceitaToolStripMenuItem.Visible = usuario.UtilizaUtilitarioExportacao;
            atualizaçãoDeNotaToolStripMenuItem.Visible = usuario.UtilizaUtilitarioAtualizacaoDeNotas;
            envioXMLsDestinatarioToolStripMenuItem.Visible = usuario.UtilizaUtilitarioEnvioXMLsDestinatario;
            alteraçãoDePreçoToolStripMenuItem1.Visible = usuario.UtilizaUtilitarioAlteracaoDePreco;
            atualizarCSTNCMCSOSNToolStripMenuItem.Visible = usuario.UtilizaUtilitarioAtualizarCSTNCMCSOSN;

            // Configuração
            configuraçõesToolStripMenuItem.Visible = usuario.UtilizaConfiguracao;
            sistemaToolStripMenuItem1.Visible = usuario.UtilizaConfiguracaoSistema;
            impressorasToolStripMenuItem.Visible = usuario.UtilizaConfiguracaoImpressora;
            backupToolStripMenuItem.Visible = usuario.UtilizaConfiguracaoBackup;

            // Orçamento de Serviço
            ordemServiçoToolStripMenuItem1.Visible = usuario.UtilizaOrcamentoDeServico;
            equipamentoToolStripMenuItem.Visible = usuario.UtilizaOrcamentoDeServicoEquipamento;
            tipoEquipamentoToolStripMenuItem.Visible = usuario.UtilizaOrcamentoDeServicoTipoEquipamento;
            impostoToolStripMenuItem.Visible = usuario.UtilizaOrcamentoDeServicoImposto;
            ordemServiçoToolStripMenuItem2.Visible = usuario.UtilizaOrcamentoDeServicoOrdemDeServico;
            notasDeServiçoToolStripMenuItem1.Visible = usuario.UtilizaOrcamentoDeServicoNotasDeServico;
            fechamentoFaturaToolStripMenuItem.Visible = usuario.UtilizaOrcamentoDeServicoFechamentoFatura;
            agendamentosToolStripMenuItem.Visible = usuario.UtilizaOrcamentoDeServicoAgendamento;
            atendimentosToolStripMenuItem.Visible = usuario.UtilizaOrcamentoDeServicoAtendimentos;
            relatóriosToolStripMenuItem6.Visible = usuario.UtilizaOrcamentoDeServicoRelatorios;
            // Orçamento de Serviço / Ícones
            tsbOrcamentoServico.Visible = usuario.UtilizaOrcamentoDeServico;

            // Segurança
            segurançaToolStripMenuItem.Visible = usuario.UtilizaSeguranca;
            cadastroUsuárioToolStripMenuItem.Visible = usuario.UtilizaSegurancaCadastroUsuario;
            trocaUsuárioToolStripMenuItem.Visible = usuario.UtilizaSegurancaTrocaUsuario;

            // Contratos
            contratosToolStripMenuItem21.Visible = usuario.UtilizaContratos;
            contratosToolStripMenuItem1.Visible = usuario.UtilizaContratosContratos;
            produtoToolStripMenuItem.Visible = usuario.UtilizaContratosProdutos;
            serviçosToolStripMenuItem2.Visible = usuario.UtilizaContratosServicos;
            pesquisarContratosToolStripMenuItem.Visible = usuario.UtilizaContratosPesquisarContratos;
            relatóriosToolStripMenuItem10.Visible = usuario.UtilizaContratosRelatorios;

            // Dashboard
            toolStripMenuDashBoard.Visible = usuario.UtilizaDashboard;
            toolStripMenuItem29.Visible = usuario.UtilizaDashboardGeral;
            // Dashboard / Ícones
            toolStripButton3.Visible = usuario.UtilizaDashboard;

            // Telefonia
            var verificador = ConfiguracaoController.Instancia.GetConfiguracao();
            if (verificador.UtilizaTelefonia != 1)
                telefoniaToolStripMenuItem.Visible = false;
            else
                telefoniaToolStripMenuItem.Visible = true;
        }

        private void estoqueToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormConsultaEstoquePorProduto form = new FormConsultaEstoquePorProduto(null);
            ShowForm(form);
        }

        private void aBCToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFiltroABC form = new FormFiltroABC();
            ShowForm(form);
        }

        private void balançoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridBalancoGen grid = new GridBalancoGen(BalancoController.Instancia.GetAll(), new FormBalanco(), false);
            ShowForm(grid);
        }

        private void acompanhamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGridProjetoHistorico form = new FormGridProjetoHistorico();
            ShowForm(form);
        }

        private void toolStripMenuItem4_Click(object sender, EventArgs e)
        {
            FormFechamentoRequisicao form = new FormFechamentoRequisicao();
            ShowForm(form);
        }

        private void movimentoPorPeriodoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            CMovimentosPorPeriodo form = new CMovimentosPorPeriodo(MO.TipoDocumentoType.Pagar, false);
            ShowForm(form);
        }

        private void movimentoPorPeriodoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CMovimentosPorPeriodo form = new CMovimentosPorPeriodo(MO.TipoDocumentoType.Receber, false);
            ShowForm(form);
        }

        private void requisicaoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Requisicao form = new Requisicao();
            ShowForm(form);
        }

        private void pedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Pedido form = new Pedido();
            ShowForm(form);
        }

        #region PCP

        private void fichaTécnicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridFichaTecnica grid = new GridFichaTecnica("Tabela de Ficha Técnica");
            ShowForm(grid);
        }

        private void ordemDeProduçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridOrdemProducaoFicha grid = new GridOrdemProducaoFicha("Tabela de Ordem de Produção");
            ShowForm(grid);
        }

        private void classificaçãoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GridGenerica<ClassificacaoOrdemProducao> grid = new GridGenerica<ClassificacaoOrdemProducao>(ClassificacaoOrdemProducaoController.Instancia.GetAll(), false, typeof(FormClassificacaoOrdemProducao));
            ShowForm(grid);
        }

        #endregion

        #region Configuração

        private void ConfiguraModuloOrcamentoVenda()
        {
            int m = ConfiguracaoLocalController.GetModuloOrcamentoVenda();
            if (m == 0)
            {
                orcamentoVenda.Visible = true;
                orcamentoVenda.Enabled = true;
                projetosToolStripMenuItem.Visible = false;
                projetosToolStripMenuItem.Enabled = false;
            }
            else
            {
                projetosToolStripMenuItem.Visible = true;
                projetosToolStripMenuItem.Enabled = true;
            }
        }

        private void ConfiguraModuloProducao()
        {
            if (produçãoToolStripMenuItem.Visible || producaoSymaToolStripMenuItem.Visible)
            {
                if (objConfiguracao.UtilizarFichaTecnica)
                {
                    produçãoToolStripMenuItem.Visible = false;
                    producaoSymaToolStripMenuItem.Visible = true;
                }
                else
                {
                    produçãoToolStripMenuItem.Visible = true;
                    producaoSymaToolStripMenuItem.Visible = false;
                }
            }
        }

        private void DefinirConfiguracaoMenu()
        {
            try
            {
                db.Refresh(RefreshMode.OverwriteCurrentValues, objConfiguracao);
                ConfiguraModuloOrcamentoVenda();
                ConfiguraModuloProducao();
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        #endregion

        private void estoqueToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ProdutosEstoque form = new ProdutosEstoque();
            ShowForm(form);
        }

        private void estoqueCustoToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            ProdutosEstoqueCusto form = new ProdutosEstoqueCusto();
            ShowForm(form);
        }

        private void centroCustosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<Projeto> form = new GridGenerica<Projeto>(ProjetoController.Instancia.GetCentrosDeCusto(), false, typeof(FormCentroCusto));
            ShowForm(form);
        }

        private void relatórioToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OrdemProducaoFiltro form = new OrdemProducaoFiltro();
            ShowForm(form);
        }

        private void orçamentoCanceladoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridProjeto grid = new GridProjeto(null, "Tabela de Orçamento Cancelado", false, 0, 2);
            ShowForm(grid);
        }

        private void projetosConcluídosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridProjeto grid = new GridProjeto(null, "Tabela de Projetos Concluído", false, 0, 3);
            ShowForm(grid);
        }

        private void atualizaçãoDeNotaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAtualizacaoNota form = new FormAtualizacaoNota();
            ShowForm(form);
        }

        private void comprasCanceladasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = GridNotaGen.Get(InOutType.Entrada, ProdutoServicoType.Produtos, true);
            ShowForm(grid);
        }

        private void toolStripButton5_Click(object sender, EventArgs e)
        {
            int m = ConfiguracaoLocalController.GetModuloOrcamentoVenda();
            if (m == 0)
                orçamentosToolStripMenuItem_Click(sender, e);
            else
                cadastroToolStripMenuItem1_Click(sender, e);
        }

        private void abertasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridOrdemProducao grid = new GridOrdemProducao();
            grid.Text = "Tabela de Ordem de Produção - Aberta";
            grid.Status = MO.StatusOPType.Abertas;
            ShowForm(grid);
        }

        private void iniciadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridOrdemProducao grid = new GridOrdemProducao();
            grid.Text = "Tabela de Ordem de Produção - Iniciada";
            grid.Status = MO.StatusOPType.Iniciadas;
            ShowForm(grid);
        }

        private void canceladasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridOrdemProducao grid = new GridOrdemProducao();
            grid.Text = "Tabela de Ordem de Produção - Cancelada";
            grid.Status = MO.StatusOPType.Canceladas;
            ShowForm(grid);
        }

        private void fechadasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridOrdemProducao grid = new GridOrdemProducao();
            grid.Text = "Tabela de Ordem de Produção - Fechada";
            grid.Status = MO.StatusOPType.Fechadas;
            ShowForm(grid);
        }

        private void tipoOrçamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrcamentoVendaPorVend form = new OrcamentoVendaPorVend(2);
            ShowForm(form);
        }

        private void dataToolStripMenuItem4_Click(object sender, EventArgs e)
        {
            OrcamentoVendaPorVend form = new OrcamentoVendaPorVend(4);
            ShowForm(form);
        }

        private void vendedorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OrcamentoVendaPorVend form = new OrcamentoVendaPorVend(3);
            ShowForm(form);
        }

        private void tipoPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrcamentoVendaPorVend form = new OrcamentoVendaPorVend(5);
            ShowForm(form);
        }

        private void tipoAtendimentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<TipoAtendimento> form = new GridGenerica<TipoAtendimento>(TipoAtendimentoController.Instancia.GetAll(), false, typeof(FormTipoAtendimento));
            ShowForm(form);
        }

        private void statusOrdemServiçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<StatusOrdemServico> form = new GridGenerica<StatusOrdemServico>(StatusOrdemServicoController.Instancia.GetAll(), false, typeof(FormStatusOrdemServico));
            ShowForm(form);
        }

        private void sistemaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<Modelo.Sistema> form = new GridGenerica<Modelo.Sistema>(SistemaController.Instancia.GetAll(), false, typeof(FormSistema));
            ShowForm(form);
        }

        private void ordemServiçoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FormGridOrdemServico form = new FormGridOrdemServico("Tabela de Ordem de Serviço");
            ShowForm(form);
        }

        private void pedidoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GridCwkPedido form = new GridCwkPedido("Tabela de Orçamento");
            ShowForm(form);
        }

        private void envioXMLsDestinatarioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormManutXMLNFe form = new FormManutXMLNFe(null, null);
            ShowForm(form);
        }

        private void notasCanceladasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormCustoPorNota form = new FormCustoPorNota();
            ShowForm(form);
        }

        private void notasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Notas form = new Notas();
            ShowForm(form);
        }

        private void cFOPToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            NotasPorCFOP form = new NotasPorCFOP(1);
            ShowForm(form);
        }

        private void cFOPResumidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NotasPorCFOP form = new NotasPorCFOP(0);
            ShowForm(form);
        }

        private void formasDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<FormaPagamento> form = new GridGenerica<FormaPagamento>(FormaPagamentoController.Instancia.GetAll(), false, typeof(FormFormaPagamentoH));
            ShowForm(form);
        }

        private void relatórioDeFaturamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFaturamentoRevendas form = new FormFaturamentoRevendas();
            ShowForm(form);
        }

        private void paísToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GridGenerica<Pais> grid = new GridGenerica<Pais>(PaisController.Instancia.GetAll(), false, typeof(FormPais));
            ShowForm(grid);
        }

        private void uFToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GridGenerica<UF> grid = new GridGenerica<UF>(UFController.Instancia.GetAll(), false, typeof(FormUF));
            ShowForm(grid, grid.RegistrosCount);
        }

        private void cidadeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GridGenerica<Cidade> grid = new GridGenerica<Cidade>(CidadeController.Instancia.GetAll(), false, typeof(FormCidade));
            ShowForm(grid);
        }

        private void grupoEstoqueToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GridGenerica<GrupoEstoque> grid = new GridGenerica<GrupoEstoque>(GrupoEstoqueController.Instancia.GetAll(), false, typeof(FormGrupoEstoque));
            ShowForm(grid);
        }

        private void unidadeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GridGenerica<Unidade> grid = new GridGenerica<Unidade>(UnidadeController.Instancia.GetAll(), false, typeof(FormUnidade));
            ShowForm(grid);
        }

        private void tabelaDePreçosToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GridGenerica<TabelaPreco> grid = new GridGenerica<TabelaPreco>(TabelaPrecoController.Instancia.GetAll(), false, typeof(FormTabelaPreco));
            ShowForm(grid);
        }

        private void condicaoDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<Condicao> grid = new GridGenerica<Condicao>(CondicaoController.Instancia.GetAll(), false, typeof(FormCondicao));
            ShowForm(grid);
        }

        private void feriadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                new GridGenerica<Feriado>(FeriadoController.Instancia.GetAll(), false, typeof(FormFeriado)).ShowDialog();
            }
            catch (Exception ex)
            {
                FormErro.ShowDialog(ex);
            }
        }

        private void classificaçãoOrdemDeProduçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<ClassificacaoOrdemProducao> grid = new GridGenerica<ClassificacaoOrdemProducao>(ClassificacaoOrdemProducaoController.Instancia.GetAll(), false, typeof(FormClassificacaoOrdemProducao));
            ShowForm(grid);
        }

        private void toolStripButton9_Click(object sender, EventArgs e)
        {
            FormBaixarOrcamentos grid = new FormBaixarOrcamentos();
            ShowForm(grid);
        }

        private void tipoDeDocumentoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GridGenerica<TipoDocumento> grid = new GridGenerica<TipoDocumento>(TipoDocumentoController.Instancia.GetAll(), false, typeof(FormTipoDocumento));
            ShowForm(grid);
        }

        private void ordemProduçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrdemProducaoFiltro form = new OrdemProducaoFiltro();
            ShowForm(form);
        }

        private void fluxoDeCaixaDetalhadoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MovimentacaoPorPlanoConta form = new MovimentacaoPorPlanoConta(false);
            ShowForm(form);
        }

        private void tipoDePedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<TipoPedido> grid = new GridGenerica<TipoPedido>(TipoPedidoController.Instancia.GetAll(), false, typeof(FormTipoPedido));
            ShowForm(grid);
        }

        private void planoDeContasToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            new GridGenerica<PlanoConta>(PlanoContaController.Instancia.GetAll(), false, typeof(FormPlanoConta)).ShowDialog();
        }

        private void serviçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<Servico> grid = new GridGenerica<Servico>(ServicoController.Instancia.GetAll(), false, typeof(FormServico));
            ShowForm(grid);
        }

        private void classificaçãoToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            GridGenerica<Classificacao> grid = new GridGenerica<Classificacao>(ClassificacaoController.Instancia.GetAll(), false, typeof(FormClassificacao));
            ShowForm(grid);
        }

        private void históricoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GridGenerica<Historico> grid = new GridGenerica<Historico>(HistoricoController.Instancia.GetAll(), false, typeof(FormHistorico));
            ShowForm(grid);
        }

        private void bancoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GridGenerica<Banco> grid = new GridGenerica<Banco>(BancoController.Instancia.GetAll(), false, typeof(FormBanco));
            ShowForm(grid);
        }

        private void portadorToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            GridGenerica<Portador> grid = new GridGenerica<Portador>(PortadorController.Instancia.GetAll(), false, typeof(FormPortador));
            ShowForm(grid);
        }

        private void arquivoRetornoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRetorno form = new FormRetorno();
            ShowForm(form);
        }

        private void ordemDeProduçãoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            OrdemProducaoFiltro grid = new OrdemProducaoFiltro();
            ShowForm(grid);
        }

        private void atualizaProdutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAtualizarProdutos form = new FormAtualizarProdutos();
            ShowForm(form);
        }

        private void atualizarProdutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAtualizarProdutos form = new FormAtualizarProdutos();
            ShowForm(form);
        }

        private void contasAPagarPorPortadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CPagarPorTipoDocumento form = new CPagarPorTipoDocumento(1);
            ShowForm(form);
        }

        private void contasAReceberPorPortadorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CReceberPorTipoDocumento form = new CReceberPorTipoDocumento(1);
            ShowForm(form);
        }

        private void portadorToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            CPagarPorTipoDocumento form = new CPagarPorTipoDocumento(2);
            ShowForm(form);
        }

        private void portadorToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CReceberPorTipoDocumento form = new CReceberPorTipoDocumento(2);
            ShowForm(form);
        }

        private void feriadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<Feriado> form = new GridGenerica<Feriado>(FeriadoController.Instancia.GetAll(), false, typeof(FormFeriado));
            ShowForm(form);
        }

        private void baixarPorArquivoDeRetornoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRetorno form = new FormRetorno();
            ShowForm(form);
        }

        private void notasDeServiçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridNotaServico grid = new GridNotaServico(NotaServicoController.Instancia.GetAll(), typeof(FormNotaServico));
            ShowForm(grid);
        }

        private void faturamentoEmLoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormBaixarOrcamentos grid = new FormBaixarOrcamentos();
            ShowForm(grid);
        }

        private void movimentoPorPeríodoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MovimentoPorPeriodoPorProduto form = new MovimentoPorPeriodoPorProduto();
            ShowForm(form);
        }

        private void equipamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<OSEquipamento> grid = new GridGenerica<OSEquipamento>(OSEquipamentoController.Instancia.GetAll(), false, typeof(FormOSEquipamento));
            ShowForm(grid);
        }

        private void tipoEquipamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<OSTipoEquipamento> grid = new GridGenerica<OSTipoEquipamento>(OSTipoEquipamentoController.Instancia.GetAll(), false, typeof(FormOSTipoEquipamento));
            ShowForm(grid);
        }

        private void ordemServiçoToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            GridOSOrdemServicoGen grid = new GridOSOrdemServicoGen(OSOrdemServicoController.Instancia.GetAllByPeriod(DateTime.Now.AddYears(-1), DateTime.Now.AddYears(1), null), true, typeof(FormOSOrdemServico));

            ShowForm(grid);
        }

        private void equipamentoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var form = new Aplicacao.Relatorios.OSEquipamento();
            ShowForm(form);
        }

        private void tipoEquipamentoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var form = new Aplicacao.Relatorios.OSTipoEquipamento();
            ShowForm(form);
        }

        private void ordemDeServiçoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var form = new OrdemDeServico();
            ShowForm(form);
        }

        private void previsãoFluxoDeCaixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FluxoDeCaixa();
            ShowForm(form);
        }

        private void clientePorVendedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ClientePorVendedor form = new ClientePorVendedor();
            ShowForm(form);
        }

        private void toolSubProduto_Click(object sender, EventArgs e)
        {
            try
            {
                GridProdutoGen grid = new GridProdutoGen(ProdutoController.Instancia.GetAll().Where(prod => prod.GrupoEstoque.ClassificacaoProduto == ClassificacaoProduto.Subproduto).OrderBy(prod => prod.NomeOrdenado).ToList(), false,
                    typeof(FormProduto), null, ClassificacaoProduto.Subproduto);
                grid.Text = "Tabela de Subproduto";
                ShowForm(grid);
            }
            catch (Exception)
            {
                MessageBox.Show("Não à nenhum produto classificado como SubProduto!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tsMateriaPrima_Click(object sender, EventArgs e)
        {
            try
            {
                GridProdutoGen grid = new GridProdutoGen(ProdutoController.Instancia.GetAll().Where(prod => prod.GrupoEstoque.ClassificacaoProduto == ClassificacaoProduto.MateriaPrima).OrderBy(prod => prod.NomeOrdenado).ToList(), false,
                    typeof(FormProduto), null, ClassificacaoProduto.MateriaPrima);
                grid.Text = "Tabela de Materia Prima";
                ShowForm(grid);
            }
            catch (Exception)
            {
                MessageBox.Show("Não à nenhum produto classificado como Matéria Prima!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void tsImobilizado_Click(object sender, EventArgs e)
        {
            try
            {
                GridProdutoGen grid = new GridProdutoGen(ProdutoController.Instancia.GetAll().Where(prod => prod.GrupoEstoque.ClassificacaoProduto == ClassificacaoProduto.Imobilizado).OrderBy(prod => prod.NomeOrdenado).ToList(), false,
                    typeof(FormProduto), null, ClassificacaoProduto.Imobilizado);
                grid.Text += "Tabela de Materia Imobilizado";
                ShowForm(grid);
            }
            catch (Exception)
            {
                MessageBox.Show("Não à nenhum produto classificado como Imobilizado!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Warning);
            }
        }

        private void atualizarCSTNCMCSOSNToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAtualizarProdutos form = new FormAtualizarProdutos();
            ShowForm(form);
        }

        private void alteraçãoDePreçoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormAlteracaoPreco form = new FormAlteracaoPreco();
            ShowForm(form);
        }

        private void toolStripMenuItem11_Click(object sender, EventArgs e)
        {
            GridGenerica<GrupoEstoque> grid = new GridGenerica<GrupoEstoque>(GrupoEstoqueController.Instancia.GetAll(), false, typeof(FormGrupoEstoque));
            ShowForm(grid);
        }

        private void toolStripMenuItem12_Click(object sender, EventArgs e)
        {
            GridGenerica<Unidade> grid = new GridGenerica<Unidade>(UnidadeController.Instancia.GetAll(), false, typeof(FormUnidade));
            ShowForm(grid);
        }

        private void toolStripMenuItem10_Click(object sender, EventArgs e)
        {
            GridGenerica<TabelaPreco> grid = new GridGenerica<TabelaPreco>(TabelaPrecoController.Instancia.GetAll(), false, typeof(FormTabelaPreco));
            ShowForm(grid);
        }

        private void toolStripMenuItem6_Click(object sender, EventArgs e)
        {
            GridProdutoGen grid = new GridProdutoGen(ProdutoController.Instancia.GetAll().OrderBy(prod => prod.NomeOrdenado).ToList(), false, typeof(FormProduto));
            ShowForm(grid);
        }

        private void danfePDFToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormExportacaoNfe form = new FormExportacaoNfe();
            if (Facade.ControleAcesso(form)) form.ShowDialog();
        }

        private void validaPRToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FormExportacaoReceita form = new FormExportacaoReceita();
            if (Facade.ControleAcesso(form)) form.ShowDialog();
        }

        private void toolStripMenuItem7_Click(object sender, EventArgs e)
        {
            GridGenerica<Grupo1> grid = new GridGenerica<Grupo1>(Grupo1Controller.Instancia.GetAll(), false, typeof(FormGrupo1));
            grid.Text = "Tabela de " + (objConfiguracao.NomeGrupo1 ?? "Grupo1");
            ShowForm(grid);
        }

        private void toolStripMenuItem8_Click(object sender, EventArgs e)
        {
            GridGenerica<Grupo2> grid = new GridGenerica<Grupo2>(Grupo2Controller.Instancia.GetAll(), false, typeof(FormGrupo2));
            grid.Text = "Tabela de " + (objConfiguracao.NomeGrupo2 ?? "Grupo2");
            ShowForm(grid);
        }

        private void toolStripMenuItem9_Click(object sender, EventArgs e)
        {
            GridGenerica<Grupo3> grid = new GridGenerica<Grupo3>(Grupo3Controller.Instancia.GetAll(), false, typeof(FormGrupo3));
            grid.Text = "Tabela de " + (objConfiguracao.NomeGrupo3 ?? "Grupo3");
            ShowForm(grid);
        }

        private void transferênciaBancáriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTransferenciaBanco form = new FormTransferenciaBanco();
            ShowForm(form);
        }

        private void toolStripMenuItem7_Click_1(object sender, EventArgs e)
        {
            GridGenerica<Filial> grid = new GridGenerica<Filial>(FilialController.Instancia.GetAll(), false, typeof(FormFilial));
            ShowForm(grid);
        }

        private void impostoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<ServicoImposto> grid = new GridGenerica<ServicoImposto>(ServicoImpostoController.Instancia.GetAll(), false, typeof(FormServicoImposto));
            ShowForm(grid);
        }

        private void serviçoToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FormRelImpostosPorServico form = new FormRelImpostosPorServico();
            ShowForm(form);
        }

        private void impostoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            FormRelServicoImposto form = new FormRelServicoImposto();
            ShowForm(form);
        }

        private void notasDeServiçoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var grid = GridNotaGen.Get(InOutType.Saída, ProdutoServicoType.Servicos, false);
            ShowForm(grid);
        }

        private void fechamentoFaturaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormFaturamentoOSLote form = new FormFaturamentoOSLote();
            ShowForm(form);
        }

        private void apontamentoHoraServiçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormApontamentoHoraServico form = new FormApontamentoHoraServico();
            ShowForm(form);
        }

        private void agendamentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormOSAgendamento form = new FormOSAgendamento();
            ShowForm(form);
        }

        private void relatórioDeOrdemServiçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            OrdensDeServicoCwork form = new OrdensDeServicoCwork();
            ShowForm(form);
        }

        private void toolStripButton8_Click_1(object sender, EventArgs e)
        {
            FormFaturamentoLoteNova grid = new FormFaturamentoLoteNova();
            ShowForm(grid);
        }

        private void apontamentoDeHoraServiçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new ApontamentoDeHoras();
            ShowForm(form);
        }

        private void toolStripProgressBar1_Click_1(object sender, EventArgs e)
        {
            var form = new FormHistoricoVenda(null, null, InOutType.Saída, FormHistoricoVenda.FiltroPor.Produto);
            ShowForm(form);
        }

        private void enviarArquivoLicençaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Facade.ChamaEnviaArquivoLicenca();
        }

        private void nCMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var ListaNCM = NCMController.Instancia.GetAll();

            var lQuery = ListaNCM.Where(o => o.DtRevogacao.HasValue);
            if (lQuery.Any())
            {
                ListaNCM = lQuery.ToList();
                var grid = new GridGenerica<NCM>(ListaNCM, false, typeof(FormNCM));
                ShowForm(grid);
            }
            else
            {
                MessageBox.Show("Nenhum NCM revogado encontrado.", "Mensagem");
                return;
            }
        }

        private void notaComplementarDeSaidaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridNotaComplementarGen grid = GridNotaComplementarGen.Get(InOutType.Saída);
            ShowForm(grid);
        }

        private void notaComplementarDeEntradaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GridNotaComplementarGen grid = GridNotaComplementarGen.Get(InOutType.Entrada);
            ShowForm(grid);
        }

        private void requisiçãoEntradaNHibernateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = GridRequisicao.Get(InOutType.Entrada);
            ShowForm(grid);
        }

        private void requisiçãoDeSaídaNHibernateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = GridRequisicao.Get(InOutType.Saída);
            ShowForm(grid);
        }

        private void requisiçãoBaixadaEntradaNHibernateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = GridRequisicaoBaixada.Get(InOutType.Entrada);
            ShowForm(grid);
        }

        private void requisiçãoBaixadaSaídaNHibernateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = GridRequisicaoBaixada.Get(InOutType.Saída);
            ShowForm(grid);
        }

        private void gerarBoletoNHibernateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = new FormImprimeBoletosRemessa();
            grid.Operacao = Acao.Incluir;
            ShowForm(grid);
        }

        private void doctoReceberNHibernateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = GridDocumentoGen.Get(null, TipoDocumentoType.Receber, false);
            ShowForm(grid);
        }

        private void doctoPagarNHibernateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = GridDocumentoGen.Get(null, TipoDocumentoType.Pagar, false);
            ShowForm(grid);
        }

        private void tsConsultaEstoquePorFilial_Click(object sender, EventArgs e)
        {
            var grid = new FormConsultaEstoquePorFilial();
            ShowForm(grid);
        }

        private void movimentoCaixaPorFormaDePagamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new MovCaixaPorFormaDePagamento();
            ShowForm(form);
        }

        private void chequeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = new GridCheque(ChequeController.Instancia.GetAllByPeriod(DateTime.Now.AddMonths(-3), DateTime.Now, null), true, typeof(FormCheque));
            ShowForm(grid);
        }

        private void toolStripEtiquetaProduto_Click(object sender, EventArgs e)
        {
            var grid = new FormImprimeEtiquetasProdutos();
            ShowForm(grid);
        }

        private void toolStripEtiquetaPessoa_Click(object sender, EventArgs e)
        {
            var grid = new FormImprimeEtiquetasPessoas();
            ShowForm(grid);
        }

        private void toolStripTransportadora_Click(object sender, EventArgs e)
        {
            GridCliente grid = new GridCliente(PessoaController.Instancia.GetAllTransportadoras(), null, "Transportadora", false, typeof(FormPessoa));
            ShowForm(grid);
        }

        private bool AbrirCaixa()
        {
            var FormAbertura = new FormFrenteCaixaAbrirCaixa();
            FormAbertura.ShowDialog();
            return FormAbertura.AbriuCaixa;
        }

        private bool VerificaAberturaCaixa()
        {
            FluxoCaixa FluxoCorrente = FluxoCaixaController.Instancia.GetFluxoDeCaixaPorUsuario(MO.cwkGlobal.objUsuarioLogado.Id);
            if (FluxoCorrente == null)
            {
                if (AbrirCaixa())
                {
                    FluxoCorrente = FluxoCaixaController.Instancia.GetFluxoDeCaixaPorUsuario(MO.cwkGlobal.objUsuarioLogado.Id);
                    return true;
                }

                return false;
            }

            return true;
        }

        private void toolStripButton8_Click(object sender, EventArgs e)
        {
            if (ConfiguracaoController.Instancia.GetConfiguracao().SenhaAoEntrarNoPDV == 1)
            {
                var chamarTelaLogin = Facade.ChamaAutenticacao(Sistema.Gestão, versao, false);
                EscondeModulos();
                AtualizaDadosUsuario(false);
                if (chamarTelaLogin)
                    AbrirPdv();
            }
            else
            {
                AbrirPdv();
            }
        }

        private void AbrirPdv()
        {
            try
            {
                if (!VerificaAberturaCaixa())
                    return;

                //new FormFrenteCaixaNotaNova().ShowDialog(this);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void magentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormIntegracaoMagento();
            ShowForm(form);
        }

        private void importaçãoLigaçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormImportacaoLigacoes();
            ShowForm(form);
        }

        private void arquivosLiagaçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new GridArquivoLigacao(ArquivoLigacaoController.Instancia.GetAll(), new FormArquivoLigacao(null), null, false);
            ShowForm(form);
        }

        private void geraçãoFaturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormGeracaoFaturas();
            ShowForm(form);
        }

        private void fechamentoFaturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormFechamentoFaturas();
            ShowForm(form);
        }

        private void desfazerFechamentoFaturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormDesfazerFechamentoFaturas();
            ShowForm(form);
        }

        private void exclusãoLoteFaturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormExcluirLoteFaturas();
            ShowForm(form);
        }

        private void faturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new GridFatura(Tel_FaturaController.Instancia.GetAllGridFaturas(), null, false, typeof(FormFatura));
            ShowForm(form);
        }

        private void tiposDeLigaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<Tel_TipoLigacao> grid = new GridGenerica<Tel_TipoLigacao>(Tel_TipoLigacaoController.Instancia.GetAll(), false, typeof(FormTel_TipoLigacao));
            ShowForm(grid);
        }

        private void toolStripProgressBar1_Click(object sender, EventArgs e)
        {
            var form = new FormImpressaoFaturas();
            ShowForm(form);
        }

        private void configuraçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CarregarTel_Configuracao();
        }

        private static void CarregarTel_Configuracao()
        {
            var form = new FormTel_Configuracao();

            try
            {
                form.Selecionado = Tel_ConfiguracaoController.Instancia.GetConfiguracao();
                form.Operacao = Acao.Alterar;
            }
            catch
            {
                form.Selecionado = new Tel_Configuracao();
                form.Operacao = Acao.Incluir;
            }

            form.Show();
        }

        private void serviçosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            GridGenerica<Tel_Servico> grid = new GridGenerica<Tel_Servico>(Tel_ServicoController.Instancia.GetAll(), false, typeof(FormTel_Servico));
            ShowForm(grid);
        }

        private void clientePedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CReceberPorCliente form = new CReceberPorCliente(true);
            ShowForm(form);
        }

        private void movimentoPorPeríodoPedidoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CMovimentosPorPeriodo form = new CMovimentosPorPeriodo(MO.TipoDocumentoType.Receber, true);
            ShowForm(form);
        }

        private void pesquisaFaturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new GridPesquisaFaturas();
            ShowForm(form);
        }

        private void impressãoFaturasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormImpressaoFaturas();
            ShowForm(form);
        }

        private void aberturaDoDiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CupomFiscal cupomFiscal = new CupomFiscal(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE"));

            if (Convert.ToInt32(XDocument.Load("ConfiguracaoCwork.xml").Element("Cwork").Element("ImpressoraFiscal").Value) == 0)
                MessageBox.Show("Nao é possivel realizar a abertura do dia, pois não foi encontrada uma impressora fiscal em sua máquina.", "Redução Z", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (MessageBox.Show("Deseja realizar a abertura do dia?", "Cupom Fiscal", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                try
                {
                    cupomFiscal.Bematech_FI_AberturaDoDia();
                }
                catch (Exception ex)
                {
                    new FormErro(new Exception("Verifique a conexão com a impressora e o diretório da aplicação, conferindo se o arquivo 'BemaFI32.dll' existe.", ex)).ShowDialog();
                }
        }

        private void fechamentoDoDiaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CupomFiscal cupomFiscal = new CupomFiscal(Environment.GetEnvironmentVariable("PROCESSOR_ARCHITECTURE"));

            if (Convert.ToInt32(XDocument.Load("ConfiguracaoCwork.xml").Element("Cwork").Element("ImpressoraFiscal").Value) == 0)
                MessageBox.Show("Nao é possivel realizar o fechamento do dia, pois não foi encontrada uma impressora fiscal em sua máquina.", "Redução Z", MessageBoxButtons.OK, MessageBoxIcon.Error);
            else if (MessageBox.Show("Deseja realizar o fechamento do dia?", "Cupom Fiscal", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                try
                {
                    cupomFiscal.Bematech_FI_FechamentoDoDia();

                    var form = new CwkAviso("Por favor aguarde enquanto a redução Z é processada.");
                    ValidaPRTipo60 VPRT60 = new ValidaPRTipo60(form);
                    Thread t = new Thread(VPRT60.ReducaoZ);
                    t.Start();
                    form.ShowDialog();
                }
                catch (Exception ex)
                {
                    new FormErro(new Exception("Verifique a conexão com a impressora e o diretório da aplicação, conferindo se o arquivo 'BemaFI32.dll' existe.", ex)).ShowDialog();
                }
        }

        private void clientesToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var grid = new GridGenerica<Tel_Cliente>(Tel_ClienteController.Instancia.GetAll(), new FormTel_Cliente(), false);
            ShowForm(grid);
        }

        private void históricoFaturasClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new HistoricoFaturasCliente();
            ShowForm(form);
        }

        private void inadimplênciaClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new InadimplenciaTel_Clientes();
            ShowForm(form);
        }

        private void engenheiroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = new GridGenerica<PessoaEngenheiro>(PessoaEngenheiroController.Instancia.GetAll(), false, typeof(FormPessoaEngenheiro));
            ShowForm(grid);
        }

        private void máQuinasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = new GridGenerica<Ofd_Maquinas>(Ofd_MaquinasController.Instancia.GetAll(), false, typeof(FormOfd_Maquinas));
            ShowForm(grid);
        }

        private void obraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = new GridOfd_Obra(Ofd_ObraController.Instancia.GetAll(), new FormOfd_Obra(), false);
            ShowForm(grid);
        }

        private void desativarTelefonesClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormDesativarTelefonesCliente();
            ShowForm(form);
        }

        private void movimentoPorPeríodoECentroDeCustoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new CReceberPorCentroCusto();
            ShowForm(form);
        }

        private void movimentoPorPeríodoECentroDeCustoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var form = new CPagarPorCentroCusto();
            ShowForm(form);
        }

        private void grupoClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new GridGrupoCliente(Tel_GrupoClienteController.Instancia.GetAll(), typeof(FormGrupoCliente));
            ShowForm(form);
        }

        private void faturamentoPropostaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormFaturamentoPropostaMedicao();
            ShowForm(form);
        }

        private void resumoMediçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormImpressaoResumoMedicao();
            ShowForm(form);
        }

        private void verificaçãoEMediçãoDePerfuraçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new VerificacacaoEMedicaoDePerfuracao();
            ShowForm(form);
        }

        private void listagemDePreçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ListagemPreco form = new ListagemPreco();
            ShowForm(form);
        }

        private void fluxoDeCaixaRealizadoPorCompetênciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MovimentacaoPorPlanoConta form = new MovimentacaoPorPlanoConta(true);
            ShowForm(form);
        }

        private void inventárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = new GridInventario(InventarioController.Instancia.GetAll(), false, typeof(FormInventario));
            ShowForm(grid);
        }

        private void acompanhamentoDasMToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAcompanhamentoObras form = new FormAcompanhamentoObras();
            ShowForm(form);
        }

        private void fechamentoDeMediçõesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = new GridGenerica<Ofd_Medicao>(Ofd_MedicaoController.Instancia.GetAll(), false, typeof(FormOfd_Medicao));
            ShowForm(grid);
        }

        private void gerarInventárioToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGerarInventario form = new FormGerarInventario();
            ShowForm(form);
        }

        private void localEstoqueToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = new GridGenerica<LocalEstoque>(LocalEstoqueController.Instancia.GetAll(), false, typeof(FormLocalEstoque));
            ShowForm(grid);
        }

        private void identificadoresToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = new GridGenerica<Identificador>(IdentificadorController.Instancia.GetAll(), false, typeof(FormIdentificador));
            grid.DesabilitarBotoes(GridGenerica<Identificador>.Botao.Incluir);
            ShowForm(grid);
        }

        private void textoLeiToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = new GridGenerica<TextoLei>(TextoLeiController.Instancia.GetAll(), false, typeof(FormTextoLei));
            ShowForm(grid);
        }

        private void faturarContratosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridContrato grid = new GridContrato();
            ShowForm(grid);
        }

        private void sincronizaçãoPendenteToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var form = new FormPedidosPendentes();
            ShowForm(form);
        }

        private void configuraçãoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var form = new FormMagentoConfiguracao();

            try
            {
                form.Selecionado = MagentoConfiguracaoController.Instancia.GetConfiguracao();
                form.Operacao = Acao.Alterar;
            }
            catch
            {
                form.Selecionado = new MagentoConfiguracao();
                form.Operacao = Acao.Incluir;
            }

            ShowForm(form);
        }

        private void tipoServiçoTransporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int tamanho = PegaAlturaFormTipoServicoTransporte();
            var grid = new GridGenerica<TipoServicoTransporte>(TipoServicoTransporteController.Instancia.GetAll(), false, typeof(FormTipoServicoTransporte), tamanho);
            ShowForm(grid);
        }

        private void códigoDeRastreamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = GridRastreamentoCorreio.Get();
            ShowForm(grid);
            //var grid = new GridGenerica<RastreamentoCorreio>(RastreamentoCorreioController.Instancia.GetAll(), new FormRastreamentoCorreio());
            //ShowForm(grid);
        }

        private void statusEntregaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = new GridGenerica<StatusEntEventoCorreio>(StatusEntEventoCorreioController.Instancia.GetAll(), false, typeof(FormStatusEntregaCorreio));
            ShowForm(grid);
        }

        private void etiquetaExpediçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = new FormImprimeEtiquetaCorreios();
            ShowForm(grid);
        }

        private void statusRastreamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = new GridRastreamentoStatus();
            ShowForm(grid);
        }

        private void statusMercadoriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormStatusMercadoria("Cliente");
            ShowForm(form);
        }

        private void faturamentoTotalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new CreditoDebitoImpostos();
            ShowForm(form);
        }

        private void statusMercadoriasPorTransportadoraToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormStatusMercadoria("Transportadora");
            ShowForm(form);
        }

        private void importarXmlFornecedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormVinculoInicial();
            ShowForm(form);
        }

        private void importaçãoXMLFornecedorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormVinculoInicial();
            ShowForm(form);
        }

        private void TempoEstoqueMenuItem_Click(object sender, EventArgs e)
        {
            TempoEstoque form = new TempoEstoque();
            ShowForm(form);
        }

        private void ProdutosInativos_Click(object sender, EventArgs e)
        {
            List<Produto> listaDeProdutos = new List<Produto>();
            try
            {
                listaDeProdutos = ProdutoController.Instancia.GetInativos().OrderBy(prod => prod.NomeOrdenado).ToList();
                if (listaDeProdutos.Count == 0)
                {
                    MessageBox.Show("Não existem Produtos Inativos cadastrados no Sistema", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    GridProdutoGen grid = new GridProdutoGen(listaDeProdutos, false, typeof(FormProduto));
                    grid.Text = "Tabela de Produtos Inativos";
                    ShowForm(grid);
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void configuraçãoManagerToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var obj = ConfiguracaoManagerController.Instancia.GetAll();

            FormConfiguracaoManager form = new FormConfiguracaoManager(obj.FirstOrDefault());
            form.ShowDialog();
        }

        private void contratosToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var grid = new GridGenerica<Contrato>(ContratoController.Instancia.GetAll(), false, typeof(FormContrato));
            ShowForm(grid);
        }

        private void planoDeProduçãoToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void impressãoDeFichaTécnicaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormImpressaoFichaTecnica();
            ShowForm(form);
        }

        private void aprovacaoPedidoToolStripMenuItem21_Click(object sender, EventArgs e)
        {
            var form = new GridAprovacaoPedido(PedidoController.Instancia.GetPedidosParaAprovar());
            ShowForm(form);
        }

        private void demonstrativoResultadoDREToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new DRE();
            ShowForm(form);
        }

        private void arquivoDeRemessaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormArquivoRemessa();
            ShowForm(form);
        }

        private void envioDeFaturasEmLoteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new GridEnvioFaturaEmLote();
            ShowForm(form);
        }

        private void configuraçãoToolStripMenuItem2_Click(object sender, EventArgs e)
        {
            var form = new FormConfiguracaoMercadoLivre();
            ShowForm(form);
        }

        private void tipoDeServiçoDeTransporteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            int tamanhoForm = PegaAlturaFormTipoServicoTransporte();
            var grid = new GridGenerica<TipoServicoTransporte>(TipoServicoTransporteController.Instancia.GetAll(), false, typeof(FormTipoServicoTransporte), tamanhoForm);
            ShowForm(grid);
        }

        private static int PegaAlturaFormTipoServicoTransporte()
        {
            FormTipoServicoTransporte form = new FormTipoServicoTransporte();
            int retornoTamanho = form.Height;
            return retornoTamanho;
        }

        private void préListaDePostagemToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridPLP gridPLP = new GridPLP();
            gridPLP.Text = "Tabela de Pré Lista de Postagem";
            ShowForm(gridPLP);
        }

        private void logsDeRetornoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new GridLogRetorno();
            ShowForm(form);
        }

        private void testeToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var form = new FormSincronizacoesPendentesMercadoLivre();
            ShowForm(form);
        }

        private void fechamentoDeCaixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormFechamentoCaixa();
            ShowForm(form);
        }

        private void toolStripMenuItem21_Click(object sender, EventArgs e)
        {
            var form = new FormRptImpostoNFSe();
            ShowForm(form);
        }

        private void ligaçõesPorGrupoDeClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new LigacoesPorGrupoDeclientes();
            form.ShowDialog();
        }

        private void toolStripMenuItemEtiquetaProdutoPRN_Click(object sender, EventArgs e)
        {
            var grid = new FormImprimeEtiquetasProdutosPRN();
            ShowForm(grid);
        }

        private void configuracaoSisECommerceToolStripMenuItem3_Click(object sender, EventArgs e)
        {
            XElement TagXmlSis = XDocument.Load("ConfiguracaoCwork.xml").Element("Cwork").Element("UtilizaSisEcommerce");
            if (TagXmlSis != null)
            {
                if (Convert.ToInt32(TagXmlSis.Value) == 1)
                {
                    FormSisECommerceConfiguracao form = new FormSisECommerceConfiguracao();
                    ShowForm(form);
                }
                else
                {
                    MessageBox.Show("A funcionalidade de integração com o sistema SIS e-Commerce não está ativada. Para ativar a funcionalidade, contate o suporte",
                        "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("A funcionalidade de integração com o sistema SIS e-Commerce não está ativada. Para ativar a funcionalidade, contate o suporte",
                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void SincronismoPendenteSisECommerceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            XElement TagXmlSis = XDocument.Load("ConfiguracaoCwork.xml").Element("Cwork").Element("UtilizaSisEcommerce");
            if (TagXmlSis != null)
            {
                if (Convert.ToInt32(TagXmlSis.Value) == 1)
                {
                    FormSincronismoPendenteSisECommerce form = new FormSincronismoPendenteSisECommerce();
                    ShowForm(form);
                }
                else
                {
                    MessageBox.Show("A funcionalidade de integração com o sistema SIS e-Commerce não está ativada. Para ativar a funcionalidade, contate o suporte",
                        "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
            }
            else
            {
                MessageBox.Show("A funcionalidade de integração com o sistema SIS e-Commerce não está ativada. Para ativar a funcionalidade, contate o suporte",
                    "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
            }
        }

        private void sincronizaçõesPendentesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormSincronizacoesPendentesSHL grid = new FormSincronizacoesPendentesSHL();
            ShowForm(grid);
        }

        private void necessidadeDeMateriaisToolStripMenuItem_Click(object sender, EventArgs e)
        {
            NecessidadeDeMateriais form = new NecessidadeDeMateriais();
            form.ShowDialog();
        }

        private void toolStripMenuItem22_Click(object sender, EventArgs e)
        {
            NotasPorServico form = new NotasPorServico();
            ShowForm(form);
        }

        private void toolStripMenuItem23_Click(object sender, EventArgs e)
        {
            var form = new GridGenerica<PeriodoApuracaoContabil>(PeriodoApuracaoContabilController.Instancia.GetAll(), false, typeof(FormPeriodoApuracaoContabil));
            ShowForm(form);
        }

        private void ConversaoUnidadeMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<ConversaoUnidade> grid = new GridGenerica<ConversaoUnidade>(ConversaoUnidadeController.Instancia.GetAll(), false, typeof(FormConversaoUnidade));
            ShowForm(grid);
        }

        private void serviçosPorGruposDeClientesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormRptServicoPorGrupoCliente();
            form.ShowDialog();
        }

        private void centroDeCustoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = new GridDocumentoRateio();
            ShowForm(grid);
        }

        private void ocorrênciasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new GridGenerica<Ofd_Ocorrencias>(Ofd_OcorrenciasController.Instancia.GetAll(), false, typeof(FormOfd_Ocorrencias));
            ShowForm(form);
        }

        private void recebimentosPorContasAReceberToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMovimentoRecebimentoPorPeriodo form = new FormMovimentoRecebimentoPorPeriodo();
            ShowForm(form);
        }

        private void agendamentoDeMaquinasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = new GridOfdAgendamentoMaquina();
            ShowForm(grid);
        }

        private void produtividadePorMaquinaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            AcompanhamentoProdutovidadeMaquina form = new AcompanhamentoProdutovidadeMaquina();
            ShowForm(form);
        }

        private void propostaOrcamentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = new GridOfd_Proposta(Ofd_PropostaController.Instancia.GetAll(), false, typeof(FormOfd_Proposta));
            ShowForm(grid);
        }

        private void atendimentosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormAtendimento();
            ShowForm(form);
        }

        private void statusDeAtendimentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = new GridGenerica<StatusAtendimento>(StatusAtendimentoController.Instancia.GetAll(), false, typeof(FormStatusAtendimento));
            ShowForm(grid);
        }

        private void trocasConferenciaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormTrocasParaConferencia form = new FormTrocasParaConferencia();
            ShowForm(form);
        }

        private void controleDeEntregasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormControleEntregas form = new FormControleEntregas();
            ShowForm(form);
        }

        private void contatosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = new GridGenerica<ContatoDetalhe>(null, true, typeof(FormContatoDetalhe));
            grid.sbFuncao11.Visible = true;
            grid.sbFuncao11.Enabled = true;
            grid.sbFuncao11.Image = Resources.ListaAviso;
            grid.sbFuncao11.Text = "Exibir Avisos";
            grid.sbFuncao11.Width = 130;
            grid.sbFuncao11.Click += sbFuncao11_Click;
            ShowForm(grid);
        }

        private void sbFuncao11_Click(object sender, EventArgs e)
        {
            dckpAvisos.Visibility = DockVisibility.Visible;
            dckpAvisos.Parent.Location = new Point(10, 600);
        }

        private void MenuInicial_Shown(object sender, EventArgs e)
        {
            Refresh();
            if (objConfiguracao.UtilizaControleAvisos)
            {
                AcertaTamanhoDock();
                ThreadAviso();
            }
            else
            {
                dckpAvisos.Visibility = DockVisibility.Hidden;
            }

            VerificarCertificado();
            EnvioAutomaticoXml();
        }

        private void VerificarCertificado()
        {
            var MsgCert = new FormMsgVencimentoCertificado();
            if (MsgCert.DeveAbrirTela())
                MsgCert.ShowDialog();
        }

        private void AcertaTamanhoDock()
        {
            dckpAvisos.Visibility = DockVisibility.Visible;
            dckpAvisos.Dock = DockingStyle.Bottom;
        }

        private void MenuInicial_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (m != null)
                if (m.ThreadState == ThreadState.Running)
                    m.Abort();
        }

        private void btAgendamentosAnteriores_Click(object sender, EventArgs e)
        {
            if (avisoAnterior != null)
            {
                List<ContatoDetalhe> listaAnteriores = new List<ContatoDetalhe>();
                listaAnteriores.AddRange(ContatoController.Instancia.LoadObjectById(avisoAnterior.IDContato).ContatoDetalhes);
                FormContatoDetalhe form = new FormContatoDetalhe(listaAnteriores, avisoAnterior.IDContatoDetalhe);
                form.Operacao = Acao.Alterar;
                form.ShowDialog();
                VerificaAberturaComponenteAviso();
                RotinaAtualizaAviso();
            }
            else
            {
                MessageBox.Show("Nenhum aviso carregado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void btAgendamentosProximos_Click(object sender, EventArgs e)
        {
            if (avisoProximo != null)
            {
                List<ContatoDetalhe> listaProximos = new List<ContatoDetalhe>();
                listaProximos.AddRange(ContatoController.Instancia.LoadObjectById(avisoProximo.IDContato).ContatoDetalhes);
                FormContatoDetalhe form = new FormContatoDetalhe(listaProximos, avisoProximo.IDContatoDetalhe);
                form.Operacao = Acao.Alterar;
                form.ShowDialog();
                VerificaAberturaComponenteAviso();
                RotinaAtualizaAviso();
            }
            else
            {
                MessageBox.Show("Nenhum aviso carregado", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
            }
        }

        private void MenuInicial_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Control && e.Alt && e.KeyCode == Keys.V) vendasToolStripMenuItem_Click(sender, e);
        }

        private void dckpAvisos_ClosedPanel(object sender, DockPanelEventArgs e)
        {
            Refresh();
        }

        private void controleDeRotasToolStripMenuItem_Click_1(object sender, EventArgs e)
        {
            FormRota form = new FormRota();
            ShowForm(form);
        }

        private void toolStripMenuItem25_Click(object sender, EventArgs e)
        {
            GridRota grid = new GridRota();
            ShowForm(grid);
        }

        private void tsbOrcamentoServico_Click(object sender, EventArgs e)
        {
            ordemServiçoToolStripMenuItem2_Click(sender, e);
        }

        private void stbNotaCompra_Click(object sender, EventArgs e)
        {
            comprasToolStripMenuItem1_Click(sender, e);
        }

        private void toolStripButton8_Click_2(object sender, EventArgs e)
        {
            toolStripButton8_Click(sender, e);
        }

        private void tsbRenegociacao_Click(object sender, EventArgs e)
        {
            extratoClienteToolStripMenuItem_Click(sender, e);
        }

        private void tsbLancamentoCaixa_Click(object sender, EventArgs e)
        {
            lançamentosToolStripMenuItem_Click(sender, e);
        }

        private void comiisaofinanceiroProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComissaoVendedorFinanceiro grid = new ComissaoVendedorFinanceiro();
            ShowForm(grid);
        }

        private void comissaofaturamentoProdutoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            ComissaoVendedor grid = new ComissaoVendedor();
            ShowForm(grid);
        }

        private void comissaofaturamentoProdutosEServiçosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComissaoVendedorProdutoServico grid = new ComissaoVendedorProdutoServico();
            ShowForm(grid);
        }

        private void comiisaofinanceiroProdutoEServiçoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ComissaoVendedorFinanceiroProdutoServico grid = new ComissaoVendedorFinanceiroProdutoServico();
            ShowForm(grid);
        }

        private void toolStripMenuItem26_Click(object sender, EventArgs e)
        {
            NotasPeriodo grid = new NotasPeriodo();
            ShowForm(grid);
        }

        private void requisiçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CPagarPorFornecedorRequisicao grid = new CPagarPorFornecedorRequisicao();
            ShowForm(grid);
        }

        private void vencimentoRequisiçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            CReceberPorVencimentoRequisicao form = new CReceberPorVencimentoRequisicao(1, MO.TipoDocumentoType.Pagar, "CPagarPorVencimento", "Relatório de Contas a Pagar por Vencimento.");
            ShowForm(form);
        }

        private void gerarFaturasContratoServicosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGeraFaturaContratoServico form = new FormGeraFaturaContratoServico();
            ShowForm(form);
        }

        private void gerarFaturasContratoProdutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormGeraFaturaContratoProduto form = new FormGeraFaturaContratoProduto();
            ShowForm(form);
        }

        private void pesquisarContratosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridPesquisaFaturasContrato grid = new GridPesquisaFaturasContrato();
            ShowForm(grid);
        }

        private void vencimentoRequisiçãoToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            CReceberPorVencimentoRequisicao form = new CReceberPorVencimentoRequisicao(0, MO.TipoDocumentoType.Receber, "CReceberPorVencimento", "Relatório de Contas a Receber por Vencimento.");
            ShowForm(form);
        }

        private void recebimentosEmAtrasoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            RelDocumentoAtraso form = new RelDocumentoAtraso();
            ShowForm(form);
        }

        private void toolStripMenuItem27_Click(object sender, EventArgs e)
        {
            FormCustoPorNota form = new FormCustoPorNota();
            ShowForm(form);
        }

        private void statusOSToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = new GridGenerica<StatusOS>(StatusOSController.Instancia.GetAll(), false, typeof(FormStatusOS));
            ShowForm(grid);
        }

        private void backupToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormBackup();
            form.ShowDialog();
            Ler_BackupConfig_XML();
        }

        private void spedFiscalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var form = new FormGeradorSpedFiscal();
            ShowForm(form);
        }

        private void configuraçãoDeBancoDeDadosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormConfigBancoDeDados form = new FormConfigBancoDeDados();
            ShowForm(form);
            //cwkControleUsuario.Facade.ChamaAutenticacao(LicenceLibrary.Sistema.Gestão, versao, true);
        }

        private void cadastroToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void tstToolStripMenuItem_Click(object sender, EventArgs e) { }

        private void tsbLancamentoCaixa_Click_1(object sender, EventArgs e)
        {
            FormDashBoardDiario win = new FormDashBoardDiario();
            ShowForm(win);
        }

        private void ToolStripMenuDashBoard_Click(object sender, EventArgs e)
        {
            var form = new FormDashBoardDiario();
            // var form = new FormDashBoardComparativoMensal();
            ShowForm(form);
        }

        #region Backup_Automatico

        private Config_Backup ConfigBackup;
        private Task _Iniciar;

        private void TempoControle_Tick(object sender, EventArgs e)
        {
            var HoraAtual = DateTime.Now.TimeOfDay;

            if (HoraAtual.Hours == ConfigBackup?.Horario.Hours && HoraAtual.Minutes == ConfigBackup?.Horario.Minutes
                                                              && HoraAtual.Seconds >= 0 && HoraAtual.Seconds <= 5)
                Iniciar_Task();
        }

        private void Iniciar_Task()
        {
            if (_Iniciar == null || _Iniciar.IsCompleted)
                _Iniciar = Task.Run(Iniciar);
        }

        private void Iniciar()
        {
            try
            {
                DateTime dateTime = DateTime.Now;

                if (ConfigBackup.Diretorio.Count == 0)
                    return;

                foreach (var item in ConfigBackup.Diretorio)
                {
                    string nomeDiretorio = item + "\\" + string.Format("backup{0:0000}.{1:00}.{2:00}-{3:00}.{4:00}", dateTime.Year, dateTime.Month,
                        dateTime.Day, dateTime.Hour, dateTime.Minute);

                    if (!Directory.Exists(nomeDiretorio))
                        Directory.CreateDirectory(nomeDiretorio);

                    if (ConfigBackup.Logs)
                        Backup.Logs(nomeDiretorio);
                    if (ConfigBackup.XML)
                        Backup.XmlDestinatario(nomeDiretorio);
                    if (ConfigBackup.Banco)
                        Backup.BancoDeDados(nomeDiretorio);
                }

                //new BackupDatabase(BackupDatabase.TipoBackup.AUTOMATICO).ShowDialog(this);
            }
            catch { }
        }

        private void Ler_BackupConfig_XML()
        {
            ConfigBackup = new Config_Backup();
            ConfigBackup.Diretorio = new List<string>();
            try
            {
                if (!File.Exists("Configuração.xml"))
                    return;

                XElement xml = XElement.Load("Configuração.xml");

                foreach (XElement x in xml.Elements())
                    if (x.Name == "config")
                    {
                        ConfigBackup.Logs = bool.Parse(x.Attribute("Logs").Value);
                        ConfigBackup.XML = bool.Parse(x.Attribute("XML").Value);
                        ConfigBackup.Banco = bool.Parse(x.Attribute("Banco").Value);
                        ConfigBackup.Automatico = bool.Parse(x.Attribute("Automatico").Value);
                        ConfigBackup.Horario = TimeSpan.Parse(x.Attribute("Horario").Value);
                    }
                    else
                    {
                        ConfigBackup.Diretorio.Add(x.Attribute("Diretorio").Value);
                    }
            }
            catch { }
        }

        #endregion

        private void toolStripButton10_Click(object sender, EventArgs e)
        {
            var todasFiliais = FilialController.Instancia.GetAll();
            var grid = new GridGenerica<Filial>(todasFiliais, todasFiliais.FirstOrDefault(), new FormFilial(), false);

            ShowForm(grid);
        }

        private void toolStripButton9_Click_1(object sender, EventArgs e)
        {
            //var form = new Aplicacao.Modulos.Caixa.GridMovCaixa(cwkGestao.Negocio.MovCaixaController.Instancia.GetAll(), null, false, typeof(FormMovCaixa));
            var form = new FormFechamentoCaixa();
            ShowForm(form);
        }

        private void toolStripButton11_Click(object sender, EventArgs e)
        {
            try
            {
                if (!VerificaAberturaCaixa())
                    return;

                var form = new FormFrenteCaixaDelivery();
                form.ShowDialog(this);
                //ShowForm(form);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void toolStripButton13_Click(object sender, EventArgs e)
        {
            var form = new GridMovCaixa(MovCaixaController.Instancia.GetAll(),
                null, false, typeof(FormMovCaixa));
            ShowForm(form);
        }

        private void frenteDeCaixaToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            try
            {
                if (!VerificaAberturaCaixa())
                    return;

                var form = new FormFrenteCaixaNotaNova();
                form.ShowDialog(this);
                //ShowForm(form);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void deliveryToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            /*
            // Carrega Tela do Cliente antes, caso for Habilitado em Parametros
            var Config = ConfiguracaoController.Instancia.GetConfiguracao();
            if (Config?.AbrirTelaDadosClienteDelivery == 1)

            FormFrenteCaixaDeliveryDadosCliente form = new FormFrenteCaixaDeliveryDadosCliente();
            ShowForm(form);       
            */

            // Delivery
            try
            {
                if (!VerificaAberturaCaixa())
                    return;

                var form = new FormFrenteCaixaDelivery();
                form.ShowDialog(this);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }
        }

        private void monitoramentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Monitoramento
            FormFrenteCaixaDeliveryMonitor form = new FormFrenteCaixaDeliveryMonitor();
            ShowForm(form);
        }

        private void toolStripButton12_Click(object sender, EventArgs e)
        {
            // Monitoramento
            FormFrenteCaixaDeliveryMonitor form = new FormFrenteCaixaDeliveryMonitor();
            ShowForm(form);
        }

        //novo menu Dashboard
        private void menuDashboardToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormMenuDashBoard f = new FormMenuDashBoard();
            f.ShowDialog();
            f.Dispose();
        }

        private void sangriaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Sangria
            ShowForm(new FormImpressaoSangria());
        }

        private void suprimentoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            // Suprimento
            // Sangria
            ShowForm(new FormImpressaoSuprimento());
        }

        private void toolStripMenuItem5_Click(object sender, EventArgs e)
        {
            var grid = new FormImprimiEtiquetas();
            ShowForm(grid);
        }

        private void etiquetaZargoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Chama Form Relatório de Etiquetas Zargo
            var grid = new FormEtiquetaZebra();
            ShowForm(grid);
        }

        private void lançamentosFrenteCaixaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(new FormImpressaoFluxoCaixa());
        }

        private void margemDeLucroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = new FormRelatorioMargemLucro();
            ShowForm(grid);
        }

        private void produtosMaisVendidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = new FormRelatorioProdutosMaisVendidos();
            ShowForm(grid);
        }

        private void maisVendidosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = new FormRelatorioProdutosMaisVendidos();
            ShowForm(grid);
        }

        private void margemDeLucroToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            var grid = new FormRelatorioMargemLucro();
            ShowForm(grid);
        }

        private void contratosDeServiçosEProdutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var rel = new FormRelatorioContratoProdutoServico();
            ShowForm(rel);
        }

        private void toolStripButton14_Click(object sender, EventArgs e)
        {
            Facade.ChamaAutenticacao(Sistema.Gestão, versao, false);
            EscondeModulos();
            AtualizaDadosUsuario(true);
            VerificarCertificado();
        }

        private void cadastroToolStripMenuItem_DropDownOpening(object sender, EventArgs e)
        {
            //var menu = sender as ToolStripMenuItem;

            //bool itemVisivel = false;

            //for (int x = 0; x < menu.DropDownItems.Count; x++)
            //{
            //    switch (menu.DropDownItems[x])
            //    {

            //        case ToolStripSeparator obj:
            //            {
            //                if (itemVisivel)
            //                {
            //                    itemVisivel = false;
            //                    continue;
            //                }

            //                var separator = obj as ToolStripSeparator;
            //                separator.Visible = false;
            //            }
            //            continue;

            //        case ToolStripMenuItem obj:
            //            {
            //                var itemMenu = obj as ToolStripMenuItem;

            //                if (itemMenu.Visible)
            //                    itemVisivel = true;
            //            }
            //            continue;

            //    }

            //}
        }

        private void toolStripMenuItem31_Click(object sender, EventArgs e)
        {
            var rel = new GridMoeda();
            ShowForm(rel);
        }

        private void toolStripMenuItem32_Click(object sender, EventArgs e)
        {
            var rel = new GridCotacaoDeMoedas();
            ShowForm(rel);
        }

        private void consultaDeCréditoDeClientesComDevoluçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormConsultaCreditoClienteDevolucao form = new FormConsultaCreditoClienteDevolucao();
            ShowForm(form);
        }

        private void históricoDeDevoluçõesNoPDVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormHistoricoDevolucaoPDV form = new FormHistoricoDevolucaoPDV();
            ShowForm(form);
        }

        private void toolStripMenuItem33_Click(object sender, EventArgs e)
        {
            ShowForm(new GridCondicaoFornecimento());
        }

        private void toolStripMenuItem34_Click(object sender, EventArgs e)
        {
            var rel = new GridResponsavel();
            ShowForm(rel);
        }

        private void toolStripMenuItem35_Click(object sender, EventArgs e)
        {
            var rel = new GridServicoResponsavel();
            ShowForm(rel);
        }

        private void toolStripMenuItem36_Click(object sender, EventArgs e)
        {
            var rel = new GridFabricante();
            ShowForm(rel);
        }

        private void toolStripMenuItem37_Click(object sender, EventArgs e)
        {
            var rel = new GridMaquina();
            ShowForm(rel);
        }

        private void porClienteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var Rel = new RelatorioPedidosPorClientes();
            ShowForm(Rel);
        }

        private void tsmConfiguracaoSat_Click(object sender, EventArgs e)
        {
            var form = new FormConfigSAT();
            ShowForm(form);
        }

        private void novoPDVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                if (!VerificaAberturaCaixa())
                    return;

                var form = new FormFrenteCaixaNota();
                form.ShowDialog(this);
                //ShowForm(form);
            }
            catch (Exception Ex)
            {
                throw Ex;
            }

        }

        private void toolStripMenuItem38_Click(object sender, EventArgs e)
        {
            new FormRelatorioLimitePorCliente().Show();
        }

        private void toolStripMenuItem30_Click(object sender, EventArgs e)
        {
            new FormResponsavelPorCliente().ShowDialog();
        }

        private void contratoPorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            new FormContratosPorClientes().ShowDialog(this);
        }

        private void toolStripMenuItem39_Click(object sender, EventArgs e)
        {
            var grid = new GridGenerica<NCM>(NCMController.Instancia.GetAll().Where(o => !o.DtRevogacao.HasValue).ToList(), false, typeof(FormNCM));
            ShowForm(grid);
        }

        private void toolStripMenuItem40_Click(object sender, EventArgs e)
        {
            List<Produto> listaDeProdutos = new List<Produto>();
            try
            {
                listaDeProdutos = ProdutoController.Instancia.GetAll().OrderBy(prod => prod.NomeOrdenado).ToList(); //ProdutoController.Instancia.GetInativos().OrderBy(prod => prod.NomeOrdenado).ToList();
                if (listaDeProdutos.Count == 0)
                {
                    MessageBox.Show("Não existem Produtos Revogados cadastrados no Sistema", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Exclamation);
                }
                else
                {
                    var NCMS = NCMController.Instancia.GetAll().Where(o => o.DtRevogacao.HasValue);

                    var lQuery = listaDeProdutos.Where(c => NCMS.Select(o => o.Ncm).Contains(c.NCM));
                    if (lQuery.Any())
                    {
                        GridProdutoGen grid = new GridProdutoGen(lQuery.ToList(), false, typeof(FormProduto));
                        grid.Text = "Tabela de Produtos Revogados";
                        ShowForm(grid);
                    }
                    else
                    {
                        MessageBox.Show("Nenhum Produto com NCM revogado encontrado.", "Mensagem");
                        return;
                    }
                }
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }

        private void toolStripMenuItem41_Click(object sender, EventArgs e)
        {
            ShowForm((new GridManifestoEletronico()));
        }

        private void importaçãoDeDadosParaTabelasToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(new FormImportaDadosExcel());
        }

        private void ImportaNcmCestCFOPtoolStripMenuItem42_Click(object sender, EventArgs e)
        {
            ShowForm((new FormImportaNcmCestCFOP()));
        }

        private void geraçãoDeEAN13ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja atualizar os produtos sem EAN?", "Atenção", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                var qtdeAtualizados = 0;
                var List = ProdutoController.Instancia.GetTodosProdutos().Where(o => string.IsNullOrEmpty(o.Barra)).Select(o => o.ID).Distinct();
                foreach (var IDProd in List)
                {
                    string Barra = ProdutoController.Instancia.GetProximoCodigoBarrasEAN13(null);
                    ProdutoController.Instancia.AtualizarEanProduto(IDProd, Barra);
                    qtdeAtualizados++;
                }
                MessageBox.Show($"{qtdeAtualizados} Produtos atualizados!", "Atenção");
            }
        }

        private void consultaCEPToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormConsultaCEP f = new FormConsultaCEP();
            f.ShowDialog();
            f.Dispose();
        }

        private void toolStripMenuItem43_Click(object sender, EventArgs e)
        {
            // Ncm por Código
            FormRelatorioNCMPorCodigo f = new FormRelatorioNCMPorCodigo();
            f.ShowDialog();
            f.Dispose();
        }

        private void toolStripMenuItem44_Click(object sender, EventArgs e)
        {
            // Ncm Completo
            FormRelatorioNcmCompleto f = new FormRelatorioNcmCompleto();
            f.ShowDialog();
            f.Dispose();
        }

        private void dashBoardComparativoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDashBoardComparativo f = new FormDashBoardComparativo();
            f.ShowDialog();
            f.Dispose();
        }

        private void produtosPorDataCadastroToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = new FormRelatorioProdutosPorDataCadastro();
            ShowForm(grid);
        }

        private void toolStripMenuItem45_Click(object sender, EventArgs e)
        {
            ShowForm((new GridCTe()));
        }

        private void toolStripMenuItem46_Click(object sender, EventArgs e)
        {
            // Importa XML de uma CTe
            var grid = new FormImportaXMLCte();
            ShowForm(grid);
        }

        private void toolStripMenuItem48_Click(object sender, EventArgs e)
        {
            var grid = GridNotaGen.Get(InOutType.Saída, ProdutoServicoType.Servicos, false);
            ShowForm(grid);
        }

        private void consultaCPFCNPJToolStripMenuItem_Click(object sender, EventArgs e)
        {
            //Consulta cpf cnpj.
            FormConsultaCPF_CNPJ f = new FormConsultaCPF_CNPJ();
            f.ShowDialog();
            f.Dispose();
        }

        private void integraçãoEcommerceToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormIntegracaoE_commerce f = new FormIntegracaoE_commerce();
            f.ShowDialog();
            f.Dispose();
        }

        private void dashboardComercialToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormDashBoardComercial f = new FormDashBoardComercial();
            f.ShowDialog();
            f.Dispose();
        }

        private void toolStripMenuItem50_Click(object sender, EventArgs e)
        {
            var grid = new GridGenerica<StatusOS>(StatusOSController.Instancia.GetAll(), false, typeof(FormStatusOS));
            ShowForm(grid);
        }

        private void toolStripMenuItem51_Click(object sender, EventArgs e)
        {
            ShowForm((new GridMDFe()));
        }

        private void cadastroDeProdutoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            ShowForm(new GridProdutoEcommerceSimplificado());
        }

        private void comandaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var formComandas = new FormComanda();
            ShowForm(formComandas);
        }

        private void itensCanceladosPDVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRelatorioItensCanceladosPDV f = new FormRelatorioItensCanceladosPDV();
            f.ShowDialog();

        }

        private void atualizaPreçoBaseProdutosToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormAtualizaProdutosPreçoBase f = new FormAtualizaProdutosPreçoBase();
            f.ShowDialog();
        }

        private void consultaDeClientesComCréditoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormConsultaCreditoCliente f = new FormConsultaCreditoCliente();
            f.ShowDialog();
        }

        private void tabelaDePreçoGeralToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRelatorioTabelaPrecosGeral f = new FormRelatorioTabelaPrecosGeral();
            f.ShowDialog();
        }

        private void tabelaDePreçoPorVendaToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRelatorioTabelaPrecosPorVenda f = new FormRelatorioTabelaPrecosPorVenda();
            f.ShowDialog();
        }

        private void cadastroDePromoçoesToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var grid = new GridPromocoes();
            grid.ShowDialog();
        }

        private void geraçãoDeBoletoD8ToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var f = new FormBoletoD8(null, Acao.Incluir);
            f.ShowDialog();
        }

        private void vendasCanceladasPDVToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FormRelatorioVendasCanceladasPDV f = new FormRelatorioVendasCanceladasPDV();
            f.ShowDialog();
        }

        private void toolStripMenuItem52_Click(object sender, EventArgs e)
        {
            var form = new GridVeiculo();
            ShowForm(form);
        }

        private void toolStripMenuItem53_Click(object sender, EventArgs e)
        {
            var form = new GridProprietarioVeiculo();
            ShowForm(form);
        }

        private void toolStripMenuItem54_Click(object sender, EventArgs e)
        {
            GridGenerica<OpcaoTributacao> grid = new GridGenerica<OpcaoTributacao>(OpcaoTributacaoController.Instancia.GetAll(), new FormOpcaoTributacao(), null, false);
            //grid.Selecionando = true;
            ShowForm(grid);
            var EditValue = grid.Selecionado;
            //GridGenerica<cwkGestao.Modelo.OpcaoTributacao> grid = new GridGenerica<cwkGestao.Modelo.OpcaoTributacao>(OpcaoTributacaoController.Instancia.GetAll(), new FormOpcaoTributacao(), null, false);
            //grid.Selecionando = true;
            //if (cwkControleUsuario.Facade.ControleAcesso(grid))
            //    grid.ShowDialog();

            //var EditValue = grid.Selecionado;
        }

        private void classificaçãoFiscalToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<ClassificacaoFiscal> grid = new GridGenerica<ClassificacaoFiscal>(ClassificacaoFiscalController.Instancia.GetAll(), new FormClassificacaoFiscal(), false);
            ShowForm(grid);
        }
        private void ImpostosTributos_Click(object sender, EventArgs e)
        {
            GridGenerica<ImpostosTributos> grid = new GridGenerica<ImpostosTributos>(ImpostosTributosController.Instancia.GetAll(), new FormImpostosTributos(), false);
            ShowForm(grid);
        }

        private void perfilTributarioCliente_Click(object sender, EventArgs e)
        {
            GridGenerica<PerfilTributarioCliente> grid = new GridGenerica<PerfilTributarioCliente>(PerfilTributarioClienteController.Instancia.GetAll(), new FormPerfilTributarioCliente(), false);
            ShowForm(grid);
        }

        private void perfilPisCofins_Click(object sender, EventArgs e)
        {
            GridGenerica<PerfilPisCofins> grid = new GridGenerica<PerfilPisCofins>(PerfilPisCofinsController.Instancia.GetAll(), new FormPerfilPisCofins(), false);
            ShowForm(grid);
        }

        private void pisCofinsNaturezaDeOperaçãoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<PisCofinsNaturezaOperacao> grid = new GridGenerica<PisCofinsNaturezaOperacao>(PisCofinsNaturezaOperacaoController.Instancia.GetAll(), new FormPisCofinsNaturezaOperacao(), false);
            ShowForm(grid);


            //GridGenerica<ImpostosTributosExcessoes> grid1 = new GridGenerica<ImpostosTributosExcessoes>(ImpostosTributosExcessoesController.Instancia.GetAll(), new FormImpostosTributosExcessoes(), false);
            //ShowForm(grid1);
        }

        private void cadastroPISToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<TabelaPIS> grid = new GridGenerica<TabelaPIS>(TabelaPISController.Instancia.GetAll(), new FormTabelaPIS(), false);
            ShowForm(grid);
        }

        private void cadastroCONFISToolStripMenuItem_Click(object sender, EventArgs e)
        {
            GridGenerica<TabelaCOFINS> grid = new GridGenerica<TabelaCOFINS>(TabelaCOFINSController.Instancia.GetAll(), new FormTabelaCOFINS(), false);
            ShowForm(grid);
        }

        private void toolStripMenuCEST_Click(object sender, EventArgs e)
        {
            // var grid = new GridGenerica<CEST>(CEST.Instancia.GetAll(), new FormCEST(), false);
            //GridGenerica<PerfilTributarioCliente> grid = new GridGenerica<PerfilTributarioCliente>(PerfilTributarioClienteController.Instancia.GetAll(), new FormPerfilTributarioCliente(), false);
            //ShowForm(grid);
            GridGenerica<CEST> grid = new GridGenerica<CEST>(CESTController.Instancia.GetAll(), new FormCEST(), false);
            ShowForm(grid);
        }
    }

    internal class Config_Backup
    {
        public bool Logs { get; set; }

        public bool XML { get; set; }

        public bool Banco { get; set; }

        public bool Automatico { get; set; }

        public TimeSpan Horario { get; set; }

        public List<string> Diretorio { get; set; }
    }
}