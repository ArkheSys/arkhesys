using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Collections;

namespace Modelo
{
    public partial class PlanoConta: Modelo.IMetodosCwork
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
            lista = from pConta in db.PlanoContas
                    select new
                    {
                        Codigo = pConta.Codigo,
                        Nome = pConta.Classificacao + " " + pConta.Nome,
                        Tipo = pConta.DebCre,
                        ID = pConta.ID
                    };
            return lista;
        }

        public PlanoConta GetPlanoConta(int pId, DataClassesDataContext db)
        {
            return (from p in db.PlanoContas
                    where p.ID == pId
                    select p).Single();
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
            lista = from pConta in db.PlanoContas
                    where pConta.Codigo.ToString() == pFiltro ||
                    pConta.Nome.Contains(pFiltro) 
                    select new
                    {
                        Codigo = pConta.Codigo,
                        Nome = pConta.Classificacao + " " + pConta.Nome,
                        Tipo = pConta.DebCre,
                        Titulo = pConta.bTitulo,
                        ID = pConta.ID
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
            return (from pConta in db.PlanoContas
                    orderby pConta.Classificacao, pConta.Nome
                    select new
                    {
                        Nome = pConta.Classificacao + " " + pConta.Nome,
                        Codigo = pConta.Codigo,
                        ID = pConta.ID
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
            lista = from pConta in db.PlanoContas
                    where pConta.Codigo.ToString() == pFiltro ||
                    pConta.Nome.Contains(pFiltro)
                    select new
                    {
                        //Nome = pConta.Codigo + " | " + pConta.Nome + " | " + pConta.DebCre,
                        Nome = pConta.Classificacao + " " + pConta.Nome,
                        ID = pConta.ID
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

            lista = from pc in db.PlanoContas
                    where pc.ID != pID &&
                    pc.bTitulo == Modelo.TituloType.Sim
                    select new
                    {
                        Nome = pc.Classificacao + " | " + pc.Nome,
                        ID = pc.ID
                    };

            return lista;
        }

        /// <summary>
        /// Método para verificar as dependencias de um Plano Conta
        /// </summary>
        /// <param name="pDb">contexto do Linq</param>
        /// <param name="pID">ID do plano conta</param>
        /// <returns>caso não exista dependencias retorna falso, se existir retorna verdadeiro</returns>
        public bool VerificaDependencia(Modelo.DataClassesDataContext pDb, int pID)
        {
            int quant = 0;
            quant = pDb.PlanoContas.Where(t => t.IDPlanoConta == pID).Count();
            if (quant == 0)
            {
                return false;
            }
            return true;
        }

        /// <summary>
        /// Método para retornar um único objeto.
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pID">ID do objeto</param>
        /// <returns>Retorna o objeto (Modelo.T)"/></returns>
        public Object getObjeto(DataClassesDataContext db, int pID)
        {
            return (from u in db.PlanoContas where u.ID == pID select u).Single();
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
                return (from u in db.PlanoContas where u.Codigo.ToString() == pValor select u).Single().ID;
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
                maxCodigo = (from p in db.PlanoContas
                             where p.Codigo == db.PlanoContas.Max(b => b.Codigo)
                             select p).Single().Codigo;
                maxCodigo = maxCodigo + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo;
        }

        public String GetClassificacao(DataClassesDataContext db, int ID)
        {
            String Classificacao = (from n in db.PlanoContas
                                        where n.ID == ID
                                        select n.Classificacao).Single();
            
            Classificacao = Classificacao.Trim() + ".";

            return Classificacao;
        }
    }
}
