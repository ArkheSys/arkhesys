using System;
using System.Collections.Generic;
using System.Collections;
using System.Linq;
using System.Text;

namespace Modelo
{
    public partial class Servico
    {
        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um DataGrid
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getLista(Modelo.DataClassesDataContext db)
        {
            IQueryable lista;
            lista = from p in db.Servicos
                    select new
                    {
                        Codigo = p.codigo,
                        Nome = p.nome,
                        Valor = p.valor,
                        ID = p.id
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
        public IQueryable getLista(Modelo.DataClassesDataContext db, String pFiltro)
        {
            IQueryable lista;
            lista = from p in db.Servicos
                    where p.codigo.ToString() == pFiltro ||
                    p.nome.Contains(pFiltro)
                    select new
                    {
                        Codigo = p.codigo,
                        Nome = p.nome,
                        Valor = p.valor,
                        ID = p.id
                    };
            return lista;
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um cwkComboBoxLookup
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IList getListaCombo(Modelo.DataClassesDataContext db)
        {
            return (from p in db.Servicos
                    select new
                    {
                        Nome = p.nome,
                        Codigo = p.codigo,
                        Valor = p.valor,
                        ID = p.id
                    }).ToList();
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um cwkComboBoxLookup
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pFiltro">Valor que deseja filtrar os objetos</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getListaCombo(Modelo.DataClassesDataContext db, String pFiltro)
        {
            IQueryable lista;
            lista = from p in db.Servicos
                    where p.codigo.ToString() == pFiltro ||
                    p.nome.Contains(pFiltro)
                    select new
                    {
                        Nome = p.nome + " | " + p.codigo,
                        ID = p.id
                    };
            return lista;
        }

        /// <summary>
        /// Método para retornar um único objeto.
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pID">ID do objeto</param>
        /// <returns>Retorna o objeto (Modelo.T)"/></returns>
        public Object getObjeto(Modelo.DataClassesDataContext db, int pID)
        {
            return (from u in db.Servicos where u.id == pID select u).Single();
        }

        /// <summary>
        /// Método para retornar o ID de um terminado objeto
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pValor">Valor para pesquisa, normalmente o Código</param>
        /// <returns>Retorna o ID (int)</returns>
        public int getID(Modelo.DataClassesDataContext db, string pValor)
        {
            if (pValor.Length != 0)
            {
                return (from u in db.Servicos where u.codigo.ToString() == pValor select u).Single().id;
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
        public Int32 RetornaMAXCodigo(Modelo.DataClassesDataContext db)
        {
            Int32 maxCodigo = 0;
            try
            {
                maxCodigo = (from p in db.Servicos
                             where p.codigo == db.Servicos.Max(b => b.codigo)
                             select p).Single().codigo;
                maxCodigo = maxCodigo + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo;
        }
        public override string ToString()
        {
            return nome;
        }
    }
}
