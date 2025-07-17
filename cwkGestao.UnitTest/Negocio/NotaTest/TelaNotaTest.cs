using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;
using cwkGestao.Modelo;
using cwkGestao.Negocio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo.Exceptions;

namespace cwkGestao.UnitTest.NotaTest
{
    [TestClass]
    public class TelaNotaTest
    {
        private void ConfigurarRepositorioNota()
        {
            var repositorioNota = new Mock<IRepositorioNota>(MockBehavior.Loose);
            repositorioNota.Setup(f => f.GetNovoNumeroSerie(It.IsAny<Nota>())).Returns(0);
            NotaController.Instancia.SetRepositorio(repositorioNota.Object);
        }

        #region Nota

        #region Serie e Numero Nota

        [TestMethod]
        public void Nota_De_Saida_Numero_Eh_Gerado_Automatico()
        {
            var tipoNota = new TipoNota
            {
                Ent_Sai = (int)InOutType.Saída,
                Serie = "1"
            };

            AssertSerieENumeroGerados(tipoNota);
        }

        [TestMethod]
        public void Nota_De_Entrada_Origem_Propria_Numero_Eh_Gerado_Automatico()
        {
            var tipoNota = new TipoNota
            {
                Ent_Sai = (int)InOutType.Entrada,
                Serie = "1",
                NFOrigem = (int)OrigemNFType.Própria
            };

            AssertSerieENumeroGerados(tipoNota);
        }

        [TestMethod]
        public void Nota_De_Entrada_Origem_Terceiros_Numero_Nao_Eh_Gerado()
        {
            var tipoNota = new TipoNota
            {
                Ent_Sai = (int)InOutType.Entrada,
                Serie = "1",
                NFOrigem = (int)OrigemNFType.Terceiro
            };

            AssertNaoGerouSerieENumero(tipoNota);
        }

        private LogicaTelaNotaFiscal InvocarSetTipoNota(TipoNota tipoNota)
        {
            var nota = new Nota();
            var logicaTelaNota = new LogicaTelaNotaFiscal(nota);
            logicaTelaNota.SetTipoNota(tipoNota);
            return logicaTelaNota;
        }

        private void AssertSerieENumeroGerados(TipoNota tipoNota)
        {
            ConfigurarRepositorioNota();
            var logicaTelaNota = InvocarSetTipoNota(tipoNota);

            Assert.IsTrue(logicaTelaNota.NumeroGeradoAutomatico);
            Assert.AreEqual(logicaTelaNota.GetNota.Serie, "1");
            Assert.AreEqual(logicaTelaNota.GetNota.Numero, 1);
        }

        private void AssertNaoGerouSerieENumero(TipoNota tipoNota)
        {
            ConfigurarRepositorioNota();
            var logicaTelaNota = InvocarSetTipoNota(tipoNota);

            Assert.IsFalse(logicaTelaNota.NumeroGeradoAutomatico);
            Assert.IsTrue(String.IsNullOrEmpty(logicaTelaNota.GetNota.Serie));
            Assert.AreEqual(logicaTelaNota.GetNota.Numero, 0);
        }

        #endregion

        [TestMethod]
        public void Seta_Condicao_TotalNotaZerado_Nao_Gera_Parcelas()
        {
            Nota nota = new Nota { Dt = DateTime.Now };

            LogicaTelaNotaFiscal logicaTelaNotaFiscal = new LogicaTelaNotaFiscal(nota);

            ConfigurarRepositorioNota();

            var condicao = new Condicao
            {
                ID = 1,
                Divide = 1,
                QtParcela = 1,
                Variacao = 0
            };
            condicao.CondicaoParcelas.Add(new CondicaoParcela
            {
                ID = 1,
                Parcela = 1,
                DiaMes = 0,
                BAVista = true,
                TipoDt = "Dia",

            });
            logicaTelaNotaFiscal.SetCondicao(condicao);

            Assert.IsTrue(nota.Condicao.Equals(condicao));
            Assert.IsTrue(nota.NotaParcelas.Count == 0);
        }

