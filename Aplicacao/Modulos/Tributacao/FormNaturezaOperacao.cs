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
    public partial class FormNaturezaOperacao : Aplicacao.IntermediariasTela.FormManutNaturezaOperacaoIntermediaria
    {
        public FormNaturezaOperacao()
        {

        }
        public FormNaturezaOperacao(NaturezaOperacao item)
        {
            Selecionado = item;
        }

        protected override void InitializeChildComponents()
        {
            InitializeComponent();
            NosGridsPegarColunasDefinidasNaEntidade = true;
        }

        protected override void SetarMascaras()
        {
        }

        protected override void AcoesAntesBase_Load(object sender, EventArgs e)
        {
        }
    }
}
