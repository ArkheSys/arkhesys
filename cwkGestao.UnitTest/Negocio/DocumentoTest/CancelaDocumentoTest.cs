using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cwkGestao.Modelo;
using cwkGestao.Negocio;
using Moq;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System.Linq.Expressions;

namespace cwkGestao.UnitTest.DocumentoTest
{
    [TestClass]
    public class CancelaDocumentoTest
    {

        #region CancelamentoParcial

        #region Documentos sem baixas

        [TestMethod]
        public void Cancela_Com_Valor_Parcial_Somente_Lancamento_Um_Plano_Conta()
        {
            Documento documento = new Documento();
            documento.Saldo = 500m;
            documento.Valor = 500m;
            documento.ValorTotal = 500m;
            documento.Situacao = "Norm";
            var movimentos = new List<Movimento>();
            movimentos.Add(new Movimento()
            {
                Sequencia = 1,
                Tipo = TipoMovimentoType.Documento,
                Documento = documento,
                Operacao = OperacaoMovimentoType.Lancamento,
                PlanoConta = new PlanoConta { Nome = "plano de conta da pegada", ID = 55 },
                Valor = documento.Valor
            });
            documento.Movimentos = movimentos;

            decimal cancelamentoValor = 300m;
            string cancelamentoMotivo = "faliu";

            Mock<IRepositorioDocumento> repositorio = new Mock<IRepositorioDocumento>(MockBehavior.Loose);

            DocumentoController.Instancia.SetRepositorio(repositorio.Object);
            DocumentoController.Instancia.CancelarDocumentoESalvar(documento, null, cancelamentoMotivo, cancelamentoValor);

            Func<Documento, bool> assert = (Documento doc) =>
            {
                if (doc.Movimentos.Count == 2)
                {
                    Assert.AreEqual(doc.Movimentos[1].ComplementoHist, "faliu");
                    Assert.AreEqual(doc.Movimentos[1].Valor, 300m);
                    Assert.AreEqual(doc.Movimentos[1].PlanoConta.ID, 55);
                    Assert.AreEqual(doc.Movimentos[1].Operacao, OperacaoMovimentoType.Cancelado);
                    Assert.AreEqual(doc.Situacao, "Norm");
                    Assert.AreEqual(doc.Saldo, 200);

                    return true;
                }
                return false;
            };

            repositorio.Verify(i => i.Update(It.Is<Documento>(doc => assert(doc))));
        }

        [TestMethod]
        public void Cancela_Com_Valor_Parcial_Somente_Lancamento_Dois_Planos_Conta()
        {
            Documento documento = new Documento();
            documento.Saldo = 500m;
            documento.Valor = 500m;
            documento.ValorTotal = 500m;
            documento.Situacao = "Norm";
            var movimentos = new List<Movimento>();
            movimentos.Add(new Movimento()
            {
                Sequencia = 1,
                Tipo = TipoMovimentoType.Documento,
                Documento = documento,
                Operacao = OperacaoMovimentoType.Lancamento,
                PlanoConta = new PlanoConta { Nome = "plano de conta da pegada", ID = 55 },
                Valor = 300
            });

            movimentos.Add(new Movimento()
            {
                Sequencia = 1,
                Tipo = TipoMovimentoType.Documento,
                Documento = documento,
                Operacao = OperacaoMovimentoType.Lancamento,
                PlanoConta = new PlanoConta { Nome = "plano de conta da galére", ID = 44 },
                Valor = 200
            });
            documento.Movimentos = movimentos;

            decimal cancelamentoValor = 300m;
            string cancelamentoMotivo = "faliu";

            Mock<IRepositorioDocumento> repositorio = new Mock<IRepositorioDocumento>(MockBehavior.Loose);

            DocumentoController.Instancia.SetRepositorio(repositorio.Object);
            DocumentoController.Instancia.CancelarDocumentoESalvar(documento, null, cancelamentoMotivo, cancelamentoValor);

            Func<Documento, bool> assert = (Documento doc) =>
            {
                if (doc.Movimentos.Count == 4)
                {
                    Assert.AreEqual(doc.Movimentos[2].Sequencia, 2);
                    Assert.AreEqual(doc.Movimentos[2].ComplementoHist, "faliu");
                    Assert.AreEqual(doc.Movimentos[2].Valor, 180m);
                    Assert.AreEqual(doc.Movimentos[2].PlanoConta.ID, 55);
                    Assert.AreEqual(doc.Movimentos[2].Operacao, OperacaoMovimentoType.Cancelado);

                    Assert.AreEqual(doc.Movimentos[3].Sequencia, 2);
                    Assert.AreEqual(doc.Movimentos[3].ComplementoHist, "faliu");
                    Assert.AreEqual(doc.Movimentos[3].Valor, 120m);
                    Assert.AreEqual(doc.Movimentos[3].PlanoConta.ID, 44);
                    Assert.AreEqual(doc.Movimentos[3].Operacao, OperacaoMovimentoType.Cancelado);

                    Assert.AreEqual(doc.Situacao, "Norm");
                    Assert.AreEqual(doc.Saldo, 200);

                    return true;
                }
                throw new Exception("Não entrou no if");
            };

            repositorio.Verify(i => i.Update(It.Is<Documento>(doc => assert(doc))));
        }

