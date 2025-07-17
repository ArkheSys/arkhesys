using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections;

namespace Modelo
{
    public partial class CondicaoParcela : IMetodosCwork
    {

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um DataGrid
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getLista(DataClassesDataContext db)
        {
            IQueryable lista;
            lista = from cp in db.CondicaoParcelas
                    select new
                    {
                        Parcela = cp.Parcela,
                        TipoDt = cp.TipoDt,
                        DiaMes = cp.DiaMes,
                        IDTipoDocumento = cp.IDTipoDocumento,
                        ID = cp.ID
                    };
            return lista;
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um DataGrid
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pFiltro">Valor que deseja filtrar os objetos</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getLista(DataClassesDataContext db, String pFiltro)
        {
            IQueryable lista;
            lista = from cp in db.CondicaoParcelas
                    where cp.Parcela.ToString() == pFiltro ||
                    cp.TipoDt.Contains(pFiltro) ||
                    cp.DiaMes.ToString() == pFiltro ||
                    cp.IDTipoDocumento.ToString() == pFiltro
                    select new
                    {
                        Parcela = cp.Parcela,
                        TipoDt = cp.TipoDt,
                        DiaMes = cp.DiaMes,
                        IDTipoDocumento = cp.IDTipoDocumento,
                        ID = cp.ID
                    };
            return lista;
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um cwkComboBoxLookup
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IList getListaCombo(DataClassesDataContext db)
        {
            return (from cp in db.CondicaoParcelas
                    select new
                    {
                        Parcela = cp.Parcela,
                        ID = cp.ID
                    }).ToList();
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um cwkComboBoxLookup
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pFiltro">Valor que deseja filtrar os objetos</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getListaCombo(DataClassesDataContext db, String pFiltro)
        {
            IQueryable lista;
            lista = from cp in db.CondicaoParcelas
                    where cp.Parcela.ToString() == pFiltro ||
                   cp.TipoDt.Contains(pFiltro) ||
                   cp.DiaMes.ToString() == pFiltro ||
                   cp.IDTipoDocumento.ToString() == pFiltro
                    select new
                    {
                        Parcela = cp.Parcela,
                        ID = cp.ID
                    };
            return lista;
        }

        /// <summary>
        /// Método para retornar um único objeto.
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pID">ID do objeto</param>
        /// <returns>Retorna o objeto (Modelo.T)"/></returns>
        public Object getObjeto(DataClassesDataContext db, int pID)
        {
            return (from u in db.CondicaoParcelas where u.ID == pID select u).Single();
        }

        /// <summary>
        /// Método para retornar o ID de um terminado objeto
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pValor">Valor para pesquisa, normalmente o Código</param>
        /// <returns>Retorna o ID (int)</returns>
        public int getID(DataClassesDataContext db, string pValor)
        {
            return 0;
        }

        /// <summary>
        /// Método para retornar o maior Código de uma determinada classe
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna o MAX (int)</returns>
        public Int32 RetornaMAXParcela(Modelo.Condicao condicao)
        {
            Int32 maxCodigo = 0;
            try
            {
                maxCodigo = (from c in condicao.CondicaoParcelas
                             where c.Parcela == condicao.CondicaoParcelas.Max(b => b.Parcela)
                             select c).Single().Parcela;
                maxCodigo = maxCodigo + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo;
        }

        /// <summary>
        /// Método para retornar o maior Código de uma determinada classe
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna o MAX (int)</returns>
        public int RetornaMAXCodigo(DataClassesDataContext db)
        {
            return 0;
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
                        //case "txtDiaMes":
                        //    erro.SetError(ctr, "");
                        //    if (ctr.Text == "0")
                        //    {
                        //        erro.SetError(ctr, "Valor tem que ser diferente de 0 (Zero).");
                        //        status = false;
                        //    }
                        //    break;
                         case "cbTipoData":
                            erro.SetError(ctr, "");
                            if (ctr.Text == "")
                            {
                                erro.SetError(ctr, "Campo não pode ficar em branco.");
                                status = false;
                            }
                            break;
                         case "cbTipoValor":
                            erro.SetError(ctr, "");
                            if (ctr.Text == "")
                            {
                                erro.SetError(ctr, "Campo não pode ficar em branco.");
                                status = false;
                            }
                            break;
                         case "cbTipoDocumento":
                            erro.SetError(ctr, "");
                            if (ctr.Text == "")
                            {
                                erro.SetError(ctr, "Campo não pode ficar em branco.");
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