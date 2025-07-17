using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Modelo
{
    public partial class TabelaPrecoProduto : IMetodosCwork
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

            lista = null;
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

            lista = null;
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
            return null;
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

            lista = null;
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
            return null;
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
        public Int32 RetornaMAXCodigo(DataClassesDataContext db)
        {
            return 0;
        }

        /// <summary>
        /// Método para retornar o maior Código de uma determinada classe
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna o MAX (int)</returns>
        public Int32 RetornaMAXCodigo(DataClassesDataContext db, Modelo.Produto pProduto)
        {
            int? maxCodigo = 0;
            try
            {
                maxCodigo = (from t in pProduto.TabelaPrecoProdutos
                             where t.Codigo == pProduto.TabelaPrecoProdutos.Max(b => b.Codigo)
                             select t).Single().Codigo;
                maxCodigo = maxCodigo + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return (Int32)maxCodigo;
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
                        case "txtCodigo":
                            erro.SetError(ctr, "");
                            if (((DevExpress.XtraEditors.SpinEdit)ctr).Value == 0)
                            {
                                erro.SetError(ctr, "Código tem que ser maior que zero(0).");
                                status = false;
                            }
                            break;
                        case "cbTabelaPreco":
                            erro.SetError(ctr, "");
                            if (((Componentes.devexpress.cwk_DevLookup)ctr).EditValue.ToString() == "0")
                            {
                                erro.SetError(ctr, "Tabela de preço não informada.");
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