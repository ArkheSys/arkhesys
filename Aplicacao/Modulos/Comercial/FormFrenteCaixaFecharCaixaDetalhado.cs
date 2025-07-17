using Aplicacao.Modulos.Comercial.ControlUser;
using Aplicacao.Modulos.Comercial.Impressao;
using Aplicacao.Modulos.Rel;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Negocio;
using cwkGestao.Negocio.Padroes;
using cwkGestao.Negocio.Utils;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Windows.Forms;

namespace Aplicacao.Modulos.Comercial
{
    public partial class FormFrenteCaixaFecharCaixaDetalhado : Form
    {
        private FluxoCaixa Fluxo = null;
        public bool Fechou = false;

        public FormFrenteCaixaFecharCaixaDetalhado(FluxoCaixa _Fluxo)
        {
            InitializeComponent();
            Fluxo = _Fluxo;

            IniciarFechamentoCaixa();
        }

        private void IniciarFechamentoCaixa()
        {
            /* Dados de Abertura do Caixa */
            TXT_DataHora.Text = Fluxo.DataAbertura.ToString();
            TXT_Usuario.Text = Modelo.cwkGlobal.objUsuarioLogado.Login;

            /* Dados de Fechamento do Caixa */
            TXT_DataFechamento.Text = DateTime.Now.ToString();
            TXT_Observacao.Text = Fluxo.ObservacaoFechamento;
            TXT_ValorAbertura.EditValue = SangriaCaixaController.Instancia.GetSaldoAtualVendasComSangriaESuprimentos(Modelo.cwkGlobal.objUsuarioLogado.Id, Fluxo.ID);

            IList<pxFrenteCaixaNota> Itens = SangriaCaixaController.Instancia.GetRateiosPorFormaDePagamento(Fluxo.ID);
            foreach (var Item in Itens)
            {
                FLP_Panel.Controls.Add(new UC_FormaPagamento(Item.IDFormaDePagamento, Item.FormaDePagamento, Item.ValorPago, 0));
            }
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void SbQuantidade_Click(object sender, EventArgs e)
        {
            FecharCaixa();
        }

        private void FecharCaixa()
        {
            List<FechamentoCaixaRateio> Rateios = new List<FechamentoCaixaRateio>();
            foreach (var Item in FLP_Panel.Controls)
            {
                Rateios.Add(new FechamentoCaixaRateio
                {
                    IDFormaDePagamento = ((UC_FormaPagamento)Item).IDFormaDePagamento,
                    IDFechamentoCaixa = Fluxo.ID,
                    ValorCaixa = ((UC_FormaPagamento)Item).ValorCaixa,
                    ValorInformado = Convert.ToDecimal(((UC_FormaPagamento)Item).TXT_Valor.EditValue)
                });
            }

            Fluxo.DataFechamento = DateTime.Now;
            Fluxo.ValorFechamento = Rateios.Sum(o => o.ValorInformado);
            Fluxo.ObservacaoFechamento = TXT_Observacao.Text;
            Fluxo.Aberto = 0;
            Fechou = true;

            // 05/07/23 -> Rotina comentada.
            //if (Fluxo.ValorFechamento == 0)
            //{
            //    MessageBox.Show("O Valor do fechamento deve ser maior que zero.", "INFORMAÇÃO", MessageBoxButtons.OK);
            //    return;
            //}

            //var ValorAtualConsiderandoSangriaESuprimento = SangriaCaixaController.Instancia.GetSaldoAtualVendasComSangriaESuprimentos(Modelo.cwkGlobal.objUsuarioLogado.Id, Fluxo.ID);
            //
            //if (Fluxo.ValorFechamento < ValorAtualConsiderandoSangriaESuprimento)
            //{
            //    MessageBox.Show("O Valor informado é menor que o valor do caixa.", "INFORMAÇÃO", MessageBoxButtons.OK);
            //    return;
            //}

            FluxoCaixaController.Instancia.Salvar(Fluxo, Acao.Alterar);
            foreach (var Item in Rateios)
                FechamentoCaixaRateioController.Instancia.Salvar(Item, Acao.Incluir);

            EnviarEmailFechamento();
            Close();
        }

        private void EnviarEmailFechamento()
        {
            try
            {
                Configuracao objConfiguracao = ConfiguracaoController.Instancia.GetConfiguracao();

                /* Validações sobre a configuração para enviar o email na abertura do caixa */
                if (objConfiguracao?.EnviarEmailPDV == 0)
                    return;

                if (string.IsNullOrEmpty(objConfiguracao?.EmailPDV))
                    return;

                string CorpoEmail = $@"<html>
                                          <body>
                                             <p>Data de fechamento: {Fluxo.DataFechamento.ToString()}</p>
                                             <p>Usuário: {Modelo.cwkGlobal.objUsuarioLogado.Login}</p>
                                             <p>{objConfiguracao.FilialFrenteCaixa.Fantasia}</p>
                                          </body>
                                       </html>";

                foreach (string Dest in objConfiguracao?.EmailPDV.Split(';'))
                    if (!EmailUtil.EnviarEmail(Dest,
                                               $"Fechamento do Fluxo de Caixa do PDV {DateTime.Now.ToString()}",
                                               CorpoEmail,
                                               null,
                                               objConfiguracao.FilialFrenteCaixa,
                                               null,
                                               false,
                                               GerarRelatorios(objConfiguracao).ToArray()))
                        throw new Exception("Não foi possível enviar o E-mail.");
            }
            catch (Exception e)
            {
                MessageBox.Show(e.Message, "INFORMAÇÃO", MessageBoxButtons.OK);
            }
        }

        private List<AnexoEmail> GerarRelatorios(Configuracao objConfiguracao)
        {
            var emails = new List<AnexoEmail>();

            XtraRelatorioPDVDetalhado RelatorioDetalhado = new XtraRelatorioPDVDetalhado(Fluxo);
            MemoryStream STRelDetalhado = new MemoryStream();
            RelatorioDetalhado.ExportToPdf(STRelDetalhado);
            emails.Add(new AnexoEmail { Anexo = STRelDetalhado, NomeAnexo = "RelatorioFechamentoCaixaPDV_Detalhado .pdf" });

            if (objConfiguracao.TipoEnvioFechamentoPDV > 0)
            {
                XRReportFluxoCaixaCego RelatorioFluxoCaixaCego = new XRReportFluxoCaixaCego(Fluxo);
                MemoryStream STRelCego = new MemoryStream();
                RelatorioFluxoCaixaCego.ExportToPdf(STRelCego);
                emails.Add(new AnexoEmail { Anexo = STRelCego, NomeAnexo = "RelatorioFechamentoCaixaCegoPDV.pdf" });

                XRReportFluxoCaixaA4 RelatorioA4 = new XRReportFluxoCaixaA4(Fluxo);
                MemoryStream STRelA4 = new MemoryStream();
                RelatorioA4.ExportToPdf(STRelA4);
                emails.Add(new AnexoEmail { Anexo = STRelA4, NomeAnexo = "RelatorioFechamentoCaixaPDV_A4.pdf" });

                XtraRelatorioPDVDetalhadoItens RelatorioItens = new XtraRelatorioPDVDetalhadoItens(Fluxo);
                MemoryStream STRelItens = new MemoryStream();
                RelatorioItens.ExportToPdf(STRelItens);
                emails.Add(new AnexoEmail { Anexo = STRelItens, NomeAnexo = "RelatorioFechamentoCaixaPDV_Itens.pdf" });

                XtraRelatorioPDVResumido RelatorioResumido = new XtraRelatorioPDVResumido(Fluxo);
                MemoryStream STRelResumido = new MemoryStream();
                RelatorioResumido.ExportToPdf(STRelResumido);
                emails.Add(new AnexoEmail { Anexo = STRelResumido, NomeAnexo = "RelatorioFechamentoCaixaPDV_Resumido .pdf" });

                XRReportFluxoCaixa RelatorioFluxoCaixa = new XRReportFluxoCaixa(Fluxo);
                MemoryStream STRel = new MemoryStream();
                RelatorioFluxoCaixa.ExportToPdf(STRel);
                emails.Add(new AnexoEmail { Anexo = STRel, NomeAnexo = "RelatorioFechamentoCaixaPDV.pdf" });
            }

            return emails;
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    Close();
                    break;
                case Keys.Enter:
                    FecharCaixa();
                    break;
            }
            return base.ProcessDialogKey(keyData);
        }

        private void label6_Click(object sender, EventArgs e)
        {

        }

        private void TXT_Observacao_TextChanged(object sender, EventArgs e)
        {

        }
    }
}