        [TestMethod]
        public void Seta_Condicao_Gera_Parcelas()
        {
            Nota nota = new Nota { Dt = DateTime.Now, Filial = new Filial(), TipoNota = new TipoNota() };

            LogicaTelaNotaFiscal logicaTelaNotaFiscal = new LogicaTelaNotaFiscal(nota);

            ConfigurarRepositorioNota();
            var repositorioFeriado = new Mock<IRepositorioFeriado>(MockBehavior.Loose);
            repositorioFeriado.Setup(f => f.GetAll()).Returns(new List<Modelo.Feriado>());
            FeriadoController.Instancia.SetRepositorio(repositorioFeriado.Object);

            logicaTelaNotaFiscal.InserirNotaItem(new NotaItem
            {
                ID = 1,
                Valor = 100,
                Quantidade = 1,
                Total = 100,
                CFOP = new TabelaCFOP { ID = 1, Codigo = 1, CFOP = "5102" }
            });
            logicaTelaNotaFiscal.AtualizaSomatoriosTotais();

            var condicao = new Condicao
            {
                ID = 1,
                Divide = 1,
                QtParcela = 1,
                Variacao = 0
            };
            condicao.CondicaoParcelas.Add(new CondicaoParcela
            {
                ID = 1,
                Parcela = 1,
                DiaMes = 0,
                BAVista = true,
                TipoDt = "Dia",

            });
            logicaTelaNotaFiscal.SetCondicao(condicao);

            Assert.IsTrue(nota.Condicao.Equals(condicao));
            Assert.IsTrue(nota.NotaParcelas.Count == 1);
        }

        #endregion

        #region NotaItem

        [TestMethod]
        public void Dah_Erro_Ao_Inserir_Nota_Sem_Preencher_Campos_Necessarios()
        {
            Nota nota = new Nota();
            LogicaTelaNotaFiscal logicaTelaNotaFiscal = new LogicaTelaNotaFiscal(nota);

            try
            {
                LogicaTelaNotaItem logicaTelaNotaItem = logicaTelaNotaFiscal.GetLogicaTelaNotaItem();
                Assert.Fail("Não lançou exceção");
            }
            catch (FormException exc)
            {
                Assert.AreEqual(5, exc.CamposErrados.Count);
            }
        }

        [TestMethod]
        public void Nova_NotaItem_SetProduto_CamposPreenchidos()
        {
            var unidade = new Unidade
          {
              Sigla = "PC",
              Nome = "Peça",
              BQtdFracionada = false
          };
            var produto = new Produto
            {
                Nome = "NOME",
                DescReduzida = "REDUZIDA",
                OrigemProd = 0,
                ENQ_IPI = 0,
                PesoBruto = 0,
                PesoLiquido = 0,
                Unidade = unidade,
                InfAdicionais = "INFO ADD",
                PrecoBase = 1,
                TabelaPrecoProdutos = new List<TabelaPrecoProduto> { new TabelaPrecoProduto { PAcrescimo = 0, MargemLucro = 0, TabelaPreco = new TabelaPreco() } }
            };
            var nota = new Nota();
            nota.Condicao = new Condicao
            {
                Variacao = 0
            };
            nota.TipoNota = new TipoNota { };
            nota.TabelaPreco = new TabelaPreco
            {
                TabelaPrecoProdutos = new List<TabelaPrecoProduto>()
            };
            nota.TabelaPreco.TabelaPrecoProdutos.Add(new TabelaPrecoProduto { PAcrescimo = 0, MargemLucro = 0, Produto = produto });
            var notaItem = new NotaItem { Nota = nota };

            var logicaTelaNotaItem = InvocarSetProduto(notaItem, produto);
            AssertCamposNotaItemPreenchidos(logicaTelaNotaItem);
            AssertValorQuantidadeETotalNotaItem(logicaTelaNotaItem, 1, 1, 1);
        }

        [TestMethod]
        public void Nova_NotaItem_Produto_Alterado_Qtd_3_CamposPreenchidos_TotalCalculado()
        {
            var unidade = new Unidade
            {
                Sigla = "PC",
                Nome = "Peça",
                BQtdFracionada = false
            };
            var produto = new Produto
                              {
                                  Nome = "NOME",
                                  DescReduzida = "REDUZIDA",
                                  OrigemProd = 0,
                                  ENQ_IPI = 0,
                                  PesoBruto = 0,
                                  PesoLiquido = 0,
                                  Unidade = unidade,
                                  InfAdicionais = "INFO ADD",
                                  PrecoBase = 2,
                                  TabelaPrecoProdutos = new List<TabelaPrecoProduto> { new TabelaPrecoProduto { PAcrescimo = 0, MargemLucro = 0, TabelaPreco = new TabelaPreco() } }
                              };
            var nota = new Nota();
            nota.Condicao = new Condicao
            {
                Variacao = 0
            };
            nota.TipoNota = new TipoNota { };
            nota.TabelaPreco = new TabelaPreco
            {
                TabelaPrecoProdutos = new List<TabelaPrecoProduto>()
            };
            nota.TabelaPreco.TabelaPrecoProdutos.Add(new TabelaPrecoProduto { PAcrescimo = 0, MargemLucro = 0, Produto = produto, TabelaPreco = nota.TabelaPreco });
            var notaItem = new NotaItem
            {
                Nota = nota,
                Quantidade = 3
            };

            var logicaTelaNotaItem = InvocarSetProduto(notaItem, produto);
            AssertCamposNotaItemPreenchidos(logicaTelaNotaItem);
            AssertValorQuantidadeETotalNotaItem(logicaTelaNotaItem, 2, 3, 6);
        }

