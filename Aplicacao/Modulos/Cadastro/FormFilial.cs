using System;
using System.Collections.Generic;
using System.Drawing;
using System.Net;
using System.Net.Mail;
using System.Windows.Forms;
using cwkGestao.NFe;
using cwkGestao.Integracao.Correios;
using cwkGestao.Integracao.Sped.Fiscal;
using cwkGestao.Negocio;
using Aplicacao.Modulos.Comercial;
using cwkGestao.Modelo;
using Aplicacao.Base;
using Cwork.Utilitarios.Componentes.FuncoesValidacao;
using System.Security.Cryptography.X509Certificates;
using System.IO;
using ACBrLib.NFSe;
using ACBrLib.Core.Extensions;
using ACBrLib.Core.NFSe;
using ACBrLib;
using ACBrLib.Core.DFe;
using ACBrLib.Core;
using System.Linq;

namespace Aplicacao
{

    public partial class FormFilial : Aplicacao.IntermediariasTela.FormManutFilialIntermediaria
    {
        public FormFilial()
        {
            FillCertificados();
        }

        protected override List<Exception> ObjetoPraTela(Control pai)
        {
            var erros = base.ObjetoPraTela(pai);

            rgTipo.SelectedIndex = Selecionado.Tipo == "Filial" ? 1 : 0;
            cbcert.SelectedValue = Selecionado.Certificado;
            txtValidadeCertificado.Text = GetExpiration(Selecionado.Certificado);
            txtValidadeCertificado.Enabled = false;

            txtQuantDiasVencCertificado.EditValue = Selecionado.QuantDiasVencCertificado;

            rdbProducao.Checked = Selecionado.AmbienteNFSe == 1;
            rdbHomologacao.Checked = Selecionado.AmbienteNFSe == 2;

            txtCaminhoNFSeXML.Text = Selecionado.CaminhoNFSeXML;
            txtCaminhoNFSePDF.Text = Selecionado.CaminhoNFSePDF;

            return erros;
        }

        protected override void TelaProObjeto(Control pai)
        {
            base.TelaProObjeto(pai);
            Selecionado.Tipo = rgTipo.SelectedIndex == 1 ? "Filial" : "Matriz";

            Selecionado.QuantDiasVencCertificado = null;
            if (!string.IsNullOrEmpty(txtQuantDiasVencCertificado.Text))
                Selecionado.QuantDiasVencCertificado = Convert.ToInt32(txtQuantDiasVencCertificado.EditValue);

            //if (Selecionado.ComponenteDfe == 0) // 0 = Tecnospeed / 1 = Zeus Automação
            //    Selecionado.Certificado = cbCertificado.Text;
            //else
            if (cbcert.SelectedValue != null)
            {
                Selecionado.Certificado = cbcert.SelectedValue.ToString();
            }
            else
            {
                Selecionado.Certificado = "";
            }

            Selecionado.AmbienteNFSe = rdbProducao.Checked ? 1 : 2;

            Selecionado.CaminhoNFSeXML = txtCaminhoNFSeXML.Text;
            Selecionado.CaminhoNFSePDF = txtCaminhoNFSePDF.Text;
        }

        protected override void InitializeChildComponents()
        {
            InitializeComponent();
            //lkbCidade.SubForm = new FormCidade();
            lkbCidade.SubFormType = typeof(FormCidade);

            tcNFe.Tag = tpPrincipal.Tag;
            tcDadosSubstituto.Tag = tpPrincipal.Tag;
            tcZeus.Tag = tpPrincipal.Tag;
            tcTecnospeed.Tag = tpPrincipal.Tag;
            //btIncluirDadosSubstituto.SubForm = btAlterarDadosSubstituto.SubForm = new FormDadosSubstituto();
            btIncluirDadosSubstituto.SubFormType = btAlterarDadosSubstituto.SubFormType = typeof(FormDadosSubstituto);
        }

        protected override void AcoesAntesBase_Load(object sender, EventArgs e)
        {
            base.AcoesAntesBase_Load(sender, e);
            tcNFe.Tag = tpPrincipal.Tag;
            tcDadosSubstituto.Tag = tpPrincipal.Tag;
            tcZeus.Tag = tpPrincipal.Tag;
            tcTecnospeed.Tag = tpPrincipal.Tag;
        }

        private void lkbEmpresa_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Tela não implementada no nhibernate");
        }

        protected override void OnShown(EventArgs e)
        {
            base.OnShown(e);
            try
            {
                txtSerieScan.Properties.ReadOnly = true;
                txtSerieScan.Text = "900";

                if (Selecionado.bSTSomenteRevenda)
                    chbSTSomenteRevenda.Checked = true;
                else
                    chbSTSomenteRevenda.Checked = false;

                cbTipoAtividadePreponderante.SelectedIndex = ConversorComboBox.TipoAtividadePreponderante(Selecionado.TipoAtividadePreponderante);
                cbCodigoIndicadorIncidenciaTributaria.SelectedIndex = ConversorComboBox.CodigoIndicadorIncidenciaTributaria(Selecionado.CodigoIndicadorIncidenciaTributaria);
                cbCodigoIndicadorDeMetodoApropCreditos.SelectedIndex = ConversorComboBox.CodigoIndicadorMetodoApropriacaoCreditosComuns(Selecionado.CodigoIndicadorMetodoApropCreditos);
                cbCodigoIndicadorTipoContribuicaoApuradaPeriodo.SelectedIndex = ConversorComboBox.CodigoIndicadorTipoContribuicaoApurada(Selecionado.CodigoIndicadorTipoContribuicaoApuradaPeriodo);
                cbCodigoIndicadorCriterioEscrituracaoRegimeCumulativo.SelectedIndex = ConversorComboBox.CodigoIndicadorCriterioEscrituracaoApuracaoAdotado(Selecionado.CodigoIndicadorCriterioEscrituracaoRegimeCumulativo);
                cbINDPERFIL.SelectedIndex = ConversorComboBox.CodigoIndicadorPerfil(Selecionado.IND_PERFIL == null ? String.Empty : Selecionado.IND_PERFIL);
                cbIND_NAT_PJ.SelectedIndex = ConversorComboBox.IND_NAT_PJ(Selecionado.IND_NAT_PJ);
                cbModeloDanfeNFCe.SelectedIndex = ConversorComboBox.tpImpDanfeNFCe(Selecionado.ModeloDanfeNFCe);
                cbModeloDanfe.SelectedIndex = ConversorComboBox.tpImpDanfeNFe(Selecionado.ModeloDanfe);
                txtCodSuframa.EditValue = Selecionado.CodSuframa;

                if (Selecionado.TipoST == 0)
                {
                    rbSubstituido.Checked = true;
                }
                else if (Selecionado.TipoST == 1)
                {
                    rbSubstituto.Checked = true;
                }

                if (!string.IsNullOrEmpty(Selecionado.CaminhoLogoEmpresa))
                {
                    txtCaminhoLogoEmpresa.Text = Selecionado.CaminhoLogoEmpresa;
                    pictureBox1.Image = File.Exists(Selecionado.CaminhoLogoEmpresa) ? new Bitmap(Selecionado.CaminhoLogoEmpresa) : null;
                }
                else
                {
                    txtCaminhoLogoEmpresa.Text = string.Empty;
                    pictureBox1.Image = null;
                }

                if (!string.IsNullOrEmpty(Selecionado.CaminhoLogoNfe))
                {
                    txtCaminhoLogoNfe.Text = Selecionado.CaminhoLogoNfe;
                    pictureBox2.Image = File.Exists(Selecionado.CaminhoLogoNfe) ? new Bitmap(Selecionado.CaminhoLogoNfe) : null;
                }
                else
                {
                    txtCaminhoLogoNfe.Text = string.Empty;
                    pictureBox2.Image = null;
                }

                LoadConfig($"ACBrLib_NFSe_{Selecionado.ID}.ini");

                //ValidarCombosSPED();
                //foreach (Control control in tpPrincipal.Controls)
                //{
                //    if (control.GetType().IsSubclassOf(typeof(BaseEdit)))
                //    {
                //        ((BaseEdit)control).Properties.ReadOnly = true;
                //    }
                //    else if (control.GetType().Equals(typeof(Lookup)))
                //    {
                //        ((Lookup)control).Properties.ReadOnly = true;
                //    }
                //    else if (control.GetType().Equals(typeof(LookupButton)))
                //    {
                //        ((LookupButton)control).Enabled = false;
                //    }
                //}
            }
            catch (Exception ex)
            {
                FormErro fErro = new FormErro(ex);
                fErro.ShowDialog();
            }
        }

