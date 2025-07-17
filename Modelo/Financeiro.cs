using Componentes.devexpress;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace Modelo
{
    public class Financeiro
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
                        case "cbFilial":
                            erro.SetError(ctr, "");
                            if (((cwk_DevLookup)ctr).EditValue.ToString() == "0")
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
                        case "txtDtDigitacao":
                            erro.SetError(ctr, "");
                            if (ctr.Text == "")
                            {
                                erro.SetError(ctr, "Data de digitação não informada.");
                                status = false;
                            }
                            break;
                        case "txtRequisicao":

                            break;
                        case "txtNumDocto":
                            erro.SetError(ctr, "");
                            if (ctr.Text == null || String.IsNullOrEmpty(((DevExpress.XtraEditors.TextEdit)ctr).Text))
                            {
                                erro.SetError(ctr, "O por favor insira o número do documento.");
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
                        case "cbPessoa":
                            erro.SetError(ctr, "");
                            if (((Componentes.devexpress.cwk_DevLookup)ctr).EditValue.ToString() == "0")
                            {
                                erro.SetError(ctr, "Pessoa não informada.");
                                status = false;
                            }
                            break;
                        case "cbTipoDocto":
                            erro.SetError(ctr, "");
                            if (((Componentes.devexpress.cwk_DevLookup)ctr).EditValue.ToString() == "0")
                            {
                                erro.SetError(ctr, "Tipo de documento não informado.");
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
                        case "cbPortador":
                            erro.SetError(ctr, "");
                            if (((Componentes.devexpress.cwk_DevLookup)ctr).EditValue.ToString() == "0")
                            {
                                erro.SetError(ctr, "Portador não informada.");
                                status = false;
                            }
                            break;
                        case "cbAcrescimo":
                            erro.SetError(ctr, "");
                            if (((Componentes.devexpress.cwk_DevLookup)ctr).EditValue.ToString() == "0")
                            {
                                erro.SetError(ctr, "Tabela de acréscimo não informada.");
                                status = false;
                            }
                            break;
                        case "cbCondicao":
                            erro.SetError(ctr, "");
                            if (((Componentes.devexpress.cwk_DevLookup)ctr).EditValue.ToString() == "0")
                            {
                                erro.SetError(ctr, "Condição não informada.");
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

        public bool ValidaPConta(ErrorProvider erro, Control.ControlCollection controles, bool pStatus)
        {
            bool status = pStatus;

            foreach (Control ctr in controles)
            {
                if ((ctr is GroupBox) || (ctr is TabControl) || (ctr is DevExpress.XtraTab.XtraTabControl) || (ctr is DevExpress.XtraTab.XtraTabPage))
                {
                    status = ValidaPConta(erro, ctr.Controls, status);
                }
                else
                {
                    switch (ctr.Name)
                    {
                        case "cbPlanoConta":
                            erro.SetError(ctr, "");
                            if ((int)((Componentes.devexpress.cwk_DevLookup)ctr).EditValue == 0)
                            {
                                erro.SetError(ctr, "Plano Conta não informado.");
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
                        default:
                            break;
                    }
                }
            }

            return status;
        }

        public bool ValidaParcela(ErrorProvider erro, Control.ControlCollection controles, bool pStatus)
        {
            bool status = pStatus;

            foreach (Control ctr in controles)
            {
                if ((ctr is GroupBox) || (ctr is TabControl) || (ctr is DevExpress.XtraTab.XtraTabControl) || (ctr is DevExpress.XtraTab.XtraTabPage))
                {
                    status = ValidaParcela(erro, ctr.Controls, status);
                }
                else
                {
                    switch (ctr.Name)
                    {
                        case "txtDtContabil":
                            erro.SetError(ctr, "");
                            if (ctr.Text == "")
                            {
                                erro.SetError(ctr, "Data contábil não informada.");
                                status = false;
                            }
                            break;
                        case "txtDtVencimento":
                            erro.SetError(ctr, "");
                            if (ctr.Text == "")
                            {
                                erro.SetError(ctr, "Data de vencimento não informada.");
                                status = false;
                            }
                            break;
                        case "txtParcela":
                            erro.SetError(ctr, "");
                            if (((DevExpress.XtraEditors.CalcEdit)ctr).Value <= 0)
                            {
                                erro.SetError(ctr, "Parcela não informada.");
                                status = false;
                            }
                            break;
                        case "txtValor":
                            erro.SetError(ctr, "");
                            if (((DevExpress.XtraEditors.CalcEdit)ctr).Value <= 0)
                            {
                                erro.SetError(ctr, "Valor incorreto.");
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
