using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cwkGestao.Modelo;
using Moq;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Negocio;
using cwkGestao.Modelo.Proxy;

namespace cwkGestao.UnitTest.EstoqueTest
{
    [TestClass]
    public class BaixaRequisicaoTest
    {
        public BaixaRequisicaoTest()
        {
            MocarRepositorioRequisicao();
            MocarRepositorioNota();
        }

        private Mock<IRepositorioPedido> repositorioRequisicao;
        private Mock<IRepositorioNota> repositorioNota;
        private Mock<IRepositorioConfiguracao> repositorioConfiguracao;
        private Filial filial1;
        private Pessoa pessoa1;
        private TipoPedido tipoPedido1;

        private void MocarRepositorioRequisicao()
        {
            repositorioRequisicao = new Mock<IRepositorioPedido>(MockBehavior.Loose);
            repositorioRequisicao.Setup(f => f.MaxCodigo()).Returns(1);
            repositorioRequisicao.Setup(f => f.GetServerDateTime());
            PedidoController.Instancia.SetRepositorio(repositorioRequisicao.Object);
        }

        private void MocarRepositorioNota()
        {
            repositorioNota = new Mock<IRepositorioNota>(MockBehavior.Loose);
            repositorioNota.Setup(f => f.GetNovoNumeroSerie(It.IsAny<Nota>())).Returns(0);
            repositorioNota.Setup(f => f.MaxCodigo()).Returns(1);
            repositorioNota.Setup(f => f.GetServerDateTime());
            NotaController.Instancia.SetRepositorio(repositorioNota.Object);
        }

        private void MocarRepositorioConfiguracao(Configuracao config)
        {
            var configuracoes = new List<Configuracao>();
            configuracoes.Add(config);

            repositorioConfiguracao = new Mock<IRepositorioConfiguracao>(MockBehavior.Loose);
            repositorioConfiguracao.Setup(f => f.GetAll()).Returns(configuracoes);
            ConfiguracaoController.Instancia.SetRepositorio(repositorioConfiguracao.Object);
        }

        private void SetarDadosBaixa(TipoNota tipoNota)
        {
            filial1 = new Filial
            {
                ID = 1,
                Codigo = 1,
                Nome = "CWORK SISTEMAS LTDA ME",
                Endereco = "AV CWORK",
                CEP = "00000-000",
                Bairro = "BAIRRO CWORK",
                Numero = "123",
                CNPJ = "",
                Inscricao = "",
            };

            pessoa1 = new Pessoa
            {
                ID = 1,
                Codigo = 1,
                Nome = "EDSAN",
                BCliente = true,

            };
            pessoa1.PessoaEnderecos.Add(new PessoaEndereco
            {
                ID = 1,
                BPrincipal = true,
                BAtivo = true,
                Cidade = new Cidade
                {
                    ID = 1,
                    Codigo = 1,
                    Nome = "CIDADE",
                    UF = new UF { ID = 1, Nome = "UF", Sigla = "UF" }
                }
            });

            pessoa1.PessoaClientes.Add(new PessoaCliente
            {
                ID = 1,
                BContribuinte = false,
                Pessoa = pessoa1
            });

            tipoPedido1 = new TipoPedido
            {
                ID = 1,
                Codigo = 1,
                Ent_Sai = (int)InOutType.Saída,
                Nome = "TIPO REQUISIÇÃO",
                Tipo = TipoPedidoType.Requisição,
                TipoNota = tipoNota
            };
        }

        private bool DadosNotaEstaoPreenchidos(Nota nota, Pedido requisicao)
        {
            return
            nota.Filial.Equals(requisicao.Filial)
            && nota.Pessoa.Equals(requisicao.Pessoa)
            && nota.TipoNota.Equals(requisicao.TipoPedido.TipoNota);
        }

        private Nota ChamarRotinaBaixa(IList<pxyBaixaRequisicao> itensBaixa)
        {
            var baixaRequisicao = new Negocio.Faturamento.Requisicao(itensBaixa);
            baixaRequisicao.Baixar();
            return baixaRequisicao.nota;
        }

