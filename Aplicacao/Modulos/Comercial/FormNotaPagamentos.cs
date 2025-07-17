using cwkGestao.Modelo;

using System;
using System.Collections.Generic;
using System.Windows.Forms;

namespace Aplicacao.Modulos.Comercial
{
    public partial class FormNotaPagamentos : Form
    {
        private List<string> fieldNames = new List<string> { "Parcela", "TipoDocumento", "Vencimento", "Valor" };

        private Nota nota = null;
        public FormNotaPagamentos(Nota notaSelecionada)
        {
            InitializeComponent();

            nota = notaSelecionada;
            PreencherTela();
        }

        private void PreencherTela()
        {
            gcNotaParcelas.DataSource = nota.NotaParcelas;
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
