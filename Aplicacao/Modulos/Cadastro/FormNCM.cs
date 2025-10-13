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

       
        
       
    }
}