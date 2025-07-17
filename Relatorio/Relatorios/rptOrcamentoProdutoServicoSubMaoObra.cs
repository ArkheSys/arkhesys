using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.IO;
using System.Reflection;

namespace Relatorio.Relatorios
{
    public partial class rptOrcamentoProdutoServicoMaoObra : DevExpress.XtraReports.UI.XtraReport
    {

        public rptOrcamentoProdutoServicoMaoObra(int IDPedido, out int totalMaoObra)
        {
            InitializeComponent();
            try
            {
                totalMaoObra = 0;
                var DataTable = dsOrcamentoProdutoServicoMaoObraTableAdapter.GetData(IDPedido);

                for (int i = 0; i < DataTable.Rows.Count; i++)
                    totalMaoObra += Convert.ToInt32(DataTable.Rows[i]["Total"]);               
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }


    }
}
