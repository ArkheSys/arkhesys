using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using Moq;
using cwkGestao.Negocio;

namespace cwkGestao.UnitTest.CotacaoTest
{
    [TestClass]
    public class GeracaoPedidoTest
    {
        public Cotacao GetCotacao()
        {
            Cotacao cotacao = new Cotacao();
            cotacao.Codigo = 1;
            cotacao.Dt = DateTime.Today;
            cotacao.Filial = new Filial() { Codigo = 1, Nome = "Filial de Teste" };
            cotacao.IDCondicao_01 = 1;
            cotacao.IDCondicao_02 = 2;
            cotacao.IDCondicao_03 = 3;
            cotacao.IDFornecedor_01 = 1;
            cotacao.IDFornecedor_02 = 2;
            cotacao.IDFornecedor_03 = 3;
            cotacao.Observacao = "Observação da cotação de teste";
            cotacao.TipoCotacao = TipoCotacao.Cotação;

            cotacao.CotacaoProdutos.Add(new CotacaoProduto
                                            {
                                                Cotacao = cotacao,
                                                DescricaoProduto = "Produto 1",
                                                Preco_01 = 5,
                                                Preco_02 = 10,
                                                Preco_03 = 7.5m,
                                                Produto = new Produto { Codigo = "1", Nome = "Produto 1", Unidade = new Unidade() },
                                                Quantidade = 100
                                            });

            cotacao.CotacaoProdutos.Add(new CotacaoProduto
            {
                Cotacao = cotacao,
                DescricaoProduto = "Produto 2 cm descricao alterada",
                Preco_01 = 900,
                Preco_02 = 523,
                Preco_03 = 965m,
                Produto = new Produto { Codigo = "2", Nome = "Produto 2", Unidade = new Unidade() },
                Quantidade = 2
            });

            cotacao.CotacaoProdutos.Add(new CotacaoProduto
            {
                Cotacao = cotacao,
                DescricaoProduto = "Produto 3",
                Preco_01 = 300,
                Preco_02 = 520,
                Preco_03 = 100m,
                Produto = new Produto { Codigo = "3", Nome = "Produto 3", Unidade = new Unidade() },
                Quantidade = 63
            });

            cotacao.CotacaoProdutos.Add(new CotacaoProduto
            {
                Cotacao = cotacao,
                DescricaoProduto = "Produto 4",
                Preco_01 = 750,
                Preco_02 = 630,
                Preco_03 = 100m,
                Produto = new Produto { Codigo = "4", Nome = "Produto 4", Unidade = new Unidade() },
                Quantidade = 10
            });
            return cotacao;
        }

        public void MockaRepositorios()
        {
            MockaPessoa();
            MockaCondicao();
            MockaTabelaPreco();
            MockaTipoPedido();
            MockaTipoNota();
            MockaPedido();

            CotacaoProdutoPedidoItemController.Instancia.SetRepositorio(new Mock<IRepositorioCotacaoProdutoPedidoItem>(MockBehavior.Loose).Object);
            Mock<IRepositorioFeriado> mock = new Mock<IRepositorioFeriado>();
            mock.Setup(i => i.GetAll()).Returns(new List<Feriado>());
            FeriadoController.Instancia.SetRepositorio(mock.Object);
        }

        private void MockaPedido()
        {
            Mock<IRepositorioPedido> repositorioCondicao = new Mock<IRepositorioPedido>();
            repositorioCondicao.Setup(i => i.CodigoDisponivelPedido()).Returns(1);
            PedidoController.Instancia.SetRepositorio(repositorioCondicao.Object);
        }

        private void MockaTipoPedido()
        {
            Mock<IRepositorioTipoPedido> repositorioCondicao = new Mock<IRepositorioTipoPedido>();
            repositorioCondicao.Setup(i => i.GetAll()).Returns(new List<TipoPedido> { new TipoPedido() { Codigo = 1, Ent_Sai = 1, Tipo = TipoPedidoType.Orçamento, TipoNota = new TipoNota() } });
            TipoPedidoController.Instancia.SetRepositorio(repositorioCondicao.Object);
        }

        private void MockaTipoNota()
        {
            Mock<IRepositorioTipoNota> repositorioCondicao = new Mock<IRepositorioTipoNota>();
            repositorioCondicao.Setup(i => i.LoadObjectById(It.IsAny<int>())).Returns(new TipoNota() { Codigo = 1, Ent_Sai = 1, ValorSTPrimeiraParcela = false });
            TipoNotaController.Instancia.SetRepositorio(repositorioCondicao.Object);
        }

