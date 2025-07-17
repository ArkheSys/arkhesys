using cwkGestao.Modelo;
using cwkGestao.Negocio;

using System;
using System.Drawing;
using System.Windows.Forms;

namespace Aplicacao.Modulos.Comercial
{
    public partial class FormFrenteCaixaNotaItemDesconto : Form
    {
        public FrenteCaixaNotaItem Item;
        public bool Salvou = false;
        public FormFrenteCaixaNotaItemDesconto(FrenteCaixaNotaItem item)
        {
            InitializeComponent();

            var Config = ConfiguracaoController.Instancia.GetConfiguracao();
            if (Config.FocarQuantidadeDescontoPDV == 0)
            {
                txtQuantidade.TabIndex = 999;
                txtQuantidade.TabStop = false;
            }


            Item = item;
            PreencherTela();
        }

        private void PreencherTela()
        {
            txtQuantidade.EditValue = Item.Quantidade;
            txtValorUnitario.EditValue = Item.Valor;
            txtPercentual.EditValue = Item.PercDesconto;
            txtDesconto.EditValue = Item.Desconto;
            txtValorTotal.EditValue = (Item.Valor * Item.Quantidade) - Item.Desconto;
            txtValorFinal.EditValue = Item.Total;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Salvou = true;
            Close();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            Salvou = false;
            Close();
        }

        private void txtDesconto_Leave(object sender, EventArgs e)
        {
            Item.Desconto = Convert.ToDecimal(txtDesconto.EditValue);
            Item.PercDesconto = (Item.Desconto / Item.Total) * 100;
            Item.Total = (Item.Valor * Item.Quantidade) - Item.Desconto;
            PreencherTela();
        }

        private void txtPercentual_Leave(object sender, EventArgs e)
        {
            CalcularPercentualDesconto();
        }

        private void CalcularPercentualDesconto()
        {
            Item.PercDesconto = Convert.ToDecimal(txtPercentual.EditValue);
            Item.Desconto = (Item.Valor * Item.Quantidade) * (Item.PercDesconto / 100);
            Item.Total = (Item.Valor * Item.Quantidade) - Item.Desconto;
            PreencherTela();
        }

        protected override bool ProcessDialogKey(Keys keyData)
        {
            switch (keyData)
            {
                case Keys.Escape:
                    Salvou = false;
                    Close();
                    break;
            }

            return base.ProcessDialogKey(keyData);
        }

        private void txtQuantidade_Leave(object sender, EventArgs e)
        {
            Item.Quantidade = Convert.ToDecimal(txtQuantidade.EditValue);
            Item.PercDesconto = Convert.ToDecimal("0,00");
            Item.Desconto = Convert.ToDecimal("0,00");
            Item.Total = (Item.Valor * Item.Quantidade);
            PreencherTela();
        }

        private void button1_Enter(object sender, EventArgs e)
        {
            button1.BackColor = Color.LightGray;
            button1.ForeColor = Color.Black;
        }

        private void button1_Leave(object sender, EventArgs e)
        {
            button1.BackColor = Color.White;
            button1.ForeColor = Color.Black;
        }

        private void button2_Enter(object sender, EventArgs e)
        {
            button2.BackColor = Color.LightGray;
            button2.ForeColor = Color.Black;
        }

        private void button2_Leave(object sender, EventArgs e)
        {
            button2.BackColor = Color.White;
            button2.ForeColor = Color.Black;
        }

        private void FormFrenteCaixaNotaItemDesconto_Shown(object sender, EventArgs e)
        {
            if (ConfiguracaoController.Instancia.GetConfiguracao().FocoDescontoPDV)
                txtDesconto.Focus();
        }
    }
}
