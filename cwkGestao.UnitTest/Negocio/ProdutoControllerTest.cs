using cwkGestao.Negocio;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cwkGestao.Modelo;
using System.Collections.Generic;
using System;

namespace cwkGestao.Negocio
{
    
    
    /// <summary>
    ///This is a test class for ProdutoControllerTest and is intended
    ///to contain all ProdutoControllerTest Unit Tests
    ///</summary>
    [TestClass()]
    public class ProdutoControllerTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion


        /// <summary>
        ///A test for getPrecoBase
        ///</summary>
        [TestMethod()]
        public void getPrecoBaseTest()
        {
            ProdutoController_Accessor target = new ProdutoController_Accessor();
            Produto produto = new Produto() { PrecoBase = 1000, PrecoFornecedor = 500, DescontoFornecedor = 10, CustoIPI = 5 };
            TabelaPreco tabelaPreco = new TabelaPreco() {  };
            TabelaPrecoProduto tabelaPrecoProduto = new TabelaPrecoProduto() { MargemLucro = 100, PDesconto = 20, PAcrescimo = 50, Produto = produto,TabelaPreco = tabelaPreco  };
            Decimal expected = 472.5m;
            Decimal actual;
            actual = target.getPrecoBase(produto);
            Assert.AreEqual(expected, actual);
        }

        /// <summary>
        ///A test for getPreco
        ///</summary>
        [TestMethod()]
        public void getPrecoTest2()
        {
            ProdutoController_Accessor target = new ProdutoController_Accessor(); // TODO: Initialize to an appropriate value
            Produto produto = new Produto() { PrecoBase = 1000 };
            TabelaPreco tabelaPreco = new TabelaPreco() { ID = 1 };
            TabelaPrecoProduto tabelaPrecoProduto = new TabelaPrecoProduto() { MargemLucro = 100, PDesconto = 20, PAcrescimo = 50, Produto = produto, TabelaPreco = tabelaPreco };
            produto.TabelaPrecoProdutos.Add(tabelaPrecoProduto);
            TipoPrecoType tipoPreco = TipoPrecoType.Normal; 
            Decimal expected = 2000; 
            Decimal actual;
            actual = target.getPreco(produto, tabelaPreco, tipoPreco);
            Assert.AreEqual(expected, actual);
        }
    }
}
