using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;


namespace Modelo
{
    public partial class Condicao: IMetodosCwork
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
            lista = from c in db.Condicaos
                    select new
                    {
                        Codigo = c.Codigo,
                        Nome = c.Nome,
                        QtParcelas = c.QtParcela,
                        ID = c.ID
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
            lista = from c in db.Condicaos
                    where c.Codigo.ToString() == pFiltro ||
                    c.Nome.Contains(pFiltro) ||
                    c.QtParcela.ToString() == pFiltro
                    select new
                    {
                        Codigo = c.Codigo,
                        Nome = c.Nome,
                        QtParcelas = c.QtParcela,
                        ID = c.ID
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
            return (from c in db.Condicaos
                    select new
                    {
                        Nome = c.Nome + " | " + c.Codigo,
                        QtParcelas = c.QtParcela,
                        ID = c.ID
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
            lista = from c in db.Condicaos
                    where c.Codigo.ToString() == pFiltro ||
                    c.Nome.Contains(pFiltro) ||
                    c.QtParcela.ToString() == pFiltro
                    select new
                    {
                        Codigo = c.Codigo,
                        Nome = c.Nome,
                        QtParcelas = c.QtParcela,
                        ID = c.ID
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
            return (from u in db.Condicaos where u.ID == pID select u).Single();
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
                return (from u in db.Condicaos where u.Codigo.ToString() == pValor select u).Single().ID;
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
                maxCodigo = (from c in db.Condicaos
                             where c.Codigo == db.Condicaos.Max(b => b.Codigo)
                             select c).Single().Codigo;
                maxCodigo = maxCodigo + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo;
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
                        case "txtCodigo":
                            erro.SetError(ctr, "");
                            if (ctr.Text == "0")
                            {
                                erro.SetError(ctr, "Valor tem que ser diferente de 0 (Zero)");
                                status = false;
                            }
                            break;
                        case "txtNome":
                            erro.SetError(ctr, "");
                            if (ctr.Text == "")
                            {
                                erro.SetError(ctr, "Campo não pode ficar em branco.");
                                status = false;
                            }
                            break;
                        case "txtQtParcelas":
                            erro.SetError(ctr, "");
                            if (ctr.Text == "")
                            {
                                erro.SetError(ctr, "Condição não pode ficar sem parcela");
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