        protected override bool ValidarFormulario()
        {
            bool ok = base.ValidarFormulario();
            int serie = Convert.ToInt32(txtSerieScan.Text);

            //dxErroProvider.SetError(txtSerieScan, (serie < 900 ||  serie > 1000 ? "O valor da série tem que estar entre 900 e 1000" : ""));
            dxErroProvider.SetError(txtSerieScan, (serie != 900 ? "O valor da série em homologação é 900" : ""));
            //dxErroProvider.SetError(cbTipoCertificadoNFe, cbTipoCertificadoNFe.SelectedIndex < 0 ? "Selecione o tipo de certificado." : "");

            if ((cbCodigoIndicadorIncidenciaTributaria.SelectedIndex == 1) &&
                (cbCodigoIndicadorCriterioEscrituracaoRegimeCumulativo.SelectedIndex == 0))
            {
                dxErroProvider.SetError(cbCodigoIndicadorCriterioEscrituracaoRegimeCumulativo,
                    "Campo obrigatório quando o Código Indicador da incidência tributária é igual a 2");

                tcPrincipal.SelectedTabPage = tcSPED;
            }

            if ((cbCodigoIndicadorIncidenciaTributaria.SelectedIndex != 1) &&
                (cbCodigoIndicadorDeMetodoApropCreditos.SelectedIndex == 0))
            {
                dxErroProvider.SetError(cbCodigoIndicadorDeMetodoApropCreditos,
                    "Campo obrigatório quando o Código Indicador da incidência tributária é diferente de 2");

                tcPrincipal.SelectedTabPage = tcSPED;
            }

            if ((rbSubstituido.Checked == false) &&
                (rbSubstituto.Checked == false))
            {
                dxErroProvider.SetError(rbSubstituido,
                    "Por Favor, selecione um tipo de substituição tributária válida");
                dxErroProvider.SetError(rbSubstituto,
                    "Por Favor, selecione um tipo de substituição tributária válida");

                tcPrincipal.SelectedTabPage = tcDadosSubstituto;
            }

            if (!(String.IsNullOrEmpty(txtTextoTributo.Text)) &&
                !((txtTextoTributo.Text.Contains("<tagtotalimposto>")) &&
                (txtTextoTributo.Text.Contains("<tagpercentualimposto>"))))
            {
                dxErroProvider.SetError(txtTextoTributo, "É obrigatório informar as Tags <tagtotalimposto> e <tagpercentualimposto>");

                tcPrincipal.SelectedTabPage = tcNFe;
            }

            return ok && !dxErroProvider.HasErrors;

        }
        private void lkbTabelaFaixaFatSimples_Click(object sender, EventArgs e)
        {
            var selecionado = (cwkGestao.Modelo.TabelaFaixaFatSimples)lkpTabelaFaixaFatSimples.Selecionado;
            GridGenerica<cwkGestao.Modelo.TabelaFaixaFatSimples> grid = new GridGenerica<cwkGestao.Modelo.TabelaFaixaFatSimples>(cwkGestao.Negocio.TabelaFaixaFatSimplesController.Instancia.GetAll(), new FormTabelaFaixaFatSimples(), selecionado, false);
            grid.Selecionando = true;
            if (cwkControleUsuario.Facade.ControleAcesso(grid))
                grid.ShowDialog();
            if (grid.Selecionado != null)
                lkpTabelaFaixaFatSimples.EditValue = grid.Selecionado;
        }
        protected override void OK()
        {
            TelaProObjeto(this);
            if (chbSTSomenteRevenda.Checked)
                Selecionado.bSTSomenteRevenda = true;
            else
                Selecionado.bSTSomenteRevenda = false;

            Selecionado.nCdEmpresa = txtnCdEmpresa.Text;
            Selecionado.sDsSenha = txtsDsSenha.Text;
            Selecionado.correioCartaoPostagem = txtCartaoPostagem.Text;
            Selecionado.correioCodigoAdministrativo = txtCodigoAdministrativo.Text;
            Selecionado.correioContrato = txtContrato.Text;
            Selecionado.correioUsuario = txtUsuario.Text;
            Selecionado.correioSenha = txtSenha.Text;

            Selecionado.Fantasia = txtFantasia.Text;
            Selecionado.TipoAtividade = !(String.IsNullOrEmpty(cbTipoAtividade.EditValue.ToString())) ? Convert.ToInt32(cbTipoAtividade.EditValue.ToString().Substring(0, 1)) : -1;
            Selecionado.PerfilSped = !(String.IsNullOrEmpty(cbPerfilSped.EditValue.ToString())) ? cbPerfilSped.EditValue.ToString().Substring(0, 1) : "";

            Selecionado.CodSuframa = txtCodSuframa.Text;
            Selecionado.TipoAtividadePreponderante = !(String.IsNullOrEmpty(cbTipoAtividadePreponderante.EditValue.ToString())) ? cbTipoAtividadePreponderante.EditValue.ToString().Substring(0, 1) : "-1";
            Selecionado.CodigoIndicadorIncidenciaTributaria = !(String.IsNullOrEmpty(cbCodigoIndicadorIncidenciaTributaria.EditValue?.ToString())) ? cbCodigoIndicadorIncidenciaTributaria.EditValue.ToString().Substring(0, 1) : "-1";
            Selecionado.CodigoIndicadorTipoContribuicaoApuradaPeriodo = !(String.IsNullOrEmpty(cbCodigoIndicadorTipoContribuicaoApuradaPeriodo.EditValue?.ToString())) ? cbCodigoIndicadorTipoContribuicaoApuradaPeriodo.EditValue.ToString().Substring(0, 1) : "-1";
            Selecionado.CodigoIndicadorCriterioEscrituracaoRegimeCumulativo = ConversorComboBox.CodigoIndicadorCriterioEscrituracaoApuracaoAdotadoSelected(cbCodigoIndicadorCriterioEscrituracaoRegimeCumulativo.SelectedIndex);

            if (Convert.ToString(cbCodigoIndicadorDeMetodoApropCreditos.EditValue) == String.Empty)
                Selecionado.CodigoIndicadorMetodoApropCreditos = cbCodigoIndicadorDeMetodoApropCreditos.EditValue.ToString();
            else if (cbCodigoIndicadorDeMetodoApropCreditos.EditValue == null)
                Selecionado.CodigoIndicadorMetodoApropCreditos = "-1";
            else
                Selecionado.CodigoIndicadorMetodoApropCreditos = cbCodigoIndicadorDeMetodoApropCreditos.EditValue.ToString().Substring(0, 1);

            Selecionado.IND_PERFIL = !(string.IsNullOrEmpty(cbINDPERFIL.EditValue?.ToString())) ? cbINDPERFIL.EditValue.ToString().Substring(0, 1) : "-1";
            Selecionado.IND_NAT_PJ = !(string.IsNullOrEmpty(cbIND_NAT_PJ.EditValue?.ToString())) ? cbIND_NAT_PJ.EditValue.ToString().Substring(0, 2) : "-1";
            Selecionado.ModeloDanfe = ConversorComboBox.tpImpDanfeNFeSelected(cbModeloDanfe.SelectedIndex);
            Selecionado.ModeloDanfeNFCe = ConversorComboBox.tpImpDanfeNFCeSelected(cbModeloDanfeNFCe.SelectedIndex);

            if (rbSubstituido.Checked == true)
            {
                Selecionado.TipoST = 0;
            }
            else if (rbSubstituto.Checked == true)
            {
                Selecionado.TipoST = 1;
            }

            SalvarConfig();
            base.OK();
        }
        private void FormFilial_Shown(object sender, EventArgs e)
        {
            ValidarComponentesTotaltributosEPercTotalTributos();
            var conf = ConfiguracaoController.Instancia.GetConfiguracao();
            if (conf.bUtilizaWsCorreios)
            {
                tcCorreios.PageEnabled = true;
                tcCorreios.PageVisible = true;
            }
            else
            {
                tcCorreios.PageEnabled = false;
                tcCorreios.PageVisible = false;
            }

            ObjetoPraTela(this);
            txtnCdEmpresa.ToolTip = "Seu código administrativo junto à ECT. O código está disponível no corpo do contrato firmado com os Correios.";
            txtsDsSenha.ToolTip = @"Senha para acesso ao serviço, associada ao seu código administrativo. A senha inicial corresponde aos 8 primeiros dígitos do CNPJ informado no contrato.
            A qualquer momento, é possível alterar a senha no endereço http://www.corporativo.correios.com.br/encomendas/servicosonline/recuperaSenha.";

            txtnCdEmpresa.Text = Selecionado.nCdEmpresa;
            txtsDsSenha.Text = Selecionado.sDsSenha;
            txtUsuario.Text = Selecionado.correioUsuario;
            txtSenha.Text = Selecionado.correioSenha;
            txtContrato.Text = Selecionado.correioContrato;
            txtCodigoAdministrativo.Text = Selecionado.correioCodigoAdministrativo;
            txtCartaoPostagem.Text = Selecionado.correioCartaoPostagem;

            if (Selecionado.TipoST == 0)
            {
                rbSubstituido.Checked = true;
            }
            else if (Selecionado.TipoST == 1)
            {
                rbSubstituto.Checked = true;
            }

            gcDadosSubstituto.DataSource = DadosSubstitutoController.Instancia.GetByIDFilial(Selecionado.ID);
        }

        private void ValidarComponentesTotaltributosEPercTotalTributos()
        {
            switch (cbSimplesNacional.SelectedIndex)
            {
                case 2:
                    txtTextoTributo.EditValue = null;
                    txtPercentualImpostoTributo.EditValue = null;
                    txtPercentualImpostoTributo.CwkValidacao = null;
                    txtTextoTributo.Visible = txtTextoTributo.Enabled = false;
                    txtPercentualImpostoTributo.Visible = txtPercentualImpostoTributo.Enabled = false;
                    labelControl23.Visible = false;
                    break;
                default:
                    txtTextoTributo.EditValue = "Valor aproximado dos tributos - <tagtotalimposto> (<tagpercentualimposto>%) - Fonte IBPT";
                    txtPercentualImpostoTributo.EditValue = 33.42;
                    txtPercentualImpostoTributo.CwkValidacao = FuncaoValidacao.NaoNulo;
                    txtTextoTributo.Visible = txtTextoTributo.Enabled = true;
                    txtPercentualImpostoTributo.Visible = txtPercentualImpostoTributo.Enabled = true;
                    labelControl23.Visible = true;
                    break;
            }
        }

