using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class Unidade : InterfaceBLL, IDisposable
    {
        private Modelo.DataClassesDataContext db;
        
        //public Unidade()
        //{
        //    db = new Modelo.DataClassesDataContext();
        //    db.ObjectTrackingEnabled = false;
        //    db.DeferredLoadingEnabled = false;
        //}

        public Unidade(Modelo.DataClassesDataContext pdb)
        {
            db = pdb;
        }      

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um DataGrid
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getLista()
        {
            IQueryable lista;

            lista = from u in db.Unidades
                    select new
                    {
                        Sigla = u.Sigla,
                        Nome = u.Nome,
                        QtdFracionada = u.bQtdFracionada,
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
        public IQueryable getLista(String pFiltro)
        {
            IQueryable lista;

            lista = from u in db.Unidades
                    where u.Nome.Contains(pFiltro) ||
                    u.Sigla == pFiltro
                    select new
                    {
                        Sigla = u.Sigla,
                        Nome = u.Nome,
                        QtdFracionada = u.bQtdFracionada,
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
        public IQueryable getListaCombo()
        {
            IQueryable lista;

            lista = from u in db.Unidades
                    select new
                    {
                        Nome = u.Sigla + " | " + u.Nome,
                        ID = u.ID
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
        public IQueryable getListaCombo(String pFiltro)
        {
            IQueryable lista;

            lista = from u in db.Unidades
                    where u.Nome.Contains(pFiltro) ||
                    u.Sigla == pFiltro
                    select new
                    {
                        Nome = u.Sigla + " | " + u.Nome,
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
        public Object getObjeto(int pID)
        {
            return (from u in db.Unidades where u.ID == pID select u).Single();
        }

        /// <summary>
        /// Método para retornar o ID de um terminado objeto
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pValor">Valor para pesquisa, normalmente o Código</param>
        /// <returns>Retorna o ID (int)</returns>
        public int getID(string pValor)
        {
            return 0;
        }

        /// <summary>
        /// Método para retornar o maior Código de uma determinada classe
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna o MAX (int)</returns>
        public Int32 RetornaMAXCodigo()
        {
            return 0;
        }

        public string getSigla(int pID)
        {
            return (from u in db.Unidades where u.ID == pID select u).Single().Sigla;
        }

        #region Membros de IDisposable

        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion
    }
}
