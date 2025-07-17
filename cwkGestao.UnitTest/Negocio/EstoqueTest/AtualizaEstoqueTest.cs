using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Auxiliares;
using cwkGestao.Negocio.ControleEstoque;
using cwkGestao.Repositorio.Interfaces.Particulares;
using Moq;
using cwkGestao.Negocio;
using cwkGestao.Modelo.Exceptions;

namespace cwkGestao.UnitTest.EstoqueTest
{
    [TestClass]
    public class AtualizaEstoqueTest
    {
        public AtualizaEstoqueTest()
        {

        }

        #region Verificação Apenas dos Parametros de Atualização

        [TestMethod]
        public void Parametro_Alteracao_Pedido_Entrada_1_ItemQtdAlterada_1_Para_2()
        {
            Pedido pedido = new Pedido();
            pedido.Filial = new Filial();
            pedido.TipoPedido = new TipoPedido { BPermiteEstoqueNegativo = true };
            pedido.TipoPedido.LocalEstoque = new LocalEstoque { Filial = new Filial() };
            pedido.Items = new List<PedidoItem>();
            PedidoItem pedidoItem = new PedidoItem();
            pedidoItem.Produto = new Produto();
            pedidoItem.Produto.Estoques = new List<Estoque>();
            pedido.ID = 1;
            pedido.Ent_Sai = (int)Modelo.InOutType.Entrada;
            pedido.Filial.ID = 1;
            pedidoItem.ID = 1;
            pedidoItem.bReservaEstoque = true;
            pedidoItem.Quantidade = 2;
            pedidoItem.Produto.ID = 1;
            var estoque = new Estoque()
            {
                ID = 1,
                Produto = pedidoItem.Produto,
                LocalEstoque = pedido.TipoPedido.LocalEstoque
            };
            pedidoItem.Produto.Estoques.Add(estoque);
            pedido.Items.Add(pedidoItem);

            Pedido pedidoOld = new Pedido();
            pedidoOld.Items = new List<PedidoItem>();
            pedidoOld.Filial = pedido.Filial;
            pedidoOld.ID = pedido.ID;
            pedidoOld.Ent_Sai = (int)InOutType.Entrada;
            PedidoItem pedidoItemOld = new PedidoItem();
            pedidoItemOld.ID = pedidoItem.ID;
            pedidoItemOld.bReservaEstoque = true;
            pedidoItemOld.Quantidade = 1;
            pedidoItemOld.Produto = pedidoItem.Produto;
            pedidoItemOld.Pedido = pedidoOld;
            pedidoOld.Items.Add(pedidoItemOld);

            Mock<IRepositorioPedido> repositorioPedido = new Mock<IRepositorioPedido>();
            repositorioPedido.Setup(i => i.LoadObjectById(pedido.ID)).Returns(pedidoOld);
            PedidoController.Instancia.SetRepositorio(repositorioPedido.Object);

            Mock<IRepositorioEstoque> repositorioEstoque = new Mock<IRepositorioEstoque>();
            repositorioEstoque.Setup(i => i.GetEstoque(It.IsAny<Filial>(), It.IsAny<Produto>())).Returns(estoque);
            EstoqueController.Instancia.SetRepositorio(repositorioEstoque.Object);

            AtualizaEstoque atualizaEstoque = AtualizaEstoque.GetAtualizaEstoque(pedido);
            atualizaEstoque.Atualizar(Modelo.Acao.Alterar);
            var resultado = atualizaEstoque.ParametrosAtualizacao;

            if (resultado.Count != 1)
            {
                Assert.Fail();
            }
            Assert.AreEqual(resultado[0].qtdCompra_Pedido, 1);
        }

