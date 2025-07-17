using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Modelo
{
    public partial class ICMS_Interestadual : IMetodosCwork
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

            lista = from u in db.ICMS_Interestaduals
                    select new
                    {
                        Origem = u.UFOrigem.Sigla,
                        Destino = u.UFDestino.Sigla + " - " + u.UFDestino.Nome,
                        Aliquota = u.AliqContrib,
                        ID = u.ID
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
        public IQueryable getLista(DataClassesDataContext db, string pFiltro)
        {
            IQueryable lista;

            lista = from u in db.ICMS_Interestaduals
                    where u.UFOrigem.Sigla.Contains(pFiltro) ||
                    u.UFDestino.Nome.Contains(pFiltro) ||
                    u.AliqContrib.ToString() == (pFiltro)
                    select new
                    {
                        Origem = u.UFOrigem.Sigla,
                        Destino = u.UFDestino.Sigla + " - " + u.UFDestino.Nome,
                        Aliquota = u.AliqContrib,
                        ID = u.ID
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
            return (from u in db.ICMS_Interestaduals
                    select new
                    {
                        Nome = u.UFOrigem.Sigla + " | " + u.UFDestino.Sigla + " | " + u.AliqContrib,
                        ID = u.ID
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

            lista = from u in db.ICMS_Interestaduals
                    select new
                    {
                        Nome = u.UFOrigem.Sigla + " | " + u.UFDestino.Sigla + " | " + u.AliqContrib,
                        ID = u.ID
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
            return (from u in db.ICMS_Interestaduals where u.ID == pID select u).Single();
        }

        /// <summary>
        /// Método para retornar o ID de um terminado objeto
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pValor">Valor para pesquisa, normalmente o Código</param>
        /// <returns>Retorna o ID (int)</returns>
        public int getID(DataClassesDataContext db, string pValor)
        {
            if (pValor.Length != 0)
            {
                try
                {
                    return (from o in db.ICMS_Interestaduals where o.ID.ToString() == pValor select o).Single().ID;
                }
                catch (Exception)
                {
                    return 0;
                }
            }
            else
            {
                return 0;
            }
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
                        case "cbEmpresa":
                            erro.SetError(ctr, "");
                            Componentes.devexpress.cwk_DevLookup aux = (Componentes.devexpress.cwk_DevLookup)ctr;
                            if ((int?)aux.EditValue == 0)
                            {
                                erro.SetError(ctr, "Selecione uma Empresa.");
                                status = false;
                            }
                            break;
                        case "cbUF":
                            erro.SetError(ctr, "");
                            Componentes.devexpress.cwk_DevLookup aux1 = (Componentes.devexpress.cwk_DevLookup)ctr;
                            if ((int?)aux1.EditValue == 0)
                            {
                                erro.SetError(ctr, "Selecione um Estado.");
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
