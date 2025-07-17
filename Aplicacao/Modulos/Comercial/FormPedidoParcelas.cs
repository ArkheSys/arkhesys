using cwkGestao.Modelo;

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Aplicacao.Modulos.Comercial
{
    public partial class FormPedidoParcelas : Form
    {
        private List<string> fieldNames = new List<string> { "Parcela", "TipoDocumento", "Vencimento", "Valor" };

        private Pedido pedido = null;
        public FormPedidoParcelas(Pedido pedidoSelecionado)
        {
            InitializeComponent();

            pedido = pedidoSelecionado;
            PreencherTela();
        }

        private void PreencherTela()
        {
            gcNotaParcelas.DataSource = pedido.Parcelas;
            gcNotaParcelas.RefreshDataSource();
            gvNotaParcelas.RefreshData();
        }

        private void FormNotaPagamentos_Shown(object sender, EventArgs e)
        {
            foreach (DevExpress.XtraGrid.Columns.GridColumn column in gvNotaParcelas.Columns)
                column.Visible = fieldNames.Contains(column.FieldName);
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            Close();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    Close();
                    break;
            }
            return base.ProcessDialogKey(keyData);
        }
    }
}