        [TestMethod]
        public void Parametro_Inclusao_Pedido_Entrada_1_Item_1_Unidade()
        {
            Pedido pedido = new Pedido();
            pedido.Filial = new Filial();
            pedido.TipoPedido = new TipoPedido { BPermiteEstoqueNegativo = true };
            pedido.TipoPedido.LocalEstoque = new LocalEstoque { Filial = new Filial() };
            pedido.Items = new List<PedidoItem>();
            PedidoItem pedidoItem = new PedidoItem();
            pedidoItem.Produto = new Produto();
            pedidoItem.Produto.Estoques = new List<Estoque>();
            pedido.Ent_Sai = (int)Modelo.InOutType.Entrada;
            pedido.Filial.ID = 1;
            pedidoItem.bReservaEstoque = true;
            pedidoItem.Quantidade = 1;
            pedidoItem.Produto.Estoques.Add(new Estoque()
            {
                ID = 1,
                Produto = pedidoItem.Produto,
                LocalEstoque = pedido.TipoPedido.LocalEstoque
            });
            pedidoItem.Pedido = pedido;
            pedido.Items.Add(pedidoItem);

            ParametroEstoque esperado = new ParametroEstoque();
            esperado.qtdCompra_Pedido = 1;
            esperado.produto = pedidoItem.Produto;

            AtualizaEstoque atualizaEstoque = AtualizaEstoque.GetAtualizaEstoque(pedido);
            atualizaEstoque.Atualizar(Modelo.Acao.Incluir);
            var resultado = atualizaEstoque.ParametrosAtualizacao;

            if (resultado.Count != 1)
            {
                Assert.Fail();
            }
            Assert.AreEqual(resultado[0].qtdCompra_Pedido, esperado.qtdCompra_Pedido);
        }

        [TestMethod]
        public void Parametro_Inclusao_Pedido_Entrada_2_Items_1_Unidade()
        {
            Pedido pedido = new Pedido();
            pedido.Filial = new Filial();
            pedido.TipoPedido = new TipoPedido { BPermiteEstoqueNegativo = true };
            pedido.TipoPedido.LocalEstoque = new LocalEstoque { Filial = new Filial() };
            pedido.Items = new List<PedidoItem>();
            pedido.Ent_Sai = (int)Modelo.InOutType.Entrada;
            pedido.Filial.ID = 1;

            PedidoItem pedidoItem1 = new PedidoItem();
            pedidoItem1.Produto = new Produto();
            pedidoItem1.Produto.ID = 1;
            pedidoItem1.Produto.Estoques = new List<Estoque>();
            pedidoItem1.bReservaEstoque = true;
            pedidoItem1.Quantidade = 1;
            pedidoItem1.Produto.Estoques.Add(new Estoque()
            {
                ID = 1,
                Produto = pedidoItem1.Produto,
                LocalEstoque = pedido.TipoPedido.LocalEstoque
            });
            pedidoItem1.Pedido = pedido;
            pedido.Items.Add(pedidoItem1);

            PedidoItem pedidoItem2 = new PedidoItem();
            pedidoItem2.Produto = new Produto();
            pedidoItem2.Produto.ID = 2;
            pedidoItem2.Produto.Estoques = new List<Estoque>();
            pedidoItem2.bReservaEstoque = true;
            pedidoItem2.Quantidade = 1;
            pedidoItem2.Produto.Estoques.Add(new Estoque()
            {
                ID = 2,
                Produto = pedidoItem2.Produto,
                LocalEstoque = pedido.TipoPedido.LocalEstoque
            });
            pedidoItem2.Pedido = pedido;
            pedido.Items.Add(pedidoItem2);

            ParametroEstoque esperado1 = new ParametroEstoque();
            esperado1.qtdCompra_Pedido = 1;
            esperado1.produto = pedidoItem1.Produto;
            ParametroEstoque esperado2 = new ParametroEstoque();
            esperado2.qtdCompra_Pedido = 1;
            esperado2.produto = pedidoItem2.Produto;

            AtualizaEstoque atualizaEstoque = AtualizaEstoque.GetAtualizaEstoque(pedido);
            atualizaEstoque.Atualizar(Modelo.Acao.Incluir);
            var resultado = atualizaEstoque.ParametrosAtualizacao;

            if (resultado.Count != 2)
            {
                Assert.Fail();
            }
            Assert.AreEqual(resultado[0].qtdCompra_Pedido, esperado1.qtdCompra_Pedido);
            Assert.AreEqual(resultado[0].produto.ID, esperado1.produto.ID);
            Assert.AreEqual(resultado[1].qtdCompra_Pedido, esperado2.qtdCompra_Pedido);
            Assert.AreEqual(resultado[1].produto.ID, esperado2.produto.ID);
        }

