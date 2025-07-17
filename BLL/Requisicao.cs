using System;
using System.Collections.Generic;
using System.Linq;

namespace BLL
{
    public partial class OrdemProducao
    {
        #region Requisicao

        private static Modelo.Pedido GerarRequisicao(Modelo.DataClassesDataContext db, int? iDTipoRequisicao, int? idFilial, Modelo.Pessoa pObjPessoa, int? idProjeto, IList<Modelo.Objeto.pxyProjetoProduto> produtos)
        {
            Modelo.Pedido objRequisicao = new Modelo.Pedido();
            objRequisicao.Codigo = objRequisicao.EncontraCodigoDisponivel(db);
            objRequisicao.Numero = objRequisicao.BuscaUltimoNumero(db, (int)idFilial, Modelo.TipoPedidoType.Requisição);
            objRequisicao.IDFilial = idFilial;
            objRequisicao.IDPessoa = pObjPessoa.ID;
            objRequisicao.IDProjeto = idProjeto;
            objRequisicao.PessoaNome = pObjPessoa.Nome;
            objRequisicao.TipoPedido = db.TipoPedidos.Where(t => t.ID == iDTipoRequisicao).Single();
            objRequisicao.Dt = DateTime.Today;
            objRequisicao.DtDigitacao = objRequisicao.Dt;
            objRequisicao.TotalPedido = 0;
            objRequisicao.TotalProduto = 0;
            objRequisicao.ValorAcrescimo = 0;
            objRequisicao.PercDesconto = 0;
            objRequisicao.ValorDesconto = 0;
            objRequisicao.Ent_Sai = Modelo.InOutType.Saída;
            objRequisicao.Status = Modelo.StatusOrcamento.Aberto;

            Modelo.PedidoItem objPedidoItem;
            int cod = 1;
            foreach (Modelo.Objeto.pxyProjetoProduto item in produtos)
            {
                objPedidoItem = new Modelo.PedidoItem();
                objPedidoItem.Sequencia = cod++;
                objPedidoItem.Produto = item.Produto;
                objPedidoItem.ProdutoNome = item.Produto.Nome;
                objPedidoItem.ProdutoDescReduzida = item.Produto.DescReduzida;
                objPedidoItem.Quantidade = item.Quantidade;
                objPedidoItem.Valor = item.Produto.PrecoBase.HasValue ? item.Produto.PrecoBase.Value : 0;
                objPedidoItem.ValorCalculado = objPedidoItem.Valor;
                objPedidoItem.ValorDesconto = 0;
                objPedidoItem.PercDesconto = 0;
                objPedidoItem.SubTotal = objPedidoItem.Valor * objPedidoItem.Quantidade;
                objPedidoItem.Total = objPedidoItem.SubTotal;
                objPedidoItem.Unidade = item.Produto.Unidade.Sigla;
                objPedidoItem.Ent_Sai = objRequisicao.Ent_Sai;
                objPedidoItem.Dt = objRequisicao.Dt;
                objPedidoItem.bReservaEstoque = objRequisicao.TipoPedido.bReservaEstoque;
                objPedidoItem.PesoBruto = item.Produto.PesoBruto;
                objPedidoItem.PesoLiquido = item.Produto.PesoLiquido;
                objRequisicao.PedidoItems.Add(objPedidoItem);
            }

            db.Pedidos.InsertOnSubmit(objRequisicao);
            db.SubmitChanges();
            return objRequisicao;
        }

        public static Modelo.Pedido GerarRequisicaoProdutos(Modelo.DataClassesDataContext db, Modelo.Configuracao objConfiguracao, Modelo.Projeto objProjeto)
        {
            var produtosProjeto = (from p in objProjeto.Projeto_ProdutoServicos where p.Produto != null select p);
            if (!objConfiguracao.IDTipoRequisicaoProduto.HasValue || objProjeto.Status != 1 || produtosProjeto.Count() == 0)
                return null;

            List<Modelo.Objeto.pxyProjetoProduto> produtos = new List<Modelo.Objeto.pxyProjetoProduto>();
            Modelo.Objeto.pxyProjetoProduto objProduto;
            foreach (Modelo.Projeto_ProdutoServico item in produtosProjeto)
            {
                objProduto = new Modelo.Objeto.pxyProjetoProduto();
                objProduto.Produto = item.Produto;
                objProduto.Quantidade = item.Quantidade;
                produtos.Add(objProduto);
            }

            return GerarRequisicao(db, objConfiguracao.IDTipoRequisicaoProduto.Value, objProjeto.IDFilial, objProjeto.Pessoa, objProjeto.ID, produtos);
        }

        public static Modelo.Pedido GerarRequisicaoMateriais(Modelo.DataClassesDataContext db, Modelo.Configuracao objConfiguracao, Modelo.Projeto objProjeto)
        {
            if (!objConfiguracao.IDTipoRequisicaoMaterial.HasValue || objProjeto.Status != 1 || objProjeto.Projeto_Materials.Count() == 0)
                return null;

            List<Modelo.Objeto.pxyProjetoProduto> produtos = new List<Modelo.Objeto.pxyProjetoProduto>();
            Modelo.Objeto.pxyProjetoProduto objProduto;
            foreach (Modelo.Projeto_Material item in objProjeto.Projeto_Materials)
            {
                objProduto = new Modelo.Objeto.pxyProjetoProduto();
                objProduto.Produto = item.Produto;
                objProduto.Quantidade = item.Quantidade;
                produtos.Add(objProduto);
            }

            return GerarRequisicao(db, objConfiguracao.IDTipoRequisicaoMaterial.Value, objProjeto.IDFilial, objProjeto.Pessoa, objProjeto.ID, produtos);
        }