        private void btnConsultaStatus_Click(object sender, EventArgs e)
        {
            string statusRetorno = "";
            bool retorno = IntegracaoWsCorreios.GetStatusCorreios(txtCartaoPostagem.Text, txtUsuario.Text, txtSenha.Text, ref statusRetorno);

            MessageBox.Show("Status do contrato: " + statusRetorno, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
        }

        private void sbInutilizaFaixaNumeracao_Click(object sender, EventArgs e)
        {
            GridFaixasInutilizadas grid = new GridFaixasInutilizadas(Selecionado.ID);
            grid.ShowDialog();
        }

        private void cbCodigoIndicadorIncidenciaTributaria_SelectedIndexChanged(object sender, EventArgs e)
        {
            ValidarCombosSPED();
        }
        private void ValidarCombosSPED()
        {
            if ((cbCodigoIndicadorIncidenciaTributaria.SelectedIndex != 1) &&
                (String.IsNullOrEmpty(Convert.ToString(cbCodigoIndicadorDeMetodoApropCreditos.EditValue))))
            {
                cbCodigoIndicadorDeMetodoApropCreditos.SelectedIndex = 1;
            }
            else
            {
                cbCodigoIndicadorDeMetodoApropCreditos.SelectedIndex = 0;
            }

            if ((cbCodigoIndicadorIncidenciaTributaria.SelectedIndex == 1) &&
                (String.IsNullOrEmpty(Convert.ToString(cbCodigoIndicadorCriterioEscrituracaoRegimeCumulativo.EditValue))))
            {
                cbCodigoIndicadorCriterioEscrituracaoRegimeCumulativo.SelectedIndex = 1;
            }
            else
            {
                cbCodigoIndicadorCriterioEscrituracaoRegimeCumulativo.SelectedIndex = 0;
            }
        }

        private void AtualizaTela()
        {
            gvDadosSubstituto.RefreshData();
            gcDadosSubstituto.Refresh();
            gcDadosSubstituto.RefreshDataSource();
        }

        private void rbSubstituido_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSubstituido.Checked == true)
            {
                gcDadosSubstituto.Enabled = false;
                btIncluirDadosSubstituto.Enabled = false;
                btAlterarDadosSubstituto.Enabled = false;
                btExcluirDadosSubstituto.Enabled = false;
            }
            else
            {
                gcDadosSubstituto.Enabled = true;
                btIncluirDadosSubstituto.Enabled = true;
                btAlterarDadosSubstituto.Enabled = true;
                btExcluirDadosSubstituto.Enabled = true;
            }
        }

        private void rbSubstituto_CheckedChanged(object sender, EventArgs e)
        {
            if (rbSubstituto.Checked == true)
            {
                gcDadosSubstituto.Enabled = true;
                btIncluirDadosSubstituto.Enabled = true;
                btAlterarDadosSubstituto.Enabled = true;
                btExcluirDadosSubstituto.Enabled = true;
            }
            else
            {
                gcDadosSubstituto.Enabled = false;
                btIncluirDadosSubstituto.Enabled = false;
                btAlterarDadosSubstituto.Enabled = false;
                btExcluirDadosSubstituto.Enabled = false;
            }
        }

        private void btExcluirDadosSubstituto_Click(object sender, EventArgs e)
        {
            try
            {
                if (((IList<DadosSubstituto>)gvDadosSubstituto.DataSource).Count > 0)
                {
                    int indiceElementoSelecionado = gvDadosSubstituto.GetSelectedRows()[0];
                    DadosSubstituto selecionado = ((DadosSubstituto)gvDadosSubstituto.GetRow(indiceElementoSelecionado));
                    FormDadosSubstituto form = new FormDadosSubstituto();
                    form.Selecionado = selecionado;
                    form.ShowDialog();

                    if (form.Selecionado != null)
                    {
                        selecionado = form.Selecionado;
                        Selecionado.DadosSubstituto.RemoveAt(indiceElementoSelecionado);
                        gcDadosSubstituto.DataSource = Selecionado.DadosSubstituto;
                        gvDadosSubstituto.RefreshData();
                        gcDadosSubstituto.RefreshDataSource();
                    }

                }
            }
            catch (Exception ex)
            {
                FormErro fErro = new FormErro(ex);
                fErro.ShowDialog();
            }
        }
        private void gcDadosSubstituto_DoubleClick(object sender, EventArgs e)
        {
            try
            {
                if (((IList<DadosSubstituto>)gvDadosSubstituto.DataSource).Count > 0)
                {
                    int indiceElementoSelecionado = gvDadosSubstituto.GetSelectedRows()[0];
                    DadosSubstituto selecionado = ((DadosSubstituto)gvDadosSubstituto.GetRow(indiceElementoSelecionado));
                    FormDadosSubstituto form = new FormDadosSubstituto();
                    form.Selecionado = selecionado;
                    form.ShowDialog();

                    if ((form.Selecionado != null) &&
                        (selecionado != form.Selecionado))
                    {
                        selecionado = form.Selecionado;
                        Selecionado.DadosSubstituto.RemoveAt(indiceElementoSelecionado);
                        Selecionado.DadosSubstituto.Insert(indiceElementoSelecionado, selecionado);
                        gcDadosSubstituto.DataSource = Selecionado.DadosSubstituto;
                        gvDadosSubstituto.RefreshData();
                        gcDadosSubstituto.RefreshDataSource();
                    }

                }
            }
            catch (Exception ex)
            {
                FormErro fErro = new FormErro(ex);
                fErro.ShowDialog();
            }
        }

        private void cbSimplesNacional_EditValueChanged(object sender, EventArgs e)
        {
            ValidarComponentesTotaltributosEPercTotalTributos();
        }

        private void txtTextoTributo_EditValueChanged(object sender, EventArgs e)
        {
            if (txtTextoTributo.Text.Length > 0)
                txtPercentualImpostoTributo.CwkValidacao = FuncaoValidacao.NaoNulo;
            else
                txtPercentualImpostoTributo.CwkValidacao = null;
        }
        private void cbComponente_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                switch (cbComponenteDfe.SelectedIndex)
                {
                    case 0:
                        //tcTecnospeed.PageEnabled = true;
                        //tcTecnospeed.PageVisible = true;
                        ////cbTipoCertificadoNFe.Enabled = true;
                        ////cbCertificado.Enabled = true;

                        tcZeus.PageEnabled = false;
                        tcZeus.PageVisible = false;
                        cbzTipoCertificadoNFe.Enabled = false;
                        chbCacheCertificado.Enabled = false;
                        txtArquivoCert.Enabled = false;
                        btnArquivoCert.Enabled = false;
                        txtPinCert.Enabled = false;


                        break;
                    case 1:
                        //tcTecnospeed.PageEnabled = false;
                        //tcTecnospeed.PageVisible = false;

                        ////cbTipoCertificadoNFe.Enabled = false;
                        ////cbCertificado.Enabled = false;

                        tcZeus.PageEnabled = true;
                        tcZeus.PageVisible = true;
                        cbzTipoCertificadoNFe.Enabled = true;
                        chbCacheCertificado.Enabled = true;
                        txtArquivoCert.Enabled = true;
                        btnArquivoCert.Enabled = true;
                        txtPinCert.Enabled = true;

                        if (ConfiguracaoController.Instancia.GetConfiguracao().ClienteEmiteNFSe == 1)
                        {
                            //tcTecnospeed.PageEnabled = true;
                            //tcTecnospeed.PageVisible = true;
                            //cbTipoCertificadoNFe.Enabled = true;
                            //cbCertificado.Enabled = true;
                            txtPinCert.Enabled = true;
                        }
                        break;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                //cbTipoCertificadoNFe.SelectedIndex = -1;
            }
        }
        private void FillCertificados()
        {
            try
            {
                X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly);
                var cert = store.Certificates;

                List<KeyValuePair<string, string>> certificados = new List<KeyValuePair<string, string>>();
                foreach (var c in cert)
                {
                    string nome = c.SubjectName.Name.Split(',')[0].Replace("CN=", "") + $" {GetExpiration(c.SerialNumber)}";
                    certificados.Add(new KeyValuePair<string, string>(c.SerialNumber, nome));
                }

                cbcert.DisplayMember = "Value";
                cbcert.ValueMember = "Key";
                cbcert.DataSource = certificados;
                cbcert.DropDownStyle = ComboBoxStyle.DropDownList;
            }
            catch (Exception ex)
            {
                FormErro fErro = new FormErro(ex);
                fErro.ShowDialog();
            }
        }


        private string GetExpiration(string SerialNumber)
        {
            try
            {
                X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
                store.Open(OpenFlags.ReadOnly);
                foreach (var item in store.Certificates)
                    if (item.SerialNumber == SerialNumber)
                        return item.GetExpirationDateString();

                return string.Empty;
            }
            catch
            {
                return string.Empty;
            }
        }
        private void btnCertificado_Click(object sender, EventArgs e)
        {
            try
            {
                //  txtCertificado.Text = NFeController.RetornaListaCertificados().SerialNumber;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void btnArquivoCert_Click(object sender, EventArgs e)
        {
            txtArquivoCert.Text = BuscarArquivo("Selecione o arquivo de Certificado", ".pfx", "Arquivos PFX (*.pfx)|*.pfx|Todos os Arquivos (*.*)|*.*");
        }

        private static string BuscarArquivo(string titulo, string extensaoPadrao, string filtro, string arquivoPadrao = null)
        {
            var dlg = new OpenFileDialog
            {
                Title = titulo,
                FileName = arquivoPadrao,
                DefaultExt = extensaoPadrao,
                Filter = filtro
            };
            dlg.ShowDialog();
            return dlg.FileName;
        }

        private void cbTipoCertificadoZeus_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                txtPinCert.Text = string.Empty;
                txtArquivoCert.Text = string.Empty;

                switch (cbzTipoCertificadoNFe.SelectedIndex)
                {
                    case 0:

                        //TextField
                        txtPinCert.Enabled = false;
                        txtArquivoCert.Enabled = false;
                        //Buttons
                        btnArquivoCert.Enabled = false;
                        break;
                    case 1:

                        //TextField
                        txtPinCert.Enabled = true;
                        txtArquivoCert.Enabled = true;
                        //Buttons
                        btnArquivoCert.Enabled = true;
                        break;
                    case 2:

                        //TextField
                        txtPinCert.Enabled = true;
                        txtArquivoCert.Enabled = false;
                        //Buttons
                        btnArquivoCert.Enabled = false;
                        break;

                    default:
                        break;
                }
            }
            catch (Exception exception)
            {
                MessageBox.Show(exception.Message);
                cbzTipoCertificadoNFe.SelectedIndex = -1;
            }
        }

        private void cbcert_SelectedIndexChanged(object sender, EventArgs e)
        {
            txtValidadeCertificado.Text = GetExpiration(cbcert.SelectedValue?.ToString());
            txtValidadeCertificado.Enabled = false;
        }

        private void btn_TestarEmail_Click(object sender, EventArgs e)
        {
            Selecionado.ServidorSMTP = txtServidorSMTP.Text;
            Selecionado.PortaSmtp = Convert.ToInt32(txtPortaSmtp.Text);
            Selecionado.UsuarioEmail = txtUsuarioEmail.Text;
            Selecionado.SenhaEmail = txtSenhaEmail.Text;

            MessageBox.Show(EnviarEmail(Selecionado, txtUsuarioEmail.Text, "Teste de Envio."), "Atenção!!");
        }



        private string EnviarEmail(Filial objFilial, string pEmailCliente, string corpoMensagem)
        {
            try
            {
                var message = new MailMessage(objFilial.UsuarioEmail, txtEmailNFe.Text, "Teste de envio de e-mail", "Teste de envio de e-mail");
                var client = new SmtpClient(objFilial.ServidorSMTP, objFilial.PortaSmtp) { EnableSsl = chkGMailAutenticacao.Checked };
                var credencial = new NetworkCredential(Selecionado.UsuarioEmail, Selecionado.SenhaEmail);
                client.UseDefaultCredentials = false;
                client.Credentials = credencial;
                client.Send(message);

                return "Email enviado com sucesso!";
            }
            catch (Exception ex)
            {
                return ex.Message;
            }
        }

        private void btnSelecionaLogoEmpresa_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Selecionado.CaminhoLogoEmpresa = openFileDialog1.FileName;
                txtCaminhoLogoEmpresa.Text = Selecionado.CaminhoLogoEmpresa;
                pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                Selecionado.CaminhoLogoNfe = openFileDialog1.FileName;
                txtCaminhoLogoNfe.Text = Selecionado.CaminhoLogoNfe;
                pictureBox2.Image = new Bitmap(openFileDialog1.FileName);
            }
        }

        /* NFSe */
        private ACBrNFSe ACBrNFSe;
        private void SalvarConfig()
        {
            try
            {
                //Config Geral
                ACBrNFSe.Config.ExibirErroSchema = ckbExibirErroSchema.Checked;
                ACBrNFSe.Config.FormatoAlerta = txtFormatoAlerta.Text;
                ACBrNFSe.Config.FormaEmissao = cmbFormaEmissao.GetSelectedValue<TipoEmissao>();
                ACBrNFSe.Config.RetirarAcentos = ckbRetirarAcentos.Checked;
                ACBrNFSe.Config.SalvarGer = ckbSalvarArqeResp.Checked;
                ACBrNFSe.Config.PathSalvar = txtCaminhoNFSeXML.Text;
                ACBrNFSe.Config.MontarPathSchema = ckbMontarPathSchemas.Checked;
                ACBrNFSe.Config.PathSchemas = txtSchemaPath.Text;
                ACBrNFSe.Config.IniServicos = txtServicosIni.Text;
                ACBrNFSe.Config.ConsultaLoteAposEnvio = ckbConsultarLoteAposEnvio.Checked;
                ACBrNFSe.Config.ConsultaAposCancelar = ckbConsultarAposCancelar.Checked;
                ACBrNFSe.Config.LayoutNFSe = cmbLayoutNFSe.GetSelectedValue<LayoutNFSe>();

                //Config WebService
                ACBrNFSe.Config.DFe.UF = cmbUfDestino.Text;
                ACBrNFSe.Config.SSLType = cmbSSlType.GetSelectedValue<SSLType>();
                ACBrNFSe.Config.Timeout = (int)nudTimeOut.Value;
                ACBrNFSe.Config.Ambiente = rdbHomologacao.Checked ? TipoAmbiente.taHomologacao : TipoAmbiente.taProducao;
                ACBrNFSe.Config.Visualizar = ckbVisualizar.Checked;
                ACBrNFSe.Config.SalvarWS = ckbSalvarSOAP.Checked;

                //Certificado
                ACBrNFSe.Config.DFe.SSLCryptLib = cmbCrypt.GetSelectedValue<SSLCryptLib>();
                ACBrNFSe.Config.DFe.SSLHttpLib = cmbHttp.GetSelectedValue<SSLHttpLib>();
                ACBrNFSe.Config.DFe.SSLXmlSignLib = cmbXmlSign.GetSelectedValue<SSLXmlSignLib>();
                ACBrNFSe.Config.DFe.ArquivoPFX = txtCertPath.Text;
                ACBrNFSe.Config.DFe.Senha = txtCertPassword.Text;
                ACBrNFSe.Config.DFe.NumeroSerie = txtCertNumero.Text;
                ACBrNFSe.Config.DFe.DadosPFX = txtDadosPFX.Text;

                //Arquivos
                ACBrNFSe.Config.SalvarArq = ckbSalvarArqs.Checked;
                ACBrNFSe.Config.SepararPorMes = ckbPastaMensal.Checked;
                ACBrNFSe.Config.AdicionarLiteral = ckbAdicionarLiteralNomePastas.Checked;
                ACBrNFSe.Config.EmissaoPathNFSe = ckbEmissaoPathNFSe.Checked;
                ACBrNFSe.Config.SepararPorCNPJ = ckbSepararPorCNPJ.Checked;

                //Documento Auxiliar
                ACBrNFSe.Config.DANFSe.PathLogo = txtLogomarcaPrefeitura.Text;
                ACBrNFSe.Config.DANFSe.Prestador.Logo = txtLogoMarcaPrestadorServico.Text;
                ACBrNFSe.Config.DANFSe.Prefeitura = txtNomePrefeitura.Text;
                ACBrNFSe.Config.DANFSe.PathPDF = txtPastaPDF.Text;

                //Emitente
                ACBrNFSe.Config.Emitente.CNPJ = txtCNPJEmitente.Text;
                ACBrNFSe.Config.Emitente.InscMun = txtInscMunicipalEmitente.Text;
                ACBrNFSe.Config.Emitente.RazSocial = txtRazaoSocialEmitente.Text;
                ACBrNFSe.Config.Emitente.WSUser = txtEmitenteUsuário.Text;
                ACBrNFSe.Config.Emitente.WSSenha = txtEmitenteSenha.Text;
                ACBrNFSe.Config.Emitente.WSFraseSecr = txtEmitenteFraseSecreta.Text;
                ACBrNFSe.Config.Emitente.WSChaveAcesso = txtEmitenteChaveAcesso.Text;
                ACBrNFSe.Config.Emitente.WSChaveAutoriz = txtEmitenteChaveAutorizacao.Text;
                ACBrNFSe.Config.Emitente.Dados.NomeFantasia = txtFantasiaEmitente.Text;
                ACBrNFSe.Config.Emitente.Dados.Telefone = txtFoneEmitente.Text;
                ACBrNFSe.Config.Emitente.Dados.CEP = txtCEPEmitente.Text;
                ACBrNFSe.Config.Emitente.Dados.Endereco = txtLogradouroEmitente.Text;
                ACBrNFSe.Config.Emitente.Dados.Numero = txtNumeroEmitente.Text;
                ACBrNFSe.Config.Emitente.Dados.Complemento = txtComplementoEmitente.Text;
                ACBrNFSe.Config.Emitente.Dados.Bairro = txtBairroEmitente.Text;
                ACBrNFSe.Config.CodigoMunicipio = cmbCidadeEmitente.GetSelectedValue<CodigoMunicipio>();
                txtCodCidadeEmitente.Text = ACBrNFSe.Config.CodigoMunicipio.GetEnumValueOrInt<CodigoMunicipio>();

                ACBrNFSe.ConfigGravar();

                Application.DoEvents();
            }
            finally
            {
            }
        }

        private void LoadConfig(string file)
        {
            cmbCidadeEmitente.EnumDataSource(CodigoMunicipio.NenhumaCidadeSelecionada);
            cmbLayoutNFSe.EnumDataSource(LayoutNFSe.lnfsProvedor);
            cmbFormaEmissao.EnumDataSource(TipoEmissao.teNormal);
            cmbUfDestino.SelectedItem = "PR";
            cmbSSlType.EnumDataSource(SSLType.LT_all);
            cmbCrypt.EnumDataSource(SSLCryptLib.cryWinCrypt);
            cmbHttp.EnumDataSource(SSLHttpLib.httpWinHttp);
            cmbXmlSign.EnumDataSource(SSLXmlSignLib.xsLibXml2);

            ACBrNFSe = new ACBrNFSe();
            ACBrNFSe.ConfigLer(file);
            ACBrNFSe.Config.IniServicos = "";
            ACBrNFSe.Config.Principal.LogNivel = NivelLog.logParanoico;

            //Config Geral
            ckbExibirErroSchema.Checked = ACBrNFSe.Config.ExibirErroSchema;
            txtFormatoAlerta.Text = ACBrNFSe.Config.FormatoAlerta;
            cmbFormaEmissao.SetSelectedValue(ACBrNFSe.Config.FormaEmissao);
            ckbRetirarAcentos.Checked = ACBrNFSe.Config.RetirarAcentos;
            ckbSalvarArqeResp.Checked = ACBrNFSe.Config.SalvarGer;
            txtCaminhoNFSeXML.Text = ACBrNFSe.Config.PathSalvar;
            ckbMontarPathSchemas.Checked = ACBrNFSe.Config.MontarPathSchema;
            txtSchemaPath.Text = ACBrNFSe.Config.PathSchemas;
            txtServicosIni.Text = ACBrNFSe.Config.IniServicos;
            ckbConsultarLoteAposEnvio.Checked = ACBrNFSe.Config.ConsultaLoteAposEnvio;
            ckbConsultarAposCancelar.Checked = ACBrNFSe.Config.ConsultaAposCancelar;
            cmbLayoutNFSe.SetSelectedValue(ACBrNFSe.Config.LayoutNFSe);

            //Config Webservice
            cmbUfDestino.SelectedItem = ACBrNFSe.Config.DFe.UF;
            cmbSSlType.SetSelectedValue(ACBrNFSe.Config.SSLType);
            nudTimeOut.Value = ACBrNFSe.Config.Timeout;

            var ambiente = ACBrNFSe.Config.Ambiente;
            rdbHomologacao.Checked = ambiente == TipoAmbiente.taHomologacao;
            rdbProducao.Checked = ambiente == TipoAmbiente.taProducao;

            ckbVisualizar.Checked = ACBrNFSe.Config.Visualizar;
            ckbSalvarSOAP.Checked = ACBrNFSe.Config.SalvarWS;
            ckbAjustarAut.Checked = ACBrNFSe.Config.AjustaAguardaConsultaRet;
            nudAguardar.Value = ACBrNFSe.Config.AguardarConsultaRet;
            nudTentativas.Value = ACBrNFSe.Config.Tentativas;
            nudIntervalos.Value = ACBrNFSe.Config.IntervaloTentativas;

            //Config Certificado
            cmbCrypt.SetSelectedValue(ACBrNFSe.Config.DFe.SSLCryptLib);
            cmbHttp.SetSelectedValue(ACBrNFSe.Config.DFe.SSLHttpLib);
            cmbXmlSign.SetSelectedValue(ACBrNFSe.Config.DFe.SSLXmlSignLib);
            txtCertPath.Text = ACBrNFSe.Config.DFe.ArquivoPFX;
            txtCertPassword.Text = ACBrNFSe.Config.DFe.Senha;
            txtCertNumero.Text = ACBrNFSe.Config.DFe.NumeroSerie;
            txtDadosPFX.Text = ACBrNFSe.Config.DFe.DadosPFX;

            //Config Arquivos
            ckbSalvarArqs.Checked = ACBrNFSe.Config.SalvarArq;
            ckbPastaMensal.Checked = ACBrNFSe.Config.SepararPorMes;
            ckbAdicionarLiteralNomePastas.Checked = ACBrNFSe.Config.AdicionarLiteral;
            ckbEmissaoPathNFSe.Checked = ACBrNFSe.Config.EmissaoPathNFSe;
            ckbSepararPorCNPJ.Checked = ACBrNFSe.Config.SepararPorCNPJ;

            //Config Documento Auxiliar
            txtLogomarcaPrefeitura.Text = ACBrNFSe.Config.DANFSe.PathLogo;
            txtLogoMarcaPrestadorServico.Text = ACBrNFSe.Config.DANFSe.Prestador.Logo;
            txtNomePrefeitura.Text = ACBrNFSe.Config.DANFSe.Prefeitura;
            txtPastaPDF.Text = ACBrNFSe.Config.DANFSe.PathPDF;

            //Config Emitente
            txtCNPJEmitente.Text = ACBrNFSe.Config.Emitente.CNPJ;
            txtInscMunicipalEmitente.Text = ACBrNFSe.Config.Emitente.InscMun;
            txtRazaoSocialEmitente.Text = ACBrNFSe.Config.Emitente.RazSocial;
            txtEmitenteUsuário.Text = ACBrNFSe.Config.Emitente.WSUser;
            txtEmitenteSenha.Text = ACBrNFSe.Config.Emitente.WSSenha;
            txtEmitenteFraseSecreta.Text = ACBrNFSe.Config.Emitente.WSFraseSecr;
            txtEmitenteChaveAcesso.Text = ACBrNFSe.Config.Emitente.WSChaveAcesso;
            txtEmitenteChaveAutorizacao.Text = ACBrNFSe.Config.Emitente.WSChaveAutoriz;
            txtFantasiaEmitente.Text = ACBrNFSe.Config.Emitente.Dados.NomeFantasia;
            txtFoneEmitente.Text = ACBrNFSe.Config.Emitente.Dados.Telefone;
            txtCEPEmitente.Text = ACBrNFSe.Config.Emitente.Dados.CEP;
            txtLogradouroEmitente.Text = ACBrNFSe.Config.Emitente.Dados.Endereco;
            txtNumeroEmitente.Text = ACBrNFSe.Config.Emitente.Dados.Numero;
            txtComplementoEmitente.Text = ACBrNFSe.Config.Emitente.Dados.Complemento;
            txtBairroEmitente.Text = ACBrNFSe.Config.Emitente.Dados.Bairro;
            cmbCidadeEmitente.SetSelectedValue(ACBrNFSe.Config.CodigoMunicipio);
            txtCodCidadeEmitente.Text = ACBrNFSe.Config.CodigoMunicipio.GetEnumValueOrInt<CodigoMunicipio>();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtCaminhoNFSeXML.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtCaminhoNFSePDF.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnSelectSchema_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtSchemaPath.Text = folderBrowserDialog1.SelectedPath;
            }
        }

        private void btnSelectLog_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtLogs.Text = folderBrowserDialog1.SelectedPath;
            }
        }
        public static class Helpers //alterado Por Diego Rodrigues 11/01/2024
        {
            public static string OpenFile(string filters, string title = "Abrir", bool checkFileExists = true)
            {
                using (var ofd = new OpenFileDialog())
                {
                    ofd.CheckPathExists = true;
                    ofd.CheckFileExists = checkFileExists;
                    ofd.Multiselect = false;
                    ofd.Filter = filters;
                    ofd.Title = title;

                    return ofd.ShowDialog().Equals(DialogResult.Cancel) ? null : ofd.FileName;
                }
            }

            public static string[] OpenFiles(string filters, string title = "Abrir")
            {
                using (var ofd = new OpenFileDialog())
                {
                    ofd.CheckPathExists = true;
                    ofd.CheckFileExists = true;
                    ofd.Multiselect = true;
                    ofd.Filter = filters;
                    ofd.Title = title;

                    return ofd.ShowDialog().Equals(DialogResult.Cancel) ? null : ofd.FileNames;
                }
            }

            public static string SaveFile(string filters, string title = "Salvar")
            {
                using (var ofd = new SaveFileDialog())
                {
                    ofd.CheckPathExists = true;
                    ofd.Filter = filters;
                    ofd.Title = title;

                    return ofd.ShowDialog().Equals(DialogResult.Cancel) ? null : ofd.FileName;
                }
            }

            public static string SelectFolder(string initialPath = "")
            {
                using (var fbd = new FolderBrowserDialog())
                {
                    if (!string.IsNullOrEmpty(initialPath)) fbd.SelectedPath = initialPath;
                    fbd.ShowNewFolderButton = true;
                    return fbd.ShowDialog().Equals(DialogResult.Cancel) ? string.Empty : fbd.SelectedPath;
                }
            }
        }

        private void btnDadosPFX_Click(object sender, EventArgs e)
        {
            try
            {
                var file = Helpers.OpenFile("Arquivos PFX (*.pfx)|*.pfx|Todos os Arquivos (*.*)|*.*");
                if (!File.Exists(file)) return;

                var dados = File.ReadAllBytes(file);
                txtDadosPFX.Text = Convert.ToBase64String(dados);
            }
            catch (Exception ex)
            {
                MessageBox.Show("Erro: " + ex.Message);
            }

        }


        private void btnServicosIni_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtServicosIni.Text = openFileDialog1.FileName;
            }
        }

        private void btnLogomarcaPrefeitura_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtLogomarcaPrefeitura.Text = openFileDialog1.FileName;
            }
        }

        private void btnLogoMarcaPrestadorServico_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                txtLogoMarcaPrestadorServico.Text = openFileDialog1.FileName;
            }
        }

        private void btnPastaPDF_Click(object sender, EventArgs e)
        {
            if (folderBrowserDialog1.ShowDialog() == DialogResult.OK)
            {
                txtPastaPDF.Text = folderBrowserDialog1.SelectedPath;
            }
        }
    }
}