        [TestMethod]
        public void Parametro_Exclusao_Pedido_Entrada_1_Item_1_Unidade()
        {
            Pedido pedido = new Pedido();
            pedido.Filial = new Filial();
            pedido.TipoPedido = new TipoPedido { BPermiteEstoqueNegativo = true };
            pedido.TipoPedido.LocalEstoque = new LocalEstoque { Filial = new Filial() };
            pedido.Items = new List<PedidoItem>();
            PedidoItem pedidoItem = new PedidoItem();
            pedidoItem.Produto = new Produto();
            pedidoItem.Produto.Estoques = new List<Estoque>();
            pedido.Ent_Sai = (int)Modelo.InOutType.Entrada;
            pedido.Filial.ID = 1;
            pedidoItem.bReservaEstoque = true;
            pedidoItem.Quantidade = 1;
            pedidoItem.Produto.Estoques.Add(new Estoque()
            {
                ID = 1,
                Produto = pedidoItem.Produto,
                LocalEstoque = pedido.TipoPedido.LocalEstoque
            });
            pedidoItem.Pedido = pedido;
            pedido.Items.Add(pedidoItem);

            ParametroEstoque esperado = new ParametroEstoque();
            esperado.qtdCompra_Pedido = -1;
            esperado.produto = pedidoItem.Produto;

            AtualizaEstoque atualizaEstoque = AtualizaEstoque.GetAtualizaEstoque(pedido);
            atualizaEstoque.Atualizar(Modelo.Acao.Excluir);
            var resultado = atualizaEstoque.ParametrosAtualizacao;

            if (resultado.Count != 1)
            {
                Assert.Fail();
            }
            Assert.AreEqual(resultado[0].qtdCompra_Pedido, esperado.qtdCompra_Pedido);
        }

        [TestMethod]
        public void Parametro_Inclusao_Pedido_Saida_1_Item_1_Unidade()
        {
            Pedido pedido = new Pedido();
            pedido.Filial = new Filial();
            pedido.TipoPedido = new TipoPedido { BPermiteEstoqueNegativo = true };
            pedido.TipoPedido.LocalEstoque = new LocalEstoque { Filial = new Filial() };
            pedido.Items = new List<PedidoItem>();
            PedidoItem pedidoItem = new PedidoItem();
            pedidoItem.Produto = new Produto();
            pedidoItem.Produto.Estoques = new List<Estoque>();
            pedido.Ent_Sai = (int)Modelo.InOutType.Saída;
            pedido.Filial.ID = 1;
            pedidoItem.bReservaEstoque = true;
            pedidoItem.Quantidade = 1;
            var estoque = new Estoque()
            {
                ID = 1,
                Produto = pedidoItem.Produto,
                LocalEstoque = pedido.TipoPedido.LocalEstoque
            };
            pedidoItem.Produto.Estoques.Add(estoque);
            pedidoItem.Pedido = pedido;
            pedido.Items.Add(pedidoItem);

            ParametroEstoque esperado = new ParametroEstoque();
            esperado.qtdEstoque_Pedido = 1;
            esperado.produto = pedidoItem.Produto;

            Mock<IRepositorioEstoque> repositorioEstoque = new Mock<IRepositorioEstoque>();
            repositorioEstoque.Setup(r => r.GetEstoque(pedido.Filial, pedidoItem.Produto)).Returns(estoque);
            EstoqueController.Instancia.SetRepositorio(repositorioEstoque.Object);

            AtualizaEstoque atualizaEstoque = AtualizaEstoque.GetAtualizaEstoque(pedido);
            atualizaEstoque.Atualizar(Modelo.Acao.Incluir);
            var resultado = atualizaEstoque.ParametrosAtualizacao;

            if (resultado.Count != 1)
            {
                Assert.Fail();
            }
            Assert.AreEqual(resultado[0].qtdEstoque_Pedido, esperado.qtdEstoque_Pedido);
        }