        private void MockaTabelaPreco()
        {
            Mock<IRepositorioTabelaPreco> repositorioCondicao = new Mock<IRepositorioTabelaPreco>();
            repositorioCondicao.Setup(i => i.GetAll()).Returns(new List<TabelaPreco> { new TabelaPreco() { Codigo = 1 } });
            TabelaPrecoController.Instancia.SetRepositorio(repositorioCondicao.Object);
        }

        private void MockaCondicao()
        {
            Mock<IRepositorioCondicao> repositorioCondicao = new Mock<IRepositorioCondicao>();
            repositorioCondicao.Setup(i => i.LoadObjectById(1)).Returns(ConstroiCondicao1x());
            repositorioCondicao.Setup(i => i.LoadObjectById(2)).Returns(ConstroiCondicao2x());
            repositorioCondicao.Setup(i => i.LoadObjectById(3)).Returns(ConstroiCondicao3x());
            CondicaoController.Instancia.SetRepositorio(repositorioCondicao.Object);
        }

        private void MockaPessoa()
        {
            Mock<IRepositorioPessoa> repositorioPessoa = new Mock<IRepositorioPessoa>();
            repositorioPessoa.Setup(i => i.LoadObjectById(1)).Returns(new Pessoa { ID = 1, Codigo = 1, Nome = "Fornecedor 1", PessoaEnderecos = new List<PessoaEndereco> { new PessoaEndereco { BPrincipal = true, Cidade = new Cidade { UF = new UF() } } } });
            repositorioPessoa.Setup(i => i.LoadObjectById(2)).Returns(new Pessoa { ID = 2, Codigo = 2, Nome = "Fornecedor 2", PessoaEnderecos = new List<PessoaEndereco> { new PessoaEndereco { BPrincipal = true, Cidade = new Cidade { UF = new UF() } } } });
            repositorioPessoa.Setup(i => i.LoadObjectById(3)).Returns(new Pessoa { ID = 3, Codigo = 3, Nome = "Fornecedor 3", PessoaEnderecos = new List<PessoaEndereco> { new PessoaEndereco { BPrincipal = true, Cidade = new Cidade { UF = new UF() } } } });
            PessoaController.Instancia.SetRepositorio(repositorioPessoa.Object);
        }

        #region Construtores de parcelas

        private Condicao ConstroiCondicao1x()
        {
            return new Condicao
                       {
                           Codigo = 1,
                           Divide = 1,
                           QtParcela = 1,
                           CondicaoParcelas = new List<CondicaoParcela>
                                                  {
                                                      new CondicaoParcela { DiaMes = 0,  TipoDt = "Dia", TipoVlr = "Percentual", VlrPerc = 100 }
                                                  }
                       };
        }

        private Condicao ConstroiCondicao2x()
        {
            return new Condicao
                       {
                           Codigo = 2,
                           Divide = 1,
                           QtParcela = 2,
                           CondicaoParcelas = new List<CondicaoParcela>
                                                  {
                                                      new CondicaoParcela {DiaMes = 30, TipoDt = "Dia", TipoVlr = "Valor", VlrPerc = 0},
                                                      new CondicaoParcela {DiaMes = 45, TipoDt = "Dia", TipoVlr = "Valor", VlrPerc = 0}

                                                  }
                       };
        }

        private Condicao ConstroiCondicao3x()
        {
            return new Condicao
                       {
                           Codigo = 3,
                           Divide = 1,
                           QtParcela = 3,
                           CondicaoParcelas = new List<CondicaoParcela>
                                                  {
                                                      new CondicaoParcela {DiaMes = 30, TipoDt = "Dia", TipoVlr = "Valor", VlrPerc = 0},
                                                      new CondicaoParcela {DiaMes = 45, TipoDt = "Dia", TipoVlr = "Valor", VlrPerc = 0},
                                                      new CondicaoParcela {DiaMes = 60, TipoDt = "Dia", TipoVlr = "Valor", VlrPerc = 0}


                                                  }
                       };
        }

        #endregion