        private void AssertCamposNotaItemPreenchidos(LogicaTelaNotaItem logicaTelaNotaItem)
        {
            var notaItem = logicaTelaNotaItem.GetNotaItem;
            var produto = notaItem.Produto;
            Assert.AreEqual(notaItem.ProdutoNome, produto.Nome);
            Assert.AreEqual(notaItem.ProdutoDescReduzida, produto.DescReduzida);
            Assert.AreEqual(notaItem.orig_N11, produto.OrigemProd);
            Assert.AreEqual(notaItem.cEnq_O06, produto.ENQ_IPI.ToString());
            Assert.AreEqual(notaItem.PesoBruto, produto.PesoBruto);
            Assert.AreEqual(notaItem.PesoLiquido, produto.PesoLiquido);
            Assert.AreEqual(notaItem.Unidade, produto.Unidade.Sigla);
            Assert.AreEqual(notaItem.InfAdicionais, produto.InfAdicionais);
        }

        private void AssertValorQuantidadeETotalNotaItem(LogicaTelaNotaItem logicaTelaNotaItem, decimal valor, decimal quantidade, decimal total)
        {
            var notaItem = logicaTelaNotaItem.GetNotaItem;
            Assert.AreEqual(notaItem.Valor, valor);
            Assert.AreEqual(notaItem.Quantidade, quantidade);
            Assert.AreEqual(notaItem.Total, total);
        }

        private LogicaTelaNotaItem InvocarSetProduto(NotaItem notaItem, Produto produto)
        {
            var logicaTelaNotaItem = new LogicaTelaNotaItem(notaItem);
            logicaTelaNotaItem.SetProduto(produto);
            return logicaTelaNotaItem;
        }

        [TestMethod]
        public void Consegue_Iniciar_Tributacao_Nota_Item()
        {
            NotaItem notaitem = new NotaItem();

            Mock<IRepositorioNotaItem> repositorioNotaItem = new Mock<IRepositorioNotaItem>();
            repositorioNotaItem.Setup(i => i.RecuperaIcmsOriginal(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Produto>(), notaitem)).Returns(new IcmsBase());
            NotaItemController.Instancia.SetRepositorio(repositorioNotaItem.Object);

            Mock<IRepositorioNCM> repositorioNCM = new Mock<IRepositorioNCM>();
            repositorioNCM.Setup(i => i.GetByNcm(It.IsAny<string>())).Returns<NCM>(null);
            NCMController.Instancia.SetRepositorio(repositorioNCM.Object);

            Produto produto = new Produto();
            TabelaPreco tabelaPreco = new TabelaPreco();

            produto.TabelaPrecoProdutos.Add(new TabelaPrecoProduto { PAcrescimo = 0, MargemLucro = 0, Produto = new Produto(), TabelaPreco = tabelaPreco });
            produto.PrecoBase = 50;
            produto.Unidade = new Unidade();
            produto.Tributacao = 2;


            Nota nota = new Nota();
            nota.Filial = new Filial { Cidade = new Cidade { UF = new UF { Sigla = "TESTE" } } };
            nota.Pessoa = new Pessoa { PessoaEnderecos = new List<PessoaEndereco> { new PessoaEndereco { BPrincipal = true, Cidade = new Cidade { UF = new UF { Sigla = "TESTE" } } } } };
            nota.TipoNota = new TipoNota { ModeloDocto = TipoNota.ModeloDocumento.NFe, Operacao = new Operacao { CFOP_DEstado = new TabelaCFOP() { BCalcICMS = true } } };
            nota.NotaItems.Add(new NotaItem { Nota = nota, Total = 500, Produto = produto });
            nota.Condicao = new Condicao { Variacao = 0 };
            nota.TabelaPreco = tabelaPreco;
            nota.BPessoaContribuinte = false;


            LogicaTelaNotaItem logicaTelaNotaItem = new LogicaTelaNotaItem(nota.NotaItems[0]);
            logicaTelaNotaItem.SetProduto(produto);
            Assert.AreEqual(logicaTelaNotaItem.GetNotaItem.TAG_CST, "20");



        }