        [TestMethod]
        public void Baixou_Com_Quantidade_Total_Atualizou_Requisicao()
        {
            #region dados Baixa

            var tipoNota = new TipoNota
            {
                ID = 1,
                Codigo = 1,
                Nome = "TIPO NOTA REQUISIÇÃO",
                Serie = "1",
                ModeloDocto = TipoNota.ModeloDocumento.Balanco,
                BAtualizaPedido = true
            };
            SetarDadosBaixa(tipoNota);

            var requisicao = new Pedido
            {
                ID = 1,
                Codigo = 1,
                Filial = filial1,
                Pessoa = pessoa1,
                Dt = Convert.ToDateTime("01/01/2011"),
                DtDigitacao = Convert.ToDateTime("01/01/2011"),
                Ent_Sai = (int)InOutType.Saída,
                TipoPedido = tipoPedido1,
                Numero = 1,
                Observacao1 = "OBS1",
                Observacao2 = "OBS2",
                Observacao3 = "OBS3",
            };

            var item1 = new PedidoItem
            {
                ID = 1,
                Sequencia = 1,
                Quantidade = 1,
                Produto = new Produto
                {
                    ID = 1,
                    Codigo = "1",
                    Nome = "PRODUTO 1",
                },
                Unidade = "BLA",
                Pedido = requisicao
            };
            requisicao.Items.Add(item1);

            var config = new Configuracao
            {
                ID = 1,
                BExcluirOrcamentoFaturado = false,
            };
            MocarRepositorioConfiguracao(config);
            
            #endregion

            IList<pxyBaixaRequisicao> itensBaixa = new List<pxyBaixaRequisicao>();
            itensBaixa.Add(new pxyBaixaRequisicao { PedidoItem = item1, Selecionado = true });

            Nota notaGerada = ChamarRotinaBaixa(itensBaixa);

            Assert.AreEqual(requisicao.Status, StatusOrcamento.Faturado);
            Assert.AreEqual(requisicao.Items.Count, notaGerada.NotaItems.Count);
            Assert.IsTrue(DadosNotaEstaoPreenchidos(notaGerada, requisicao));
            
            foreach (var notaItem in notaGerada.NotaItems)
            {
                var ri = requisicao.Items.Where(r => r.Produto.Equals(notaItem.Produto)).First();
                Assert.AreEqual(notaItem.Quantidade, ri.Quantidade);
                Assert.AreEqual(notaItem.Quantidade, ri.QtdRetirada);
            }
        }

        [TestMethod]
        public void Baixou_Com_Quantidade_Total_Nao_Atualizou_Requisicao()
        {
            #region dados Baixa

            var tipoNota = new TipoNota
            {
                ID = 1,
                Codigo = 1,
                Nome = "TIPO NOTA REQUISIÇÃO",
                Serie = "1",
                ModeloDocto = TipoNota.ModeloDocumento.Balanco,
                BAtualizaPedido = false
            };
            SetarDadosBaixa(tipoNota);

            var requisicao = new Pedido
            {
                ID = 1,
                Codigo = 1,
                Filial = filial1,
                Pessoa = pessoa1,
                Dt = Convert.ToDateTime("01/01/2011"),
                DtDigitacao = Convert.ToDateTime("01/01/2011"),
                Ent_Sai = (int)InOutType.Saída,
                TipoPedido = tipoPedido1,
                Numero = 1,
                Observacao1 = "OBS1",
                Observacao2 = "OBS2",
                Observacao3 = "OBS3",
            };

            var item1 = new PedidoItem
            {
                ID = 1,
                Sequencia = 1,
                Quantidade = 1,
                Produto = new Produto
                {
                    ID = 1,
                    Codigo = "1",
                    Nome = "PRODUTO 1",
                },
                Unidade = "BLA",
                Pedido = requisicao
            };
            requisicao.Items.Add(item1);

            var config = new Configuracao
            {
                ID = 1,
                BExcluirOrcamentoFaturado = false,
            };
            MocarRepositorioConfiguracao(config);

            #endregion

            IList<pxyBaixaRequisicao> itensBaixa = new List<pxyBaixaRequisicao>();
            itensBaixa.Add(new pxyBaixaRequisicao { PedidoItem = item1, Selecionado = true });

            Nota notaGerada = ChamarRotinaBaixa(itensBaixa);

            Assert.AreNotEqual(requisicao.Status, StatusOrcamento.Faturado);

            foreach (var ri in requisicao.Items)
            {
                Assert.AreEqual(ri.QtdRetirada, 0);
            }
        }

