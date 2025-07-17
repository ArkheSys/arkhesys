﻿using System;
using cwkGestao.Modelo;
using Relatorio.Relatorios;
using Aplicacao.Base;
using System.IO;
using DevExpress.XtraReports.UI;

namespace Aplicacao.Modulos.Comercial.Impressao
{
    public class ImpressaoOrcamentoModelo7 : ImpressaoOrcamento
    {
        public string Email;
        public string ObservacaoEmail;
        public DestinoImpressao DestinoImpressao;
        public bool EnvioEmailCancelado = false;
        public ImpressaoOrcamentoModelo7(cwkGestao.Modelo.Pedido objPedido) : base(objPedido) { }

        public override void Imprimir()
        {
            DestinoImpressao = new Aplicacao.DestinoImpressaoPedido().ShowDialog();
            rptImpressaoPedido07 rpt = new rptImpressaoPedido07(objPedido.ID);
            DecideCaminhoImpressao(rpt);
        }

        public override void Imprimir(bool a)
        {
            if (a)
            {
                DestinoImpressao = DestinoImpressao.Impressora;
                rptImpressaoPedido07 rpt = new rptImpressaoPedido07(objPedido.ID);
                DecideCaminhoImpressao(rpt);
            }
        }

        private void DecideCaminhoImpressao(DevExpress.XtraReports.UI.XtraReport report)
        {
            try
            {
                switch (DestinoImpressao)
                {
                    case DestinoImpressao.Vídeo:
                        report.ShowPreview();
                        objPedido.BImpressa = true;
                        cwkGestao.Negocio.PedidoController.Instancia.Salvar(objPedido, cwkGestao.Modelo.Acao.Alterar);
                        break;
                    case DestinoImpressao.Email:
                        FormEnviaEmailPedido form = new FormEnviaEmailPedido(objPedido);
                        form.ShowDialog();
                        break;
                    case DestinoImpressao.Impressora:
                        report.PrintDialog();
                        objPedido.BImpressa = true;
                        cwkGestao.Negocio.PedidoController.Instancia.Salvar(objPedido, cwkGestao.Modelo.Acao.Alterar);
                        break;
                }
            }
            catch (Exception ex)
            {
                if (!EnvioEmailCancelado)
                    new FormErro(ex).ShowDialog();
            }
        }

        public String GeraPDF(DevExpress.XtraReports.UI.XtraReport report)
        {
            string Caminho = Modelo.cwkGlobal.DirApp;

            if (!Directory.Exists(Caminho += @"\OrcamentoPDF"))
                Directory.CreateDirectory(Caminho);

            Caminho += @"\Orcamento_" + objPedido.Numero.ToString() + ".pdf";

            report.ExportToPdf(Caminho);
            return Caminho;
        }

    }
}
