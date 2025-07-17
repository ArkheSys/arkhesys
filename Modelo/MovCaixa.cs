using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace Modelo
{
    public partial class MovCaixa
    {
        public bool Valida(ErrorProvider erro, Control.ControlCollection controles, bool pStatus)
        {
            bool status = pStatus;

            foreach (Control ctr in controles)
            {
                if ((ctr is GroupBox) || (ctr is TabControl) || (ctr is DevExpress.XtraTab.XtraTabControl) || (ctr is DevExpress.XtraTab.XtraTabPage))
                {
                    status = Valida(erro, ctr.Controls, status);
                }
                else
                {
                    switch (ctr.Name)
                    {
                        case "txtNumDocto":
                            erro.SetError(ctr, "");
                            if (((DevExpress.XtraEditors.TextEdit)ctr).Text.Trim() == String.Empty)
                            {
                                erro.SetError(ctr, "Informe o número do documento.");
                                status = false;
                            }
                            break;
                        case "cbFilial":
                            erro.SetError(ctr, "");
                            if (((Componentes.devexpress.cwk_DevLookup)ctr).EditValue.ToString() == "0")
                            {
                                erro.SetError(ctr, "Filial não informada.");
                                status = false;
                            }
                            break;
                        case "txtDtContabil":
                            erro.SetError(ctr, "");
                            if (ctr.Text == "")
                            {
                                erro.SetError(ctr, "Data contábil não informada.");
                                status = false;
                            }
                            break;
                        case "cbBanco":
                            erro.SetError(ctr, "");
                            if (((Componentes.devexpress.cwk_DevLookup)ctr).EditValue.ToString() == "0")
                            {
                                erro.SetError(ctr, "Banco não informado.");
                                status = false;
                            }
                            break;
                        case "cbTipo":
                            erro.SetError(ctr, "");
                            if (!((DevExpress.XtraEditors.ComboBoxEdit)ctr).Properties.Items.Contains(ctr.Text))
                            {
                                erro.SetError(ctr, "Valor incorreto.");
                                status = false;
                            }
                            break;
                        case "txtValor":
                            erro.SetError(ctr, "");
                            if (((DevExpress.XtraEditors.CalcEdit)ctr).Value <= 0)
                            {
                                erro.SetError(ctr, "Valor tem que ser maior que zero (0).");
                                status = false;
                            }
                            break;
                        case "cbHistorico":
                            erro.SetError(ctr, "");
                            if (((Componentes.devexpress.cwk_DevLookup)ctr).EditValue.ToString() == "0")
                            {
                                erro.SetError(ctr, "Histórico não informado.");
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