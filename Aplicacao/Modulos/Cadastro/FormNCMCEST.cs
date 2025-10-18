using System;
using System.Windows.Forms;
using cwkGestao.Modelo;
using cwkGestao.Negocio;

namespace Aplicacao
{
    public partial class FormNCMCEST : Aplicacao.IntermediariasTela.FormManutNCMCESTIntermediaria
    {
        public FormNCMCEST()
        {
        }
        public FormNCMCEST(NCMCEST ncmCest)
        {
            this.Selecionado = ncmCest;
        }

        protected override void InitializeChildComponents()
        {
            InitializeComponent();
        }

        protected override void sbGravar_Click(object sender, EventArgs e)
        {
            string codigoNCMDigitado = txtNCM.Text.Trim();

            if (string.IsNullOrEmpty(codigoNCMDigitado))
            {
                MessageBox.Show("É necessário informar o código do NCM.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                return;
            }

            Selecionado.NCMCodigo = codigoNCMDigitado;

            base.sbGravar_Click(sender, e);
        }
    }
}