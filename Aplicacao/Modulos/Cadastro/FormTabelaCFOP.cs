using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using cwkGestao.Modelo;
using cwkGestao.Negocio;

namespace Aplicacao
{
    public partial class FormTabelaCFOP : Aplicacao.IntermediariasTela.FormTabelaCFOPIntermediaria
    {
        public FormTabelaCFOP()
        {
           
        }

        protected override void InitializeChildComponents()
        {
            InitializeComponent();
            object sessao = ConfiguracaoController.Instancia.getSession();
           }
    }
}
