using System;
using System.Windows.Forms;

namespace Aplicacao.Modulos.Comercial.ControlUser
{
    public partial class UC_PagamentoPDV : UserControl
    {
        public int IDFormaDePagamento { get; set; }
        int TipoPagamento;
        public string FormaDePagamento { get; set; }
        public int Parcela { get; set; }
        public decimal Valor { get; set; }
        public int QuantidadeParcela { get; set; }
        public string Atalho { get; set; }

        public UC_PagamentoPDV()
        {
            InitializeComponent();
        }

        public UC_PagamentoPDV(int IDFormaDePagamento, string FormaDePagamento, bool Parcelar, decimal Valor, int Index, 
            int Parcela, string Atalho)
        {
            InitializeComponent();

            this.IDFormaDePagamento = IDFormaDePagamento;
            this.FormaDePagamento = FormaDePagamento;
            this.Valor = Valor;
            this.Parcela = Parcela;

            TXT_FormaPagamento.Text = FormaDePagamento;
            if(!string.IsNullOrEmpty(Atalho))
            {
                TXT_FormaPagamento.Text += System.Environment.NewLine + $"[{Atalho.ToUpper()}]";
            }

            this.Atalho = String.IsNullOrEmpty(Atalho)? string.Empty: Atalho;

            TXT_Valor.EditValue = Valor;
            TXT_Valor.Leave += TXT_Valor_Leave;
            TXT_Valor.KeyDown += FormFinalizarVendaFrenteCaixa_KeyDown;
            TXT_Valor.TabIndex = Index;
            TXT_Valor.TabStop = true;
            txtParcelas.TabIndex = Index + 1;
            txtParcelas.TabStop = true;

            txtParcelas.TabStop = true;
            txtParcelas.Visible = Parcelar;
            txtParcelas.EditValue = 1;

            LBL_Total.Visible = Parcelar;
            LBL_Total.Text = "";

            if (!Parcelar)
            {
                TXT_FormaPagamento.Height = 62;
            }
        }

        private void TXT_Valor_Leave(object sender, EventArgs e)
        {
            AtualizaValor();
        }

        public void AplicarValorNaForma(decimal valor)
        {
            TXT_Valor.EditValue = valor;
            (ParentForm as FormFinalizarVendaFrenteCaixaNovaPagamentos)?.EventoAtualizarFormaPagamento(IDFormaDePagamento, Convert.ToDecimal(TXT_Valor.EditValue.ToString().Replace(".", ",")), Convert.ToInt32(txtParcelas.EditValue), Parcela);
            AtualizarTotal();
        }

        private void FormFinalizarVendaFrenteCaixa_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyData)
            {
                case Keys.F4:
                    if ((ParentForm as FormFinalizarVendaFrenteCaixaNovaPagamentos) != null)
                    {
                        (ParentForm as FormFinalizarVendaFrenteCaixaNovaPagamentos).Finalizou = false;
                        (ParentForm as FormFinalizarVendaFrenteCaixaNovaPagamentos).Close();
                    }
                    break;
                case Keys.F12:
                    (ParentForm as FormFinalizarVendaFrenteCaixaNovaPagamentos)?.FinalizarVenda();
                    break;
                case Keys.Enter:
                    AtualizaValor();
                    break;
            }
        }

        private void TXT_Valor_Enter(object sender, EventArgs e)
        {
            DuploClique();
            AtualizaValor();
            TXT_Valor.SelectAll();
        }

        private void txtParcelas_Leave(object sender, EventArgs e)
        {
            if (Convert.ToDecimal(txtParcelas.EditValue) != QuantidadeParcela)
            {
                QuantidadeParcela = Convert.ToInt32(txtParcelas.EditValue);
                (ParentForm as FormFinalizarVendaFrenteCaixaNovaPagamentos)?.EventoAtualizarFormaPagamento(IDFormaDePagamento, Convert.ToDecimal(TXT_Valor.EditValue.ToString().Replace(".", ",")), QuantidadeParcela, Parcela);
                AtualizarTotal();
            }
        }

        private void AtualizarTotal()
        {
            var Total = (Valor / (QuantidadeParcela == 0 ? 1 : QuantidadeParcela));
            LBL_Total.Text = Total == 0 ? "" : $"{QuantidadeParcela} X {Total.ToString("c2")}";
        }

        private void TXT_FormaPagamento_Click(object sender, EventArgs e)
        {
            DuploClique();
            AtualizaValor();
        }

        private void LBL_Total_Click(object sender, EventArgs e)
        {
            DuploClique();
            AtualizaValor();
        }

        private void DuploClique()
        {
            (ParentForm as FormFinalizarVendaFrenteCaixaNovaPagamentos)?.EventoDuploCliqueFormaPagamento(this);
        }

        private void TXT_Valor_Leave_1(object sender, EventArgs e)
        {
            //AtualizaValor();

        }

        private void LBL_Total_Leave(object sender, EventArgs e)
        {
            //DuploClique();
        }

        private void TXT_Valor_Click(object sender, EventArgs e)
        {
            DuploClique();
            AtualizaValor();
        }

        private void AtualizaValor()
        {

            if (Convert.ToDecimal(TXT_Valor.EditValue) != Valor || TipoPagamento != IDFormaDePagamento)
            {
                Valor = Convert.ToDecimal(TXT_Valor.EditValue.ToString().Replace(".", ","));
                (ParentForm as FormFinalizarVendaFrenteCaixaNovaPagamentos)?.EventoAtualizarFormaPagamento(IDFormaDePagamento, Convert.ToDecimal(TXT_Valor.EditValue.ToString().Replace(".", ",")), Convert.ToInt32(txtParcelas.EditValue), Parcela);
                AtualizarTotal();
                TipoPagamento = IDFormaDePagamento;
            }
        }
    }
}
