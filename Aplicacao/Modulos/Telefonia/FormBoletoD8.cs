using Aplicacao.Modulos.Rel;
using Aplicacao.ValueObjects;

using cwkGestao.Modelo;
using cwkGestao.Modelo.Util;
using cwkGestao.Negocio;

using DevExpress.XtraReports.UI;

using Newtonsoft.Json;

using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aplicacao.Modulos.Telefonia
{
    public partial class FormBoletoD8 : Form
    {
        #region Propriedades

        private Tel_FaturaD8 telFatura = null;
        private Acao acao;

        #endregion

        #region Construtores

        public FormBoletoD8(Tel_FaturaD8 telFatura, Acao acao)
        {
            InitializeComponent();

            if (telFatura == null)
            {
                telFatura = new Tel_FaturaD8
                {
                    DataCriacao = DateTime.Now,
                    DataVencimento = DateTime.Now
                };
                acao = Acao.Incluir;
            }

            this.telFatura = telFatura;
            this.acao = acao;
        }

        #endregion

        #region Eventos

        private void btnCancelar_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Deseja realmente sair desta tela?", "Fatura Arrecadação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.No) return;
            Close();
        }

        private void btnGravarFatura_ClickAsync(object sender, EventArgs e)
        {
            #region Validações

            #endregion

            #region Salvar Tel_FaturaD8

            telFatura.Codigo = txtCodigo.Text.ToInt();
            telFatura.DataCriacao = Convert.ToDateTime(txtDataCriacao.EditValue);
            telFatura.DataVencimento = Convert.ToDateTime(txtDataVencimento.EditValue);
            telFatura.NumeroDocumento = txtNumeroDocumento.EditValue.ToString();
            telFatura.EspecieDocumento = txtEspecieDocumento.EditValue.ToString();
            telFatura.Valor = Convert.ToDecimal(txtValor.EditValue);
            telFatura.Observacao = txtObservacao.Text;
            telFatura.Filial = lkpFilial.Selecionado as Filial;
            telFatura.Cliente = lkpCliente.Selecionado as Tel_Cliente;
            telFatura.Servico = lkpServico.Selecionado as Tel_Servico;
            telFatura.LinhaDigitavel = GuiaArrecadacao.Obter(telFatura, false);
            telFatura.LinhaDigitavelComDv = GuiaArrecadacao.Obter(telFatura, true);

            var qrCode = GerarQrCode();
            if (qrCode != null)
            {
                telFatura.Location = qrCode.cobv.loc.location;
                telFatura.PixCopiaECola = qrCode.emv;
                telFatura.JsonPix = JsonConvert.SerializeObject(qrCode);
                telFatura.Base64Image = qrCode.base64;
            }

            Tel_FaturaD8Controller.Instancia.Salvar(telFatura, acao);

            #endregion

            #region Documento A Receber

            #endregion            

            Close();
            MessageBox.Show("Registro salvo com sucesso!", "Fatura Arrecadação", MessageBoxButtons.OK, MessageBoxIcon.Information);

            if (MessageBox.Show("Deseja gerar o Boleto D8?", "Atenção!", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
            {
                // Gerar o Relatório
                var rel = new XtraImpressaoBoletoD8(telFatura);
                var tool = new ReportPrintTool(rel);
                tool.ShowPreviewDialog();
            }
        }

        private ResultadoCriacaoPix GerarQrCode()
        {
            try
            {
                var telConfig = Tel_ConfiguracaoController.Instancia.GetConfiguracao();
                var txID = $"APIPixBradesco{GuiaArrecadacao.ObterCampoLivre(telFatura).Substring(0, 21)}";
                var tokenBradesco = TokenBracescoUtils.GetToken($"{telConfig.UrlBanco}/auth/server/oauth/token", telConfig.UrlCertificado, telConfig.ClientIDBanco, telConfig.SecretIDBanco);
                if (string.IsNullOrEmpty(tokenBradesco.AccessToken))
                    throw new Exception("Erro ao buscar o Token do bradesco.");

                var DocumentoCpfCnpj = telFatura.Cliente.CPF_CNPJ.OnlyNumbers();
                var resultCobranca = TokenBracescoUtils.GerarCobrancaPIX($"{telConfig.UrlBanco}/v2/cobv-emv/{txID}", telConfig.UrlCertificado, tokenBradesco, new CobrancaPIXBradesco
                {
                    Valor = new CobrancaPIXBradescoValor
                    {
                        Original = telFatura.Valor.ToString("n2").Replace(".", "").Replace(",", ".")
                    },
                    Calendario = new CobrancaPIXBradescoCalendario
                    {
                        DataVencimento = telFatura.DataVencimento.ToString("yyyy-MM-dd"),
                        ValidadeAposVencimento = 0
                    },
                    Devedor = new CobrancaPIXBradescoDevedor
                    {
                        CPF = DocumentoCpfCnpj.Length == 11 ? DocumentoCpfCnpj : null,
                        CNPJ = DocumentoCpfCnpj.Length == 14 ? DocumentoCpfCnpj : null,
                        Nome = telFatura.NomeCliente
                    },
                    Chave = telConfig.ChavePix
                });

                return resultCobranca;
            }
            catch (Exception Ex)
            {
                MessageBox.Show(Ex.Message, "Atencao, problemas ao gerar QRCode!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                return null;
            }
        }

        private void lkbFilial_Click(object sender, EventArgs e)
        {
            var grid = new GridGenerica<Filial>(FilialController.Instancia.GetAll(), new FormFilial(), (Filial)lkpFilial.Selecionado, false)
            {
                FocoNaGrid = true,
                Selecionando = true
            };

            if (cwkControleUsuario.Facade.ControleAcesso(grid))
                grid.ShowDialog();

            if (grid.Selecionado != null)
                lkpFilial.Localizar(grid.Selecionado.ID);

            lkpFilial.Focus();
        }

        private void lkbCliente_Click(object sender, EventArgs e)
        {
            var grid = new GridGenerica<Tel_Cliente>(Tel_ClienteController.Instancia.GetAll(), new FormTel_Cliente(), (Tel_Cliente)lkpCliente.Selecionado, false)
            {
                FocoNaGrid = true,
                Selecionando = true
            };

            if (cwkControleUsuario.Facade.ControleAcesso(grid))
                grid.ShowDialog();

            if (grid.Selecionado != null)
                lkpCliente.Localizar(grid.Selecionado.ID);

            lkpCliente.Focus();
        }

        private void lkbServico_Click(object sender, EventArgs e)
        {
            var grid = new GridGenerica<Tel_Servico>(Tel_ServicoController.Instancia.GetAll(), new FormTel_Servico(), (Tel_Servico)lkpServico.Selecionado, false)
            {
                FocoNaGrid = true,
                Selecionando = true
            };

            if (cwkControleUsuario.Facade.ControleAcesso(grid))
                grid.ShowDialog();

            if (grid.Selecionado != null)
                lkpServico.Localizar(grid.Selecionado.ID);

            lkpServico.Focus();
        }

        private void FormBoletoD8_Load(object sender, EventArgs e)
        {
            lkpFilial.Sessao = FilialController.Instancia.getSession();
            lkbFilial.SubFormType = typeof(FormFilial);
            lkpFilial.Exemplo = new Filial();

            lkpCliente.Sessao = Tel_ClienteController.Instancia.getSession();
            lkbCliente.SubFormType = typeof(FormTel_Cliente);
            lkpCliente.Exemplo = new Tel_Cliente();
            lkpCliente.CamposRestricoesAND = new List<string>();
            lkpCliente.CamposRestricoesOR = new List<string>();

            lkpServico.Sessao = Tel_ServicoController.Instancia.getSession();
            lkbServico.SubFormType = typeof(FormTel_Servico);
            lkpServico.Exemplo = new Tel_Servico();

            preencherTela();
        }

        #endregion

        #region Métodos

        private void preencherTela()
        {
            txtDataCriacao.EditValue = telFatura.DataCriacao;
            txtDataVencimento.EditValue = telFatura.DataVencimento;
            txtNumeroDocumento.EditValue = telFatura.NumeroDocumento;
            txtEspecieDocumento.EditValue = telFatura.EspecieDocumento;
            txtValor.EditValue = telFatura.Valor;
            txtObservacao.Text = telFatura.Observacao;

            if (telFatura.Cliente != null)
                lkpCliente.Localizar(telFatura.Cliente.ID);

            if (telFatura.Servico != null)
                lkpServico.Localizar(telFatura.Servico.ID);

            if (telFatura.Filial != null)
                lkpFilial.Localizar(telFatura.Filial.ID);

            if (acao == Acao.Incluir)
                txtCodigo.Text = Tel_FaturaD8Controller.Instancia.MaxCodigo().ToString();
        }

        #endregion
    }
}
