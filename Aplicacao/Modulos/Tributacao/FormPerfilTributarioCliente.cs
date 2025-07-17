using Aplicacao.Util;
using cwkGestao.Modelo;
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
    public partial class FormPerfilTributarioCliente : Aplicacao.IntermediariasTela.FormManutPerfilTributarioClienteIntermediaria
    {
        public FormPerfilTributarioCliente()
        {
            
        }

        protected override void InitializeChildComponents()
        {
            InitializeComponent();
        }
    }
}