        #endregion

        #region Documentos com baixas

        [TestMethod]
        public void Cancela_Com_Valor_Parcial_Varios_Lancamento_Um_Planos_Conta()
        {
            Documento documento = new Documento();
            documento.Saldo = 300m;
            documento.Valor = 500m;
            documento.ValorTotal = 500m;
            documento.Situacao = "BxP";
            var movimentos = new List<Movimento>();
            movimentos.Add(new Movimento()
            {
                Sequencia = 1,
                Tipo = TipoMovimentoType.Documento,
                Documento = documento,
                Operacao = OperacaoMovimentoType.Lancamento,
                PlanoConta = new PlanoConta { Nome = "plano de conta da pegada", ID = 55 },
                Valor = 500
            });

            movimentos.Add(new Movimento()
            {
                Sequencia = 2,
                Tipo = TipoMovimentoType.Documento,
                Documento = documento,
                Operacao = OperacaoMovimentoType.BxParcial,
                PlanoConta = new PlanoConta { Nome = "plano de conta da pegada", ID = 55 },
                Valor = 200
            });
            documento.Movimentos = movimentos;

            decimal cancelamentoValor = 100m;
            string cancelamentoMotivo = "faliu";

            Mock<IRepositorioDocumento> repositorio = new Mock<IRepositorioDocumento>(MockBehavior.Loose);

            DocumentoController.Instancia.SetRepositorio(repositorio.Object);
            DocumentoController.Instancia.CancelarDocumentoESalvar(documento, null, cancelamentoMotivo, cancelamentoValor);

            Func<Documento, bool> assert = (Documento doc) =>
            {
                if (doc.Movimentos.Count == 3)
                {
                    Assert.AreEqual(3, doc.Movimentos[2].Sequencia);
                    Assert.AreEqual("faliu", doc.Movimentos[2].ComplementoHist);
                    Assert.AreEqual(100m, doc.Movimentos[2].Valor);
                    Assert.AreEqual(55, doc.Movimentos[2].PlanoConta.ID);
                    Assert.AreEqual(OperacaoMovimentoType.Cancelado, doc.Movimentos[2].Operacao);

                    Assert.AreEqual("BxP", doc.Situacao);
                    Assert.AreEqual(200, doc.Saldo);

                    return true;
                }
                throw new Exception("Não entrou no if");
            };

            repositorio.Verify(i => i.Update(It.Is<Documento>(doc => assert(doc))));
        }