        public static string DeletarRequisicoesProjeto(Modelo.DataClassesDataContext db, Modelo.Projeto pObjProjeto)
        {

            List<Modelo.Pedido> ListaobjRequisicao = (from c in db.Pedidos
                                                      where c.IDProjeto == pObjProjeto.ID
                                                      select c).ToList();

#pragma warning disable CS0168 // A variável "idCotacao" está declarada, mas nunca é usada
            string idCotacao;
#pragma warning restore CS0168 // A variável "idCotacao" está declarada, mas nunca é usada
            List<int?> listaIdPedidoItem = new List<int?>();
            List<int> listaIdCotacao = new List<int>();
            List<Modelo.TipoPedidoType> listaTipoPedidos = new List<Modelo.TipoPedidoType>();
            bool ControleDelete = false;
            foreach (Modelo.Pedido itemPedido in ListaobjRequisicao)
            {
                if (itemPedido.TipoPedido.Tipo != Modelo.TipoPedidoType.Requisição)
                {
                    ControleDelete = true; break;
                }
                foreach (Modelo.PedidoItem item in itemPedido.PedidoItems)
                {
                    listaIdPedidoItem.Add(item.ID);
                }
            }

            if (!ControleDelete)
            {

                listaIdCotacao = (from c in db.Cotacaos
                                  join p in db.CotacaoProdutos on c.ID equals p.IDCotacao
                                  join pi in db.CotacaoProdutoPedidoItems on p.ID equals pi.IDCotacaoProduto
                                  where listaIdPedidoItem.Contains(pi.IDPedidoItem)
                                  select c.ID).ToList();


                List<Modelo.Cotacao> ListaobjCotacao = (from c in db.Cotacaos
                                                        where listaIdCotacao.Contains(c.ID)
                                                        select c).ToList();
                foreach (Modelo.Cotacao objCotacao in ListaobjCotacao)
                {
                    db.Cotacaos.DeleteOnSubmit(objCotacao);
                }

                foreach (Modelo.Pedido objRequisicao in ListaobjRequisicao)
                {
                    if (objRequisicao.Status == Modelo.StatusOrcamento.Aberto)
                        db.Pedidos.DeleteOnSubmit(objRequisicao);
                    else
                        return "As requisições relacionadas a este projeto devem estar com status aberto";

                }
                List<Modelo.OrdemProducao> ListaobjOrdemProducao = (from ordemproducao in db.OrdemProducaos
                                                                    where ordemproducao.IdProjeto == pObjProjeto.ID
                                                                    select ordemproducao).ToList();

                foreach (Modelo.OrdemProducao objOrdemProducao in ListaobjOrdemProducao)
                {
                    if (objOrdemProducao.OrdemProducaoExecucaos.Count > 0)
                    {
                        return "A ordem de produção relacionada à este projeto já está em execução.";
                    }
                    else
                    {
                        if (objOrdemProducao.ClassificacaoOrdemProducao.Codigo == 1)
                            db.OrdemProducaos.DeleteOnSubmit(objOrdemProducao);
                        else
                            return "A ordem de produção relacionada com este projeto deve estar com o status aberto";
                    }
                }

                return "";
            }
            else
            {
                return "Existe um pedido ou orçamento vinculado a este projeto.";
            }

        }

        #endregion

        public static bool GerarOrdemProducao(Modelo.DataClassesDataContext db, Modelo.Projeto objProjeto)
        {
            

            var servicos = (from p in objProjeto.Projeto_ProdutoServicos where p.IDServico.HasValue select p);
            if (objProjeto.Status != 1)
                return false;

            List<Modelo.OrdemProducao> ops = new List<Modelo.OrdemProducao>();

            foreach (var servico in servicos)
            {
                Modelo.OrdemProducao objOrdemProducao = ops.FirstOrDefault(a => a.Projeto_ProdutoServicos.First().Agrupamento == servico.Agrupamento) ??
                                                 new Modelo.OrdemProducao{
                                                                         IdFilial = objProjeto.IDFilial.Value,
                                                                         IdCliente = objProjeto.IDPessoa.Value,
                                                                         IdProjeto = objProjeto.ID,
                                                                         Projeto =  objProjeto,
                                                                         IdClassificacaoOrdemProducao = 1,
                                                                         IncData = DateTime.Now,
                                                                         
                                                                         };

                if (objOrdemProducao.Codigo == 0)
                    objOrdemProducao.Codigo = objOrdemProducao.RetornaMAXCodigo(db) + ops.Count;

                servico.OrdemProducao = objOrdemProducao;
                if (!ops.Contains(objOrdemProducao))
                {
                    ops.Add(objOrdemProducao);
                    db.OrdemProducaos.InsertOnSubmit(objOrdemProducao);
                }
            }


            db.SubmitChanges();
           
            return true;
        }
    }
}
