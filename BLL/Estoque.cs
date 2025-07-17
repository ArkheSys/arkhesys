using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class Estoque
    {
        public struct StructEstoque
        {
            public int idProduto;
            public decimal? qtdDescontar_Fisico;
            public decimal? qtdAcrescentar_Fisico;

            public decimal? qtdEstoque_Pedido;
            public decimal? qtdCompra_Pedido;
        }

        #region Atualização do Estoque por meio de Nota Fiscal emitida
        /// <summary>
        /// Esse método não persiste os dados diretamente no bd.
        /// Para que o estoque seja atualizado no bd é necessário invocar o método SubimitChanges do contexto
        /// passado por parâmetro.
        /// </summary>
        /// <param name="objNota">nota</param>
        /// /// <param name="excluirNota">indica se é uma nota sendo excluída</param>
        /// <param name="db">contexto</param>
        public void AtualizaEstoqueNota(Modelo.Nota objNota, bool excluirNota, Modelo.DataClassesDataContext db)
        {
            List<StructEstoque> produtosAtualizar = new List<StructEstoque>();

            AuxAtualizaEstoqueNota(objNota, excluirNota, produtosAtualizar);

            Atualizar(produtosAtualizar, objNota.Filial.ID, db);
        }
        /// <summary>
        /// Esse método não persiste os dados diretamente no bd.
        /// Para que o estoque seja atualizado no bd é necessário invocar o método SubimitChanges do contexto
        /// passado por parâmetro. 
        /// </summary>
        /// <param name="notas">notas</param>
        /// <param name="idFilial">id da filial</param>
        /// <param name="db">contexto</param>
        public void AtualizaEstoqueNota(List<Modelo.Nota> notas, int idFilial, Modelo.DataClassesDataContext db)
        {
            List<StructEstoque> produtosAtualizar = new List<StructEstoque>();
            foreach (Modelo.Nota objNota in notas)
            {
                AuxAtualizaEstoqueNota(objNota, false, produtosAtualizar);
            }

            Atualizar(produtosAtualizar, idFilial, db);
        }

        private void AuxAtualizaEstoqueNota(Modelo.Nota objNota, bool excluirNota, List<StructEstoque> produtosAtualizar)
        {
            if (!excluirNota)
            {
                StructEstoque stcEstoque;
                foreach (Modelo.NotaItem item in objNota.NotaItems)
                {
                    if (item.bAtualizaEstoque == Modelo.TituloType.Sim)
                    {
                        if (item.ID > 0)
                        {
                            stcEstoque = EstoqueAtualizacaoNota(item);
                        }
                        else
                        {
                            stcEstoque = EstoqueInclusaoNota(item);
                        }

                        produtosAtualizar.Add(stcEstoque);
                    }
                }

                if (objNota.itensExcluir.Count > 0)
                {
                    produtosAtualizar.AddRange(EstoqueExclusaoNota(objNota.itensExcluir));
                }
            }
            else
            {
                //Vai excluir a nota e todos os seus itens
                produtosAtualizar.AddRange(EstoqueExclusaoNota(objNota.NotaItems.ToList()));
            }
        }

        private StructEstoque EstoqueInclusaoNota(Modelo.NotaItem item)
        {
            StructEstoque stcEstoque = new StructEstoque()
            {
                idProduto = item.IDProduto
            };
            if (item.Ent_Sai == Modelo.InOutType.Entrada)
            {
                //stcEstoque.qtdAcrescentar_Fisico = 0;
                //stcEstoque.qtdDescontar_Fisico = item.Quantidade;

                stcEstoque.qtdAcrescentar_Fisico = item.Quantidade;
                stcEstoque.qtdDescontar_Fisico = 0;

                if (item.PedidoItem != null)
                {
                    if (item.PedidoItem.bReservaEstoque == Modelo.TituloType.Sim)
                    {
                        stcEstoque.qtdCompra_Pedido = -item.Quantidade;
                    }
                }
            }
            else
            {
                stcEstoque.qtdAcrescentar_Fisico = 0;
                stcEstoque.qtdDescontar_Fisico = item.Quantidade;

                //stcEstoque.qtdAcrescentar_Fisico = item.Quantidade;
                //stcEstoque.qtdDescontar_Fisico = 0;

                if (item.PedidoItem != null)
                {
                    if (item.PedidoItem.bReservaEstoque == Modelo.TituloType.Sim)
                    {
                        stcEstoque.qtdEstoque_Pedido = -item.Quantidade;
                    }
                }
            }
            return stcEstoque;
        }

        private StructEstoque EstoqueAtualizacaoNota(Modelo.NotaItem item)
        {
            StructEstoque stcEstoque = new StructEstoque()
            {
                idProduto = item.IDProduto
            };
            if (item.Ent_Sai == Modelo.InOutType.Entrada)
            {
                stcEstoque.qtdAcrescentar_Fisico = item.Quantidade;
                stcEstoque.qtdDescontar_Fisico = item.Quantidade_Ant;

                if (item.PedidoItem != null)
                {
                    if (item.PedidoItem.bReservaEstoque == Modelo.TituloType.Sim)
                    {
                        stcEstoque.qtdCompra_Pedido = (item.Quantidade - item.Quantidade_Ant);
                    }
                }
            }
            else
            {
                stcEstoque.qtdAcrescentar_Fisico = item.Quantidade_Ant;
                stcEstoque.qtdDescontar_Fisico = item.Quantidade;

                if (item.PedidoItem != null)
                {
                    if (item.PedidoItem.bReservaEstoque == Modelo.TituloType.Sim)
                    {
                        stcEstoque.qtdEstoque_Pedido = (item.Quantidade - item.Quantidade_Ant);
                    }
                }
            }
            return stcEstoque;
        }

        private List<StructEstoque> EstoqueExclusaoNota(List<Modelo.NotaItem> itensExclusao)
        {
            List<StructEstoque> ret = new List<StructEstoque>();
            StructEstoque stcEstoque;
            foreach (Modelo.NotaItem item in itensExclusao)
            {
                if (item.bAtualizaEstoque == Modelo.TituloType.Sim)
                {
                    stcEstoque = new StructEstoque()
                    {
                        idProduto = item.IDProduto
                    };
                    if (item.Ent_Sai == Modelo.InOutType.Entrada)
                    {
                        stcEstoque.qtdAcrescentar_Fisico = 0;
                        stcEstoque.qtdDescontar_Fisico = item.Quantidade_Ant;

                        if (item.PedidoItem != null)
                        {
                            if (item.PedidoItem.bReservaEstoque == Modelo.TituloType.Sim)
                            {
                                stcEstoque.qtdCompra_Pedido = item.Quantidade_Ant;
                            }
                        }
                    }
                    else
                    {
                        stcEstoque.qtdAcrescentar_Fisico = item.Quantidade_Ant;
                        stcEstoque.qtdDescontar_Fisico = 0;

                        if (item.PedidoItem != null)
                        {
                            if (item.PedidoItem.bReservaEstoque == Modelo.TituloType.Sim)
                            {
                                stcEstoque.qtdEstoque_Pedido = item.Quantidade_Ant;
                            }
                        }
                    }
                    ret.Add(stcEstoque);
                }
            }

            return ret;
        }
        #endregion

        #region Atualização do Estoque Reservado por meio de Pedido
        /// <summary>
        /// Esse método não persiste os dados diretamente no bd.
        /// Para que o estoque seja atualizado no bd é necessário invocar o método SubimitChanges do contexto
        /// passado por parâmetro.
        /// </summary>
        /// <param name="objPedido">pedido</param>
        /// /// <param name="excluirPedido">indica se é um pedido sendo excluído</param>
        /// <param name="db">contexto</param>
        public void AtualizaEstoquePedido(Modelo.Pedido objPedido, bool excluirPedido, Modelo.DataClassesDataContext db)
        {
            List<StructEstoque> produtosAtualizar = new List<StructEstoque>();
            if (!excluirPedido)
            {
                StructEstoque stcEstoque;
                foreach (Modelo.PedidoItem item in objPedido.PedidoItems.Where(p => !objPedido.PedidoItems.Contains(p.PedidoItem1)))
                {
                    if (item.bReservaEstoque == Modelo.TituloType.Sim)
                    {
                        if (item.ID > 0)
                        {
                            stcEstoque = EstoqueAtualizacaoPedido(item);
                        }
                        else
                        {
                            stcEstoque = EstoqueInclusaoPedido(item);
                        }

                        produtosAtualizar.Add(stcEstoque);
                    }
                }

                if (objPedido.itensExcluir.Count > 0)
                {
                    produtosAtualizar.AddRange(EstoqueExclusaoPedido(objPedido.itensExcluir));
                }
            }
            else
            {
                //Vai excluir a nota e todos os seus itens
                produtosAtualizar.AddRange(EstoqueExclusaoPedido(objPedido.PedidoItems.ToList()));
            }

            Atualizar(produtosAtualizar, objPedido.Filial != null ? objPedido.Filial.ID : objPedido.IDFilial.Value, db);
        }

        private StructEstoque EstoqueInclusaoPedido(Modelo.PedidoItem item)
        {
            StructEstoque stcEstoque = new StructEstoque()
            {
                idProduto = item.IDProduto
            };
            if (item.Ent_Sai == Modelo.InOutType.Entrada)
            {
                stcEstoque.qtdCompra_Pedido = item.Quantidade;
            }
            else
            {
                stcEstoque.qtdEstoque_Pedido = item.Quantidade;
            }
            return stcEstoque;
        }

        private StructEstoque EstoqueAtualizacaoPedido(Modelo.PedidoItem item)
        {
            StructEstoque stcEstoque = new StructEstoque()
            {
                idProduto = item.IDProduto
            };
            if (item.Ent_Sai == Modelo.InOutType.Entrada)
            {
                stcEstoque.qtdCompra_Pedido = (item.Quantidade - item.Quantidade_Ant);
            }
            else
            {
                stcEstoque.qtdEstoque_Pedido = (item.Quantidade - item.Quantidade_Ant);
            }
            return stcEstoque;
        }

        private List<StructEstoque> EstoqueExclusaoPedido(List<Modelo.PedidoItem> itensExclusao)
        {
            List<StructEstoque> ret = new List<StructEstoque>();
            StructEstoque stcEstoque;
            foreach (Modelo.PedidoItem item in itensExclusao)
            {
                if (item.bReservaEstoque == Modelo.TituloType.Sim)
                {
                    stcEstoque = new StructEstoque()
                    {
                        idProduto = item.IDProduto
                    };
                    if (item.Ent_Sai == Modelo.InOutType.Entrada)
                    {
                        stcEstoque.qtdCompra_Pedido = -item.Quantidade_Ant;
                    }
                    else
                    {
                        stcEstoque.qtdEstoque_Pedido = -item.Quantidade_Ant;
                    }
                    ret.Add(stcEstoque);
                }
            }

            return ret;
        }

        #endregion

        private void Atualizar(List<StructEstoque> produtosAtualizar, int idFilial, Modelo.DataClassesDataContext db)
        {
            int[] idsProduto = (from p in produtosAtualizar select p.idProduto).ToArray();
            List<Modelo.Estoque> estoques = db.Estoques.Where(
                                                            e => e.IDFilial == idFilial
                                                            && idsProduto.Contains(e.IDProduto)
                                                        ).ToList();
            foreach (Modelo.Estoque est in estoques)
            {
                db.Refresh(System.Data.Linq.RefreshMode.OverwriteCurrentValues, est);
            }
            Modelo.Estoque objEstoque;
            foreach (StructEstoque item in produtosAtualizar)
            {
                var aux = estoques.Where(e => e.IDProduto == item.idProduto);
                if (aux.Count() > 0)
                {
                    objEstoque = aux.First();
                }
                else
                {
                    objEstoque = new Modelo.Estoque();
                    objEstoque.IDFilial = idFilial;
                    objEstoque.Localidade = String.Empty;
                    objEstoque.IDProduto = item.idProduto;
                    db.Estoques.InsertOnSubmit(objEstoque);
                    estoques.Add(objEstoque);
                }

                //Atualiza o Estoque Físico
                if (item.qtdAcrescentar_Fisico.HasValue)
                    objEstoque.Qtd_Estoque_Fisico += item.qtdAcrescentar_Fisico.Value;
                if (item.qtdDescontar_Fisico.HasValue)
                    objEstoque.Qtd_Estoque_Fisico -= item.qtdDescontar_Fisico.Value;
                //Atualiza o Estoque Reservado
                if (item.qtdCompra_Pedido.HasValue)
                    objEstoque.Qtd_Compra_Pedido += item.qtdCompra_Pedido.Value;
                if (item.qtdEstoque_Pedido.HasValue)
                    objEstoque.Qtd_Estoque_Pedido += item.qtdEstoque_Pedido.Value;
            }
            db.SubmitChanges();
        }

        public IQueryable ConsultarEstoque(int idProduto, Modelo.DataClassesDataContext db)
        {
            return (from e in db.Estoques
                    join p in db.Produtos on e.IDProduto equals p.ID
                    where e.IDProduto == idProduto
                    select new
                    {
                        Empresa = e.Filial.Nome,
                        EstoqueFisico = e.Qtd_Estoque_Fisico,
                        Reservas = e.Qtd_Estoque_Pedido,
                        Efetivo = e.Qtd_Estoque_Fisico - e.Qtd_Estoque_Pedido,
                        //Compra = e.Qtd_Compra,
                        CompraPedido = e.Qtd_Compra_Pedido,
                        Localidade = e.Localidade,
#pragma warning disable CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
                        EstoqueMinimo = e.Qtd_Estoque_Min == null ? 0 : e.Qtd_Estoque_Min
#pragma warning restore CS0472 // O resultado da expressão é sempre 'false', pois um valor do tipo 'decimal' nunca é igual a "null" do tipo 'decimal?'
                    });
        }
    }
}