        [TestMethod]
        public void Cancela_Com_Valor_Parcial_Varios_Lancamento_Um_Planos_Conta2()
        {
            Documento documento = new Documento();
            documento.Saldo = 250m;
            documento.Valor = 500m;
            documento.ValorTotal = 500m;
            documento.Situacao = "BxP";
            var movimentos = new List<Movimento>();
            movimentos.Add(new Movimento()
            {
                Sequencia = 1,
                Tipo = TipoMovimentoType.Documento,
                Documento = documento,
                Operacao = OperacaoMovimentoType.Lancamento,
                PlanoConta = new PlanoConta { Nome = "plano de conta da pegada", ID = 55 },
                Valor = 500
            });

            movimentos.Add(new Movimento()
            {
                Sequencia = 2,
                Tipo = TipoMovimentoType.Documento,
                Documento = documento,
                Operacao = OperacaoMovimentoType.BxParcial,
                PlanoConta = new PlanoConta { Nome = "plano de conta da pegada", ID = 55 },
                Valor = 200
            });


            movimentos.Add(new Movimento()
            {
                Sequencia = 3,
                Tipo = TipoMovimentoType.Documento,
                Documento = documento,
                Operacao = OperacaoMovimentoType.BxParcial,
                PlanoConta = new PlanoConta { Nome = "plano de conta da pegada", ID = 55 },
                Valor = 50
            });
            documento.Movimentos = movimentos;

            decimal cancelamentoValor = 100m;
            string cancelamentoMotivo = "faliu";

            Mock<IRepositorioDocumento> repositorio = new Mock<IRepositorioDocumento>(MockBehavior.Loose);

            DocumentoController.Instancia.SetRepositorio(repositorio.Object);
            DocumentoController.Instancia.CancelarDocumentoESalvar(documento, null, cancelamentoMotivo, cancelamentoValor);

            Func<Documento, bool> assert = (Documento doc) =>
            {
                if (doc.Movimentos.Count == 4)
                {
                    Assert.AreEqual(4, doc.Movimentos[3].Sequencia);
                    Assert.AreEqual("faliu", doc.Movimentos[3].ComplementoHist);
                    Assert.AreEqual(100m, doc.Movimentos[3].Valor);
                    Assert.AreEqual(55, doc.Movimentos[3].PlanoConta.ID);
                    Assert.AreEqual(OperacaoMovimentoType.Cancelado, doc.Movimentos[3].Operacao);

                    Assert.AreEqual("BxP", doc.Situacao);
                    Assert.AreEqual(150, doc.Saldo);

                    return true;
                }
                throw new Exception("Não entrou no if");
            };

            repositorio.Verify(i => i.Update(It.Is<Documento>(doc => assert(doc))));
        }

