using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Modelo
{
    public partial class TipoNota : Modelo.IMetodosCwork
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
            lista = from c in db.TipoNotas
                    select new
                    {
                        Codigo = c.Codigo,
                        Ordem = c.Ordem,
                        Nome = c.Nome,                        
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
            lista = from c in db.TipoNotas
                    where c.Codigo.ToString() == pFiltro ||
                    c.Nome.Contains(pFiltro)
                    select new
                    {
                        Codigo = c.Codigo,
                        Ordem = c.Ordem,
                        Nome = c.Nome,
                        ID = c.ID
                    };
            return lista;
        }

        public IQueryable getLista(DataClassesDataContext db, String pFiltro, Modelo.InOutType ent_sai)
        {
            IQueryable lista;
            lista = from c in db.TipoNotas
                    where c.Ent_Sai == ent_sai &&
                    (c.Codigo.ToString() == pFiltro ||
                    c.Nome.Contains(pFiltro))
                    select new
                    {
                        Codigo = c.Codigo,
                        Ordem = c.Ordem,
                        Nome = c.Nome,
                        ID = c.ID
                    };
            return lista;
        }

        public IQueryable getLista(DataClassesDataContext db, String pFiltro, List<int> pIDs)
        {
            IQueryable lista;
            lista = from c in db.TipoNotas
                    where c.Codigo.ToString() == pFiltro ||
                    c.Nome.Contains(pFiltro) && (c.ID == pIDs[0] || c.ID == pIDs[1])
                    select new
                    {
                        Codigo = c.Codigo,
                        Ordem = c.Ordem,
                        Nome = c.Nome,
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
            return (from c in db.TipoNotas
                    select new
                    {
                        Nome = c.Nome + " | " + c.Codigo,
                        ID = c.ID
                    }).ToList();
        }

        public IList getListaComboServico(DataClassesDataContext db)
        {
            return (from c in db.TipoNotas
                    where c.Categoria == 1
                    select new
                    {
                        Nome = c.Nome + " | " + c.Codigo,
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
            lista = from c in db.TipoNotas
                    where c.Codigo.ToString() == pFiltro ||
                    c.Nome.Contains(pFiltro)
                    select new
                    {
                        Nome = c.Nome + " | " + c.Codigo,
                        ID = c.ID
                    };
            return lista;
        }

        public IQueryable getListaComboModeloDocumento(DataClassesDataContext db, int modeloDoc)
        {
            IQueryable lista;
            lista = from c in db.TipoNotas
                    where c.ModeloDocto == modeloDoc
                    select new
                    {
                        Nome = c.Nome + " | " + c.Codigo,
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
            return (from u in db.TipoNotas where u.ID == pID select u).Single();
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
                return (from u in db.TipoNotas where u.Codigo.ToString() == pValor select u).Single().ID;
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
                maxCodigo = (from c in db.TipoNotas
                             where c.Codigo == db.TipoNotas.Max(b => b.Codigo)
                             select c).Single().Codigo;
                maxCodigo = maxCodigo + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo;
        }

        public List<int> GetCodigos(Modelo.DataClassesDataContext db)
        {
            List<int> Lista;

            Lista = (from t in db.TipoNotas
                     select t.Codigo).ToList();

            return Lista;
        }
    }
}
