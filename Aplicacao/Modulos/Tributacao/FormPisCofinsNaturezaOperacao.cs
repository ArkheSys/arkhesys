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
    public partial class FormPisCofinsNaturezaOperacao : Aplicacao.IntermediariasTela.FormManutPisCofinsNaturezaOperacaoIntermediaria
    {
        public FormPisCofinsNaturezaOperacao()
        {
        }

        protected override void InitializeChildComponents()
        {
            InitializeComponent();
            NosGridsPegarColunasDefinidasNaEntidade = true;

            lkpTabelaPIS.Exemplo = new TabelaPIS();
            lkpTabelaCOFINS.Exemplo = new TabelaCOFINS();
            //gcNaturezaOperacao.Tag = Selecionado;

            object[] parms = new[] { new NaturezaOperacao() { PisCofinsNaturezaOperacao = Selecionado } };
            Type formType = typeof(FormNaturezaOperacao);

            btIncluirNaturezaOperacao.SubFormType = btAlterarNaturezaOperacao.SubFormType = btExcluirNaturezaOperacao.SubFormType = btConsultarNaturezaOperacao.SubFormType = formType;
            btIncluirNaturezaOperacao.SubFormTypeParams = btAlterarNaturezaOperacao.SubFormTypeParams = btExcluirNaturezaOperacao.SubFormTypeParams = btConsultarNaturezaOperacao.SubFormTypeParams = parms;
        }
        private void btnlkpPis_Click(object sender, EventArgs e)
        {
            LookupUtil.GridLookup<TabelaPIS>(lkpTabelaPIS, typeof(FormTabelaPIS));
        }

        private void btnlkpCofins_Click(object sender, EventArgs e)
        {
            LookupUtil.GridLookup<TabelaCOFINS>(lkpTabelaCOFINS, typeof(FormTabelaCOFINS));
        }
    }
}