        [TestMethod]
        public void Cancela_Com_Valor_Parcial_Varios_Lancamento_2_Planos_Conta()
        {
            Documento documento = new Documento();
            documento.Saldo = 400m;
            documento.Valor = 500m;
            documento.ValorTotal = 500m;
            documento.Situacao = "BxP";
            var movimentos = new List<Movimento>();
            movimentos.Add(new Movimento()
            {
                Sequencia = 1,
                Tipo = TipoMovimentoType.Documento,
                Documento = documento,
                Operacao = OperacaoMovimentoType.Lancamento,
                PlanoConta = new PlanoConta { Nome = "plano de conta da pegada", ID = 55 },
                Valor = 360
            });

            movimentos.Add(new Movimento()
            {
                Sequencia = 1,
                Tipo = TipoMovimentoType.Documento,
                Documento = documento,
                Operacao = OperacaoMovimentoType.Lancamento,
                PlanoConta = new PlanoConta { Nome = "plano de conta da galére", ID = 44 },
                Valor = 140
            });


            movimentos.Add(new Movimento()
            {
                Sequencia = 2,
                Tipo = TipoMovimentoType.Documento,
                Documento = documento,
                Operacao = OperacaoMovimentoType.BxParcial,
                PlanoConta = new PlanoConta { Nome = "plano de conta da pegada", ID = 55 },
                Valor = 72
            });

            movimentos.Add(new Movimento()
            {
                Sequencia = 2,
                Tipo = TipoMovimentoType.Documento,
                Documento = documento,
                Operacao = OperacaoMovimentoType.BxParcial,
                PlanoConta = new PlanoConta { Nome = "plano de conta da galére", ID = 44 },
                Valor = 28
            });
            documento.Movimentos = movimentos;

            decimal cancelamentoValor = 136m;
            string cancelamentoMotivo = "faliu";

            Mock<IRepositorioDocumento> repositorio = new Mock<IRepositorioDocumento>(MockBehavior.Loose);

            DocumentoController.Instancia.SetRepositorio(repositorio.Object);
            DocumentoController.Instancia.CancelarDocumentoESalvar(documento, null, cancelamentoMotivo, cancelamentoValor);

            Func<Documento, bool> assert = (Documento doc) =>
            {
                if (doc.Movimentos.Count == 6)
                {
                    Assert.AreEqual(3, doc.Movimentos[4].Sequencia);
                    Assert.AreEqual("faliu", doc.Movimentos[4].ComplementoHist);
                    Assert.AreEqual(97.92m, doc.Movimentos[4].Valor);
                    Assert.AreEqual(55, doc.Movimentos[4].PlanoConta.ID);
                    Assert.AreEqual(OperacaoMovimentoType.Cancelado, doc.Movimentos[4].Operacao);

                    Assert.AreEqual(3, doc.Movimentos[5].Sequencia);
                    Assert.AreEqual("faliu", doc.Movimentos[5].ComplementoHist);
                    Assert.AreEqual(38.08m, doc.Movimentos[5].Valor);
                    Assert.AreEqual(44, doc.Movimentos[5].PlanoConta.ID);
                    Assert.AreEqual(OperacaoMovimentoType.Cancelado, doc.Movimentos[5].Operacao);

                    Assert.AreEqual("BxP", doc.Situacao);
                    Assert.AreEqual(264, doc.Saldo);

                    return true;
                }
                throw new Exception("Não entrou no if");
            };

            repositorio.Verify(i => i.Update(It.Is<Documento>(doc => assert(doc))));
        }

        #endregion

        #endregion

        #region CancelamentoTotal

        #region Documentos sem baixas

        [TestMethod]
        public void Cancela_Com_Valor_Total_Somente_Lancamento_Um_Plano_Conta()
        {
            Documento documento = new Documento();
            documento.Saldo = 500m;
            documento.Valor = 500m;
            documento.ValorTotal = 500m;
            documento.Situacao = "Norm";
            var movimentos = new List<Movimento>();
            movimentos.Add(new Movimento()
            {
                Sequencia = 1,
                Tipo = TipoMovimentoType.Documento,
                Documento = documento,
                Operacao = OperacaoMovimentoType.Lancamento,
                PlanoConta = new PlanoConta { Nome = "plano de conta da pegada", ID = 55 },
                Valor = documento.Valor
            });
            documento.Movimentos = movimentos;

            decimal cancelamentoValor = 500m;
            string cancelamentoMotivo = "faliu";

            Mock<IRepositorioDocumento> repositorio = new Mock<IRepositorioDocumento>(MockBehavior.Loose);

            DocumentoController.Instancia.SetRepositorio(repositorio.Object);
            DocumentoController.Instancia.CancelarDocumentoESalvar(documento, null, cancelamentoMotivo, cancelamentoValor);

            Func<Documento, bool> assert = (Documento doc) =>
            {
                if (doc.Movimentos.Count == 2)
                {
                    Assert.AreEqual("faliu", doc.Movimentos[1].ComplementoHist);
                    Assert.AreEqual(500m, doc.Movimentos[1].Valor);
                    Assert.AreEqual(55, doc.Movimentos[1].PlanoConta.ID);
                    Assert.AreEqual(OperacaoMovimentoType.Cancelado, doc.Movimentos[1].Operacao);
                    Assert.AreEqual("Canc", doc.Situacao);
                    Assert.AreEqual(0, doc.Saldo);
                    return true;
                }
                return false;
            };

            repositorio.Verify(i => i.Update(It.Is<Documento>(doc => assert(doc))));
        }