        [TestMethod]
        public void Alterado_Quantidade_Atualiza_Dados_Restantes()
        {
            NotaItem notaitem = new NotaItem();

            Mock<IRepositorioNotaItem> repositorioNotaItem = new Mock<IRepositorioNotaItem>();
            repositorioNotaItem.Setup(i => i.RecuperaIcmsOriginal(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Produto>(), notaitem)).Returns(new IcmsBase());
            NotaItemController.Instancia.SetRepositorio(repositorioNotaItem.Object);

            Mock<IRepositorioNCM> repositorioNCM = new Mock<IRepositorioNCM>();
            repositorioNCM.Setup(i => i.GetByNcm(It.IsAny<string>())).Returns<NCM>(null);
            NCMController.Instancia.SetRepositorio(repositorioNCM.Object);

            Produto produto = new Produto();
            TabelaPreco tabelaPreco = new TabelaPreco();

            produto.TabelaPrecoProdutos.Add(new TabelaPrecoProduto { PAcrescimo = 0, MargemLucro = 0, Produto = new Produto(), TabelaPreco = tabelaPreco });
            produto.PrecoBase = 50;
            produto.Unidade = new Unidade();

            Nota nota = new Nota();
            nota.Filial = new Filial { Cidade = new Cidade { UF = new UF { Sigla = "TESTE" } } };
            nota.Pessoa = new Pessoa { PessoaEnderecos = new List<PessoaEndereco> { new PessoaEndereco { BPrincipal = true, Cidade = new Cidade { UF = new UF { Sigla = "TESTE" } } } } };
            nota.TipoNota = new TipoNota { ModeloDocto = TipoNota.ModeloDocumento.NFe, Operacao = new Operacao { CFOP_DEstado = new TabelaCFOP() { BCalcICMS = true } } };
            nota.NotaItems.Add(new NotaItem { Nota = nota, Total = 500, Produto = produto });
            nota.Condicao = new Condicao { Variacao = 0 };
            nota.TabelaPreco = tabelaPreco;
            nota.BPessoaContribuinte = false;


            LogicaTelaNotaItem logicaTelaNotaItem = new LogicaTelaNotaItem(nota.NotaItems[0]);
            logicaTelaNotaItem.SetProduto(produto);
            logicaTelaNotaItem.SetQuantidade(3.55m);

            Assert.AreEqual(177.5m, logicaTelaNotaItem.GetNotaItem.Total);
            Assert.AreEqual(177.5m, logicaTelaNotaItem.GetNotaItem.VBC_O10);
        }

        [TestMethod]
        public void Alterado_Valor_Atualiza_Dados_Restantes()
        {
            NotaItem notaitem = new NotaItem();

            Mock<IRepositorioNotaItem> repositorioNotaItem = new Mock<IRepositorioNotaItem>();
            repositorioNotaItem.Setup(i => i.RecuperaIcmsOriginal(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Produto>(), notaitem)).Returns(new IcmsBase());
            NotaItemController.Instancia.SetRepositorio(repositorioNotaItem.Object);

            Mock<IRepositorioNCM> repositorioNCM = new Mock<IRepositorioNCM>();
            repositorioNCM.Setup(i => i.GetByNcm(It.IsAny<string>())).Returns<NCM>(null);
            NCMController.Instancia.SetRepositorio(repositorioNCM.Object);

            Produto produto = new Produto();
            TabelaPreco tabelaPreco = new TabelaPreco();

            produto.TabelaPrecoProdutos.Add(new TabelaPrecoProduto { PAcrescimo = 0, MargemLucro = 0, Produto = new Produto(), TabelaPreco = tabelaPreco });
            produto.PrecoBase = 50;
            produto.Unidade = new Unidade();

            Nota nota = new Nota();
            nota.Filial = new Filial { Cidade = new Cidade { UF = new UF { Sigla = "TESTE" } } };
            nota.Pessoa = new Pessoa { PessoaEnderecos = new List<PessoaEndereco> { new PessoaEndereco { BPrincipal = true, Cidade = new Cidade { UF = new UF { Sigla = "TESTE" } } } } };
            nota.TipoNota = new TipoNota { ModeloDocto = TipoNota.ModeloDocumento.NFe, Operacao = new Operacao { CFOP_DEstado = new TabelaCFOP() { BCalcICMS = true } } };
            nota.NotaItems.Add(new NotaItem { Nota = nota, Total = 500, Produto = produto });
            nota.Condicao = new Condicao { Variacao = 0 };
            nota.TabelaPreco = tabelaPreco;
            nota.BPessoaContribuinte = false;


            LogicaTelaNotaItem logicaTelaNotaItem = new LogicaTelaNotaItem(nota.NotaItems[0]);
            logicaTelaNotaItem.SetProduto(produto);
            logicaTelaNotaItem.SetValor(999);
            logicaTelaNotaItem.SetQuantidade(2);

            Assert.AreEqual(1998m, logicaTelaNotaItem.GetNotaItem.Total);
            Assert.AreEqual(1998m, logicaTelaNotaItem.GetNotaItem.VBC_O10);
        }

