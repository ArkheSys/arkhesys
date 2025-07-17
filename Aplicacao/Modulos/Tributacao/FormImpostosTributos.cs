using Aplicacao.Util;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Proxy;
using cwkGestao.Negocio;
using cwkGestao.Negocio.Padroes;
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
    public partial class FormImpostosTributos : Aplicacao.IntermediariasTela.FormManutImpostosTributosIntermediaria
    {
        public FormImpostosTributos()
        {
        }

        private void DiscriminaTabs()
        {
            tcPrincipal.Tag = Selecionado;
            tpPrincipal.Tag = Selecionado;

            xtraTabControl1.Tag = Selecionado;

            tabDadosPrincipais.Tag = Selecionado;
            tabExcecoes.Tag = Selecionado;
        }
        protected override void InitializeChildComponents()
        {
            InitializeComponent();
            NosGridsPegarColunasDefinidasNaEntidade = true;
            DiscriminaTabs();

            object[] parms = new[] { new ImpostosTributosExcessoes() { ImpostosTributos = Selecionado } };
            Type formType = typeof(FormImpostosTributosExcessoes);

            btIncluirImpostosTributosExcessoes.SubFormType = btAlterarImpostosTributosExcessoes.SubFormType = btExcluirImpostosTributosExcessoes.SubFormType = btConsultarImpostosTributosExcessoes.SubFormType = formType;
            btIncluirImpostosTributosExcessoes.SubFormTypeParams = btAlterarImpostosTributosExcessoes.SubFormTypeParams = btExcluirImpostosTributosExcessoes.SubFormTypeParams = btConsultarImpostosTributosExcessoes.SubFormTypeParams = parms;

            lkpClassificacaoFiscal.Exemplo = new ClassificacaoFiscal();
            lkpCfopDentroDoEstado.Exemplo = new TabelaCFOP();
            lkpCfopForaDoEstado.Exemplo = new TabelaCFOP();
        }

        protected override void AcoesAntesBase_Load(object sender, EventArgs e)
        {
            DiscriminaTabs();
            xtraTabControl1.SelectedTabPage = tabDadosPrincipais;
        }

        private void btnlkpMovimento_Click(object sender, EventArgs e)
        {
            LookupUtil.GridLookup<ClassificacaoFiscal>(lkpClassificacaoFiscal, typeof(FormClassificacaoFiscal));
        }

        private void btnlkpCfopDentroDoEstado_Click(object sender, EventArgs e)
        {
            LookupUtil.GridLookup<TabelaCFOP>(lkpCfopDentroDoEstado, typeof(FormTabelaCFOP));
        }

        private void btnlkpCfopForaDoEstado_Click(object sender, EventArgs e)
        {
            LookupUtil.GridLookup<TabelaCFOP>(lkpCfopForaDoEstado, typeof(FormTabelaCFOP));
        }

        private void lkbTipoNota_Click(object sender, EventArgs e)
        {
            LookupUtil.GridLookup<TipoNota>(lkpTipoNota, typeof(FormTipoNota));
        }

        private void lkbPerfilTributarioCliente_Click(object sender, EventArgs e)
        {
            LookupUtil.GridLookup<PerfilTributarioCliente>(lkpPerfilTributarioCliente, typeof(FormPerfilTributarioCliente));
        }
    }
}
