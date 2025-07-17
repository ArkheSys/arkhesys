using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Modelo
{
    public partial class TabelaFaixaFatSimples : IMetodosCwork
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
            lista = from f in db.TabelaFaixaFatSimples
                    select new
                    {
                        Codigo = f.Codigo,
                        Faixa_I = f.Faixa_I,
                        Faixa_F = f.Faixa_F,
                        Aliquota = f.Aliquota,
                        IRPJ = f.IRPJ,
                        CSLL = f.CSLL,
                        COFINS = f.COFINS,
                        PIS = f.PIS_PASEP,
                        CPP = f.CPP,
                        ICMS = f.ICMS,
                        ID = f.ID
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
            lista = from f in db.TabelaFaixaFatSimples
                    //where f.Codigo.ToString() == pFiltro
                    select new
                    {
                        Codigo = f.Codigo,
                        Faixa_I = f.Faixa_I,
                        Faixa_F = f.Faixa_F,
                        Aliquota = f.Aliquota,
                        IRPJ = f.IRPJ,
                        CSLL = f.CSLL,
                        COFINS = f.COFINS,
                        PIS = f.PIS_PASEP,
                        CPP = f.CPP,
                        ICMS = f.ICMS,
                        ID = f.ID
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
            return (from f in db.TabelaFaixaFatSimples
                    select new
                    {
                        Faixa = f.Faixa_I + " | " + f.Faixa_F,
                        ID = f.ID
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
            lista = from f in db.TabelaFaixaFatSimples
                    where f.Codigo.ToString() == pFiltro 
                    select new
                    {
                        Faixa = f.Faixa_I + " | " + f.Faixa_F,
                        ID = f.ID
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
            return (from u in db.TabelaFaixaFatSimples where u.ID == pID select u).Single();
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
                return (from u in db.TabelaFaixaFatSimples where u.Codigo.ToString() == pValor select u).Single().ID;
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
        public Int32 RetornaMAXCodigo(DataClassesDataContext db)
        {
            Int32 maxCodigo = 0;
            try
            {
                maxCodigo = (from b in db.TabelaFaixaFatSimples
                             where b.Codigo == db.TabelaFaixaFatSimples.Max(c => c.Codigo)
                             select b).Single().Codigo;
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
                        case "txtCodigo":
                            erro.SetError(ctr, "");
                            if (((DevExpress.XtraEditors.CalcEdit)ctr).Value == 0)
                            {
                                erro.SetError(ctr, "Código tem que ser maior que zero(0).");
                                status = false;
                            }
                            break;
                        case "txtFaixaI":
                            erro.SetError(ctr, "");
                            if (((DevExpress.XtraEditors.CalcEdit)ctr).Value == 0)
                            {
                                erro.SetError(ctr, "Faixa Inicial tem que ser maior que zero.");
                                status = false;
                            }
                            break;
                        case "txtFaixaF":
                            erro.SetError(ctr, "");
                            if (((DevExpress.XtraEditors.CalcEdit)ctr).Value == 0)
                            {
                                erro.SetError(ctr, "Faixa Final tem que ser maior que zero.");
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