        [TestMethod]
        public void Alterado_Desconto_Atualiza_Dados_Restantes()
        {
            NotaItem notaitem = new NotaItem();

            Mock<IRepositorioNotaItem> repositorioNotaItem = new Mock<IRepositorioNotaItem>();
            repositorioNotaItem.Setup(i => i.RecuperaIcmsOriginal(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Produto>(), notaitem)).Returns(new IcmsBase());
            NotaItemController.Instancia.SetRepositorio(repositorioNotaItem.Object);

            Produto produto = new Produto();
            TabelaPreco tabelaPreco = new TabelaPreco();

            produto.TabelaPrecoProdutos.Add(new TabelaPrecoProduto { PAcrescimo = 0, MargemLucro = 0, Produto = new Produto(), TabelaPreco = tabelaPreco });
            produto.PrecoBase = 50;
            produto.Unidade = new Unidade();

            Nota nota = new Nota();
            nota.Filial = new Filial { Cidade = new Cidade { UF = new UF { Sigla = "TESTE" } } };
            nota.Pessoa = new Pessoa { PessoaEnderecos = new List<PessoaEndereco> { new PessoaEndereco { BPrincipal = true, Cidade = new Cidade { UF = new UF { Sigla = "TESTE" } } } } };
            nota.TipoNota = new TipoNota { ModeloDocto = TipoNota.ModeloDocumento.NFe, Operacao = new Operacao { CFOP_DEstado = new TabelaCFOP() { BCalcICMS = true } } };
            nota.NotaItems.Add(new NotaItem { Nota = nota, Total = 500, Produto = produto });
            nota.Condicao = new Condicao { Variacao = 0 };
            nota.TabelaPreco = tabelaPreco;
            nota.BPessoaContribuinte = false;


            LogicaTelaNotaItem logicaTelaNotaItem = new LogicaTelaNotaItem(nota.NotaItems[0]);
            logicaTelaNotaItem.SetProduto(produto);
            logicaTelaNotaItem.SetDesconto(50m);
            logicaTelaNotaItem.SetQuantidade(2);

            Assert.AreEqual(50m, logicaTelaNotaItem.GetNotaItem.Total);
            Assert.AreEqual(50m, logicaTelaNotaItem.GetNotaItem.VBC_O10);
        }

