using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public partial class OrdemProducaoHistorico
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
            lista = from p in db.OrdemProducaoHistoricos
                    select new
                    {
                        Sequencia = p.Sequencia,
                        Data = p.Data,
                        ID = p.ID
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
        public IQueryable getLista(Modelo.DataClassesDataContext db, String pIdOrdemServico)
        {
            IQueryable lista;
            lista = from p in db.OrdemProducaoHistoricos  
                    join c in db.ClassificacaoOrdemProducaos on p.IdClassificacao equals c.ID
                    where p.IdOrdemProducao.ToString() == pIdOrdemServico
                    select new
                    {
                        Sequencia = p.Sequencia,
                        Data = p.Data,
                        Classificacao = c.Nome,
                        ID = p.ID
                    };
            return lista;
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um cwkComboBoxLookup
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getListaCombo(Modelo.DataClassesDataContext db)
        {
            IQueryable lista = null;            
            return lista;
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
            IQueryable lista = null;   
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
            return (from u in db.OrdemProducaoHistoricos where u.ID == pID select u).Single();
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
                return (from u in db.OrdemProducaoHistoricos where u.IdOrdemProducao.ToString() == pValor select u).Single().ID;
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
        public Int32 RetornaMAXCodigo(Modelo.DataClassesDataContext db, int pIdOrdemServico)
        {
            Int32 maxCodigo = 0;
            try
            {
                maxCodigo = (from p in db.OrdemProducaoHistoricos
                             where p.Sequencia == db.OrdemProducaoHistoricos.Max(b => b.Sequencia)
                             && p.IdOrdemProducao == pIdOrdemServico
                             select p).Single().Sequencia;
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