        [TestMethod]
        public void Parametro_Inclusao_Pedido_Saida_2_Items_1_Unidade()
        {
            Pedido pedido = new Pedido();
            pedido.Filial = new Filial();
            pedido.TipoPedido = new TipoPedido { BPermiteEstoqueNegativo = true };
            pedido.TipoPedido.LocalEstoque = new LocalEstoque { Filial = new Filial() };
            pedido.Items = new List<PedidoItem>();
            pedido.Ent_Sai = (int)Modelo.InOutType.Saída;
            pedido.Filial.ID = 1;

            PedidoItem pedidoItem1 = new PedidoItem();
            pedidoItem1.Produto = new Produto();
            pedidoItem1.Produto.ID = 1;
            pedidoItem1.Produto.Estoques = new List<Estoque>();
            pedidoItem1.bReservaEstoque = true;
            pedidoItem1.Quantidade = 1;
            var estoque1 = new Estoque()
            {
                ID = 1,
                Produto = pedidoItem1.Produto,
                LocalEstoque = pedido.TipoPedido.LocalEstoque
            };
            pedidoItem1.Produto.Estoques.Add(estoque1);
            pedidoItem1.Pedido = pedido;
            pedido.Items.Add(pedidoItem1);

            PedidoItem pedidoItem2 = new PedidoItem();
            pedidoItem2.Produto = new Produto();
            pedidoItem2.Produto.ID = 2;
            pedidoItem2.Produto.Estoques = new List<Estoque>();
            pedidoItem2.bReservaEstoque = true;
            pedidoItem2.Quantidade = 1;
            var estoque2 = new Estoque()
            {
                ID = 2,
                Produto = pedidoItem2.Produto,
                LocalEstoque = pedido.TipoPedido.LocalEstoque
            };
            pedidoItem2.Produto.Estoques.Add(estoque2);
            pedidoItem2.Pedido = pedido;
            pedido.Items.Add(pedidoItem2);

            ParametroEstoque esperado1 = new ParametroEstoque();
            esperado1.qtdEstoque_Pedido = 1;
            esperado1.produto = pedidoItem1.Produto;
            ParametroEstoque esperado2 = new ParametroEstoque();
            esperado2.qtdEstoque_Pedido = 1;
            esperado2.produto = pedidoItem2.Produto;

            Mock<IRepositorioEstoque> repositorioEstoque = new Mock<IRepositorioEstoque>();
            repositorioEstoque.Setup(i => i.GetEstoque(pedido.Filial, pedidoItem1.Produto)).Returns(estoque1);
            repositorioEstoque.Setup(i => i.GetEstoque(pedido.Filial, pedidoItem2.Produto)).Returns(estoque2);
            EstoqueController.Instancia.SetRepositorio(repositorioEstoque.Object);

            AtualizaEstoque atualizaEstoque = AtualizaEstoque.GetAtualizaEstoque(pedido);
            atualizaEstoque.Atualizar(Modelo.Acao.Incluir);
            var resultado = atualizaEstoque.ParametrosAtualizacao;

            if (resultado.Count != 2)
            {
                Assert.Fail();
            }
            Assert.AreEqual(resultado[0].qtdEstoque_Pedido, esperado1.qtdEstoque_Pedido);
            Assert.AreEqual(resultado[0].produto.ID, esperado1.produto.ID);
            Assert.AreEqual(resultado[1].qtdEstoque_Pedido, esperado2.qtdEstoque_Pedido);
            Assert.AreEqual(resultado[1].produto.ID, esperado2.produto.ID);
        }