// ALTERADO 23/07/2022 - 17:51 - BINELI - PARA RESOLVER O PROBLEMA DE ERRO QUANDO O CLIENTE NÃO TEM CERTIFICADO DIGITAL. OS CÓDIGOS DE CIMA FORAM ALTERADOS PARA RESOLVER ISSO 

//using System;
//using System.Collections.Generic;
//using System.Drawing;
//using System.Net;
//using System.Net.Mail;
//using System.Windows.Forms;
//using cwkGestao.NFe;
//using cwkGestao.Integracao.Correios;
//using cwkGestao.Integracao.Sped.Fiscal;
//using cwkGestao.Negocio;
//using Aplicacao.Modulos.Comercial;
//using cwkGestao.Modelo;
//using Aplicacao.Base;
//using Cwork.Utilitarios.Componentes.FuncoesValidacao;
//using System.Security.Cryptography.X509Certificates;
//using System.IO;

//namespace Aplicacao
//{

//    public partial class FormFilial : Aplicacao.IntermediariasTela.FormManutFilialIntermediaria
//    {
//        public FormFilial()
//        {
//            FillCertificados();
//            FillCertificadoOpenAC();
//        }

//        protected override List<Exception> ObjetoPraTela(Control pai)
//        {
//            var erros = base.ObjetoPraTela(pai);

