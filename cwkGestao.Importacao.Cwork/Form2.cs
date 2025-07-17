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
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
            txtDataSource.Text = @".\cwork";
            txtBancoDeDados.Text = "cwkAdministracao";
            txtUsuario.Text = "sa";
            txtSenha.Text = "cwork#0110";
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
                ImportacaoOS imp = new ImportacaoOS();
                imp.Importar(builder.ConnectionString);
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
