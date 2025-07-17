using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Transactions;

namespace BLL.Cotacao
{
    public class GeraOrdemCompra
    {
        public static void GerarOrdemCompraMenorPrecoTotal(Modelo.DataClassesDataContext db, Modelo.Cotacao objCotacao, DateTime data, DateTime dataValidade)
        {
            decimal[] precos = new decimal[6];
            foreach (Modelo.CotacaoProduto item in objCotacao.CotacaoProdutos)
            {
                precos[0] += item.Preco_01.HasValue ? item.Preco_01.Value : 0;
                precos[1] += item.Preco_02.HasValue ? item.Preco_02.Value : 0;
                precos[2] += item.Preco_03.HasValue ? item.Preco_03.Value : 0;
                precos[3] += item.Preco_04.HasValue ? item.Preco_04.Value : 0;
                precos[4] += item.Preco_05.HasValue ? item.Preco_05.Value : 0;
                precos[5] += item.Preco_06.HasValue ? item.Preco_06.Value : 0;
            }
            int index = 0; decimal valor = 0;
            for (int i = 0; i < precos.Length; i++)
            {
                if (i == 0)
                {
                    index = i;
                    valor = precos[index];
                }
                else if (precos[i] < valor && precos[i] != 0)
                {
                    index = i;
                    valor = precos[i];
                }
            }
            GerarOrcamentoCompra(db, index + 1, objCotacao, objCotacao.CotacaoProdutos, data, dataValidade);
        }

        public static void GerarOrdemCompraMenorPrecoProd(Modelo.DataClassesDataContext db, Modelo.Cotacao objCotacao, DateTime data, DateTime dataValidade)
        {
            decimal?[] precos = new decimal?[6];
            List<Modelo.CotacaoProduto> produtosFornecedor1 = new List<Modelo.CotacaoProduto>();
            List<Modelo.CotacaoProduto> produtosFornecedor2 = new List<Modelo.CotacaoProduto>();
            List<Modelo.CotacaoProduto> produtosFornecedor3 = new List<Modelo.CotacaoProduto>();
            List<Modelo.CotacaoProduto> produtosFornecedor4 = new List<Modelo.CotacaoProduto>();
            List<Modelo.CotacaoProduto> produtosFornecedor5 = new List<Modelo.CotacaoProduto>();
            List<Modelo.CotacaoProduto> produtosFornecedor6 = new List<Modelo.CotacaoProduto>();
            int indexMenorPreco = 0;
            decimal menorPreco = 0;
            foreach (Modelo.CotacaoProduto item in objCotacao.CotacaoProdutos)
            {
                precos[0] = item.Preco_01;
                precos[1] = item.Preco_02;
                precos[2] = item.Preco_03;
                precos[3] = item.Preco_04;
                precos[4] = item.Preco_05;
                precos[5] = item.Preco_06;
                indexMenorPreco = -1;
                for (int i = 0; i < precos.Length; i++)
                {
                    if (i == 0)
                    {
                        if (precos[i].HasValue)
                        {
                            menorPreco = precos[i].Value;
                            indexMenorPreco = i;
                        }
                        else
                            break;
                    }
                    else if (precos[i].HasValue)
                    {
                        if (precos[i].Value < menorPreco)
                        {
                            menorPreco = precos[i].Value;
                            indexMenorPreco = i;
                        }
                    }
                }
                if (indexMenorPreco >= 0)
                {
                    switch (indexMenorPreco)
                    {
                        case 0:
                            produtosFornecedor1.Add(item);
                            break;
                        case 1:
                            produtosFornecedor2.Add(item);
                            break;
                        case 2:
                            produtosFornecedor3.Add(item);
                            break;
                        case 3:
                            produtosFornecedor4.Add(item);
                            break;
                        case 4:
                            produtosFornecedor5.Add(item);
                            break;
                        case 5:
                            produtosFornecedor6.Add(item);
                            break;
                    }
                }
            }

            if (produtosFornecedor1.Count > 0)
                GerarOrcamentoCompra(db, 1, objCotacao, produtosFornecedor1, data, dataValidade);
            if (produtosFornecedor2.Count > 0)
                GerarOrcamentoCompra(db, 2, objCotacao, produtosFornecedor2, data, dataValidade);
            if (produtosFornecedor3.Count > 0)
                GerarOrcamentoCompra(db, 3, objCotacao, produtosFornecedor3, data, dataValidade);
            if (produtosFornecedor4.Count > 0)
                GerarOrcamentoCompra(db, 4, objCotacao, produtosFornecedor4, data, dataValidade);
            if (produtosFornecedor5.Count > 0)
                GerarOrcamentoCompra(db, 5, objCotacao, produtosFornecedor5, data, dataValidade);
            if (produtosFornecedor6.Count > 0)
                GerarOrcamentoCompra(db, 6, objCotacao, produtosFornecedor6, data, dataValidade);
        }

