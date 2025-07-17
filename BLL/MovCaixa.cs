using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class MovCaixa : InterfaceBLL, IDisposable
    {
        private Modelo.DataClassesDataContext db;

        public MovCaixa()
        {
            db = new Modelo.DataClassesDataContext();
            db.ObjectTrackingEnabled = false;
            db.DeferredLoadingEnabled = false;
        }

        public MovCaixa(Modelo.DataClassesDataContext pdb)
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

            lista = from u in db.MovCaixas where u.Valor > 0
                    select new
                    {
                        Codigo = u.Codigo,
                        Origem = u.Movimentos.Count == 0 ? Modelo.OrigemMovimentoType.Caixa : u.Movimentos.First().Origem,
                        Filial = u.Filial.Codigo,
                        Banco = u.Banco.Nome,
                        Data = u.Dt,
                        DtConciliacao = u.DtConciliacao,
                        Doc = u.NumDocumento,
                        Entrada = u.Ent_Sai == 0 ? u.Valor : 0,
                        Saida = u.Ent_Sai == 1 ? u.Valor : 0,
                        Historico = u.ComplementoHist,
                        ID = u.ID,
                        IncData = u.IncData,
                        IncUsuario = u.IncUsuario,
                        AltData = u.AltData,
                        AltUsuario = u.AltUsuario,
                        FormaPagamento = u.FormaPagamentos.Nome
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

            lista = from u in db.MovCaixas
                    where u.Codigo.ToString() == pFiltro && u.Valor > 0
                    select new
                    {
                        Codigo = u.Codigo,
                        Filial = u.Filial.Codigo,
                        Banco = u.Banco.Nome,
                        Data = u.Dt,
                        Doc = u.NumDocumento,
                        Entrada = u.Ent_Sai == 0 ? u.Valor : 0,
                        Saida = u.Ent_Sai == 1 ? u.Valor : 0,
                        Historico = u.ComplementoHist,
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

            lista = from u in db.MovCaixas
                    select new
                    {
                        Nome = u.Codigo + " | " + u.Banco.Nome,
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

            lista = from u in db.MovCaixas
                    where u.Codigo.ToString() == pFiltro ||
                    u.Banco.Nome.Contains(pFiltro)
                    select new
                    {
                        Nome = u.Codigo + " | " + u.Banco.Nome,
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
            return (from u in db.MovCaixas where u.ID == pID select u).Single();
        }

        /// <summary>
        /// Método para retornar o ID de um terminado objeto
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pValor">Código</param>
        /// <returns>Retorna o ID (int)</returns>
        public int getID(string pValor)
        {
            return (from p in db.MovCaixas where p.Codigo.ToString() == pValor select p).Single().ID;
        }

        /// <summary>
        /// Método para retornar o maior Código de uma determinada classe
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna o MAX (int)</returns>
        public Int32 RetornaMAXCodigo()
        {
            Int32 maxCodigo = 0;
            try
            {
                maxCodigo = db.MovCaixas.Max(b => b.Codigo) + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo;
        }
    }
}