        [TestMethod]
        public void Constroi_Inicio_Orcamento_Com_Fornecedor_Especifico()
        {
            MockaRepositorios();

            Pedido pedido = CotacaoController.Instancia.GerarPedidoDeFornecedor(GetCotacao(), PessoaController.Instancia.LoadObjectById(1), new DateTime(2000, 1, 1), new DateTime(2000, 3, 1));

            Assert.AreEqual(1, pedido.Ent_Sai);
            Assert.AreEqual(1, pedido.Pessoa.Codigo);
            Assert.AreEqual(1, pedido.Condicao.Codigo);
            Assert.AreEqual(1, pedido.Filial.Codigo);
            Assert.AreEqual(StatusOrcamento.Aberto, pedido.Status);
            Assert.AreEqual(new DateTime(2000, 1, 1), pedido.Dt);
            Assert.AreEqual(new DateTime(2000, 3, 1), pedido.DtValidade);
            Assert.AreEqual(1, pedido.TabelaPreco.Codigo);
            Assert.AreEqual(1, pedido.TipoPedido.Codigo);
        }

        [TestMethod]
        public void Constroi_Itens_Orcamento_Com_Fornecedor_Especifico()
        {
            MockaRepositorios();

            Pedido pedido = CotacaoController.Instancia.GerarPedidoDeFornecedor(GetCotacao(), PessoaController.Instancia.LoadObjectById(1), new DateTime(2000, 1, 1), new DateTime(2000, 3, 1));

            Assert.AreEqual(4, pedido.Items.Count);

            Assert.AreEqual("1", pedido.Items[0].Produto.Codigo);
            Assert.AreEqual(100, pedido.Items[0].Quantidade);
            Assert.AreEqual(5, pedido.Items[0].ValorCalculado);
            Assert.AreEqual(5, pedido.Items[0].Valor);
            Assert.AreEqual(500, pedido.Items[0].SubTotal);

            Assert.AreEqual("2", pedido.Items[1].Produto.Codigo);
            Assert.AreEqual(2, pedido.Items[1].Quantidade);
            Assert.AreEqual(900, pedido.Items[1].ValorCalculado);
            Assert.AreEqual(900, pedido.Items[1].Valor);
            Assert.AreEqual(1800, pedido.Items[1].SubTotal);

            Assert.AreEqual("3", pedido.Items[2].Produto.Codigo);
            Assert.AreEqual(63, pedido.Items[2].Quantidade);
            Assert.AreEqual(300, pedido.Items[2].ValorCalculado);
            Assert.AreEqual(300, pedido.Items[2].Valor);
            Assert.AreEqual(18900, pedido.Items[2].SubTotal);

            Assert.AreEqual("4", pedido.Items[3].Produto.Codigo);
            Assert.AreEqual(10, pedido.Items[3].Quantidade);
            Assert.AreEqual(750, pedido.Items[3].ValorCalculado);
            Assert.AreEqual(750, pedido.Items[3].Valor);
            Assert.AreEqual(7500, pedido.Items[3].SubTotal);
        }

        [TestMethod]
        public void Calcula_Totais_Orcamento_Com_Fornecedor_Especifico()
        {
            MockaRepositorios();

            Pedido pedido = CotacaoController.Instancia.GerarPedidoDeFornecedor(GetCotacao(), PessoaController.Instancia.LoadObjectById(1), new DateTime(2000, 1, 1), new DateTime(2000, 3, 1));

            Assert.AreEqual(28700, pedido.TotalProduto);
            Assert.AreEqual(28700, pedido.TotalPedido);
        }