//            rgTipo.SelectedIndex = Selecionado.Tipo == "Filial" ? 1 : 0;
//            //if (Selecionado.ComponenteDfe == 0) // 0 = Tecnospeed / 1 = Zeus Automação
//            //{
//            //cbCertificado.Text = Selecionado.Certificado;
//            //}
//            //else
//            //{
//            cbcert.SelectedValue = Selecionado.Certificado;
//            //}
//            cbCertificadoOpenAC.SelectedValue = Selecionado.Certificado;

//            txtValidadeCertificado.Text = GetExpiration(Selecionado.Certificado);
//            txtValidadeCertificado.Enabled = false;

//            txtQuantDiasVencCertificado.EditValue = Selecionado.QuantDiasVencCertificado;
//            cbAmbienteNFSe.SelectedIndex = Selecionado.AmbienteNFSe == 1 ? 0 : 1;

//            return erros;
//        }

//        protected override void TelaProObjeto(Control pai)
//        {
//            base.TelaProObjeto(pai);
//            Selecionado.Tipo = rgTipo.SelectedIndex == 1 ? "Filial" : "Matriz";

//            Selecionado.QuantDiasVencCertificado = null;
//            if (!string.IsNullOrEmpty(txtQuantDiasVencCertificado.Text))
//                Selecionado.QuantDiasVencCertificado = Convert.ToInt32(txtQuantDiasVencCertificado.EditValue);

//            if (Selecionado.ComponenteDfe == 0) // 0 = Tecnospeed / 1 = Zeus Automação
//                Selecionado.Certificado = cbCertificado.Text;
//            else
//                Selecionado.Certificado = cbcert.SelectedValue.ToString();

//            Selecionado.Certificado = cbCertificadoOpenAC.SelectedValue?.ToString();
//            Selecionado.AmbienteNFSe = cbAmbienteNFSe.SelectedIndex == 0 ? 1 : 2;
//        }

//        protected override void InitializeChildComponents()
//        {
//            InitializeComponent();
//            //lkbCidade.SubForm = new FormCidade();
//            lkbCidade.SubFormType = typeof(FormCidade);

//            tcNFe.Tag = tpPrincipal.Tag;
//            tcDadosSubstituto.Tag = tpPrincipal.Tag;
//            tcZeus.Tag = tpPrincipal.Tag;
//            tcTecnospeed.Tag = tpPrincipal.Tag;
//            //btIncluirDadosSubstituto.SubForm = btAlterarDadosSubstituto.SubForm = new FormDadosSubstituto();
//            btIncluirDadosSubstituto.SubFormType = btAlterarDadosSubstituto.SubFormType = typeof(FormDadosSubstituto);
//        }

//        protected override void AcoesAntesBase_Load(object sender, EventArgs e)
//        {
//            base.AcoesAntesBase_Load(sender, e);
//            tcNFe.Tag = tpPrincipal.Tag;
//            tcDadosSubstituto.Tag = tpPrincipal.Tag;
//            tcZeus.Tag = tpPrincipal.Tag;
//            tcTecnospeed.Tag = tpPrincipal.Tag;
//        }

//        private void lkbEmpresa_Click(object sender, EventArgs e)
//        {
//            MessageBox.Show("Tela não implementada no nhibernate");
//        }

//        private void cbTipoCertificadoNFe_SelectedIndexChanged(object sender, EventArgs e)
//        {

//            try
//            {
//                cbCertificado.Properties.Items.Clear();
//                switch (cbTipoCertificadoNFe.SelectedIndex)
//                {
//                    case 0: cbCertificado.Properties.Items.AddRange(NFeController.RetornaListaCertificados(cwkGestao.NFe.Enums.TipoCertificado.ckFile)); break;
//                    case 1: cbCertificado.Properties.Items.AddRange(NFeController.RetornaListaCertificados(cwkGestao.NFe.Enums.TipoCertificado.ckSmartCard)); break;
//                    case 2: cbCertificado.Properties.Items.AddRange(NFeController.RetornaListaCertificados(cwkGestao.NFe.Enums.TipoCertificado.ckMemory)); break;
//                    case 3: cbCertificado.Properties.Items.AddRange(NFeController.RetornaListaCertificados(cwkGestao.NFe.Enums.TipoCertificado.ckLocalMachine)); break;
//                    case 4: cbCertificado.Properties.Items.AddRange(NFeController.RetornaListaCertificados(cwkGestao.NFe.Enums.TipoCertificado.ckActiveDirectory)); break;
//                    default:
//                        break;
//                }
//            }
//            catch (Exception exception)
//            {
//                MessageBox.Show(exception.Message);
//                cbTipoCertificadoNFe.SelectedIndex = -1;
//            }
//        }

//        protected override void OnShown(EventArgs e)
//        {
//            base.OnShown(e);
//            try
//            {
//                txtSerieScan.Properties.ReadOnly = true;
//                txtSerieScan.Text = "900";

