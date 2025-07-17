using cwkConsultaOS.Controller;
using DevExpress.XtraGrid.Views.Base;
using DevExpress.XtraGrid.Views.Grid;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace cwkConsultaOS
{
    public partial class GridOSOrdemServico : Form
    {
        protected IList<OrdemServico> lista;
        private OSOrdemServicoController controller = OSOrdemServicoController.Instancia;
        private FormOSOrdemServico formOS = new FormOSOrdemServico();

        public GridOSOrdemServico()
        {
            InitializeComponent();
            lista = controller.GetAll();

            CarregarDados();
            gvPrincipal.MoveLastVisible();
            gvPrincipal.Focus();
        }

        protected virtual OrdemServico GetRegistroSelecionado()
        {
            try
            {
                OrdemServico retorno;
                if (controller != null)
                {
                    try
                    {
                        retorno = controller.LoadObjectById(((OrdemServico)gvPrincipal.GetRow(gvPrincipal.GetSelectedRows()[0])).IDORDEMSERVICO);
                        return retorno;
                    }
                    catch (Exception)
                    {
                        retorno = (OrdemServico)gvPrincipal.GetRow(gvPrincipal.GetSelectedRows()[0]);
                        return retorno;
                    }
                }
                retorno = (OrdemServico)gvPrincipal.GetRow(gvPrincipal.GetSelectedRows()[0]);
                return retorno;
            }
            catch (IndexOutOfRangeException e)
            {
                return null;
            }
        }

        protected void SelecionaRegistroNoGrid(int linha)
        {
            gvPrincipal.ClearSelection();
            gvPrincipal.FocusedRowHandle = linha;
            gvPrincipal.SelectRow(linha);
        }

        private void CarregarDados()
        {
            if (lista != null)
            {
                try
                {
                    gcPrincipal.DataSource = null;
                    gcPrincipal.DataSource = lista;
                    SelecionaRegistroNoGrid(lista.Count - 1);
                }
                catch (Exception e)
                {

                    throw e;
                }
            }
            else
            {
                try
                {
                    lista = new List<OrdemServico>();
                    AtualizarGrid();
                }
                catch (Exception ex)
                {
                    throw ex;
                }
            }
        }

        protected virtual void AtualizarGrid()
        {
            try
            {
               gcPrincipal.DataSource = null;
               gcPrincipal.DataSource = lista = controller.GetAll();
               gcPrincipal.RefreshDataSource();
               gvPrincipal.RefreshData();
               gcPrincipal.Refresh();
               gvPrincipal.MoveLastVisible();
               SelecionaRegistroNoGrid(lista.Count -1);
            }
            catch (Exception e)
            {
            }
        }

        protected virtual void Consultar()
        {
            OrdemServico registroSelecionado = GetRegistroSelecionado();
            if (registroSelecionado != null)
            {
                formOS.Selecionado = registroSelecionado;
                formOS.ShowDialog();
            }
            else
            {
                MessageBox.Show("Nenhum registro selecionado. Somente consulta.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }
        }

        private void gcPrincipal_DoubleClick(object sender, EventArgs e)
        {
            Consultar();
        }

        private void sbConsultar_Click(object sender, EventArgs e)
        {
            Consultar();
        }

        private void sbFechar_Click(object sender, EventArgs e)
        {
            Close();
        }

        private void _sbFiltrar_Click(object sender, EventArgs e)
        {
            if (deDataInicial.EditValue == null || deDataInicial.EditValue == null)
            {
                gcPrincipal.DataSource = lista.ToList();
                return;
            }

            DateTime inicio = ((DateTime)deDataInicial.EditValue).Date;
            DateTime fim = ((DateTime)deDataFinal.EditValue).Date;

            if (fim < inicio)
            {
                MessageBox.Show("Data Final deve ser maior que a Data Inicial!", "Aviso", MessageBoxButtons.OK, MessageBoxIcon.Information);
            }

            if (inicio.Equals(fim))
            {
                fim = fim.AddHours(23)
                         .AddMinutes(59)
                         .AddSeconds(59);
            }

            var novaLista = lista.Where(os => os.ENTRADA >= inicio && os.ENTRADA <= fim).ToList();
            gcPrincipal.DataSource = novaLista.ToList();
            SelecionaRegistroNoGrid(novaLista.Count -1);
        }

        private void GridOSOrdemServico_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.Escape:
                    this.Close();
                    break;
                case Keys.F5:
                    AtualizarGrid();
                    break;
            }
        }
    }
}
