using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;
using cwkGestao.Modelo;
using cwkGestao.Negocio;

namespace Aplicacao
{
    public partial class FormNCM : Aplicacao.IntermediariasTela.FormManutNCMIntermediaria
    {
        private GridGenerica<CEST> gridCest;

        public FormNCM()
        {
        }

        protected override void InitializeChildComponents()
        {
            InitializeComponent();

            gridCest = new GridGenerica<CEST>(CESTController.Instancia.GetAll(), new FormCEST(), null, false);
            gridCest.Selecionando = true;
        }

        private void FormNCM_Load(object sender, EventArgs e)
        {
            if (Selecionado.CestsVinculados == null)
                Selecionado.CestsVinculados = new List<NCMCEST>();
            
            ConfigurarGridCests();
            CarregaGridCests();
        }

        private void AddCest(CEST cest)
        {
            var novoVinculo = new NCMCEST { NCM = Selecionado, CEST = cest };
            if (!Selecionado.CestsVinculados.Any(vc => vc.CEST.ID == cest.ID))
            {
                Selecionado.CestsVinculados.Add(novoVinculo);
            }
        }

        private void RemoveCest(NCMCEST vinculo)
        {
            if (vinculo != null)
            {
                Selecionado.CestsVinculados.Remove(vinculo);
            }
        }

        private void CarregaGridCests()
        {
            gcCestsVinculados.DataSource = Selecionado.CestsVinculados.Select(v => v.CEST).ToList();
            gcCestsVinculados.RefreshDataSource();
        }

        private void btIncluirCest_Click(object sender, EventArgs e)
        {
            gridCest.ShowDialog();
            if (gridCest.Selecionado != null)
            {
                AddCest(gridCest.Selecionado);
                CarregaGridCests();
            }
        }

        private NCMCEST NcmCestSelecionado
        {
            get
            {
                var cestNoGrid = gvCestsVinculados.GetFocusedRow() as CEST;
                if (cestNoGrid != null)
                {
                    return Selecionado.CestsVinculados.FirstOrDefault(v => v.CEST.ID == cestNoGrid.ID);
                }
                return null;
            }
        }
        private void ConfigurarGridCests()
        {
            gvCestsVinculados.Columns.Clear();

            var colunaCodigo = new DevExpress.XtraGrid.Columns.GridColumn()
            {
                Caption = "Código CEST", // Texto do cabeçalho
                FieldName = "Codigo",     // Nome da propriedade na classe CEST
                Visible = true,           // Torna a coluna visível
                VisibleIndex = 0          // Posição da coluna (a primeira)
            };

            gvCestsVinculados.Columns.Add(colunaCodigo);

            gvCestsVinculados.OptionsBehavior.AutoPopulateColumns = false;
        }
        private void btExcluirCest_Click(object sender, EventArgs e)
        {
            var vinculoSelecionado = NcmCestSelecionado;
            if (vinculoSelecionado != null)
            {
                if (MessageBox.Show($"Deseja realmente desvincular o CEST {vinculoSelecionado.CEST.Codigo}?", "Confirmação", MessageBoxButtons.YesNo, MessageBoxIcon.Question) == DialogResult.Yes)
                {
                    RemoveCest(vinculoSelecionado);
                    CarregaGridCests();
                }
            }
            else
            {
                MessageBox.Show("Nenhum CEST selecionado.", "Atenção", MessageBoxButtons.OK, MessageBoxIcon.Information); 
            }
        }
    }
}