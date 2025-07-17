using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

using cwkGestao.Modelo;

namespace Aplicacao.Modulos.Comercial
{
    public partial class FormFrenteCaixaDescartaItemcs : Form
    {
        private IList<FrenteCaixaNotaItem> lista = null;
        public FrenteCaixaNotaItem itemDescartar = null;

        public FormFrenteCaixaNota TelaFrenteCaixa;
        public FormFrenteCaixaDescartaItemcs(IList<FrenteCaixaNotaItem> _lista)
        {
            InitializeComponent();

            lista = _lista;
        }

        private void BtnDescartar_Click(object sender, EventArgs e)
        {
            Descartar();
        }

        private void Descartar()
        {
            int numero = int.Parse(txtNumero.Text);
            if (numero >= 1 && lista.Count() > 0)
            {
                itemDescartar = null;
                foreach (var item in lista)
                {
                    if (item.Sequencia == numero)
                    {
                        itemDescartar = item;
                        itemDescartar.MotivoCancelamento = txtMotivo.Text;
                        itemDescartar.Cancelado = true;
                        break;
                    }
                }
                if (itemDescartar != null)
                    Close();
                else
                {
                    MessageBox.Show("Informe um número válido !", "Item não encontrado !", MessageBoxButtons.OK, MessageBoxIcon.Information);
                    txtNumero.Focus();
                }
            }
            else
            {
                MessageBox.Show("Informe um número válido !", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
                txtNumero.Focus();
            }
        }

        private void BtnCancelar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormFrenteCaixaDescartaItemcs_Shown(object sender, EventArgs e)
        {
            if (lista != null)
                txtNumero.Text = lista[lista.Count() - 1].Sequencia.ToString();

            txtNumero.Focus();
        }

        private void TxtNumero_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == Convert.ToChar(Keys.Enter))
                Descartar();
        }
    }
}
