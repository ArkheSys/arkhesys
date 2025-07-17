using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Reflection;

namespace Relatorio.Relatorios
{
    public partial class rptOrcamentoProdutoServicoSubMaterial : DevExpress.XtraReports.UI.XtraReport
    {
        public rptOrcamentoProdutoServicoSubMaterial(int IDPedido, out int totalMaterial)
        {
            InitializeComponent();
            try
            {
                totalMaterial = 0;
                var DataTable = dsOrcamentoProdutoServicoMaterialTableAdapter.GetData(IDPedido);

                for (int i = 0; i < DataTable.Rows.Count; i++)
                    totalMaterial += Convert.ToInt32(DataTable.Rows[i]["Total"]);               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
