using cwkGestao.Negocio;

using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace Aplicacao.Modulos.Comercial
{
    public partial class FormConsultaCreditoCliente : Form
    {
        public FormConsultaCreditoCliente()
        {
            InitializeComponent();

            var TodosCreditosAndDebitos = MovimentacaoCreditoPedidoController.Instancia.GetAllClientesComCredito(); //GetAll().Where(o => o.Saldo > 0);

            gcValoresCreditoClientes.DataSource = TodosCreditosAndDebitos; //ProdutoController.Instancia.GetAll().Where(o => !string.IsNullOrEmpty(o.Barra)).OrderBy(prod => prod.NomeOrdenado).Distinct().ToList();
            gcValoresCreditoClientes.RefreshDataSource();
            gcValoresCreditoClientes.Refresh();
        }
    }
}
