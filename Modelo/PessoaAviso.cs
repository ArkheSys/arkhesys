using System;
using System.Linq;
using System.Windows.Forms;

namespace Modelo
{
    public partial class PessoaAviso
    {

        /// <summary>
        /// Método para retornar a última parcela da Classe
        /// </summary>
        /// <param name="db">Contexto utilizado na tela de Manutenção</param>
        /// <returns>Retorna o último código + 1 (Int32)</returns>
        public Int32 RetornaMAXSequencia(Modelo.Pessoa pessoa)
        {
            Int32 maxCodigo = 0;
            try
            {
                maxCodigo = (from p in pessoa.PessoaAvisos
                             where p.Sequencia == pessoa.PessoaAvisos.Max(b => b.Sequencia)
                             select p).Single().Sequencia;
                maxCodigo = maxCodigo + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo;
        }

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
                        case "txtOrdem":
                            erro.SetError(ctr, "");
                            if (ctr.Text == "")
                            {
                                erro.SetError(ctr, "Campo não pode ficar em branco.");
                                status = false;
                            }
                            break;
                        case "txtAviso":
                            erro.SetError(ctr, "");
                            if (ctr.Text == "")
                            {
                                erro.SetError(ctr, "Campo não pode ficar em branco.");
                                status = false;
                            }
                            break;
                        case "txtDtVal":
                            erro.SetError(ctr, "");
                            if (ctr.Text == "")
                            {
                                erro.SetError(ctr, "Preencha a data de validade.");
                                status = false;
                            }
                            break;
                        case "txtDtCadAviso":
                            erro.SetError(ctr, "");
                            if (ctr.Text == "")
                            {
                                erro.SetError(ctr, "Preencha a data de cadastro.");
                                status = false;
                            }
                            break;
                    }
                }
            }
            return status;
        }
    }
}
