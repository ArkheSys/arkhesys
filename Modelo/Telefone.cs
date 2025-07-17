using System;
using System.Linq;
using System.Windows.Forms;
using System.Collections;

namespace Modelo
{
    public partial class Telefone : Modelo.IMetodosCwork
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
            lista = from t in db.Telefones
                    select new
                    {
                        Numero = t.Numero,
                        Tipo = t.Tipo,
                        Contato = t.Contato,
                        IDPessoa = t.IDPessoa,
                        ID = t.ID
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
            lista = from t in db.Telefones
                    where t.Numero == pFiltro ||
                    t.Contato.Contains(pFiltro) ||
                    t.Tipo.Contains(pFiltro)
                    select new
                    {
                        Numero = t.Numero,
                        Tipo = t.Tipo,
                        Contato = t.Contato,
                        IDPessoa = t.IDPessoa,
                        ID = t.ID
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
            return (from t in db.Telefones
                    select new
                    {
                        Numero = t.Numero,
                        ID = t.ID
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
            lista = from t in db.Telefones
                    where t.Numero == pFiltro ||
                    t.Contato.Contains(pFiltro) ||
                    t.Tipo.Contains(pFiltro)
                    select new
                    {
                        Numero = t.Numero,
                        ID = t.ID
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
            return (from u in db.Telefones where u.ID == pID select u).Single();
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
        /// Método para retornar a última Sequencia da Classe
        /// </summary>
        /// <param name="db">Contexto utilizado na tela de Manutenção</param>
        /// <returns>Retorna o último código + 1 (Int32)</returns>
        public Int32 RetornaMAXSequencia(Modelo.Pessoa pessoa)
        {
            Int32 maxCodigo = 0;
            try
            {
                maxCodigo = (from t in pessoa.Telefones
                             where t.Sequencia == pessoa.Telefones.Max(b => b.Sequencia)
                             select t).Single().Sequencia;
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
                        case "txtNumero":
                            erro.SetError(ctr, "");
                            if (ctr.Text == "")
                            {
                                erro.SetError(ctr, "Campo não pode ficar em branco.");
                                status = false;
                            }
                            break;
                        case "cbTipo":
                            if (ctr.Text == "")
                            {
                                erro.SetError(ctr, "Campo não pode ficar em branco.");
                                status = false;
                            }
                            else
                            {
                                if (!((DevExpress.XtraEditors.ComboBoxEdit)ctr).Properties.Items.Contains(ctr.Text))
                                {
                                    erro.SetError(ctr, "Valor incorreto.");
                                    status = false;
                                }
                            }
                            break;
                    }
                }                
            }
            return status;
        }
    }
}
