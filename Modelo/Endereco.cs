using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Data.SqlClient;
using System.Collections;

namespace Modelo
{
    public partial class Endereco : Modelo.IMetodosCwork
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
            lista = from e in db.Enderecos
                    //join c in db.Cidades on e.IDCidade equals c.ID
                    //join u in db.UFs on c.IDUF equals u.ID                       
                    select new
                    {
                        Rua = e.Rua,
                        Bairro = e.Bairro,
                        CEP = e.CEP,
                        Cidade = e.Cidade.Nome + " | " + e.Cidade.UF.Nome,
                        IDPessoa = e.IDPessoa,
                        ID = e.ID
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
            lista = from e in db.Enderecos
                    //join c in db.Cidades on e.IDCidade equals c.ID
                    //join u in db.UFs on c.IDUF equals u.ID
                    where e.Rua.Contains(pFiltro) ||
                    e.Bairro.Contains(pFiltro)
                    select new
                    {
                        Rua = e.Rua,
                        Bairro = e.Bairro,
                        CEP = e.CEP,
                        Cidade = e.Cidade.Nome + " | " + e.Cidade.UF.Nome,
                        IDPessoa = e.IDPessoa,
                        ID = e.ID
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
            return (from e in db.Enderecos
                    select new
                    {
                        Endereco = e.Rua + " | " + e.Bairro,
                        ID = e.ID
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
            lista = from e in db.Enderecos
                    //join c in db.Cidades on e.IDCidade equals c.ID                      
                    where e.Rua.Contains(pFiltro) ||
                    e.Bairro.Contains(pFiltro)
                    select new
                    {
                        Endereco = e.Rua + " | " + e.Bairro,
                        ID = e.ID
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
            return (from u in db.Enderecos where u.ID == pID select u).Single();
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
        public Int32 RetornaMAXSequencia(Modelo.Pessoa pessoa)
        {
            Int32 maxCodigo = 0;
            try
            {
                maxCodigo = (from e in pessoa.Enderecos
                             where e.Sequencia == pessoa.Enderecos.Max(b => b.Sequencia)
                             select e).Single().Sequencia;
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
                        case "txtEnd":
                            erro.SetError(ctr, "");
                            if (String.IsNullOrEmpty(ctr.Text))
                            {
                                erro.SetError(ctr, "Campo não pode ficar em branco.");
                                status = false;
                            }
                            break;
                        case "cbCidade":
                            erro.SetError(ctr, "");
                            if (String.IsNullOrEmpty(ctr.Text))
                            {
                                erro.SetError(ctr, "Campo não pode ficar em branco.");
                                status = false;
                            }
                            break;
                        case "txtNumero":
                            erro.SetError(ctr, "");
                            if (String.IsNullOrEmpty(ctr.Text))
                            {
                                erro.SetError(ctr, "Campo não pode ficar em branco.");
                                status = false;
                            }
                            break;
                        case "txtBairro":
                            erro.SetError(ctr, "");
                            if (String.IsNullOrEmpty(ctr.Text))
                            {
                                erro.SetError(ctr, "Campo não pode ficar em branco.");
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