        [TestMethod]
        public void Parametro_Exclusao_Pedido_Saida_1_Item_1_Unidade()
        {
            Pedido pedido = new Pedido();
            pedido.Filial = new Filial();
            pedido.TipoPedido = new TipoPedido { BPermiteEstoqueNegativo = true };
            pedido.TipoPedido.LocalEstoque = new LocalEstoque { Filial = new Filial() };
            pedido.Items = new List<PedidoItem>();
            PedidoItem pedidoItem = new PedidoItem();
            pedidoItem.Produto = new Produto();
            pedidoItem.Produto.Estoques = new List<Estoque>();
            pedido.Ent_Sai = (int)Modelo.InOutType.Saída;
            pedido.Filial.ID = 1;
            pedidoItem.bReservaEstoque = true;
            pedidoItem.Quantidade = 1;
            var estoque = new Estoque()
            {
                ID = 1,
                Produto = pedidoItem.Produto,
                LocalEstoque = pedido.TipoPedido.LocalEstoque
            };
            pedidoItem.Produto.Estoques.Add(estoque);
            pedidoItem.Pedido = pedido;
            pedido.Items.Add(pedidoItem);

            Mock<IRepositorioEstoque> repositorioEstoque = new Mock<IRepositorioEstoque>();
            repositorioEstoque.Setup(i => i.GetEstoque(It.IsAny<Filial>(), It.IsAny<Produto>())).Returns(estoque);
            EstoqueController.Instancia.SetRepositorio(repositorioEstoque.Object);

            ParametroEstoque esperado = new ParametroEstoque();
            esperado.qtdEstoque_Pedido = -1;
            esperado.produto = pedidoItem.Produto;

            AtualizaEstoque atualizaEstoque = AtualizaEstoque.GetAtualizaEstoque(pedido);
            atualizaEstoque.Atualizar(Modelo.Acao.Excluir);
            var resultado = atualizaEstoque.ParametrosAtualizacao;

            if (resultado.Count != 1)
            {
                Assert.Fail();
            }
            Assert.AreEqual(resultado[0].qtdEstoque_Pedido, esperado.qtdEstoque_Pedido);
        }

        [TestMethod]
        public void Nao_Permite_Estoque_Negativo_Inclusao_Pedido_Saida_1_Item_1_Unidade_Gera_Excecao()
        {
            Pedido pedido = new Pedido();
            pedido.Filial = new Filial();
            pedido.TipoPedido = new TipoPedido { BPermiteEstoqueNegativo = false };
            pedido.TipoPedido.LocalEstoque = new LocalEstoque { Filial = new Filial() };
            pedido.Items = new List<PedidoItem>();
            PedidoItem pedidoItem = new PedidoItem();
            pedidoItem.Produto = new Produto();
            pedidoItem.Produto.Estoques = new List<Estoque>();
            pedido.Ent_Sai = (int)Modelo.InOutType.Saída;
            pedido.Filial.ID = 1;
            pedidoItem.bReservaEstoque = true;
            pedidoItem.Quantidade = 1;
            var estoque = new Estoque()
            {
                ID = 1,
                Produto = pedidoItem.Produto,
                LocalEstoque = pedido.TipoPedido.LocalEstoque
            };
            pedidoItem.Produto.Estoques.Add(estoque);
            pedidoItem.Pedido = pedido;
            pedido.Items.Add(pedidoItem);

            ParametroEstoque esperado = new ParametroEstoque();
            esperado.qtdEstoque_Pedido = 1;
            esperado.produto = pedidoItem.Produto;

            Mock<IRepositorioEstoque> repositorioEstoque = new Mock<IRepositorioEstoque>();
            repositorioEstoque.Setup(r => r.GetEstoque(pedido.Filial, pedidoItem.Produto)).Returns(estoque);
            EstoqueController.Instancia.SetRepositorio(repositorioEstoque.Object);

            try
            {
                AtualizaEstoque atualizaEstoque = AtualizaEstoque.GetAtualizaEstoque(pedido);
                atualizaEstoque.Atualizar(Modelo.Acao.Incluir);
                var resultado = atualizaEstoque.ParametrosAtualizacao;
                Assert.Fail();
            }
            catch (EstoqueNegativoException) { }
        }

