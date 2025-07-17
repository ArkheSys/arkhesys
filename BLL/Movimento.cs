using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public partial class Movimento : InterfaceBLL, IDisposable
    {
        private Modelo.DataClassesDataContext db;

        public Movimento()
        {
            db = new Modelo.DataClassesDataContext();
            db.ObjectTrackingEnabled = false;
            db.DeferredLoadingEnabled = false;
        }

        public Movimento(Modelo.DataClassesDataContext pdb)
        {
            db = pdb;
        }

        public void Dispose()
        {
            db.Dispose();
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

            lista = from u in db.Movimentos
                    select new
                    {
                        Sequência = u.Sequencia,
                        Data = u.Dt,
                        Operação = u.Operacao,
                        Tipo = u.Tipo,
                        Valor = u.Valor,
                        PConta = u.PlanoConta.Nome,
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

            lista = from u in db.Movimentos
                    where u.Sequencia.ToString() == pFiltro
                    select new
                    {
                        Sequência = u.Sequencia,
                        Data = u.Dt,
                        Operação = u.Operacao,
                        Tipo = u.Tipo,
                        Valor = u.Valor,
                        PConta = u.PlanoConta.Classificacao,
                        ID = u.ID
                    };
            return lista;
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um DataGrid
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public List<Modelo.Objeto.pxyMovimento> getLista(int IDDocumento)
        {
            List<Modelo.Objeto.pxyMovimento> lista;

            lista = (from u in db.Movimentos
                     where u.IDDocumento == IDDocumento
                     select new Modelo.Objeto.pxyMovimento
                     {
                         Sequencia = u.Sequencia.Value,
                         Data = u.Dt.Value,
                         Operacao = u.Operacao,
                         Tipo = u.Tipo,
                         Valor = u.Operacao == Modelo.OperacaoMovimentoType.Lancamento ? u.Valor.Value : (u.Valor.Value * -1),
                         PConta = u.PlanoConta.Nome,
                         ID = u.ID,
                         Acao = 4
                     }).ToList();

            return lista;
        }

        /// <summary>
        /// Método para recuperar os movimentos de um determinado lançamento do caixa
        /// </summary>


        public IQueryable getListaMovCaixa(int IDMovCaixa)
        {
            IQueryable lista;

            lista = from u in db.Movimentos
                    where u.IDMovCaixa == IDMovCaixa
                    select new
                    {
                        Sequência = u.Sequencia,
                        Data = u.Dt,
                        Operação = u.Operacao,
                        Tipo = u.Tipo,
                        Valor = u.Valor,
                        PConta = u.PlanoConta.Nome,
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
        public IQueryable getLista(String pFiltro, int IDDocumento)
        {
            IQueryable lista;

            lista = from u in db.Movimentos
                    where u.IDDocumento == IDDocumento &&
                    u.Sequencia.ToString() == pFiltro
                    select new
                    {
                        Sequência = u.Sequencia,
                        Data = u.Dt,
                        Operação = u.Operacao,
                        Tipo = u.Tipo,
                        Valor = String.Format("{0:C2}", u.Valor),
                        PConta = u.PlanoConta.Classificacao,
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

            lista = from u in db.Movimentos
                    select new
                    {
                        Nome = u.PlanoConta.Classificacao,
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

            lista = from u in db.Movimentos
                    where u.Codigo.ToString() == pFiltro
                    select new
                    {
                        Nome = u.PlanoConta.Classificacao,
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
            return (from u in db.Movimentos where u.ID == pID select u).Single();
        }

        /// <summary>
        /// Método para retornar o ID de um terminado objeto
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pValor">Código</param>
        /// <returns>Retorna o ID (int)</returns>
        public int getID(string pValor)
        {
            return (from p in db.Movimentos where p.Codigo.ToString() == pValor select p).Single().ID;
        }

        /// <summary>
        /// Método para retornar o maior Código de uma determinada classe
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna o MAX (int)</returns>
        public Int32 RetornaMAXCodigo()
        {
            return 1;
        }

        /// <summary>
        /// Método para retornar o maior Código de uma determinada classe
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna o MAX (int)</returns>
        public Int32 RetornaMAXCodigo(Modelo.Documento objDoc)
        {
            Int32 maxCodigo = 1;
            try
            {
                maxCodigo = (from n in objDoc.Movimentos
                             where n.Codigo == objDoc.Movimentos.Max(b => b.Codigo)
                             select n).Single().Codigo;
                maxCodigo = maxCodigo + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo;
        }

        /// <summary>
        /// Método para retornar o maior Código de uma determinada classe
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna o MAX (int)</returns>
        public Int32 RetornaMAXCodigo(Modelo.MovCaixa objMov)
        {
            Int32 maxCodigo = 1;
            try
            {
                maxCodigo = (from n in objMov.Movimentos
                             where n.Codigo == objMov.Movimentos.Max(b => b.Codigo)
                             select n).Single().Codigo;
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
