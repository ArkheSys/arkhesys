using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections;
using System.Collections.Generic;
using Componentes.devexpress;

namespace Modelo
{
    public partial class Nota
    {
        partial void OnCreated()
        {
            itensExcluir = new List<NotaItem>();
        }

        public List<Modelo.NotaItem> itensExcluir;

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
                        case "lkFilial":
                            erro.SetError(ctr, "");
                            if ((int)((cwk_DevLookup)ctr).EditValue == 0)
                            {
                                erro.SetError(ctr, "Filial não informada.");
                                status = false;
                            }
                            break;
                        case "lkTipoNota":
                            erro.SetError(ctr, "");
                            if ((int)((cwk_DevLookup)ctr).EditValue == 0)
                            {
                                erro.SetError(ctr, "Tipo de Orçamento não informado.");
                                status = false;
                            }
                            break;
                        case "lkTabPreco":
                            erro.SetError(ctr, "");
                            if ((int)((cwk_DevLookup)ctr).EditValue == 0)
                            {
                                erro.SetError(ctr, "Tabela de Preço não informada.");
                                status = false;
                            }
                            break;
                        case "lkCondicao":
                            erro.SetError(ctr, "");
                            if ((int)((Componentes.devexpress.cwk_DevLookup)ctr).EditValue == 0)
                            {
                                erro.SetError(ctr, "Condição não informada.");
                                status = false;
                            }
                            break;
                        case "txtData":
                            erro.SetError(ctr, "");
                            if (((DevExpress.XtraEditors.DateEdit)ctr).DateTime == null)
                            {
                                erro.SetError(ctr, "Data não informada.");
                                status = false;
                            }
                            break;
                        case "txtValorTotal":
                            erro.SetError(ctr, "");
                            if ((decimal)((DevExpress.XtraEditors.CalcEdit)ctr).EditValue == 0)
                            {
                                erro.SetError(ctr, "Total do Orçamento não pode ser igual a 0 (zero).");
                                status = false;
                            }
                            break;
                        case "txtSerie":
                            erro.SetError(ctr, "");
                            if (String.IsNullOrEmpty(ctr.Text))
                            {
                                erro.SetError(ctr, "Série não informada.");
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
