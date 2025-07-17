using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace BLL.Cotacao
{
    public class Cotacao
    {
        #region Cotacao Cadastro Produto
        public static Dictionary<string, string> Salvar(Modelo.DataClassesDataContext db, Modelo.CotacaoProduto objCotacaoProduto)
        {
            Dictionary<string, string> ret = ValidaObjeto(objCotacaoProduto);
            if (ret.Count == 0)
            {
                db.SubmitChanges();
            }
            return ret;
        }

        public static Dictionary<string, string> ValidaObjeto(Modelo.CotacaoProduto objCotacaoProduto)
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (objCotacaoProduto.Quantidade == 0)
            {
                ret.Add("txtQuantidade", "Preencha a quantidade de produtos.");
            }

            if (objCotacaoProduto.IDProduto == null)
            {
                ret.Add("cbProduto", "Selecione um Produto da lista.");
            }

            return ret;

        }
        #endregion

        #region Cotacao
        public static Dictionary<string, string> Salvar(Modelo.DataClassesDataContext db, Modelo.Cotacao objCotacao, int ValidaForncedores)
        {
            Dictionary<string, string> ret = ValidaObjeto(objCotacao, ValidaForncedores);
            if (ret.Count == 0)
            {
                db.SubmitChanges();
            }
            return ret;
        }

        public static Dictionary<string, string> ValidaObjeto(Modelo.Cotacao objCotacao, int pValidaFornecedores )
        {
            Dictionary<string, string> ret = new Dictionary<string, string>();

            if (objCotacao.Resumo == String.Empty)
            {
                ret.Add("txtResumo", "Preencha o resumo da cotação.");
            }

            if (objCotacao.IDFornecedor_01 == null && objCotacao.IDFornecedor_02 == null && objCotacao.IDFornecedor_03 == null && objCotacao.IDFornecedor_04 == null && objCotacao.IDFornecedor_05 == null && objCotacao.IDFornecedor_06 == null && pValidaFornecedores != 0)
            {
                ret.Add("cbFornecedor1", "Escolha pelo menos um fornecedor.");
            }

            if (objCotacao.TipoCotacao == (Modelo.TipoCotacao)(-1)) 
            {
                ret.Add("cbTipoCotacao", "Escolha o tipo de Cotação que deseja.");
            }

            if (objCotacao.IDFilial == null) 
            {
                ret.Add("cbEmpresa", "Escolha uma empresa para a sua cotação.");
            }

            return ret;

        }

        public static IQueryable GetPedidoItensCotacao(Modelo.DataClassesDataContext db, Modelo.Cotacao objCotacao)
        {
            return from p in db.PedidoItems
                   join cpp in db.CotacaoProdutoPedidoItems on p.ID equals cpp.IDPedidoItem
                   join cp in db.CotacaoProdutos on cpp.IDCotacaoProduto equals cp.ID
                   where cp.IDCotacao == objCotacao.ID
                   select new
                   {
                       CodigoProduto = p.Produto.Codigo,
                       Produto = p.Produto.Nome,
                       Valor = p.Valor,
                       Quantidade = p.Quantidade,
                       Total = p.Total,
                       Pedido = p.Pedido.Codigo
                   };
        }
        #endregion

        /// <summary>
        /// Método que gera uma cotação do tipo lista para os produtos de uma requisição.
        /// Entram na lista, somente os produtos que estão com a quantidade em estoque físico abaixo da quantidade mínima.
        /// </summary>
        /// <param name="db">contexto</param>
        /// <param name="objRequisicao">requisição</param>
        /// <returns>caso a lista seja gerada retorna true</returns>
        public bool GerarListaProjeto(Modelo.DataClassesDataContext db, Modelo.Pedido objRequisicao)
        {
            List<Modelo.PedidoItem> itens = new List<Modelo.PedidoItem>();

            var aux = (from r in objRequisicao.PedidoItems
                       where r.Produto.Estoques.Count == 0
                       select r);

            itens.AddRange(aux.ToList());

            aux = (from i in db.PedidoItems
                    where i.IDPedido == objRequisicao.ID
                    join e in db.Estoques on i.IDProduto equals e.IDProduto
                    where (e.Qtd_Estoque_Pedido - i.Quantidade < e.Qtd_Estoque_Min)
                    select i);

            //List<Modelo.PedidoItem> aux2 = new List<Modelo.PedidoItem>();
            
            //aux2 = (from i in db.PedidoItems
            //        where i.IDPedido == objRequisicao.ID
            //        join e in db.Estoques on i.IDProduto equals e.IDProduto
            //        where (e.Qtd_Estoque_Pedido - i.Quantidade < e.Qtd_Estoque_Min)
            //        select new Modelo.PedidoItem
            //         {
            //             ID = i.ID,
            //             Sequencia = i.Sequencia,
            //             IDProduto = i.IDProduto,
            //             ProdutoNome = i.ProdutoNome,
            //             ProdutoDescReduzida = i.ProdutoDescReduzida,
            //             Quantidade = i.Quantidade + Math.Abs((e.Qtd_Estoque_Pedido - i.Quantidade) - e.Qtd_Estoque_Min),
            //             Unidade = i.Unidade,
            //             ValorCalculado = i.ValorCalculado,
            //             Valor = i.Valor,
            //             SubTotal = i.SubTotal,
            //             PercDesconto = i.PercDesconto,
            //             ValorDesconto = i.ValorDesconto,
            //             RAT_Desconto = i.RAT_Desconto,
            //             RAT_Acrescimo = i.RAT_Acrescimo,
            //             Total = i.Total,
            //             PesoBruto = i.PesoBruto,
            //             PesoLiquido = i.PesoLiquido,
            //             Dt = i.Dt,
            //             bReservaEstoque = i.bReservaEstoque,
            //             Ent_Sai = i.Ent_Sai,
            //             PrazoEntrega = i.PrazoEntrega,
            //             OS = i.OS,
            //             Gerente = i.Gerente,
            //             BaseICMSSubst = i.BaseICMSSubst,
            //             ValorRetidoICMS = i.ValorRetidoICMS,
            //             QtdCancelada = i.QtdCancelada,
            //             QtdRetirada = i.QtdRetirada,
            //             IDOrcamentoItem = i.IDOrcamentoItem
            //             InformacoesAdicionais = i.InformacoesAdicionais
            //         }).ToList();


              
            itens.AddRange(aux.ToList());

            if (itens.Count() > 0)
            {
                Modelo.Cotacao objCotacao = new Modelo.Cotacao();
                objCotacao.TipoCotacao = Modelo.TipoCotacao.Lista;
                objCotacao.IDFilial = objRequisicao.IDFilial;
                try
                {
                    objCotacao.Codigo = (from c in db.Cotacaos
                                         select c.Codigo).Max();
                }
                catch(Exception)
                {
                }
                objCotacao.Codigo++;
                objCotacao.Resumo = "REQUISIÇÃO: " + objRequisicao.Codigo.ToString();
                if (objRequisicao.Projeto != null)
                    objCotacao.Resumo += " | " + objRequisicao.Projeto.Nome;

                objCotacao.Status = 1;
                objCotacao.Observacao = String.Empty;
                Modelo.CotacaoProduto objCotacaoProduto;
                Modelo.CotacaoProdutoPedidoItem objCotacaoProdPedioItem;
                int codigoCotProd = 1;
                foreach (Modelo.PedidoItem item in itens)
                {                    
                    objCotacaoProduto = new Modelo.CotacaoProduto();
                    objCotacaoProduto.Codigo = codigoCotProd;
                    objCotacaoProduto.IDProduto = item.IDProduto;
                    objCotacaoProduto.Quantidade = item.Quantidade;
                    objCotacaoProduto.DescricaoProduto = item.Produto.Nome;

                    objCotacaoProdPedioItem = new Modelo.CotacaoProdutoPedidoItem();
                    objCotacaoProdPedioItem.Codigo = codigoCotProd;
                    objCotacaoProdPedioItem.PedidoItem = item;
                    objCotacaoProdPedioItem.Quantidade = item.Quantidade;
                    objCotacaoProduto.CotacaoProdutoPedidoItems.Add(objCotacaoProdPedioItem);

                    objCotacao.CotacaoProdutos.Add(objCotacaoProduto);
                    codigoCotProd++;
                }

                db.Cotacaos.InsertOnSubmit(objCotacao);
                db.SubmitChanges();
                return true;
            }
            return false;
        }
    }
}