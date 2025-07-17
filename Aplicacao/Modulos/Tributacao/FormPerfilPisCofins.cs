
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
    public partial class FormPerfilPisCofins : Aplicacao.IntermediariasTela.FormManutPerfilPisCofinsIntermediaria
    {
        public FormPerfilPisCofins()
        {

        }
        private void DiscriminaTabs()
        {
            tpPrincipal.Tag = Selecionado;

            xtraTabControl1.Tag = Selecionado;

            tabGeral.Tag = Selecionado;
            groupTipoTributacao.Tag = Selecionado;
            groupAliquotasGeral.Tag = Selecionado;


            tabEntrada.Tag = Selecionado;
            xtraTabControl2.Tag = Selecionado;
            tabEntGeral.Tag = Selecionado;
            tabEntAliquotaDiferenciada.Tag = Selecionado;
            groupEntCreditoAliquotaDiferenciada.Tag = Selecionado;
            tabEntUnidadeMedida.Tag = Selecionado;



            tabSaida.Tag = Selecionado;
            xtraTabControl3.Tag = Selecionado;
            tabSaiGeral.Tag = Selecionado;
            tabSaiAliquotaDiferenciada.Tag = Selecionado;
            groupSaiCreditoAliquotaDiferenciada.Tag = Selecionado;
            tabSaiUnidadeMedida.Tag = Selecionado;
        }
        protected override void InitializeChildComponents()
        {
            InitializeComponent();
            NosGridsPegarColunasDefinidasNaEntidade = false;
            DiscriminaTabs();
            
            lkpUnidadeEntrada.Exemplo = new Unidade();
            lkpUnidadeSaida.Exemplo = new Unidade();
            lkpSaiPisCofinsNaturezaOperacao.Exemplo = new PisCofinsNaturezaOperacao();
        }

        protected override void AcoesAntesBase_Load(object sender, EventArgs e)
        {
            DiscriminaTabs();
            xtraTabControl1.SelectedTabPage = tabGeral;
        }

        private void lkbUnidadeEntrada_Click_1(object sender, EventArgs e)
        {
            LookupUtil.GridLookup<Unidade>(lkpUnidadeEntrada, typeof(FormUnidade));
        }

        private void lookupButton1_Click(object sender, EventArgs e)
        {
            LookupUtil.GridLookup<Unidade>(lkpUnidadeSaida, typeof(FormUnidade));
        }

        private void lkbPisCofinsNaturezaOperacao_Click(object sender, EventArgs e)
        {
            LookupUtil.GridLookup<PisCofinsNaturezaOperacao>(lkpSaiPisCofinsNaturezaOperacao, typeof(FormPisCofinsNaturezaOperacao));
        }

        private void lkpPisCofinsNaturezaOperacao_EditValueChanged(object sender, EventArgs e)
        {
            var esse = (PisCofinsNaturezaOperacao)lkpSaiPisCofinsNaturezaOperacao.Selecionado;

            if (esse != null)
            {
                cbeSaiNaturezaReceita.Properties.Items.Clear();

                foreach (NaturezaOperacao itemNaturezaOperacao in esse.NaturezaOperacao)
                {
                    cbeSaiNaturezaReceita.Properties.Items.Add(itemNaturezaOperacao.Codigo + "-" + itemNaturezaOperacao.Descricao);
                }
                cbeSaiNaturezaReceita.SelectedIndex = 0;
            }
        }

        private void lkbEntPisCofinsNaturezaOperacao_Click(object sender, EventArgs e)
        {
            LookupUtil.GridLookup<PisCofinsNaturezaOperacao>(lkpEntPisCofinsNaturezaOperacao, typeof(FormPisCofinsNaturezaOperacao));
        }
    }
}
