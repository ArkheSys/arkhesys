using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Relatorio.Relatorios.Obras_Fundacoes
{
    public partial class rptMedicaoServico : DevExpress.XtraReports.UI.XtraReport
    {
        public rptMedicaoServico()
        {
            InitializeComponent();
        }

        public void SetTotais(decimal valorMobilizacao, decimal valorProjetoConsultoria, decimal valorTotal)
        {
            lblValorMobilizacao.Text = String.Format("{0:c2}", valorMobilizacao);
            lblValorProjetoConsultoria.Text = String.Format("{0:c2}", valorProjetoConsultoria);
            lblSubTotalMobilizacao.Text = lblValorMobilizacao.Text;
            lblSubTotalProjetoConsultoria.Text = lblValorProjetoConsultoria.Text;
            lblTotal.Text = String.Format("{0:c2}", valorTotal);
        }
    }
}
