using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Modelo
{
    public partial class GrupoEstoque: Modelo.IMetodosCwork
    {

        /// <summary>
        /// REtorna uma Lista da classe para ser utilizada no Grid
        /// </summary>
        /// <param name="db">Contexto utilizado no Grid</param>
        /// <returns>Retorna a lista (IQueryable)</returns>
        public IQueryable getLista(DataClassesDataContext db)
        {
            IQueryable lista;

            lista = from ge in db.GrupoEstoques
                    select new
                    {
                        Codigo = ge.Codigo,
                        Nome = ge.Nome,
                        Classificação = ge.Classificacao,
                        Titulo = ge.bTitulo,
                        ID = ge.ID
                    };
            return lista;
        }

        /// <summary>
        /// REtorna uma Lista da classe para ser utilizada no Grid
        /// </summary>
        /// <param name="db">Contexto utilizado no Grid</param>
        /// <param name="pFiltro">Filtro para pesquisa dos registros</param>
        /// <returns>Retorna a lista (IQueryable)</returns>
        public IQueryable getLista(DataClassesDataContext db, String pFiltro)
        {
            IQueryable lista;

            lista = from ge in db.GrupoEstoques
                    where ge.Nome.Contains(pFiltro) ||
                    ge.Codigo.ToString() == pFiltro ||
                    ge.Classificacao.ToString() == pFiltro ||
                    ge.bTitulo.ToString() == pFiltro
                    select new
                    {
                        Codigo = ge.Codigo,
                        Nome = ge.Nome,
                        Classificação = ge.Classificacao,
                        Titulo = ge.bTitulo,
                        ID = ge.ID
                    };
            return lista;
        }

        /// <summary>
        /// Retorna uma lista para ser utilizada em Combobox
        /// </summary>
        /// <param name="db">Contexto utilizado no Formulário ou  Grid</param>
        /// <returns>Retorna a lista de registros (IQueryable)</returns>
        public IList getListaCombo(DataClassesDataContext db)
        {
            return (from ge in db.GrupoEstoques
                    select new
                    {
                        Nome = ge.Nome + " | " + ge.Classificacao,
                        ID = ge.ID
                    }).ToList();
        }

        /// <summary>
        /// Retorna uma lista para ser utilizada em Combobox
        /// </summary>
        /// <param name="db">Contexto utilizado no Formulário ou  Grid</param>
        /// <param name="pFiltro">Filtro para pesquisa dos registros</param>
        /// <returns>Retorna a lista de registros (IQueryable)</returns>
        public IQueryable getListaCombo(DataClassesDataContext db, String pFiltro)
        {
            IQueryable lista;

            lista = from ge in db.GrupoEstoques
                    where ge.Nome.Contains(pFiltro) ||
                    ge.Codigo.ToString() == pFiltro ||
                    ge.Classificacao.ToString() == pFiltro ||
                    ge.bTitulo.ToString() == pFiltro
                    select new
                    {
                        Nome = ge.Nome + " | " + ge.Classificacao,
                        ID = ge.ID
                    };

            return lista;
        }

        /// <summary>
        /// Retorna uma lista para ser utilizada em Combobox na seleção do nível anterior
        /// Esta lista traz somente os registros que são Título
        /// </summary>
        /// <param name="db">Contexto utilizado no Formulário ou  Grid</param>
        /// <param name="pID">Indica o registro atual que será desconsiderado na lista</param>
        /// <returns>Retorna a lista de registros (IQueryable)</returns>
        public IQueryable getListaComboNivelAnterior(DataClassesDataContext db, int pID)
        {
            IQueryable lista;

            lista = from ge in db.GrupoEstoques
                    where ge.ID != pID &&
                    ge.bTitulo == Modelo.TituloType.Sim
                    select new
                    {
                        Nome = ge.Classificacao + " | " + ge.Nome,
                        ID = ge.ID
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
            return (from u in db.GrupoEstoques where u.ID == pID select u).Single();
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
                return (from u in db.GrupoEstoques where u.Codigo.ToString() == pValor select u).Single().ID;
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

        /// <summary>
        /// Método que retorna o maior código existente no banco de dados
        /// </summary>
        /// <param name="db">Contexto do linq</param>
        /// <returns>maior código salvo no banco</returns>
        public Int32 RetornaMAXCodigo(DataClassesDataContext db)
        {
            Int32 maxCodigo = 0;
            try
            {
                maxCodigo = (from p in db.GrupoEstoques
                             where p.Codigo == db.GrupoEstoques.Max(b => b.Codigo)
                             select p).Single().Codigo;
                maxCodigo = maxCodigo + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo;
        }
                      
        /// <summary>
        /// Método para verificar as dependencias de um Grupo Estoque
        /// </summary>
        /// <param name="pDb">contexto do Linq</param>
        /// <param name="pID">ID do Grupo Estoque</param>
        /// <returns>caso não exista dependencias retorna falso, se existir retorna verdadeiro</returns>
        public bool VerificaDependencia(Modelo.DataClassesDataContext pDb, int pID)
        {
            int quant = 0;
            quant = pDb.GrupoEstoques.Where(t => t.IDGrupoEstoque == pID).Count();
            if (quant == 0)
            {
                return false;
            }
            return true;
        }

        public String GetClassifcacao(Modelo.DataClassesDataContext db, int ID) 
        {
            String Classificacao = (from n in db.GrupoEstoques
                                    where n.ID == ID
                                    select n.Classificacao).Single();


            return Classificacao;
        }
    }
}