        [TestMethod]
        public void Cancela_Com_Valor_Total_Somente_Lancamento_Dois_Planos_Conta()
        {
            Documento documento = new Documento();
            //documento.Saldo = 500m;
            documento.Valor = 500m;
            documento.ValorTotal = 500m;
            documento.Situacao = "Norm";
            var movimentos = new List<Movimento>();
            movimentos.Add(new Movimento()
            {
                Sequencia = 1,
                Tipo = TipoMovimentoType.Documento,
                Documento = documento,
                Operacao = OperacaoMovimentoType.Lancamento,
                PlanoConta = new PlanoConta { Nome = "plano de conta da pegada", ID = 55 },
                Valor = 300
            });

            movimentos.Add(new Movimento()
            {
                Sequencia = 1,
                Tipo = TipoMovimentoType.Documento,
                Documento = documento,
                Operacao = OperacaoMovimentoType.Lancamento,
                PlanoConta = new PlanoConta { Nome = "plano de conta da galére", ID = 44 },
                Valor = 200
            });
            documento.Movimentos = movimentos;

            decimal cancelamentoValor = 500m;
            string cancelamentoMotivo = "faliu";

            Mock<IRepositorioDocumento> repositorio = new Mock<IRepositorioDocumento>(MockBehavior.Loose);

            DocumentoController.Instancia.SetRepositorio(repositorio.Object);
            DocumentoController.Instancia.CancelarDocumentoESalvar(documento, null, cancelamentoMotivo, cancelamentoValor);

            Func<Documento, bool> assert = (Documento doc) =>
            {
                if (doc.Movimentos.Count == 4)
                {
                    Assert.AreEqual(doc.Movimentos[2].Sequencia, 2);
                    Assert.AreEqual(doc.Movimentos[2].ComplementoHist, "faliu");
                    Assert.AreEqual(doc.Movimentos[2].Valor, 300m);
                    Assert.AreEqual(doc.Movimentos[2].PlanoConta.ID, 55);
                    Assert.AreEqual(doc.Movimentos[2].Operacao, OperacaoMovimentoType.Cancelado);

                    Assert.AreEqual(doc.Movimentos[3].Sequencia, 2);
                    Assert.AreEqual(doc.Movimentos[3].ComplementoHist, "faliu");
                    Assert.AreEqual(doc.Movimentos[3].Valor, 200m);
                    Assert.AreEqual(doc.Movimentos[3].PlanoConta.ID, 44);
                    Assert.AreEqual(doc.Movimentos[3].Operacao, OperacaoMovimentoType.Cancelado);

                    Assert.AreEqual(doc.Situacao, "Canc");
                    Assert.AreEqual(doc.Saldo, 0);

                    return true;
                }
                throw new Exception("Não entrou no if");
            };

            repositorio.Verify(i => i.Update(It.Is<Documento>(doc => assert(doc))));
        }

        #endregion

        #region Documentos com baixas

