using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Modelo
{
    public partial class TipoPedido: Modelo.IMetodosCwork
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
            lista = from c in db.TipoPedidos
                    select new
                    {
                        Codigo = c.Codigo,
                        Ordem = c.Ordem,
                        Nome = c.Nome,
                        Tipo = c.Tipo,
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
            lista = from c in db.TipoPedidos
                    where c.Codigo.ToString() == pFiltro ||
                    c.Nome.Contains(pFiltro)                    
                    select new
                    {
                        Codigo = c.Codigo,
                        Ordem = c.Ordem,
                        Nome = c.Nome,
                        Tipo = c.Tipo,
                        ID = c.ID
                    };
            return lista;
        }

        public IQueryable getLista(DataClassesDataContext db, Modelo.InOutType tipo)
        {
            IQueryable lista;
            lista = from c in db.TipoPedidos
                    where c.Ent_Sai == tipo
                    select new
                    {
                        Codigo = c.Codigo,
                        Ordem = c.Ordem,
                        Nome = c.Nome,
                        Tipo = c.Tipo,
                        ID = c.ID
                    };
            return lista;
        }

        public IQueryable getLista(DataClassesDataContext db, Modelo.InOutType tipo, Modelo.TipoPedidoType tipoPedido)
        {
            IQueryable lista;
            lista = from c in db.TipoPedidos
                    where c.Ent_Sai == tipo
                    && c.Tipo == tipoPedido
                    select new
                    {
                        Codigo = c.Codigo,
                        Ordem = c.Ordem,
                        Nome = c.Nome,
                        Tipo = c.Tipo,
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
            return (from c in db.TipoPedidos
                    select new
                    {
                        Nome = c.Nome + " | " + c.Codigo,
                        ID = c.ID
                    }).ToList();
        }

        public IQueryable getListaCombo(DataClassesDataContext db, Modelo.InOutType tipo)
        {
            IQueryable lista;
            lista = from c in db.TipoPedidos
                    where c.Ent_Sai == tipo
                    select new
                    {
                        Nome = c.Nome + " | " + c.Codigo,
                        ID = c.ID
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
            lista = from c in db.TipoPedidos
                    where c.Codigo.ToString() == pFiltro ||
                    c.Nome.Contains(pFiltro)
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
            return (from u in db.TipoPedidos where u.ID == pID select u).Single();
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
                return (from u in db.TipoPedidos where u.Codigo.ToString() == pValor select u).Single().ID;
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
                maxCodigo = (from c in db.TipoPedidos
                             where c.Codigo == db.TipoPedidos.Max(b => b.Codigo)
                             select c).Single().Codigo;
                maxCodigo = maxCodigo + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo;
        }

        public IQueryable RetornaTipoPedido(DataClassesDataContext db) 
        {
            IQueryable lista;
            lista = from t in db.TipoPedidos
                    where t.Tipo == Modelo.TipoPedidoType.Requisição
                    select new
                    {
                        t.Nome,
                        t.ID
                    };
            return lista;
        } 
    }
}
