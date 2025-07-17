using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Modelo
{
    public partial class OrdemProducao
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
            lista = from p in db.OrdemProducaos
                    select new
                    {
                        Codigo = p.Codigo,
                        IdClassificacao = p.IdClassificacaoOrdemProducao,
                        Id = p.ID
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
        public List<Modelo.Objeto.pxyOrdemProducao> getListaProjeto(Modelo.DataClassesDataContext db, Modelo.StatusOPType? status)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("SELECT");
            str.AppendLine("OrdemProducao.ID");
            str.AppendLine(", OrdemProducao.Codigo");
            str.AppendLine(", Filial.Nome AS Empresa");
            str.AppendLine(", Pessoa.Nome AS Cliente");
            str.AppendLine(", Projeto.Nome AS Projeto");
            str.AppendLine(", ClassificacaoOrdemProducao.Nome AS Classificacao");
            str.AppendLine(", OrdemProducao.Descricao AS Descricao");
            str.AppendLine("FROM OrdemProducao");
            str.AppendLine("INNER JOIN Filial ON Filial.ID = OrdemProducao.IDFilial");
            str.AppendLine("INNER JOIN Pessoa ON Pessoa.ID = OrdemProducao.IDCliente");
            str.AppendLine("INNER JOIN Projeto ON Projeto.ID = OrdemProducao.IDProjeto");
            str.AppendLine("LEFT JOIN ClassificacaoOrdemProducao ON ClassificacaoOrdemProducao.ID = OrdemProducao.IdClassificacaoOrdemProducao");
            if (status.HasValue)
                str.AppendLine("WHERE ClassificacaoOrdemProducao.Status = " + ((int)status).ToString());
            return db.ExecuteQuery<Modelo.Objeto.pxyOrdemProducao>(str.ToString(), new object[0]).ToList();
        }

        public List<Modelo.Objeto.pxyOrdemProducaoPedido> getListaPedido(Modelo.DataClassesDataContext db, int? idpedido)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("SELECT ");
            str.AppendLine("(SELECT TOP 1 Descricao ");
			str.AppendLine("FROM OrdemProducaoExecucao ");
            str.AppendLine("WHERE OrdemProducaoExecucao.IdOrdemProducao = OrdemProducao.ID ");
		    str.AppendLine("AND OrdemProducaoExecucao.DataInicio is not null ");
            str.AppendLine("ORDER BY Sequencia DESC) AS Execucao ");
            str.AppendLine(", OrdemProducao.ID ");
            str.AppendLine(", OrdemProducao.Codigo ");
            str.AppendLine(", Pedido.Codigo AS Pedido ");
            str.AppendLine(", Filial.Nome AS Empresa ");
            str.AppendLine(", Pessoa.Nome AS Cliente ");
            str.AppendLine(", ClassificacaoOrdemProducao.Nome AS Classificacao ");
            str.AppendLine("FROM OrdemProducao ");
            str.AppendLine("INNER JOIN PedidoItem ON PedidoItem.ID = OrdemProducao.IdPedidoItem ");
            str.AppendLine("INNER JOIN Pedido ON Pedido.ID = PedidoItem.IDPedido ");
            str.AppendLine("INNER JOIN Filial ON Filial.ID = Pedido.IDFilial ");
            str.AppendLine("INNER JOIN Pessoa ON Pessoa.ID = Pedido.IDPessoa ");
            str.AppendLine("LEFT JOIN ClassificacaoOrdemProducao ON ClassificacaoOrdemProducao.ID = OrdemProducao.IdClassificacaoOrdemProducao");
            if (idpedido.HasValue)
                str.AppendLine("WHERE Pedido.ID = " + idpedido.Value.ToString());


            List<Modelo.Objeto.pxyOrdemProducaoPedido> ret = db.ExecuteQuery<Modelo.Objeto.pxyOrdemProducaoPedido>(str.ToString(), new object[0]).ToList();

            return ret;
        }

        public static bool ExisteOrdemProducao(Modelo.DataClassesDataContext db, int idpedido)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("SELECT");
            str.AppendLine("OrdemProducao.Codigo");
            str.AppendLine("FROM OrdemProducao");
            str.AppendLine("INNER JOIN PedidoItem ON PedidoItem.ID = OrdemProducao.IdPedidoItem");
            str.AppendLine("INNER JOIN Pedido ON Pedido.ID = PedidoItem.IDPedido");
            str.AppendLine("WHERE Pedido.ID = " + idpedido.ToString());

            var ret = db.ExecuteQuery<int>(str.ToString(), new object[0]);

            return ret.Count() > 0;
        }

        public List<Modelo.Objeto.pxyOrdemProducaoPedidoManut> getGridPedido(Modelo.DataClassesDataContext db, int idPedido)
        {
            StringBuilder str = new StringBuilder();
            str.AppendLine("SELECT");
            str.AppendLine("OrdemProducao.ID");
            str.AppendLine(", OrdemProducao.Codigo");
            str.AppendLine(", PedidoItem.ProdutoNome AS Produto");
            str.AppendLine(", PedidoItem.Quantidade");
            str.AppendLine(", ClassificacaoOrdemProducao.Nome AS Classificacao");
            str.AppendLine("FROM OrdemProducao");
            str.AppendLine("INNER JOIN PedidoItem ON PedidoItem.ID = OrdemProducao.IdPedidoItem");
            str.AppendLine("INNER JOIN Pedido ON Pedido.ID = PedidoItem.IDPedido");
            str.AppendLine("LEFT JOIN ClassificacaoOrdemProducao ON ClassificacaoOrdemProducao.ID = OrdemProducao.IdClassificacaoOrdemProducao");
            str.AppendLine("WHERE Pedido.ID = " + idPedido.ToString());

            List<Modelo.Objeto.pxyOrdemProducaoPedidoManut> ret = db.ExecuteQuery<Modelo.Objeto.pxyOrdemProducaoPedidoManut>(str.ToString(), new object[0]).ToList();

            return ret;
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um cwkComboBoxLookup
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getListaCombo(Modelo.DataClassesDataContext db)
        {
            IQueryable lista;
            lista = from p in db.OrdemProducaos
                    where p.IdProjeto.HasValue
                    select new
                    {
                        Nome = " | " + p.Codigo,
                        ID = p.ID
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
        public IQueryable getListaCombo(Modelo.DataClassesDataContext db, String pFiltro)
        {
            IQueryable lista;
            lista = from p in db.OrdemProducaos
                    where p.Codigo.ToString() == pFiltro ||
                    p.Codigo.ToString().Contains(pFiltro)
                    select new
                    {
                        Nome = " | " + p.Codigo,
                        ID = p.ID
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
            return (from u in db.OrdemProducaos where u.ID == pID select u).Single();
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
                return (from u in db.OrdemProducaos where u.Codigo.ToString() == pValor select u).Single().ID;
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
                maxCodigo = (from p in db.OrdemProducaos
                             where p.Codigo == db.OrdemProducaos.Max(b => b.Codigo)
                             select p).Single().Codigo;
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
