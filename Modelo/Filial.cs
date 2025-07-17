using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Modelo
{
    public partial class Filial : Modelo.IMetodosCwork
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
            lista = from filial in db.Filials
                    select new
                    {
                        Codigo = filial.Codigo,
                        Nome = filial.Nome,
                        CNPJ = filial.CNPJ,
                        Empresa = filial.Empresa.Nome,
                        ID = filial.ID
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
            lista = from filial in db.Filials
                    where filial.Nome.Contains(pFiltro) ||
                    filial.CNPJ.Contains(pFiltro) ||
                    filial.Codigo.ToString() == pFiltro
                    select new
                    {
                        Codigo = filial.Codigo,
                        Nome = filial.Nome,
                        CNPJ = filial.CNPJ,
                        Empresa = filial.Empresa.Nome,
                        ID = filial.ID
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
            return (from filial in db.Filials
                    select new
                    {
                        Nome = filial.Responsavel + " | " + filial.Codigo,                        
                        ID = filial.ID
                    }).ToList();
        }

        public IQueryable getComboBox(DataClassesDataContext db)
        {
            IQueryable lista;
            lista = from filial in db.Filials
                    select new
                    {
                        Nome = filial.Nome + " | " + filial.Codigo,
                        ID = filial.ID
                    };
            return lista;
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
            lista = from filial in db.Filials
                    where filial.Codigo.ToString() == pFiltro ||
                    filial.Nome.Contains(pFiltro) ||
                    filial.CNPJ.Contains(pFiltro)
                    select new
                    {
                        Nome = filial.Responsavel + " | " + filial.Codigo,
                        ID = filial.ID
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
            return (from u in db.Filials where u.ID == pID select u).Single();
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
                return (from u in db.Filials where u.Codigo.ToString() == pValor select u).Single().ID;
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
            int maxCodigo = 0;
            try
            {
                maxCodigo = (from f in db.Filials
                             where f.Codigo == db.Filials.Max(b => b.Codigo)
                             select f).Single().Codigo;
                maxCodigo = maxCodigo + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo;
        }

    }
}