        [TestMethod]
        public void Calcula_Totais_Orcamento_Com_Menor_Preco_Total()
        {
            MockaRepositorios();

            Pedido pedido = CotacaoController.Instancia.GerarPedidoMenorPrecoTotal(GetCotacao(), new DateTime(2000, 1, 1), new DateTime(2000, 3, 1));

            Assert.AreEqual(4, pedido.Items.Count);

            Assert.AreEqual("1", pedido.Items[0].Produto.Codigo);
            Assert.AreEqual(100m, pedido.Items[0].Quantidade);
            Assert.AreEqual(7.5m, pedido.Items[0].ValorCalculado);
            Assert.AreEqual(7.5m, pedido.Items[0].Valor);
            Assert.AreEqual(750m, pedido.Items[0].SubTotal);

            Assert.AreEqual("2", pedido.Items[1].Produto.Codigo);
            Assert.AreEqual(2m, pedido.Items[1].Quantidade);
            Assert.AreEqual(965m, pedido.Items[1].ValorCalculado);
            Assert.AreEqual(965m, pedido.Items[1].Valor);
            Assert.AreEqual(1930m, pedido.Items[1].SubTotal);

            Assert.AreEqual("3", pedido.Items[2].Produto.Codigo);
            Assert.AreEqual(63m, pedido.Items[2].Quantidade);
            Assert.AreEqual(100m, pedido.Items[2].ValorCalculado);
            Assert.AreEqual(100m, pedido.Items[2].Valor);
            Assert.AreEqual(6300m, pedido.Items[2].SubTotal);

            Assert.AreEqual("4", pedido.Items[3].Produto.Codigo);
            Assert.AreEqual(10m, pedido.Items[3].Quantidade);
            Assert.AreEqual(100m, pedido.Items[3].ValorCalculado);
            Assert.AreEqual(100m, pedido.Items[3].Valor);
            Assert.AreEqual(1000m, pedido.Items[3].SubTotal);

            Assert.AreEqual(9980, pedido.TotalProduto);
            Assert.AreEqual(9980, pedido.TotalPedido);
        }

        [TestMethod]
        public void Calcula_Totais_Orcamento_Com_Menor_Preco_Por_Produto()
        {
            MockaRepositorios();

            IList<Pedido> pedido = CotacaoController.Instancia.GerarPedidoMenorPrecoProduto(GetCotacao(), new DateTime(2000, 1, 1), new DateTime(2000, 3, 1));

            Pedido pedidoForn1 = pedido.Where(i => i.Pessoa.ID == 1).First();
            Assert.AreEqual(1, pedidoForn1.Items.Count);
            Assert.AreEqual("1", pedidoForn1.Items[0].Produto.Codigo);
            Assert.AreEqual(100m, pedidoForn1.Items[0].Quantidade);
            Assert.AreEqual(5m, pedidoForn1.Items[0].ValorCalculado);
            Assert.AreEqual(5m, pedidoForn1.Items[0].Valor);
            Assert.AreEqual(500m, pedidoForn1.Items[0].SubTotal);
            Assert.AreEqual(500m, pedidoForn1.TotalProduto);
            Assert.AreEqual(500m, pedidoForn1.TotalPedido);

            Pedido pedidoForn2 = pedido.Where(i => i.Pessoa.ID == 2).First();
            Assert.AreEqual(1, pedidoForn2.Items.Count);
            Assert.AreEqual("2", pedidoForn2.Items[0].Produto.Codigo);
            Assert.AreEqual(2m, pedidoForn2.Items[0].Quantidade);
            Assert.AreEqual(523m, pedidoForn2.Items[0].ValorCalculado);
            Assert.AreEqual(523m, pedidoForn2.Items[0].Valor);
            Assert.AreEqual(1046m, pedidoForn2.Items[0].SubTotal);
            Assert.AreEqual(1046m, pedidoForn2.TotalProduto);
            Assert.AreEqual(1046m, pedidoForn2.TotalPedido);

            Pedido pedidoForn3 = pedido.Where(i => i.Pessoa.ID == 3).First();
            Assert.AreEqual(2, pedidoForn3.Items.Count);

            PedidoItem itemComProduto3 = pedidoForn3.Items.Where(i => i.Produto.Codigo == "3").First();
            Assert.AreEqual("3", itemComProduto3.Produto.Codigo);
            Assert.AreEqual(63m, itemComProduto3.Quantidade);
            Assert.AreEqual(100m, itemComProduto3.ValorCalculado);
            Assert.AreEqual(100m, itemComProduto3.Valor);
            Assert.AreEqual(6300m, itemComProduto3.SubTotal);

            PedidoItem itemComProduto4 = pedidoForn3.Items.Where(i => i.Produto.Codigo == "4").First();
            Assert.AreEqual("4", itemComProduto4.Produto.Codigo);
            Assert.AreEqual(10m, itemComProduto4.Quantidade);
            Assert.AreEqual(100m, itemComProduto4.ValorCalculado);
            Assert.AreEqual(100m, itemComProduto4.Valor);
            Assert.AreEqual(1000m, itemComProduto4.SubTotal);

            Assert.AreEqual(7300m, pedidoForn3.TotalProduto);
            Assert.AreEqual(7300m, pedidoForn3.TotalPedido);
        }

    }
}