        [TestMethod]
        public void Baixou_Todos_Itens_Com_Quantidade_Parcial_Atualizou_Requisicao()
        {
            #region dados Baixa

            var tipoNota = new TipoNota
            {
                ID = 1,
                Codigo = 1,
                Nome = "TIPO NOTA REQUISIÇÃO",
                Serie = "1",
                ModeloDocto = TipoNota.ModeloDocumento.Balanco,
                BAtualizaPedido = true
            };
            SetarDadosBaixa(tipoNota);

            var requisicao = new Pedido
            {
                ID = 1,
                Codigo = 1,
                Filial = filial1,
                Pessoa = pessoa1,
                Dt = Convert.ToDateTime("01/01/2011"),
                DtDigitacao = Convert.ToDateTime("01/01/2011"),
                Ent_Sai = (int)InOutType.Saída,
                TipoPedido = tipoPedido1,
                Numero = 1,
                Observacao1 = "OBS1",
                Observacao2 = "OBS2",
                Observacao3 = "OBS3",
            };

            var item1 = new PedidoItem
            {
                ID = 1,
                Sequencia = 1,
                Quantidade = 6.6m,
                Produto = new Produto
                {
                    ID = 1,
                    Codigo = "1",
                    Nome = "PRODUTO 1",
                    Unidade = new Unidade
                    {
                        BQtdFracionada = true,
                        Sigla = "%",
                        Nome = "PERC"
                    }
                },
                Unidade = "%",
                Pedido = requisicao
            };
            requisicao.Items.Add(item1);

            var item2 = new PedidoItem
            {
                ID = 2,
                Sequencia = 2,
                Quantidade = 10,
                QtdRetirada = 5,
                Produto = new Produto
                {
                    ID = 2,
                    Codigo = "2",
                    Nome = "PRODUTO 2",
                },
                Unidade = "BLA",
                Pedido = requisicao
            };
            requisicao.Items.Add(item2);

            var config = new Configuracao
            {
                ID = 1,
                BExcluirOrcamentoFaturado = false,
            };
            MocarRepositorioConfiguracao(config);

            #endregion

            IList<pxyBaixaRequisicao> itensBaixa = new List<pxyBaixaRequisicao>();
            itensBaixa.Add(new pxyBaixaRequisicao { PedidoItem = item1, Selecionado = true, Quantidade = 3.3m });
            itensBaixa.Add(new pxyBaixaRequisicao { PedidoItem = item2, Selecionado = true, Quantidade = 5 });

            Nota notaGerada = ChamarRotinaBaixa(itensBaixa);

            Assert.AreEqual(requisicao.Status, StatusOrcamento.FaturadoParcial);
            Assert.AreEqual(requisicao.Items.Count, notaGerada.NotaItems.Count);
            foreach (var item in itensBaixa)
            {
                var ni = notaGerada.NotaItems.Where(n => n.Produto.Equals(item.PedidoItem.Produto)).First();
                Assert.AreEqual(ni.Quantidade, item.Quantidade);
                //Quantidade retirada agora deve ser maior do que a quantidade retirada antes da baixa
                Assert.IsTrue(item.PedidoItem.QtdRetirada > item.PedidoItem.Quantidade - item.QuantidadeAnt);
            }
        }