        [TestMethod]
        public void Cancela_Com_Valor_Total_Varios_Lancamento_1_Planos_Conta()
        {
            Documento documento = new Documento();
            documento.Saldo = 288m;
            documento.Valor = 500m;
            documento.ValorTotal = 500m;
            documento.Situacao = "BxP";
            var movimentos = new List<Movimento>();
            movimentos.Add(new Movimento()
            {
                Sequencia = 1,
                Tipo = TipoMovimentoType.Documento,
                Documento = documento,
                Operacao = OperacaoMovimentoType.Lancamento,
                PlanoConta = new PlanoConta { Nome = "plano de conta da pegada", ID = 55 },
                Valor = 500
            });

            movimentos.Add(new Movimento()
            {
                Sequencia = 2,
                Tipo = TipoMovimentoType.Documento,
                Documento = documento,
                Operacao = OperacaoMovimentoType.BxParcial,
                PlanoConta = new PlanoConta { Nome = "plano de conta da pegada", ID = 55 },
                Valor = 140
            });


            movimentos.Add(new Movimento()
            {
                Sequencia = 3,
                Tipo = TipoMovimentoType.Documento,
                Documento = documento,
                Operacao = OperacaoMovimentoType.BxParcial,
                PlanoConta = new PlanoConta { Nome = "plano de conta da pegada", ID = 55 },
                Valor = 72
            });
            documento.Movimentos = movimentos;

            decimal cancelamentoValor = 288m;
            string cancelamentoMotivo = "faliu";

            Mock<IRepositorioDocumento> repositorio = new Mock<IRepositorioDocumento>(MockBehavior.Loose);

            DocumentoController.Instancia.SetRepositorio(repositorio.Object);
            DocumentoController.Instancia.CancelarDocumentoESalvar(documento, null, cancelamentoMotivo, cancelamentoValor);

            Func<Documento, bool> assert = (Documento doc) =>
            {
                if (doc.Movimentos.Count == 4)
                {
                    Assert.AreEqual(4, doc.Movimentos[3].Sequencia);
                    Assert.AreEqual("faliu", doc.Movimentos[3].ComplementoHist);
                    Assert.AreEqual(288m, doc.Movimentos[3].Valor);
                    Assert.AreEqual(55, doc.Movimentos[3].PlanoConta.ID);
                    Assert.AreEqual(OperacaoMovimentoType.Cancelado, doc.Movimentos[3].Operacao);

                    Assert.AreEqual("BxT", doc.Situacao);
                    Assert.AreEqual(0, doc.Saldo);

                    return true;
                }
                throw new Exception("Não entrou no if");
            };

            repositorio.Verify(i => i.Update(It.Is<Documento>(doc => assert(doc))));
        }

        [TestMethod]
        public void Cancela_Com_Valor_Total_Varios_Lancamento_Varios_Planos_Conta()
        {
            Documento documento = new Documento();
            documento.Saldo = 400m;
            documento.Valor = 500m;
            documento.ValorTotal = 500m;
            documento.Situacao = "BxP";
            var movimentos = new List<Movimento>();
            movimentos.Add(new Movimento()
            {
                Sequencia = 1,
                Tipo = TipoMovimentoType.Documento,
                Documento = documento,
                Operacao = OperacaoMovimentoType.Lancamento,
                PlanoConta = new PlanoConta { Nome = "plano de conta da pegada", ID = 55 },
                Valor = 360
            });

            movimentos.Add(new Movimento()
            {
                Sequencia = 1,
                Tipo = TipoMovimentoType.Documento,
                Documento = documento,
                Operacao = OperacaoMovimentoType.Lancamento,
                PlanoConta = new PlanoConta { Nome = "plano de conta da galére", ID = 44 },
                Valor = 140
            });


            movimentos.Add(new Movimento()
            {
                Sequencia = 2,
                Tipo = TipoMovimentoType.Documento,
                Documento = documento,
                Operacao = OperacaoMovimentoType.BxParcial,
                PlanoConta = new PlanoConta { Nome = "plano de conta da pegada", ID = 55 },
                Valor = 72
            });

            movimentos.Add(new Movimento()
            {
                Sequencia = 2,
                Tipo = TipoMovimentoType.Documento,
                Documento = documento,
                Operacao = OperacaoMovimentoType.BxParcial,
                PlanoConta = new PlanoConta { Nome = "plano de conta da galére", ID = 44 },
                Valor = 28
            });
            documento.Movimentos = movimentos;

            decimal cancelamentoValor = 400m;
            string cancelamentoMotivo = "faliu";

            Mock<IRepositorioDocumento> repositorio = new Mock<IRepositorioDocumento>(MockBehavior.Loose);

            DocumentoController.Instancia.SetRepositorio(repositorio.Object);
            DocumentoController.Instancia.CancelarDocumentoESalvar(documento, null, cancelamentoMotivo, cancelamentoValor);

            Func<Documento, bool> assert = (Documento doc) =>
            {
                if (doc.Movimentos.Count == 6)
                {
                    Assert.AreEqual(3, doc.Movimentos[4].Sequencia);
                    Assert.AreEqual("faliu", doc.Movimentos[4].ComplementoHist);
                    Assert.AreEqual(288m, doc.Movimentos[4].Valor);
                    Assert.AreEqual(55, doc.Movimentos[4].PlanoConta.ID);
                    Assert.AreEqual(OperacaoMovimentoType.Cancelado, doc.Movimentos[4].Operacao);

                    Assert.AreEqual(3, doc.Movimentos[5].Sequencia);
                    Assert.AreEqual("faliu", doc.Movimentos[5].ComplementoHist);
                    Assert.AreEqual(112m, doc.Movimentos[5].Valor);
                    Assert.AreEqual(44, doc.Movimentos[5].PlanoConta.ID);
                    Assert.AreEqual(OperacaoMovimentoType.Cancelado, doc.Movimentos[5].Operacao);

                    Assert.AreEqual("BxT", doc.Situacao);
                    Assert.AreEqual(0, doc.Saldo);

                    return true;
                }
                throw new Exception("Não entrou no if");
            };

            repositorio.Verify(i => i.Update(It.Is<Documento>(doc => assert(doc))));
        }