        public static void GerarOrdemCompraSelecionado(Modelo.DataClassesDataContext db, Modelo.Cotacao objCotacao, int fornecedor, DateTime data, DateTime dataValidade)
        {
            GerarOrcamentoCompra(db, fornecedor, objCotacao, objCotacao.CotacaoProdutos, data, dataValidade);
        }

        private static void GerarOrcamentoCompra(Modelo.DataClassesDataContext db, int fornecedor, Modelo.Cotacao objCotacao, IList<Modelo.CotacaoProduto> produtos, DateTime data, DateTime dataValidade)
        {
            Modelo.Pedido objPedido = new Modelo.Pedido();
            objPedido.Codigo = objPedido.EncontraCodigoDisponivel(db);
            objPedido.Ent_Sai = Modelo.InOutType.Entrada;
            objPedido.Status = Modelo.StatusOrcamento.Aberto;
            objPedido.IDFilial = objCotacao.IDFilial;
            objPedido.DtDigitacao = DateTime.Now;
            objPedido.Dt = data;
            objPedido.DtValidade = dataValidade;
            var auxTipoPedido = db.TipoPedidos.Where(p => p.Ent_Sai == Modelo.InOutType.Entrada).OrderBy(t => t.Codigo).Take(1);
            if (auxTipoPedido.Count() > 0)
                objPedido.TipoPedido = auxTipoPedido.First();
            var auxTabelaPreco = db.TabelaPrecos.OrderBy(t => t.Codigo).Take(1);
            if (auxTabelaPreco.Count() > 0)
                objPedido.TabelaPreco = auxTabelaPreco.First();
            switch (fornecedor)
            {
                case 1:
                    objPedido.IDPessoa = objCotacao.IDFornecedor_01.Value;
                    objPedido.PercDesconto = objCotacao.Desconto_01;
                    var c1 = db.Condicaos.Where(c => c.ID == objCotacao.IDCondicao_01);
                    if (c1.Count() > 0)
                        objPedido.Condicao = c1.First();
                    break;
                case 2:
                    objPedido.IDPessoa = objCotacao.IDFornecedor_02.Value;
                    objPedido.PercDesconto = objCotacao.Desconto_02;
                    var c2 = db.Condicaos.Where(c => c.ID == objCotacao.IDCondicao_02);
                    if (c2.Count() > 0)
                        objPedido.Condicao = c2.First();
                    break;
                case 3:
                    objPedido.IDPessoa = objCotacao.IDFornecedor_03.Value;
                    objPedido.PercDesconto = objCotacao.Desconto_03;
                    var c3 = db.Condicaos.Where(c => c.ID == objCotacao.IDCondicao_03);
                    if (c3.Count() > 0)
                        objPedido.Condicao = c3.First();
                    break;
                case 4:
                    objPedido.IDPessoa = objCotacao.IDFornecedor_04.Value;
                    objPedido.PercDesconto = objCotacao.Desconto_04;
                    var c4 = db.Condicaos.Where(c => c.ID == objCotacao.IDCondicao_04);
                    if (c4.Count() > 0)
                        objPedido.Condicao = c4.First();
                    break;
                case 5:
                    objPedido.IDPessoa = objCotacao.IDFornecedor_05.Value;
                    objPedido.PercDesconto = objCotacao.Desconto_05;
                    var c5 = db.Condicaos.Where(c => c.ID == objCotacao.IDCondicao_05);
                    if (c5.Count() > 0)
                        objPedido.Condicao = c5.First();
                    break;
                case 6:
                    objPedido.IDPessoa = objCotacao.IDFornecedor_06.Value;
                    objPedido.PercDesconto = objCotacao.Desconto_06;
                    var c6 = db.Condicaos.Where(c => c.ID == objCotacao.IDCondicao_06);
                    if (c6.Count() > 0)
                        objPedido.Condicao = c6.First();
                    break;
            }
            if (objPedido.Condicao == null)
            {
                var auxCondicao = db.Condicaos.OrderBy(t => t.Codigo).Take(1);
                if (auxCondicao.Count() > 0)
                    objPedido.Condicao = auxCondicao.First();
            }
            objPedido.Pessoa = db.Pessoas.Where(p => p.ID == objPedido.IDPessoa).First();
            objPedido.PessoaNome = objPedido.Pessoa.Nome;
            objPedido.PessoaCNPJCPF = objPedido.Pessoa.CNPJ_CPF;
            objPedido.PessoaInscRG = objPedido.Pessoa.TipoPessoa == "Física" ? objPedido.Pessoa.RG : objPedido.Pessoa.Inscricao;
            var ends = objPedido.Pessoa.Enderecos.Where(e => e.bPrincipal == 1);
            if (ends.Count() > 0)
            {
                Modelo.Endereco objEndereco = ends.First();
                objPedido.PessoaBairro = objEndereco.Bairro;
                objPedido.PessoaCEP = objEndereco.CEP;
                objPedido.PessoaCidade = objEndereco.Cidade.Nome;
                objPedido.PessoaEndereco = objEndereco.Rua;
                objPedido.PessoaUF = objEndereco.Cidade.UF.Sigla;
            }
            CriarItens(fornecedor, objCotacao, produtos, objPedido);
            objPedido.TotalProduto = objPedido.PedidoItems.Sum(p => p.Total);
            objPedido.ValorDesconto = (objPedido.TotalProduto * objPedido.PercDesconto);
            objPedido.PercDesconto = objPedido.PercDesconto * 100;
            objPedido.ValorAcrescimo = 0;
            objPedido.TotalPedido = objPedido.TotalProduto - objPedido.ValorDesconto;
            if (!objPedido.gerarParcelas(db, null))
            {
                throw new Exception("Erro ao gerar orçamento. Não foi possível gerar as parcelas, verifique se as condições de pagamento estão configuradas corretamente.");
            }
            db.Pedidos.InsertOnSubmit(objPedido);
            Modelo.CotacaoProdutoPedidoItem cotacaoProdutoPedidoItem;
            int count = 1;
            foreach (Modelo.PedidoItem item in objPedido.PedidoItems)
            {
                cotacaoProdutoPedidoItem = new Modelo.CotacaoProdutoPedidoItem();
                cotacaoProdutoPedidoItem.Codigo = count++;
                cotacaoProdutoPedidoItem.PedidoItem = item;
                var c = objCotacao.CotacaoProdutos.Where(p => p.IDProduto == item.IDProduto);
                if (c.Count() > 0)
                    cotacaoProdutoPedidoItem.CotacaoProduto = c.First();
                else
                    continue;
                cotacaoProdutoPedidoItem.Quantidade = cotacaoProdutoPedidoItem.CotacaoProduto.Quantidade;
                db.CotacaoProdutoPedidoItems.InsertOnSubmit(cotacaoProdutoPedidoItem);
            }
            db.SubmitChanges();
        }