        [TestMethod]
        public void Baixou_1_de_2_Itens_Atualizou_Requisicao()
        {
            #region dados Baixa

            var tipoNota = new TipoNota
            {
                ID = 1,
                Codigo = 1,
                Nome = "TIPO NOTA REQUISIÇÃO",
                Serie = "1",
                ModeloDocto = TipoNota.ModeloDocumento.Balanco,
                BAtualizaPedido = true
            };
            SetarDadosBaixa(tipoNota);

            var requisicao = new Pedido
            {
                ID = 1,
                Codigo = 1,
                Filial = filial1,
                Pessoa = pessoa1,
                Dt = Convert.ToDateTime("01/01/2011"),
                DtDigitacao = Convert.ToDateTime("01/01/2011"),
                Ent_Sai = (int)InOutType.Saída,
                TipoPedido = tipoPedido1,
                Numero = 1,
                Observacao1 = "OBS1",
                Observacao2 = "OBS2",
                Observacao3 = "OBS3",
            };

            var item1 = new PedidoItem
            {
                ID = 1,
                Sequencia = 1,
                Quantidade = 6.6m,
                Produto = new Produto
                {
                    ID = 1,
                    Codigo = "1",
                    Nome = "PRODUTO 1",
                    Unidade = new Unidade
                    {
                        BQtdFracionada = true,
                        Sigla = "%",
                        Nome = "PERC"
                    }
                },
                Unidade = "%",
                Pedido = requisicao
            };
            requisicao.Items.Add(item1);

            var item2 = new PedidoItem
            {
                ID = 2,
                Sequencia = 2,
                Quantidade = 10,
                QtdRetirada = 5,
                Produto = new Produto
                {
                    ID = 2,
                    Codigo = "2",
                    Nome = "PRODUTO 2",
                },
                Unidade = "BLA",
                Pedido = requisicao
            };
            requisicao.Items.Add(item2);

            var config = new Configuracao
            {
                ID = 1,
                BExcluirOrcamentoFaturado = false,
            };
            MocarRepositorioConfiguracao(config);

            #endregion

            IList<pxyBaixaRequisicao> itensBaixa = new List<pxyBaixaRequisicao>();
            itensBaixa.Add(new pxyBaixaRequisicao { PedidoItem = item1, Selecionado = true, Quantidade = 6.6m });

            Nota notaGerada = ChamarRotinaBaixa(itensBaixa);

            Assert.AreEqual(requisicao.Status, StatusOrcamento.FaturadoParcial);
            Assert.AreNotEqual(requisicao.Items.Count, notaGerada.NotaItems.Count);            
        }

        [TestMethod]
        public void Baixou_Total_Requisicao_Com_Baixa_Parcial()
        {
            #region dados Baixa

            var tipoNota = new TipoNota
            {
                ID = 1,
                Codigo = 1,
                Nome = "TIPO NOTA REQUISIÇÃO",
                Serie = "1",
                ModeloDocto = TipoNota.ModeloDocumento.Balanco,
                BAtualizaPedido = true
            };
            SetarDadosBaixa(tipoNota);

            var requisicao = new Pedido
            {
                ID = 1,
                Codigo = 1,
                Filial = filial1,
                Pessoa = pessoa1,
                Dt = Convert.ToDateTime("01/01/2011"),
                DtDigitacao = Convert.ToDateTime("01/01/2011"),
                Ent_Sai = (int)InOutType.Saída,
                TipoPedido = tipoPedido1,
                Status = StatusOrcamento.FaturadoParcial,
                Numero = 1,
                Observacao1 = "OBS1",
                Observacao2 = "OBS2",
                Observacao3 = "OBS3",
            };

            var item1 = new PedidoItem
            {
                ID = 1,
                Sequencia = 1,
                Quantidade = 6.6m,
                QtdRetirada = 3.6m,
                Produto = new Produto
                {
                    ID = 1,
                    Codigo = "1",
                    Nome = "PRODUTO 1",
                    Unidade = new Unidade
                    {
                        BQtdFracionada = true,
                        Sigla = "%",
                        Nome = "PERC"
                    }
                },
                Unidade = "%",
                Pedido = requisicao
            };
            requisicao.Items.Add(item1);

            var item2 = new PedidoItem
            {
                ID = 2,
                Sequencia = 2,
                Quantidade = 10,
                QtdRetirada = 10,
                Produto = new Produto
                {
                    ID = 2,
                    Codigo = "2",
                    Nome = "PRODUTO 2",
                },
                Unidade = "BLA",
                Pedido = requisicao
            };
            requisicao.Items.Add(item2);

            var config = new Configuracao
            {
                ID = 1,
                BExcluirOrcamentoFaturado = false,
            };
            MocarRepositorioConfiguracao(config);

            #endregion

            IList<pxyBaixaRequisicao> itensBaixa = new List<pxyBaixaRequisicao>();
            itensBaixa.Add(new pxyBaixaRequisicao { PedidoItem = item1, Selecionado = true, Quantidade = 3m });

            Nota notaGerada = ChamarRotinaBaixa(itensBaixa);

            Assert.AreEqual(requisicao.Status, StatusOrcamento.Faturado);            
        }        
    }
}
