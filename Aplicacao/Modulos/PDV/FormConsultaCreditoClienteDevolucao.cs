﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aplicacao.Modulos.PDV
{
    public partial class FormConsultaCreditoClienteDevolucao : Form
    {
        public FormConsultaCreditoClienteDevolucao()
        {
            InitializeComponent();
        }

        private void btn_Fechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void FormConsultaCreditoClienteDevolucao_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyValue.Equals(27)) // Esc                
            {
                this.Close();
            }
               
        }
    }
}