        [TestMethod]
        public void Nao_Permite_Estoque_Negativo_Produto_Com_Estoque_Permite_Pedido_De_Saida()
        {
            Pedido pedido = new Pedido();
            pedido.Filial = new Filial();
            pedido.TipoPedido = new TipoPedido { BPermiteEstoqueNegativo = false };
            pedido.TipoPedido.LocalEstoque = new LocalEstoque { Filial = new Filial() };
            pedido.Items = new List<PedidoItem>();
            PedidoItem pedidoItem = new PedidoItem();
            pedidoItem.Produto = new Produto();
            pedidoItem.Produto.Estoques = new List<Estoque>();
            pedido.Ent_Sai = (int)Modelo.InOutType.Saída;
            pedido.Filial.ID = 1;
            pedidoItem.bReservaEstoque = true;
            pedidoItem.Quantidade = 1;
            var estoque = new Estoque()
            {
                ID = 1,
                Produto = pedidoItem.Produto,
                LocalEstoque = pedido.TipoPedido.LocalEstoque,
                Qtd_Estoque_Fisico = 1
            };
            pedidoItem.Produto.Estoques.Add(estoque);
            pedidoItem.Pedido = pedido;
            pedido.Items.Add(pedidoItem);

            ParametroEstoque esperado = new ParametroEstoque();
            esperado.qtdEstoque_Pedido = 1;
            esperado.produto = pedidoItem.Produto;

            Mock<IRepositorioEstoque> repositorioEstoque = new Mock<IRepositorioEstoque>();
            repositorioEstoque.Setup(r => r.GetEstoque(pedido.TipoPedido.LocalEstoque, pedidoItem.Produto)).Returns(estoque);
            EstoqueController.Instancia.SetRepositorio(repositorioEstoque.Object);

            try
            {
                AtualizaEstoque atualizaEstoque = AtualizaEstoque.GetAtualizaEstoque(pedido);
                atualizaEstoque.Atualizar(Modelo.Acao.Incluir);
                var resultado = atualizaEstoque.ParametrosAtualizacao;
            }
            catch (EstoqueNegativoException) { Assert.Fail(); }
        }

        #endregion

        #region Verificação dos Registros de Estoque

        [TestMethod]
        public void Estoque_Zerado_Acrescenta_1_Qtd_Compra_Pedido()
        {
            var atualizaEstoque = new Mock<AtualizaEstoque>();
            var repositorioEstoque = new Mock<IRepositorioEstoque>();
            var parametrosAtualizacao = new List<ParametroEstoque>();
            var filial1 = new Modelo.Filial() { ID = 1 };
            var localEstoque1 = new Modelo.LocalEstoque() { ID = 1, Filial = filial1 };
            var produto1 = new Modelo.Produto() { ID = 1, Codigo = "1" };
            var estoqueProduto1 = new Modelo.Estoque() { ID = 1, Produto = produto1, LocalEstoque = localEstoque1 };
            parametrosAtualizacao.Add(new ParametroEstoque()
            {
                localEstoque = localEstoque1,
                produto = produto1,
                qtdCompra_Pedido = 1
            });
            atualizaEstoque.SetupGet(a => a.ParametrosAtualizacao).Returns(parametrosAtualizacao);
            atualizaEstoque.Setup(a => a.EstoquesAtualizar).Returns(new List<Modelo.Estoque>());
            repositorioEstoque.Setup(i => i.GetEstoque(It.IsAny<LocalEstoque>(), It.IsAny<Produto>())).Returns(estoqueProduto1);
            repositorioEstoque.Setup(i => i.GetUsuarioLogado()).Returns("admin");

            EstoqueController.Instancia.SetRepositorio(repositorioEstoque.Object);
            atualizaEstoque.Object.Atualizar(Modelo.Acao.Incluir);

            if (atualizaEstoque.Object.EstoquesAtualizar.Count != 1)
                Assert.Fail();

            var expected = new Modelo.Estoque() { ID = 1, Produto = produto1, LocalEstoque = localEstoque1, Qtd_Compra_Pedido = 1 };
            var actual = atualizaEstoque.Object.EstoquesAtualizar[0];

            Assert.AreEqual(expected.ID, actual.ID);
            Assert.AreEqual(expected.Produto, actual.Produto);
            Assert.AreEqual(expected.LocalEstoque, actual.LocalEstoque);
            Assert.AreEqual(expected.Qtd_Compra_Pedido, actual.Qtd_Compra_Pedido);
            Assert.AreEqual(expected.Qtd_Estoque_Fisico, actual.Qtd_Estoque_Fisico);
            Assert.AreEqual(expected.Qtd_Estoque_Min, actual.Qtd_Estoque_Min);
            Assert.AreEqual(expected.Qtd_Estoque_Pedido, actual.Qtd_Estoque_Pedido);
        }

        #endregion
    }
}
