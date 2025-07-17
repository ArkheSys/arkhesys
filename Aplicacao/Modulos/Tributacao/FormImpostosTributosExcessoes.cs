using Aplicacao.Util;
using cwkGestao.Modelo;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aplicacao.Modulos.Tributacao
{
    public partial class FormImpostosTributosExcessoes : Aplicacao.IntermediariasTela.FormManutImpostosTributosExcessoesIntermediaria
    {

        public FormImpostosTributosExcessoes()
        {
        }

        public FormImpostosTributosExcessoes(ImpostosTributosExcessoes item)
        {
            Selecionado = item;
        }

        protected override void InitializeChildComponents()
        {
            InitializeComponent();
            lkpUF.Exemplo = new UF();
            lkpTabelaCFOP.Exemplo = new TabelaCFOP();
        }

        private void btnlkpUF_Click(object sender, EventArgs e)
        {
            LookupUtil.GridLookup<UF>(lkpUF, typeof(FormUF));
        }

        private void btnlkpCfop_Click(object sender, EventArgs e)
        {
            LookupUtil.GridLookup<TabelaCFOP>(lkpTabelaCFOP, typeof(FormTabelaCFOP));
        }
    }
}
