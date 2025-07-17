using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BLL
{
    public class PedidoParcela : IDisposable
    {
        private Modelo.DataClassesDataContext db;

        public PedidoParcela()
        {
            db = new Modelo.DataClassesDataContext();
            //db.ObjectTrackingEnabled = false;
            //db.DeferredLoadingEnabled = false;
        }

        public PedidoParcela(Modelo.DataClassesDataContext pdb)
        {
            db = pdb;
        }

        public void Dispose()
        {
            db.Dispose();
        }

        public bool Valida(ErrorProvider erro, Control.ControlCollection controles)
        {
            bool status = true;

            foreach (Control ctr in controles)
            {
                if ((ctr is GroupBox) || (ctr is TabControl) || (ctr is DevExpress.XtraTab.XtraTabControl) || (ctr is DevExpress.XtraTab.XtraTabPage))
                {
                    status = Valida(erro, ctr.Controls);
                }
                else
                {
                    switch (ctr.Name)
                    {
                        case "cbTipoDocumento":
                            erro.SetError(ctr, "");
                            if (((Componentes.devexpress.cwk_DevLookup)ctr).EditValue.ToString() == "0")
                            {
                                erro.SetError(ctr, "Tipo de Documento não informado.");
                                status = false;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            return status;
        }
     }
}
