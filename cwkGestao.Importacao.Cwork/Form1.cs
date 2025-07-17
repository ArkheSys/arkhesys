using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace cwkGestao.Importacao.Cwork
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
            txtDataSource.Text = @".\cwork";
            txtBancoDeDados.Text = "cwkAdministracao";
            txtUsuario.Text = "sa";
            txtSenha.Text = "cwork#0110";
            txtUnidade.EditValue = 1;
            txtPais.EditValue = 30;
            txtFilial.EditValue = 1;
            txtTabelaPreco.EditValue = 1;
            txtFornecedor.EditValue = 1;
            txtPortador.EditValue = 1;
            txtTipoNota.EditValue = 1;
        }

        private void sbImportar_Click(object sender, EventArgs e)
        {
            SqlConnectionStringBuilder builder = new SqlConnectionStringBuilder();
                builder.UserID = txtUsuario.Text;
                builder.Password = txtSenha.Text;
                builder.InitialCatalog = txtBancoDeDados.Text;
                builder.DataSource = txtDataSource.Text;

                try
                {
                    ImportacaoBasicos imp = new ImportacaoBasicos();
                    imp.Importar(builder.ConnectionString, Convert.ToInt32(txtFilial.EditValue), Convert.ToInt32(txtTabelaPreco.EditValue), Convert.ToInt32(txtFornecedor.EditValue)
                        , Convert.ToInt32(txtUnidade.EditValue), Convert.ToInt32(txtPais.EditValue), Convert.ToInt32(txtPortador.EditValue), Convert.ToInt32(txtTipoNota.EditValue));
                    MessageBox.Show("Importação efetuada com sucesso.");
                    this.Close();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message + Environment.NewLine + ex.StackTrace);
                }
        }
    }
}