//                if (Selecionado.bSTSomenteRevenda)
//                    chbSTSomenteRevenda.Checked = true;
//                else
//                    chbSTSomenteRevenda.Checked = false;

//                cbTipoAtividadePreponderante.SelectedIndex = ConversorComboBox.TipoAtividadePreponderante(Selecionado.TipoAtividadePreponderante);
//                cbCodigoIndicadorIncidenciaTributaria.SelectedIndex = ConversorComboBox.CodigoIndicadorIncidenciaTributaria(Selecionado.CodigoIndicadorIncidenciaTributaria);
//                cbCodigoIndicadorDeMetodoApropCreditos.SelectedIndex = ConversorComboBox.CodigoIndicadorMetodoApropriacaoCreditosComuns(Selecionado.CodigoIndicadorMetodoApropCreditos);
//                cbCodigoIndicadorTipoContribuicaoApuradaPeriodo.SelectedIndex = ConversorComboBox.CodigoIndicadorTipoContribuicaoApurada(Selecionado.CodigoIndicadorTipoContribuicaoApuradaPeriodo);
//                cbCodigoIndicadorCriterioEscrituracaoRegimeCumulativo.SelectedIndex = ConversorComboBox.CodigoIndicadorCriterioEscrituracaoApuracaoAdotado(Selecionado.CodigoIndicadorCriterioEscrituracaoRegimeCumulativo);
//                cbINDPERFIL.SelectedIndex = ConversorComboBox.CodigoIndicadorPerfil(Selecionado.IND_PERFIL == null ? String.Empty : Selecionado.IND_PERFIL);
//                cbIND_NAT_PJ.SelectedIndex = ConversorComboBox.IND_NAT_PJ(Selecionado.IND_NAT_PJ);
//                cbModeloDanfeNFCe.SelectedIndex = ConversorComboBox.tpImpDanfeNFCe(Selecionado.ModeloDanfeNFCe);
//                cbModeloDanfe.SelectedIndex = ConversorComboBox.tpImpDanfeNFe(Selecionado.ModeloDanfe);
//                txtCodSuframa.EditValue = Selecionado.CodSuframa;

//                if (Selecionado.TipoST == 0)
//                {
//                    rbSubstituido.Checked = true;
//                }
//                else if (Selecionado.TipoST == 1)
//                {
//                    rbSubstituto.Checked = true;
//                }

//                if (!string.IsNullOrEmpty(Selecionado.CaminhoLogoEmpresa))
//                {
//                    txtCaminhoLogoEmpresa.Text = Selecionado.CaminhoLogoEmpresa;
//                    pictureBox1.Image = File.Exists(Selecionado.CaminhoLogoEmpresa) ? new Bitmap(Selecionado.CaminhoLogoEmpresa) : null;
//                }
//                else
//                {
//                    txtCaminhoLogoEmpresa.Text = string.Empty;
//                    pictureBox1.Image = null;
//                }

//                if (!string.IsNullOrEmpty(Selecionado.CaminhoLogoNfe))
//                {
//                    txtCaminhoLogoNfe.Text = Selecionado.CaminhoLogoNfe;
//                    pictureBox2.Image = File.Exists(Selecionado.CaminhoLogoNfe) ? new Bitmap(Selecionado.CaminhoLogoNfe) : null;
//                }
//                else
//                {
//                    txtCaminhoLogoNfe.Text = string.Empty;
//                    pictureBox2.Image = null;
//                }

//                if (!string.IsNullOrEmpty(Selecionado.CaminhoLogoPrefeitura))
//                {
//                    txtCaminhoLogoNFSe.Text = Selecionado.CaminhoLogoPrefeitura;
//                    pictureBox3.Image = File.Exists(Selecionado.CaminhoLogoPrefeitura) ? new Bitmap(Selecionado.CaminhoLogoPrefeitura) : null;
//                }
//                else
//                {
//                    txtCaminhoLogoNFSe.Text = string.Empty;
//                    pictureBox3.Image = null;
//                }

//                //ValidarCombosSPED();
//                //foreach (Control control in tpPrincipal.Controls)
//                //{
//                //    if (control.GetType().IsSubclassOf(typeof(BaseEdit)))
//                //    {
//                //        ((BaseEdit)control).Properties.ReadOnly = true;
//                //    }
//                //    else if (control.GetType().Equals(typeof(Lookup)))
//                //    {
//                //        ((Lookup)control).Properties.ReadOnly = true;
//                //    }
//                //    else if (control.GetType().Equals(typeof(LookupButton)))
//                //    {
//                //        ((LookupButton)control).Enabled = false;
//                //    }
//                //}
//            }
//            catch (Exception ex)
//            {
//                FormErro fErro = new FormErro(ex);
//                fErro.ShowDialog();
//            }
//        }

//        protected override bool ValidarFormulario()
//        {
//            bool ok = base.ValidarFormulario();
//            int serie = Convert.ToInt32(txtSerieScan.Text);

//            //dxErroProvider.SetError(txtSerieScan, (serie < 900 ||  serie > 1000 ? "O valor da série tem que estar entre 900 e 1000" : ""));
//            dxErroProvider.SetError(txtSerieScan, (serie != 900 ? "O valor da série em homologação é 900" : ""));
//            //dxErroProvider.SetError(cbTipoCertificadoNFe, cbTipoCertificadoNFe.SelectedIndex < 0 ? "Selecione o tipo de certificado." : "");

//            if ((cbCodigoIndicadorIncidenciaTributaria.SelectedIndex == 1) &&
//                (cbCodigoIndicadorCriterioEscrituracaoRegimeCumulativo.SelectedIndex == 0))
//            {
//                dxErroProvider.SetError(cbCodigoIndicadorCriterioEscrituracaoRegimeCumulativo,
//                    "Campo obrigatório quando o Código Indicador da incidência tributária é igual a 2");

//                tcPrincipal.SelectedTabPage = tcSPED;
//            }

//            if ((cbCodigoIndicadorIncidenciaTributaria.SelectedIndex != 1) &&
//                (cbCodigoIndicadorDeMetodoApropCreditos.SelectedIndex == 0))
//            {
//                dxErroProvider.SetError(cbCodigoIndicadorDeMetodoApropCreditos,
//                    "Campo obrigatório quando o Código Indicador da incidência tributária é diferente de 2");

//                tcPrincipal.SelectedTabPage = tcSPED;
//            }

//            if ((rbSubstituido.Checked == false) &&
//                (rbSubstituto.Checked == false))
//            {
//                dxErroProvider.SetError(rbSubstituido,
//                    "Por Favor, selecione um tipo de substituição tributária válida");
//                dxErroProvider.SetError(rbSubstituto,
//                    "Por Favor, selecione um tipo de substituição tributária válida");

//                tcPrincipal.SelectedTabPage = tcDadosSubstituto;
//            }

//            if (!(String.IsNullOrEmpty(txtTextoTributo.Text)) &&
//                !((txtTextoTributo.Text.Contains("<tagtotalimposto>")) &&
//                (txtTextoTributo.Text.Contains("<tagpercentualimposto>"))))
//            {
//                dxErroProvider.SetError(txtTextoTributo, "É obrigatório informar as Tags <tagtotalimposto> e <tagpercentualimposto>");

//                tcPrincipal.SelectedTabPage = tcNFe;
//            }

//            return ok && !dxErroProvider.HasErrors;

//        }
//        private void lkbTabelaFaixaFatSimples_Click(object sender, EventArgs e)
//        {
//            var selecionado = (cwkGestao.Modelo.TabelaFaixaFatSimples)lkpTabelaFaixaFatSimples.Selecionado;
//            GridGenerica<cwkGestao.Modelo.TabelaFaixaFatSimples> grid = new GridGenerica<cwkGestao.Modelo.TabelaFaixaFatSimples>(cwkGestao.Negocio.TabelaFaixaFatSimplesController.Instancia.GetAll(), new FormTabelaFaixaFatSimples(), selecionado, false);
//            grid.Selecionando = true;
//            if (cwkControleUsuario.Facade.ControleAcesso(grid))
//                grid.ShowDialog();
//            if (grid.Selecionado != null)
//                lkpTabelaFaixaFatSimples.EditValue = grid.Selecionado;
//        }
//        protected override void OK()
//        {
//            TelaProObjeto(this);
//            if (chbSTSomenteRevenda.Checked)
//                Selecionado.bSTSomenteRevenda = true;
//            else
//                Selecionado.bSTSomenteRevenda = false;

//            Selecionado.nCdEmpresa = txtnCdEmpresa.Text;
//            Selecionado.sDsSenha = txtsDsSenha.Text;
//            Selecionado.correioCartaoPostagem = txtCartaoPostagem.Text;
//            Selecionado.correioCodigoAdministrativo = txtCodigoAdministrativo.Text;
//            Selecionado.correioContrato = txtContrato.Text;
//            Selecionado.correioUsuario = txtUsuario.Text;
//            Selecionado.correioSenha = txtSenha.Text;

//            Selecionado.Fantasia = txtFantasia.Text;
//            Selecionado.TipoAtividade = !(String.IsNullOrEmpty(cbTipoAtividade.EditValue.ToString())) ? Convert.ToInt32(cbTipoAtividade.EditValue.ToString().Substring(0, 1)) : -1;
//            Selecionado.PerfilSped = !(String.IsNullOrEmpty(cbPerfilSped.EditValue.ToString())) ? cbPerfilSped.EditValue.ToString().Substring(0, 1) : "";

//            Selecionado.CodSuframa = txtCodSuframa.Text;
//            Selecionado.TipoAtividadePreponderante = !(String.IsNullOrEmpty(cbTipoAtividadePreponderante.EditValue.ToString())) ? cbTipoAtividadePreponderante.EditValue.ToString().Substring(0, 1) : "-1";
//            Selecionado.CodigoIndicadorIncidenciaTributaria = !(String.IsNullOrEmpty(cbCodigoIndicadorIncidenciaTributaria.EditValue?.ToString())) ? cbCodigoIndicadorIncidenciaTributaria.EditValue.ToString().Substring(0, 1) : "-1";
//            Selecionado.CodigoIndicadorTipoContribuicaoApuradaPeriodo = !(String.IsNullOrEmpty(cbCodigoIndicadorTipoContribuicaoApuradaPeriodo.EditValue?.ToString())) ? cbCodigoIndicadorTipoContribuicaoApuradaPeriodo.EditValue.ToString().Substring(0, 1) : "-1";
//            Selecionado.CodigoIndicadorCriterioEscrituracaoRegimeCumulativo = ConversorComboBox.CodigoIndicadorCriterioEscrituracaoApuracaoAdotadoSelected(cbCodigoIndicadorCriterioEscrituracaoRegimeCumulativo.SelectedIndex);