        [TestMethod]
        public void Alterado_Base_Icms_Atualiza_Dados_Restantes()
        {
            NotaItem notaitem = new NotaItem();

            Mock<IRepositorioNotaItem> repositorioNotaItem = new Mock<IRepositorioNotaItem>();
            repositorioNotaItem.Setup(i => i.RecuperaIcmsOriginal(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Produto>(), notaitem)).Returns(new IcmsBase { AliqNContrib = 18m, AliqNContribNormal = 18m });
            NotaItemController.Instancia.SetRepositorio(repositorioNotaItem.Object);

            Produto produto = new Produto();
            TabelaPreco tabelaPreco = new TabelaPreco();

            produto.TabelaPrecoProdutos.Add(new TabelaPrecoProduto { PAcrescimo = 0, MargemLucro = 0, Produto = new Produto(), TabelaPreco = tabelaPreco });
            produto.PrecoBase = 11.04m;
            produto.AliqNContrib = 18;
            produto.Unidade = new Unidade();

            Nota nota = new Nota();
            nota.Filial = new Filial { Cidade = new Cidade { UF = new UF { Sigla = "TESTE" } }, SimplesNacional = 2 };
            nota.Pessoa = new Pessoa { PessoaEnderecos = new List<PessoaEndereco> { new PessoaEndereco { BPrincipal = true, Cidade = new Cidade { UF = new UF { Sigla = "TESTE" } } } } };
            nota.TipoNota = new TipoNota { ModeloDocto = TipoNota.ModeloDocumento.NFe, Operacao = new Operacao { CFOP_DEstado = new TabelaCFOP() { BCalcICMS = true } } };
            NotaItem notaItem = new NotaItem { Nota = nota, Total = 500, Produto = produto, CFOP = new TabelaCFOP { BCalcICMS = true } };
            nota.NotaItems.Add(notaItem);
            nota.Condicao = new Condicao { Variacao = 0 };
            nota.TabelaPreco = tabelaPreco;
            nota.BPessoaContribuinte = false;



            LogicaTelaNotaItem logicaTelaNotaItem = new LogicaTelaNotaItem(nota.NotaItems[0]);
            logicaTelaNotaItem.SetProduto(produto);
            logicaTelaNotaItem.SetBaseIcms(5m);

            Assert.AreEqual(11.04m, logicaTelaNotaItem.GetNotaItem.Total);
            Assert.AreEqual(5m, logicaTelaNotaItem.GetNotaItem.BaseICMS);
            Assert.AreEqual(18m, logicaTelaNotaItem.GetNotaItem.AliqICMS);
            Assert.AreEqual(0.90m, logicaTelaNotaItem.GetNotaItem.ValorICMS);
            Assert.AreEqual(6.04m, logicaTelaNotaItem.GetNotaItem.ValorOutroICMS);
        }


        [TestMethod]
        public void Alterado_Base_Icms_E_Depois_A_Qtd_Deve_Calcular_Bases_Normais()
        {
            NotaItem notaitem = new NotaItem();

            Mock<IRepositorioNotaItem> repositorioNotaItem = new Mock<IRepositorioNotaItem>();
            repositorioNotaItem.Setup(i => i.RecuperaIcmsOriginal(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<Produto>(), notaitem)).Returns(new IcmsBase { AliqNContrib = 18m, AliqNContribNormal = 18m });
            NotaItemController.Instancia.SetRepositorio(repositorioNotaItem.Object);

            Produto produto = new Produto();
            TabelaPreco tabelaPreco = new TabelaPreco();

            produto.TabelaPrecoProdutos.Add(new TabelaPrecoProduto { PAcrescimo = 0, MargemLucro = 0, Produto = new Produto(), TabelaPreco = tabelaPreco });
            produto.PrecoBase = 11.04m;
            produto.AliqNContrib = 18;
            produto.Unidade = new Unidade();

            Nota nota = new Nota();
            nota.Filial = new Filial { Cidade = new Cidade { UF = new UF { Sigla = "TESTE" } }, SimplesNacional = 2 };
            nota.Pessoa = new Pessoa { PessoaEnderecos = new List<PessoaEndereco> { new PessoaEndereco { BPrincipal = true, Cidade = new Cidade { UF = new UF { Sigla = "TESTE" } } } } };
            nota.TipoNota = new TipoNota { ModeloDocto = TipoNota.ModeloDocumento.NFe, Operacao = new Operacao { CFOP_DEstado = new TabelaCFOP() { BCalcICMS = true } } };
            NotaItem notaItem = new NotaItem { Nota = nota, Total = 500, Produto = produto, CFOP = new TabelaCFOP { BCalcICMS = true } };
            nota.NotaItems.Add(notaItem);
            nota.Condicao = new Condicao { Variacao = 0 };
            nota.TabelaPreco = tabelaPreco;
            nota.BPessoaContribuinte = false;



            LogicaTelaNotaItem logicaTelaNotaItem = new LogicaTelaNotaItem(nota.NotaItems[0]);
            logicaTelaNotaItem.SetProduto(produto);
            logicaTelaNotaItem.SetBaseIcms(5m);
            logicaTelaNotaItem.SetQuantidade(2);

            Assert.AreEqual(22.08m, logicaTelaNotaItem.GetNotaItem.Total);
            Assert.AreEqual(22.08m, logicaTelaNotaItem.GetNotaItem.BaseICMS);
            Assert.AreEqual(18m, logicaTelaNotaItem.GetNotaItem.AliqICMS);
            Assert.AreEqual(3.97m, logicaTelaNotaItem.GetNotaItem.ValorICMS);
            Assert.AreEqual(0m, logicaTelaNotaItem.GetNotaItem.ValorOutroICMS);
        }


        #endregion

        #region NotaItemServico



        #endregion

    }
}
