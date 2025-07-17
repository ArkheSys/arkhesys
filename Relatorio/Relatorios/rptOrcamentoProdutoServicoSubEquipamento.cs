using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using DevExpress.XtraReports.UI;
using System.IO;

using System.Reflection;

namespace Relatorio.Relatorios
{
    public partial class rptOrcamentoProdutoServicoEquipamento : DevExpress.XtraReports.UI.XtraReport
    {
        public rptOrcamentoProdutoServicoEquipamento(int IDPedido, out int totalEquipamento)
        {
            InitializeComponent();
            try
            {
                totalEquipamento = 0;
                var DataTable = dsOrcamentoProdutoServicoEquipamentoTableAdapter.GetData(IDPedido);

                for (int i = 0; i < DataTable.Rows.Count; i++)
                    totalEquipamento += Convert.ToInt32(DataTable.Rows[i]["Total"]);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.Message);
            }
        }
    }
}