//            if (Convert.ToString(cbCodigoIndicadorDeMetodoApropCreditos.EditValue) == String.Empty)
//                Selecionado.CodigoIndicadorMetodoApropCreditos = cbCodigoIndicadorDeMetodoApropCreditos.EditValue.ToString();
//            else if (cbCodigoIndicadorDeMetodoApropCreditos.EditValue == null)
//                Selecionado.CodigoIndicadorMetodoApropCreditos = "-1";
//            else
//                Selecionado.CodigoIndicadorMetodoApropCreditos = cbCodigoIndicadorDeMetodoApropCreditos.EditValue.ToString().Substring(0, 1);

//            Selecionado.IND_PERFIL = !(string.IsNullOrEmpty(cbINDPERFIL.EditValue?.ToString())) ? cbINDPERFIL.EditValue.ToString().Substring(0, 1) : "-1";
//            Selecionado.IND_NAT_PJ = !(string.IsNullOrEmpty(cbIND_NAT_PJ.EditValue?.ToString())) ? cbIND_NAT_PJ.EditValue.ToString().Substring(0, 2) : "-1";
//            Selecionado.ModeloDanfe = ConversorComboBox.tpImpDanfeNFeSelected(cbModeloDanfe.SelectedIndex);
//            Selecionado.ModeloDanfeNFCe = ConversorComboBox.tpImpDanfeNFCeSelected(cbModeloDanfeNFCe.SelectedIndex);

//            if (rbSubstituido.Checked == true)
//            {
//                Selecionado.TipoST = 0;
//            }
//            else if (rbSubstituto.Checked == true)
//            {
//                Selecionado.TipoST = 1;
//            }
//            base.OK();
//        }
//        private void FormFilial_Shown(object sender, EventArgs e)
//        {
//            ValidarComponentesTotaltributosEPercTotalTributos();
//            var conf = ConfiguracaoController.Instancia.GetConfiguracao();
//            if (conf.bUtilizaWsCorreios)
//            {
//                tcCorreios.PageEnabled = true;
//                tcCorreios.PageVisible = true;
//            }
//            else
//            {
//                tcCorreios.PageEnabled = false;
//                tcCorreios.PageVisible = false;
//            }

//            ObjetoPraTela(this);
//            txtnCdEmpresa.ToolTip = "Seu código administrativo junto à ECT. O código está disponível no corpo do contrato firmado com os Correios.";
//            txtsDsSenha.ToolTip = @"Senha para acesso ao serviço, associada ao seu código administrativo. A senha inicial corresponde aos 8 primeiros dígitos do CNPJ informado no contrato.
//            A qualquer momento, é possível alterar a senha no endereço http://www.corporativo.correios.com.br/encomendas/servicosonline/recuperaSenha.";

//            txtnCdEmpresa.Text = Selecionado.nCdEmpresa;
//            txtsDsSenha.Text = Selecionado.sDsSenha;
//            txtUsuario.Text = Selecionado.correioUsuario;
//            txtSenha.Text = Selecionado.correioSenha;
//            txtContrato.Text = Selecionado.correioContrato;
//            txtCodigoAdministrativo.Text = Selecionado.correioCodigoAdministrativo;
//            txtCartaoPostagem.Text = Selecionado.correioCartaoPostagem;

//            if (Selecionado.TipoST == 0)
//            {
//                rbSubstituido.Checked = true;
//            }
//            else if (Selecionado.TipoST == 1)
//            {
//                rbSubstituto.Checked = true;
//            }

//            gcDadosSubstituto.DataSource = DadosSubstitutoController.Instancia.GetByIDFilial(Selecionado.ID);
//        }
//        private void ValidarComponentesTotaltributosEPercTotalTributos()
//        {
//            switch (cbSimplesNacional.SelectedIndex)
//            {
//                case 2:
//                    txtTextoTributo.EditValue = null;
//                    txtPercentualImpostoTributo.EditValue = null;
//                    txtPercentualImpostoTributo.CwkValidacao = null;
//                    txtTextoTributo.Visible = txtTextoTributo.Enabled = false;
//                    txtPercentualImpostoTributo.Visible = txtPercentualImpostoTributo.Enabled = false;
//                    labelControl23.Visible = false;
//                    break;
//                default:
//                    txtTextoTributo.EditValue = "Valor aproximado dos tributos - <tagtotalimposto> (<tagpercentualimposto>%) - Fonte IBPT";
//                    txtPercentualImpostoTributo.EditValue = 33.42;
//                    txtPercentualImpostoTributo.CwkValidacao = FuncaoValidacao.NaoNulo;
//                    txtTextoTributo.Visible = txtTextoTributo.Enabled = true;
//                    txtPercentualImpostoTributo.Visible = txtPercentualImpostoTributo.Enabled = true;
//                    labelControl23.Visible = true;
//                    break;
//            }
//        }

//        private void btnConsultaStatus_Click(object sender, EventArgs e)
//        {
//            string statusRetorno = "";
//            bool retorno = IntegracaoWsCorreios.GetStatusCorreios(txtCartaoPostagem.Text, txtUsuario.Text, txtSenha.Text, ref statusRetorno);

//            MessageBox.Show("Status do contrato: " + statusRetorno, "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
//        }

//        private void sbInutilizaFaixaNumeracao_Click(object sender, EventArgs e)
//        {
//            GridFaixasInutilizadas grid = new GridFaixasInutilizadas(Selecionado.ID);
//            grid.ShowDialog();
//        }

//        private void cbCodigoIndicadorIncidenciaTributaria_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            ValidarCombosSPED();
//        }
//        private void ValidarCombosSPED()
//        {
//            if ((cbCodigoIndicadorIncidenciaTributaria.SelectedIndex != 1) &&
//                (String.IsNullOrEmpty(Convert.ToString(cbCodigoIndicadorDeMetodoApropCreditos.EditValue))))
//            {
//                cbCodigoIndicadorDeMetodoApropCreditos.SelectedIndex = 1;
//            }
//            else
//            {
//                cbCodigoIndicadorDeMetodoApropCreditos.SelectedIndex = 0;
//            }

//            if ((cbCodigoIndicadorIncidenciaTributaria.SelectedIndex == 1) &&
//                (String.IsNullOrEmpty(Convert.ToString(cbCodigoIndicadorCriterioEscrituracaoRegimeCumulativo.EditValue))))
//            {
//                cbCodigoIndicadorCriterioEscrituracaoRegimeCumulativo.SelectedIndex = 1;
//            }
//            else
//            {
//                cbCodigoIndicadorCriterioEscrituracaoRegimeCumulativo.SelectedIndex = 0;
//            }
//        }

//        private void AtualizaTela()
//        {
//            gvDadosSubstituto.RefreshData();
//            gcDadosSubstituto.Refresh();
//            gcDadosSubstituto.RefreshDataSource();
//        }

//        private void rbSubstituido_CheckedChanged(object sender, EventArgs e)
//        {
//            if (rbSubstituido.Checked == true)
//            {
//                gcDadosSubstituto.Enabled = false;
//                btIncluirDadosSubstituto.Enabled = false;
//                btAlterarDadosSubstituto.Enabled = false;
//                btExcluirDadosSubstituto.Enabled = false;
//            }
//            else
//            {
//                gcDadosSubstituto.Enabled = true;
//                btIncluirDadosSubstituto.Enabled = true;
//                btAlterarDadosSubstituto.Enabled = true;
//                btExcluirDadosSubstituto.Enabled = true;
//            }
//        }

//        private void rbSubstituto_CheckedChanged(object sender, EventArgs e)
//        {
//            if (rbSubstituto.Checked == true)
//            {
//                gcDadosSubstituto.Enabled = true;
//                btIncluirDadosSubstituto.Enabled = true;
//                btAlterarDadosSubstituto.Enabled = true;
//                btExcluirDadosSubstituto.Enabled = true;
//            }
//            else
//            {
//                gcDadosSubstituto.Enabled = false;
//                btIncluirDadosSubstituto.Enabled = false;
//                btAlterarDadosSubstituto.Enabled = false;
//                btExcluirDadosSubstituto.Enabled = false;
//            }
//        }

//        private void btExcluirDadosSubstituto_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                if (((IList<DadosSubstituto>)gvDadosSubstituto.DataSource).Count > 0)
//                {
//                    int indiceElementoSelecionado = gvDadosSubstituto.GetSelectedRows()[0];
//                    DadosSubstituto selecionado = ((DadosSubstituto)gvDadosSubstituto.GetRow(indiceElementoSelecionado));
//                    FormDadosSubstituto form = new FormDadosSubstituto();
//                    form.Selecionado = selecionado;
//                    form.ShowDialog();

//                    if (form.Selecionado != null)
//                    {
//                        selecionado = form.Selecionado;
//                        Selecionado.DadosSubstituto.RemoveAt(indiceElementoSelecionado);
//                        gcDadosSubstituto.DataSource = Selecionado.DadosSubstituto;
//                        gvDadosSubstituto.RefreshData();
//                        gcDadosSubstituto.RefreshDataSource();
//                    }

//                }
//            }
//            catch (Exception ex)
//            {
//                FormErro fErro = new FormErro(ex);
//                fErro.ShowDialog();
//            }
//        }
//        private void gcDadosSubstituto_DoubleClick(object sender, EventArgs e)
//        {
//            try
//            {
//                if (((IList<DadosSubstituto>)gvDadosSubstituto.DataSource).Count > 0)
//                {
//                    int indiceElementoSelecionado = gvDadosSubstituto.GetSelectedRows()[0];
//                    DadosSubstituto selecionado = ((DadosSubstituto)gvDadosSubstituto.GetRow(indiceElementoSelecionado));
//                    FormDadosSubstituto form = new FormDadosSubstituto();
//                    form.Selecionado = selecionado;
//                    form.ShowDialog();

//                    if ((form.Selecionado != null) &&
//                        (selecionado != form.Selecionado))
//                    {
//                        selecionado = form.Selecionado;
//                        Selecionado.DadosSubstituto.RemoveAt(indiceElementoSelecionado);
//                        Selecionado.DadosSubstituto.Insert(indiceElementoSelecionado, selecionado);
//                        gcDadosSubstituto.DataSource = Selecionado.DadosSubstituto;
//                        gvDadosSubstituto.RefreshData();
//                        gcDadosSubstituto.RefreshDataSource();
//                    }

//                }
//            }
//            catch (Exception ex)
//            {
//                FormErro fErro = new FormErro(ex);
//                fErro.ShowDialog();
//            }
//        }

//        private void cbSimplesNacional_EditValueChanged(object sender, EventArgs e)
//        {
//            ValidarComponentesTotaltributosEPercTotalTributos();
//        }

