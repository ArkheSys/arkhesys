using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using cwkGestao.Modelo;
using cwkGestao.Negocio;

using MetroFramework.Forms;

namespace Aplicacao.Modulos.Comercial
{
    public partial class FormFrenteCaixaNotaTabelaPreco : MetroForm
    {
        public TabelaPrecoProduto Selecionado = null;
        public TabelaPreco SelecionadoTabelaPreco = null;
        private Produto p = null;
        private Boolean usarSomenteTabelaPreco = false;

        public FormFrenteCaixaNotaTabelaPreco(Produto p)
        {
            InitializeComponent();
            this.p = p;
            ListBox.Items.Clear();

            foreach (var item in p.TabelaPrecoProdutos)
                ListBox.Items.Add(item);

            ListBox.SelectedIndex = 0;
            ListBox.Focus();
        }

        public FormFrenteCaixaNotaTabelaPreco(IList<TabelaPreco> lstTabelaPreco, Boolean pUsarSomenteTabelaPreco)
        {
            InitializeComponent();
            //this.p = p;
            ListBox.Items.Clear();

            foreach (var item in lstTabelaPreco)
                ListBox.Items.Add(item);

            ListBox.SelectedIndex = 0;
            ListBox.Focus();
            usarSomenteTabelaPreco = pUsarSomenteTabelaPreco;
        }

        private void ListBox_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                if (!usarSomenteTabelaPreco)
                {
                    Selecionado = ListBox.SelectedItem as TabelaPrecoProduto;
                }
                else
                {
                    SelecionadoTabelaPreco = ListBox.SelectedItem as TabelaPreco;
                }
                Close();
            }
        }

        private void ListBox_DoubleClick(object sender, EventArgs e)
        {
            if (!usarSomenteTabelaPreco)
            {
                Selecionado = ListBox.SelectedItem as TabelaPrecoProduto;
            }
            else
            {
                SelecionadoTabelaPreco = ListBox.SelectedItem as TabelaPreco;
            }
            Close();
        }

        private void ListBox_Format(object sender, ListControlConvertEventArgs e)
        {
            if ((e.ListItem != null)&&(!usarSomenteTabelaPreco))
            {
                var Preco = ProdutoController.Instancia.getPreco(p, (e.ListItem as TabelaPrecoProduto)?.TabelaPreco, TipoPrecoType.Normal);
                e.Value = $"{(e.ListItem as TabelaPrecoProduto)} - {Preco:c2}";
            }
        }
    }
}