        private static void CriarItens(int fornecedor, Modelo.Cotacao objCotacao, IList<Modelo.CotacaoProduto> produtos, Modelo.Pedido objPedido)
        {
            Modelo.PedidoItem objPedidoItem;
            int sequencia = 1;
            foreach (Modelo.CotacaoProduto item in produtos)
            {
                objPedidoItem = new Modelo.PedidoItem();
                objPedidoItem.ProdutoNome = item.Produto.Nome;
                objPedidoItem.ProdutoDescReduzida = item.Produto.DescReduzida;
                objPedidoItem.Unidade = item.Produto.Unidade.Sigla;
                objPedidoItem.Sequencia = sequencia++;
                objPedidoItem.Ent_Sai = objPedido.Ent_Sai;
                objPedidoItem.IDProduto = item.IDProduto.Value;
                objPedidoItem.Quantidade = item.Quantidade.Value;
                objPedidoItem.PercDesconto = 0;
                objPedidoItem.ValorDesconto = 0;
                objPedidoItem.ValorRetidoICMS = 0;
                switch (fornecedor)
                {
                    case 1:
                        objPedidoItem.Valor = item.Preco_01.Value;
                        break;
                    case 2:
                        objPedidoItem.Valor = item.Preco_02.Value;
                        break;
                    case 3:
                        objPedidoItem.Valor = item.Preco_03.Value;
                        break;
                    case 4:
                        objPedidoItem.Valor = item.Preco_04.Value;
                        break;
                    case 5:
                        objPedidoItem.Valor = item.Preco_05.Value;
                        break;
                    case 6:
                        objPedidoItem.Valor = item.Preco_06.Value;
                        break;
                }
                objPedidoItem.ValorCalculado = objPedidoItem.Valor;
                objPedidoItem.Total = objPedidoItem.Valor * objPedidoItem.Quantidade;
                objPedidoItem.SubTotal = objPedidoItem.Total;
                objPedido.PedidoItems.Add(objPedidoItem);
            }
        }
    }
}