//        private void txtTextoTributo_EditValueChanged(object sender, EventArgs e)
//        {
//            if (txtTextoTributo.Text.Length > 0)
//                txtPercentualImpostoTributo.CwkValidacao = FuncaoValidacao.NaoNulo;
//            else
//                txtPercentualImpostoTributo.CwkValidacao = null;
//        }
//        private void cbComponente_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            try
//            {
//                switch (cbComponenteDfe.SelectedIndex)
//                {
//                    case 0:
//                        //tcTecnospeed.PageEnabled = true;
//                        //tcTecnospeed.PageVisible = true;
//                        cbTipoCertificadoNFe.Enabled = true;
//                        cbCertificado.Enabled = true;

//                        tcZeus.PageEnabled = false;
//                        tcZeus.PageVisible = false;
//                        cbzTipoCertificadoNFe.Enabled = false;
//                        chbCacheCertificado.Enabled = false;
//                        txtArquivoCert.Enabled = false;
//                        btnArquivoCert.Enabled = false;
//                        txtPinCert.Enabled = false;


//                        break;
//                    case 1:
//                        //tcTecnospeed.PageEnabled = false;
//                        //tcTecnospeed.PageVisible = false;

//                        cbTipoCertificadoNFe.Enabled = false;
//                        cbCertificado.Enabled = false;

//                        tcZeus.PageEnabled = true;
//                        tcZeus.PageVisible = true;
//                        cbzTipoCertificadoNFe.Enabled = true;
//                        chbCacheCertificado.Enabled = true;
//                        txtArquivoCert.Enabled = true;
//                        btnArquivoCert.Enabled = true;
//                        txtPinCert.Enabled = true;

//                        if (ConfiguracaoController.Instancia.GetConfiguracao().ClienteEmiteNFSe == 1)
//                        {
//                            //tcTecnospeed.PageEnabled = true;
//                            //tcTecnospeed.PageVisible = true;
//                            cbTipoCertificadoNFe.Enabled = true;
//                            cbCertificado.Enabled = true;
//                            txtPinCert.Enabled = true;
//                        }
//                        break;
//                }
//            }
//            catch (Exception exception)
//            {
//                MessageBox.Show(exception.Message);
//                cbTipoCertificadoNFe.SelectedIndex = -1;
//            }
//        }
//        private void FillCertificados()
//        {
//            try
//            {
//                X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
//                store.Open(OpenFlags.ReadOnly);
//                var cert = store.Certificates;

//                List<KeyValuePair<string, string>> certificados = new List<KeyValuePair<string, string>>();
//                foreach (var c in cert)
//                {
//                    string nome = c.SubjectName.Name.Split(',')[0].Replace("CN=", "") + $" {GetExpiration(c.SerialNumber)}";
//                    certificados.Add(new KeyValuePair<string, string>(c.SerialNumber, nome));
//                }

//                cbcert.DisplayMember = "Value";
//                cbcert.ValueMember = "Key";
//                cbcert.DataSource = certificados;
//                cbcert.DropDownStyle = ComboBoxStyle.DropDownList;
//            }
//            catch (Exception ex)
//            {
//                FormErro fErro = new FormErro(ex);
//                fErro.ShowDialog();
//            }
//        }
//        private void FillCertificadoOpenAC() // Certificado OpenAC
//        {
//            try
//            {
//                X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
//                store.Open(OpenFlags.ReadOnly);
//                var cert = store.Certificates;

//                List<KeyValuePair<string, string>> certificados = new List<KeyValuePair<string, string>>();
//                foreach (var c in cert)
//                {
//                    string nome = c.SubjectName.Name.Split(',')[0].Replace("CN=", "") + $" {GetExpiration(c.SerialNumber)}";
//                    certificados.Add(new KeyValuePair<string, string>(c.SerialNumber, nome));
//                }

//                cbCertificadoOpenAC.DisplayMember = "Value";
//                cbCertificadoOpenAC.ValueMember = "Key";
//                cbCertificadoOpenAC.DataSource = certificados;
//                cbCertificadoOpenAC.DropDownStyle = ComboBoxStyle.DropDownList;
//            }
//            catch (Exception ex)
//            {
//                FormErro fErro = new FormErro(ex);
//                fErro.ShowDialog();
//            }
//        }
//        private string GetExpiration(string SerialNumber)
//        {
//            try
//            {
//                X509Store store = new X509Store(StoreName.My, StoreLocation.CurrentUser);
//                store.Open(OpenFlags.ReadOnly);
//                foreach (var item in store.Certificates)
//                    if (item.SerialNumber == SerialNumber)
//                        return item.GetExpirationDateString();

//                return string.Empty;
//            }
//            catch
//            {
//                return string.Empty;
//            }
//        }
//        private void btnCertificado_Click(object sender, EventArgs e)
//        {
//            try
//            {
//                //  txtCertificado.Text = NFeController.RetornaListaCertificados().SerialNumber;
//            }
//            catch (Exception ex)
//            {
//                MessageBox.Show(ex.Message, string.Empty, MessageBoxButtons.OK, MessageBoxIcon.Information);
//            }
//        }

//        private void btnArquivoCert_Click(object sender, EventArgs e)
//        {
//            txtArquivoCert.Text = BuscarArquivo("Selecione o arquivo de Certificado", ".pfx", "Arquivos PFX (*.pfx)|*.pfx|Todos os Arquivos (*.*)|*.*");
//        }

//        private static string BuscarArquivo(string titulo, string extensaoPadrao, string filtro, string arquivoPadrao = null)
//        {
//            var dlg = new OpenFileDialog
//            {
//                Title = titulo,
//                FileName = arquivoPadrao,
//                DefaultExt = extensaoPadrao,
//                Filter = filtro
//            };
//            dlg.ShowDialog();
//            return dlg.FileName;
//        }

//        private void cbTipoCertificadoZeus_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            try
//            {
//                txtPinCert.Text = string.Empty;
//                txtArquivoCert.Text = string.Empty;

//                switch (cbzTipoCertificadoNFe.SelectedIndex)
//                {
//                    case 0:

//                        //TextField
//                        txtPinCert.Enabled = false;
//                        txtArquivoCert.Enabled = false;
//                        //Buttons
//                        btnArquivoCert.Enabled = false;
//                        break;
//                    case 1:

//                        //TextField
//                        txtPinCert.Enabled = true;
//                        txtArquivoCert.Enabled = true;
//                        //Buttons
//                        btnArquivoCert.Enabled = true;
//                        break;
//                    case 2:

//                        //TextField
//                        txtPinCert.Enabled = true;
//                        txtArquivoCert.Enabled = false;
//                        //Buttons
//                        btnArquivoCert.Enabled = false;
//                        break;

//                    default:
//                        break;
//                }
//            }
//            catch (Exception exception)
//            {
//                MessageBox.Show(exception.Message);
//                cbzTipoCertificadoNFe.SelectedIndex = -1;
//            }
//        }

//        private void cbcert_SelectedIndexChanged(object sender, EventArgs e)
//        {

//        }

//        private void btn_TestarEmail_Click(object sender, EventArgs e)
//        {
//            Selecionado.ServidorSMTP = txtServidorSMTP.Text;
//            Selecionado.PortaSmtp = Convert.ToInt32(txtPortaSmtp.Text);
//            Selecionado.UsuarioEmail = txtUsuarioEmail.Text;
//            Selecionado.SenhaEmail = txtSenhaEmail.Text;

//            MessageBox.Show(EnviarEmail(Selecionado, txtUsuarioEmail.Text, "Teste de Envio."), "Atenção!!");
//        }



//        private string EnviarEmail(Filial objFilial, string pEmailCliente, string corpoMensagem)
//        {
//            try
//            {
//                var message = new MailMessage(objFilial.UsuarioEmail, txtEmailNFe.Text, "Teste de envio de e-mail", "Teste de envio de e-mail");
//                var client = new SmtpClient(objFilial.ServidorSMTP, objFilial.PortaSmtp) { EnableSsl = chkGMailAutenticacao.Checked };
//                var credencial = new NetworkCredential(Selecionado.UsuarioEmail, Selecionado.SenhaEmail);
//                client.UseDefaultCredentials = false;
//                client.Credentials = credencial;
//                client.Send(message);

//                return "Email enviado com sucesso!";
//            }
//            catch (Exception ex)
//            {
//                return ex.Message;
//            }
//        }

//        private void cbCertificadoOpenAC_SelectedIndexChanged(object sender, EventArgs e)
//        {
//            txtValidadeCertificado.Text = GetExpiration(cbCertificadoOpenAC.SelectedValue?.ToString());
//            txtValidadeCertificado.Enabled = false;
//        }

//        private void btnSelecionaLogoEmpresa_Click(object sender, EventArgs e)
//        {
//            if (openFileDialog1.ShowDialog() == DialogResult.OK)
//            {
//                Selecionado.CaminhoLogoEmpresa = openFileDialog1.FileName;
//                txtCaminhoLogoEmpresa.Text = Selecionado.CaminhoLogoEmpresa;
//                pictureBox1.Image = new Bitmap(openFileDialog1.FileName);
//            }
//        }

//        private void button1_Click(object sender, EventArgs e)
//        {
//            if (openFileDialog1.ShowDialog() == DialogResult.OK)
//            {
//                Selecionado.CaminhoLogoNfe = openFileDialog1.FileName;
//                txtCaminhoLogoNfe.Text = Selecionado.CaminhoLogoNfe;
//                pictureBox2.Image = new Bitmap(openFileDialog1.FileName);
//            }
//        }

//        private void btnSelecionaLogoPrefeitura_Click(object sender, EventArgs e)
//        {
//            if (openFileDialog1.ShowDialog() == DialogResult.OK)
//            {
//                Selecionado.CaminhoLogoPrefeitura = openFileDialog1.FileName;
//                txtCaminhoLogoNFSe.Text = Selecionado.CaminhoLogoPrefeitura;
//                pictureBox3.Image = new Bitmap(openFileDialog1.FileName);
//            }
//        }

//        //private void btnExcluirCidade_Click(object sender, EventArgs e)
//        //{
//        //    ExecuteSafe(() =>
//        //    {
//        //        if (lstGerenciadorCidades.SelectedItems.Count < 1) return;

//        //        if (MessageBox.Show(@"Você tem certeza?", @"ACBrNFSe Demo", MessageBoxButtons.YesNo).Equals(DialogResult.No)) return;

//        //        var municipio = lstGerenciadorCidades.SelectedItems[0];
//        //        lstGerenciadorCidades.Items.Remove(municipio);
//        //        lstGerenciadorCidades();
//        //    });
//        //}
//    }
//}
