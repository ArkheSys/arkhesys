using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Collections;

namespace Modelo
{
    public partial class PedidoItem : Modelo.IMetodosCwork
    {
        public decimal Quantidade_Ant { get; set; }

        partial void OnLoaded()
        {
            Quantidade_Ant = Quantidade;
        }

        /// <summary>
        /// Método para retornar uma lista com os objetos de uma determinada classe
        /// Utilizar esse método para carregar as informações em um DataGrid
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna a lista com o tipo IQueryable</returns>
        public IQueryable getLista(DataClassesDataContext db)
        {
            IQueryable lista;

            lista = from o in db.PedidoItems
                    select new
                    {
                        Seq = o.Sequencia,
                        Produto = o.Produto.Codigo,
                        Descricao = o.Produto.Nome,
                        Quantidade = o.Quantidade,
                        Unidade = o.Produto.Unidade.Sigla,
                        Valor = o.Valor,
                        Desc = o.ValorDesconto,
                        Total = o.Total,
                        ID = o.ID
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
        public IQueryable getLista(DataClassesDataContext db, string pFiltro)
        {
            IQueryable lista;

            lista = from o in db.PedidoItems
                    where o.Sequencia.ToString() == pFiltro ||
                    o.Produto.Nome.Contains(pFiltro) ||
                    o.Produto.Codigo.ToString() == pFiltro
                    select new
                    {
                        Seq = o.Sequencia,
                        Produto = o.Produto.Codigo,
                        Descricao = o.Produto.Nome,
                        Quantidade = o.Quantidade,
                        Unidade = o.Produto.Unidade.Sigla,
                        Valor = o.Valor,
                        Desc = o.ValorDesconto,                        
                        Total = o.Total,
                        ID = o.ID
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
            return (from o in db.PedidoItems
                    select new
                    {
                        Seq = o.Sequencia,
                        Produto = o.Produto.Codigo,
                        Descricao = o.Produto.Nome,
                        Quantidade = o.Quantidade,
                        Unidade = o.Unidade,
                        Valor = o.Valor,
                        Desc = o.ValorDesconto,
                        Total = o.Total,
                        ID = o.ID
                    }).ToList();
        }


        public IQueryable getListaPorPedido(DataClassesDataContext db, int pIdPedido)
        {
            IQueryable lista;

            lista = from o in db.PedidoItems
                    where o.IDPedido == pIdPedido
                    select new
                    {
                        Seq = o.Sequencia,
                        Produto = o.Produto.Codigo,
                        Descricao = o.Produto.Nome,
                        Quantidade = o.Quantidade,
                        Unidade = o.Unidade,
                        Valor = o.Valor,
                        Desc = o.ValorDesconto,
                        Total = o.Total,
                        ID = o.ID
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

            lista = from o in db.PedidoItems
                    where o.Sequencia.ToString() == pFiltro ||
                    o.Produto.Nome.Contains(pFiltro) ||
                    o.Produto.Codigo.ToString() == pFiltro
                    select new
                    {
                        Seq = o.Sequencia,
                        Produto = o.Produto.Codigo,
                        Descricao = o.Produto.Nome,
                        Quantidade = o.Quantidade,
                        Unidade = o.Unidade,
                        Valor = o.Valor,
                        Desc = o.ValorDesconto,
                        Total = o.Total,
                        ID = o.ID
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
            return (from o in db.PedidoItems where o.ID == pID select o).Single();
        }

        /// <summary>
        /// Método para retornar o ID de um terminado objeto
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <param name="pValor">Valor para pesquisa, normalmente o Código</param>
        /// <returns>Retorna o ID (int)</returns>
        public int getID(DataClassesDataContext db, string pValor)
        {
            return 0;
        }

        /// <summary>
        /// Método para retornar o maior Código de uma determinada classe
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna o MAX (int)</returns>
        public int RetornaMAXCodigo(DataClassesDataContext db)
        {
            return 0;
        }

        /// <summary>
        /// Método para retornar a maior sequencia de uma determinada lista
        /// </summary>
        /// <param name="db">Contexto do linq utilizado para pesquisa</param>
        /// <returns>Retorna o MAX (int)</returns>
        public Int32 RetornaMAXSequencia(Modelo.Pedido pPedido)
        {
            Int32 maxCodigo = 0;
            try
            {
                maxCodigo = (from c in pPedido.PedidoItems
                             where c.Sequencia == pPedido.PedidoItems.Max(b => b.Sequencia)
                             select c).Single().Sequencia;
                maxCodigo = maxCodigo + 1;
            }
            catch (Exception)
            {
                maxCodigo = 1;
            }
            return maxCodigo;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public Decimal getTotal()
        {
            this.Total = ((this.ValorDesconto != null) ? (decimal)(this.SubTotal - this.ValorDesconto) : 0);
            return this.Total;                                               
        }

        public bool Valida(ErrorProvider erro, Control.ControlCollection controles)
        {
            bool status = true;

            foreach (Control ctr in controles)
            {
                if ((ctr is GroupBox) || (ctr is TabControl) || (ctr is DevExpress.XtraTab.XtraTabControl) || (ctr is DevExpress.XtraTab.XtraTabPage))
                {
                    status = Valida(erro, ctr.Controls);
                }
                else
                {
                    switch (ctr.Name)
                    {
                        case "cbProduto":
                            erro.SetError(ctr, "");
                            if (((Componentes.devexpress.cwk_DevLookup)ctr).EditValue.ToString() == "0")
                            {
                                erro.SetError(ctr, "Produto não informado.");
                                status = false;
                            }
                            break;
                        case "txtQuantidade":
                            erro.SetError(ctr, "");
                            if (ctr.Text == "0")
                            {
                                erro.SetError(ctr, "Quantidade tem que ser diferente de zero (0).");
                                status = false;
                            }
                            break;
                        case "txtTotal":
                            erro.SetError(ctr, "");
                            if (ctr.Text == "")
                            {
                                erro.SetError(ctr, "Total tem que ser diferente de zero (0).");
                                status = false;
                            }
                            break;
                        default:
                            break;
                    }
                }
            }

            return status;
        }
    }
}
