using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;

namespace Relatorio.Relatorios
{
    public partial class rptOrcamentoDimaggio : DevExpress.XtraReports.UI.XtraReport
    {
        public rptOrcamentoDimaggio(int ID)
        {
            InitializeComponent();
            this.IDPedido.Value = ID;
            try
            {
                this.dsImpressaoDimaggio1.EnforceConstraints = false;
                dttaPedidoDimaggio.ClearBeforeFill = true;
                try
                {
                    dttaPedidoDimaggio.GetData(ID);
                }
                catch (Exception)
                {
                }
                rptOrcamentoDimaggioSubRel subrel = new rptOrcamentoDimaggioSubRel(ID);
                xrSubreport1.ReportSource = subrel;
            }
            catch (Exception e)
            {
                
                throw e;
            }
        }
    }
}
