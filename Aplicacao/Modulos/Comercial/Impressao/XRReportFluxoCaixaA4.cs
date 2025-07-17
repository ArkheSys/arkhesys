using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Negocio;
using cwkGestao.Negocio.Padroes;

using System;
using System.Collections.Generic;
using System.Linq;

namespace Aplicacao.Modulos.Comercial.Impressao
{
    public partial class XRReportFluxoCaixaA4 : DevExpress.XtraReports.UI.XtraReport
    {
        public XRReportFluxoCaixaA4(FluxoCaixa Fluxo)
        {
            InitializeComponent();

            Configuracao objConfiguracao = ConfiguracaoController.Instancia.GetConfiguracao();

            TXT_EMPRESA.Text = objConfiguracao.FilialFrenteCaixa.Nome.ToUpper();
            //TXT_ENDERECO.Text = $"{objConfiguracao.FilialFrenteCaixa.Endereco},{objConfiguracao.FilialFrenteCaixa.Numero} {objConfiguracao.FilialFrenteCaixa.Cidade.Nome}-{objConfiguracao.FilialFrenteCaixa.Cidade.UF.Sigla}".ToUpper();
            //TXT_TELEFONE.Text = $"CNPJ/CPF: {objConfiguracao.FilialFrenteCaixa.CNPJ} FONE: {objConfiguracao.FilialFrenteCaixa.Telefone}";
            //TXT_INSCRICAOESTADUAL.Text = $"INSC.ESTADUAL: {objConfiguracao.FilialFrenteCaixa.Inscricao}";

            ovTXT_DataAbertura.Text = Fluxo.DataAbertura.ToString();
            ovTXT_DataFechamento.Text = Fluxo.DataFechamento.ToString();
            ovTXT_DataEmissao.Text = DateTime.Now.ToString();
            ovTXT_UsuarioAbertura.Text = Fluxo.IDUsuario.HasValue ? Cw_usuarioController.Instancia.LoadObjectById(Fluxo.IDUsuario.Value)?.Nome : "";
            ovTXT_ValorRecebido.Text = Fluxo.ValorCaixa?.ToString("n2");
            //ovTXT_ValorFechamento.Text = Fluxo.ValorFechamento?.ToString("n2");
            ovTXT_Observacao.Text = string.IsNullOrEmpty(Fluxo.Observacao) ? "<Não Informado>" : Fluxo.Observacao;
            ovTXT_ObservacaoFechamento.Text = string.IsNullOrEmpty(Fluxo.ObservacaoFechamento) ? "<Não Informado>" : Fluxo.ObservacaoFechamento;

            /* Sub-Relatório de Sangrias */
            var Sangrias = SangriaCaixaController.Instancia.GetSangriasPorFluxoCaixa(Fluxo.ID);
            XRSubReport_Sangria.ReportSource = new XRReportFluxoCaixa_SangriaA4(Sangrias.Where(o => (o.Retirada ?? 0) == 0).ToList());
            XRSubReport_Retirada.ReportSource = new XRReportFluxoCaixa_SangriaA4(Sangrias.Where(o => (o.Retirada ?? 0) == 1).ToList());
            XRSubReport_BaixasRecebidas.ReportSource = new XRReportFluxoCaixa_BaixasRecebidasA4(Fluxo.ID);

            SubBandOutrasBaixas.Visible = objConfiguracao.ImprimirOutrasBaixas == 1;

            /* Sub-Relatórios de Suprimentos */
            var suprimentos = SuprimentoCaixaController.Instancia.GetSuprimentosPorFluxoCaixa(Fluxo.ID);
            XRSubReport_Suprimentos.ReportSource = new XRReportFluxoCaixa_SuprimentosA4(suprimentos);

            List<pxFrenteCaixaNota> Itens = SangriaCaixaController.Instancia.GetRateiosPorFormaDePagamento(Fluxo.ID).ToList();
            foreach (var Item in Itens)
            {
                var DataRow = ovDS_Dados.Tables[0].NewRow();
                DataRow["DESCFORMAPAGAMENTO"] = Item.FormaDePagamento;
                DataRow["TOTALPAGO"] = Item.ValorPago;
                ovDS_Dados.Tables[0].Rows.Add(DataRow);
            }

            TXT_DATAIMPRESSAO.Text = DateTime.Now.ToString();

            PreencherTotalizadores(Sangrias, suprimentos, Itens, Fluxo);
        }

        private void PreencherTotalizadores(List<SangriaCaixa> Sangrias, List<SuprimentoCaixa> suprimentos, List<pxFrenteCaixaNota> recebimentos, FluxoCaixa Fluxo)
        {
            var movimentosRecebimentos = MovCaixaController.Instancia.GetMovimentosPorFluxoCaixa(Fluxo.ID).ToList();

            decimal totalSangria = Sangrias.Where(o => (o.Retirada ?? 0) == 0).ToList().Sum(s => s.Valor) ?? 0;
            decimal totalRetirada = Sangrias.Where(o => (o.Retirada ?? 0) == 1).ToList().Sum(s => s.Valor) ?? 0;
            decimal totalSuprimentos = suprimentos.Sum(s=> s.Valor)??0;
            decimal totalRecebido = recebimentos.Sum(r => r.ValorPago);
            decimal totalRecebimentos = SubBandOutrasBaixas.Visible? movimentosRecebimentos.Sum(v=> v.Valor): 0;

            var detalheFechamento = SangriaCaixaController.Instancia.GetDetalhesFechamento(Fluxo.ID);

            ovTXT_TotalCreditoGerado.Text = detalheFechamento.CreditoGerado.ToString("c2");
            ovTXT_TotalDevolucoes.Text = detalheFechamento.TotalDevolucoes.ToString("n1");
            ovTXT_TotalItensDevolvidos.Text = detalheFechamento.ItensDevolvidos.ToString("n1");
            ovTXT_TotalVendas.Text = detalheFechamento.TotalVendas.ToString("n1");
            ovTXT_ValorAbertura.Text = Fluxo.ValorCaixa?.ToString("c2");
            ovTXT_ValorRecebido.Text = totalRecebido.ToString("c2");
            ovTXT_ValorSangria.Text = totalSangria.ToString("c2");
            ovTXT_ValorSuprimento.Text = totalSuprimentos.ToString("c2");
            ovTXT_ValorFechamento.Text = (totalRecebido + (Fluxo.ValorCaixa ?? 0) + totalSuprimentos + totalRecebimentos - totalSangria - totalRetirada).ToString("c2");
        }
    }
}