        [TestMethod]
        public void Documento_Com_Movimentos_Somente_De_Lancamento_De_Cancelamento()
        {
            Documento documento = new Documento();
            documento.Saldo = 340m;
            documento.Valor = 500m;
            documento.ValorTotal = 500m;
            documento.Situacao = "Norm";
            var movimentos = new List<Movimento>();
            movimentos.Add(new Movimento()
            {
                Sequencia = 1,
                Tipo = TipoMovimentoType.Documento,
                Documento = documento,
                Operacao = OperacaoMovimentoType.Lancamento,
                PlanoConta = new PlanoConta { Nome = "plano de conta da pegada", ID = 55 },
                Valor = 500
            });

            movimentos.Add(new Movimento()
            {
                Sequencia = 2,
                Tipo = TipoMovimentoType.Documento,
                Documento = documento,
                Operacao = OperacaoMovimentoType.Cancelado,
                PlanoConta = new PlanoConta { Nome = "plano de conta da pegada", ID = 55 },
                Valor = 140
            });


            movimentos.Add(new Movimento()
            {
                Sequencia = 3,
                Tipo = TipoMovimentoType.Documento,
                Documento = documento,
                Operacao = OperacaoMovimentoType.Cancelado,
                PlanoConta = new PlanoConta { Nome = "plano de conta da pegada", ID = 55 },
                Valor = 20
            });
            documento.Movimentos = movimentos;

            decimal cancelamentoValor = 340m;
            string cancelamentoMotivo = "faliu";

            Mock<IRepositorioDocumento> repositorio = new Mock<IRepositorioDocumento>(MockBehavior.Loose);

            DocumentoController.Instancia.SetRepositorio(repositorio.Object);
            DocumentoController.Instancia.CancelarDocumentoESalvar(documento, null, cancelamentoMotivo, cancelamentoValor);

            Func<Documento, bool> assert = (Documento doc) =>
            {
                if (doc.Movimentos.Count == 4)
                {
                    Assert.AreEqual(4, doc.Movimentos[3].Sequencia);
                    Assert.AreEqual("faliu", doc.Movimentos[3].ComplementoHist);
                    Assert.AreEqual(340m, doc.Movimentos[3].Valor);
                    Assert.AreEqual(55, doc.Movimentos[3].PlanoConta.ID);
                    Assert.AreEqual(OperacaoMovimentoType.Cancelado, doc.Movimentos[3].Operacao);

                    Assert.AreEqual("Canc", doc.Situacao);
                    Assert.AreEqual(0, doc.Saldo);

                    return true;
                }
                throw new Exception("Não entrou no if");
            };

            repositorio.Verify(i => i.Update(It.Is<Documento>(doc => assert(doc))));
        }
        #endregion

        #endregion

        
    }
}
