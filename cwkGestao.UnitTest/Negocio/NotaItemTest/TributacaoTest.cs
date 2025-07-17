using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cwkGestao.Negocio.Tributacao;
using Moq;
using cwkGestao.Negocio.Enums;
using cwkGestao.Negocio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Modelo;

namespace cwkGestao.UnitTest.NotaItemTest
{
    /// <summary>
    /// Summary description for TributacaoTest
    /// </summary>
    [TestClass]
    public class TributacaoTest
    {
        public TributacaoTest()
        {

        }

        #region CalculosBasicos
        [TestMethod]
        public void ReduzirBaseCalculoTest()
        {
            decimal baseCalculo = 120.3333m;
            decimal reducaoBaseCalculo = 39.6567m;

            Tributacao tributacao = new Tributacao(null);
            decimal baseReduzida = tributacao.ReduzirBaseCalculo(baseCalculo, reducaoBaseCalculo);

            Assert.AreEqual(72.61m, baseReduzida);
        }

        [TestMethod]
        public void ValorAliquota()
        {
            decimal baseCalculo = 17.88m;
            decimal aliquota = 20m;

            Tributacao tributacao = new Tributacao(null);
            decimal baseReduzida = tributacao.CalcularValorAliquota(baseCalculo, aliquota);

            Assert.AreEqual(3.58m, baseReduzida);
        }
        #endregion

        #region Pis
        [TestMethod]
        public void Nao_Calcula_Pis_Para_Tag_De_Nao_Tributacao()
        {
            Mock<ITributavel> tributavel = new Mock<ITributavel>(MockBehavior.Strict);
            Mock<ICstCalculoProvider> cstProvider = new Mock<ICstCalculoProvider>(MockBehavior.Strict);
            cstProvider.SetupGet<IList<string>>(i => i.PisCsts).Returns(new List<string>() { "05", "06", "01", "02" });

            tributavel.Setup(i => i.PisCST).Returns("08");
            tributavel.SetupSet(i => i.PisBaseCalculo = 0);
            tributavel.SetupSet(i => i.PisAliquota = 0);
            tributavel.SetupSet(i => i.PisValor = 0);

            Tributacao tributacao = new Tributacao(tributavel.Object);
            tributacao.CstCalculoProvider = cstProvider.Object;
            tributacao.CalculaPis();

            tributavel.VerifyAll();
        }

        [TestMethod]
        public void Calcula_Pis_Para_Tag_De_Tributacao()
        {
            Mock<ITributavel> tributavel = new Mock<ITributavel>(MockBehavior.Strict);
            Mock<ICstCalculoProvider> cstProvider = new Mock<ICstCalculoProvider>(MockBehavior.Strict);
            cstProvider.SetupGet<IList<string>>(i => i.PisCsts).Returns(new List<string>() { "05", "06", "01", "02" });

//            tributavel.Setup(i => i.BaseCalculoGeral).Returns(120.66m);
            tributavel.Setup(i => i.PisCST).Returns("01");
            tributavel.Setup(i => i.PisAliquota).Returns(3.22m);
//            tributavel.SetupSet(i => i.PisBaseCalculo = 120.66m);
            tributavel.SetupGet(i => i.PisBaseCalculo).Returns(120.66m);

            tributavel.SetupSet(i => i.PisValor = 3.89m);

            Tributacao tributacao = new Tributacao(tributavel.Object);
            tributacao.CstCalculoProvider = cstProvider.Object;
            tributacao.CalculaPis();

            tributavel.VerifyAll();
        }
        #endregion

        #region Ipi
        [TestMethod]
        public void Nao_Calcula_Ipi_Para_Tag_De_Nao_Tributacao()
        {
            Mock<ITributavel> tributavel = new Mock<ITributavel>(MockBehavior.Strict);
            Mock<ICstCalculoProvider> cstProvider = new Mock<ICstCalculoProvider>(MockBehavior.Strict);
            cstProvider.SetupGet<IList<string>>(i => i.IpiCsts).Returns(new List<string>() { "05", "06", "01", "02" });

            tributavel.Setup(i => i.IpiCST).Returns("08");
            tributavel.SetupSet(i => i.IpiBaseCalculo = 0);
            tributavel.SetupSet(i => i.IpiAliquota = 0);
            tributavel.SetupSet(i => i.IpiValor = 0);

            Tributacao tributacao = new Tributacao(tributavel.Object);
            tributacao.CstCalculoProvider = cstProvider.Object;
            tributacao.CalculaIpi();

            tributavel.VerifyAll();
        }

        [TestMethod]
        public void Calcula_Ipi_Para_Tag_De_Tributacao()
        {
            Mock<ITributavel> tributavel = new Mock<ITributavel>(MockBehavior.Strict);
            Mock<ICstCalculoProvider> cstProvider = new Mock<ICstCalculoProvider>(MockBehavior.Strict);
            cstProvider.SetupGet<IList<string>>(i => i.IpiCsts).Returns(new List<string>() { "05", "06", "01", "02" });

            tributavel.Setup(i => i.IpiCST).Returns("01");
            tributavel.Setup(i => i.IpiAliquota).Returns(15.69m);
            tributavel.SetupGet(i => i.IpiBaseCalculo).Returns(2545.99m);

            tributavel.SetupSet(i => i.IpiValor = 399.47m);

            Tributacao tributacao = new Tributacao(tributavel.Object);
            tributacao.CstCalculoProvider = cstProvider.Object;
            tributacao.CalculaIpi();

            tributavel.VerifyAll();
        }

        #endregion

        #region Cofins
        [TestMethod]
        public void Nao_Calcula_Cofins_Para_Tag_De_Nao_Tributacao()
        {
            Mock<ITributavel> tributavel = new Mock<ITributavel>(MockBehavior.Strict);
            Mock<ICstCalculoProvider> cstProvider = new Mock<ICstCalculoProvider>(MockBehavior.Strict);
            cstProvider.SetupGet<IList<string>>(i => i.CofinsCsts).Returns(new List<string>() { "05", "06", "01", "02" });

            tributavel.Setup(i => i.CofinsCST).Returns("08");
            tributavel.SetupSet(i => i.CofinsBaseCalculo = 0);
            tributavel.SetupSet(i => i.CofinsAliquota = 0);
            tributavel.SetupSet(i => i.CofinsValor = 0);

            Tributacao tributacao = new Tributacao(tributavel.Object);
            tributacao.CstCalculoProvider = cstProvider.Object;
            tributacao.CalculaCofins();

            tributavel.VerifyAll();
        }

        [TestMethod]
        public void Calcula_Cofins_Para_Tag_De_Tributacao()
        {
            Mock<ITributavel> tributavel = new Mock<ITributavel>(MockBehavior.Strict);
            Mock<ICstCalculoProvider> cstProvider = new Mock<ICstCalculoProvider>(MockBehavior.Strict);
            cstProvider.SetupGet<IList<string>>(i => i.CofinsCsts).Returns(new List<string>() { "05", "06", "01", "02" });

            tributavel.Setup(i => i.CofinsCST).Returns("01");
            tributavel.Setup(i => i.CofinsAliquota).Returns(15.69m);
            tributavel.SetupGet(i => i.CofinsBaseCalculo).Returns(2545.99m);

            tributavel.SetupSet(i => i.CofinsValor = 399.47m);

            Tributacao tributacao = new Tributacao(tributavel.Object);
            tributacao.CstCalculoProvider = cstProvider.Object;
            tributacao.CalculaCofins();

            tributavel.VerifyAll();
        }

        #endregion

        #region Icms Sem Substituição
        [TestMethod]
        public void Calcula_Icms_Para_Empresa_Simples_Com_ST_Substituido_Deve_Gerar_Icms_Outros()
        {
            Mock<ITributavel> tributavel = new Mock<ITributavel>(MockBehavior.Strict);
            Mock<ICstCalculoProvider> cstProvider = new Mock<ICstCalculoProvider>(MockBehavior.Strict);

            tributavel.Setup(i => i.EmpresaST).Returns(EmpresaST.Simples);
            tributavel.Setup(i => i.EhSubstitutoTributario).Returns(false);
            tributavel.Setup(i => i.BaseCalculoGeral).Returns(85546.66m);
            tributavel.Setup(i => i.EhICMSProprio).Returns(false);

            tributavel.SetupSet(i => i.IcmsAliquota = 0);
            tributavel.SetupSet(i => i.IcmsBaseCalculo = 0);
            tributavel.SetupSet(i => i.IcmsReducaoBaseCalculo = 0);
            tributavel.SetupSet(i => i.IcmsValor = 0);
            tributavel.SetupSet(i => i.IcmsValorIsento = 0);
            tributavel.SetupSet(i => i.IcmsValorOutros = 85546.66m);
            tributavel.SetupSet(i => i.IcmsValorRetido = 0);
            tributavel.SetupSet(i => i.IcmsSTBaseCalculo = 0);
            tributavel.SetupSet(i => i.IcmsSTAliquota = 0);
            tributavel.SetupSet(i => i.IcmsSTMargemValorAgregado = 0);

            Tributacao tributacao = new Tributacao(tributavel.Object);
            tributacao.CstCalculoProvider = cstProvider.Object;
            tributacao.CalculaIcms();

            tributavel.VerifyAll();
        }

        [TestMethod]
        public void Calcula_Icms_Para_Empresa_Normal_Com_ST_Substituido_Com_Cfop_Isento_Deve_Gerar_Icms_Isento()
        {
            Mock<ITributavel> tributavel = new Mock<ITributavel>(MockBehavior.Strict);
            Mock<ICstCalculoProvider> cstProvider = new Mock<ICstCalculoProvider>(MockBehavior.Strict);

            tributavel.Setup(i => i.EmpresaST).Returns(EmpresaST.Normal);
            tributavel.Setup(i => i.EhSubstitutoTributario).Returns(false);
            tributavel.Setup(i => i.EhCfopTributavelEmIcms).Returns(false);
            tributavel.Setup(i => i.IcmsCST).Returns("40");
            tributavel.Setup(i => i.BaseCalculoGeral).Returns(656498.96m);
            tributavel.Setup(i => i.EhClienteOrgaoPublico).Returns(false);

            tributavel.SetupSet(i => i.IcmsBaseCalculo = 0);
            tributavel.SetupSet(i => i.IcmsReducaoBaseCalculo = 0);
            tributavel.SetupSet(i => i.IcmsValor = 0);
            tributavel.SetupSet(i => i.IcmsValorIsento = 656498.96m);
            tributavel.SetupSet(i => i.IcmsValorOutros = 0);
            tributavel.SetupSet(i => i.IcmsValorRetido = 0);
            tributavel.SetupSet(i => i.IcmsSTBaseCalculo = 0);
            tributavel.SetupSet(i => i.IcmsSTAliquota = 0);
            tributavel.SetupSet(i => i.IcmsSTMargemValorAgregado = 0);


            Tributacao tributacao = new Tributacao(tributavel.Object);
            tributacao.CstCalculoProvider = cstProvider.Object;
            tributacao.CalculaIcms();

            tributavel.VerifyAll();
        }

        [TestMethod]
        public void Calcula_Icms_Para_Empresa_Normal_Com_ST_Substituido_Com_Cfop_Tributavel_Mas_Com_Cst_Isento_Deve_Gerar_Icms_Isento()
        {
            Mock<ITributavel> tributavel = new Mock<ITributavel>(MockBehavior.Strict);
            Mock<ICstCalculoProvider> cstProvider = new Mock<ICstCalculoProvider>(MockBehavior.Strict);

            tributavel.Setup(i => i.EmpresaST).Returns(EmpresaST.Normal);
            tributavel.Setup(i => i.EhSubstitutoTributario).Returns(false);
            tributavel.Setup(i => i.EhCfopTributavelEmIcms).Returns(true);
            tributavel.Setup(i => i.BaseCalculoGeral).Returns(656498.96m);
            tributavel.Setup(i => i.IcmsCST).Returns("40");
            cstProvider.Setup(i => i.IcmsCsts).Returns(new List<string>() { "10", "20", "30" });

            tributavel.SetupSet(i => i.IcmsBaseCalculo = 0);
            tributavel.SetupSet(i => i.IcmsReducaoBaseCalculo = 0);
            tributavel.SetupSet(i => i.IcmsValor = 0);
            tributavel.SetupSet(i => i.IcmsValorIsento = 656498.96m);
            tributavel.SetupSet(i => i.IcmsValorOutros = 0);
            tributavel.SetupSet(i => i.IcmsValorRetido = 0);
            tributavel.SetupSet(i => i.IcmsSTBaseCalculo = 0);
            tributavel.SetupSet(i => i.IcmsSTAliquota = 0);
            tributavel.SetupSet(i => i.IcmsSTMargemValorAgregado = 0);

            tributavel.SetupGet(i => i.EhClienteOrgaoPublico).Returns(false);

            Tributacao tributacao = new Tributacao(tributavel.Object);
            tributacao.CstCalculoProvider = cstProvider.Object;
            tributacao.CalculaIcms();

            tributavel.VerifyAll();
        }

        [TestMethod]
        public void Calcula_Icms_Para_Empresa_Normal_Com_ST_Substituido_Com_Cfop_Tributavel_E_Cst_Tributavel_Sem_RedBC_Deve_Calcular_Icms()
        {
            Mock<ITributavel> tributavel = new Mock<ITributavel>(MockBehavior.Strict);
            Mock<ICstCalculoProvider> cstProvider = new Mock<ICstCalculoProvider>(MockBehavior.Strict);

            tributavel.Setup(i => i.EmpresaST).Returns(EmpresaST.Normal);
            tributavel.Setup(i => i.EhSubstitutoTributario).Returns(false);
            tributavel.Setup(i => i.EhCfopTributavelEmIcms).Returns(true);
            tributavel.Setup(i => i.BaseCalculoGeral).Returns(656498.96m);
            tributavel.Setup(i => i.IcmsCST).Returns("10");
            tributavel.SetupGet(i => i.IcmsAliquota).Returns(10.2m);
            tributavel.SetupGet(i => i.IcmsAliquotaNormal).Returns(10.2m);
            tributavel.SetupGet(i => i.IcmsTextoLei).Returns("Texto lei de teste.");
            
            cstProvider.Setup(i => i.IcmsCsts).Returns(new List<string>() { "10", "20", "30" });

            tributavel.SetupSet(i => i.IcmsBaseCalculo = 656498.96m);
            tributavel.SetupSet(i => i.IcmsValor = 66962.89m);
            tributavel.SetupGet(i => i.IcmsValor).Returns(66962.89m);
            tributavel.SetupSet(i => i.IcmsValorIsento = 0);
            tributavel.SetupSet(i => i.IcmsValorOutros = 0);
            tributavel.SetupSet(i => i.IcmsValorDiferencaComReducao = 0);
            tributavel.SetupSet(i => i.IcmsValorRetido = 0);
            tributavel.SetupSet(i => i.IcmsSTBaseCalculo = 0);
            tributavel.SetupSet(i => i.IcmsSTAliquota = 0);
            tributavel.SetupSet(i => i.IcmsSTMargemValorAgregado = 0);

            tributavel.SetupSet(i => i.IcmsTextoLei = "Texto lei de teste.");
            tributavel.SetupSet(i => i.IcmsValorOutros = 0);
            tributavel.SetupGet(i => i.IcmsBaseCalculo).Returns(656498.96m);
            tributavel.SetupGet(i => i.IcmsValorOutros).Returns(0);
            tributavel.SetupGet(i => i.IcmsReducaoBaseCalculo).Returns(0);
            tributavel.SetupGet(i => i.EhClienteOrgaoPublico).Returns(false);

            Tributacao tributacao = new Tributacao(tributavel.Object);
            tributacao.CstCalculoProvider = cstProvider.Object;
            tributacao.CalculaIcms();

            tributavel.VerifyAll();
        }

        [TestMethod]
        public void Calcula_Icms_Para_Empresa_Normal_Com_ST_Substituido_Com_Cfop_Tributavel_E_Cst_Tributavel_Com_RedBC_Deve_Calcular_Icms()
        {
            Mock<ITributavel> tributavel = new Mock<ITributavel>(MockBehavior.Strict);
            Mock<ICstCalculoProvider> cstProvider = new Mock<ICstCalculoProvider>(MockBehavior.Strict);

            tributavel.Setup(i => i.EmpresaST).Returns(EmpresaST.Normal);
            tributavel.Setup(i => i.EhSubstitutoTributario).Returns(false);
            tributavel.Setup(i => i.EhCfopTributavelEmIcms).Returns(true);
            tributavel.Setup(i => i.IcmsCST).Returns("10");
            tributavel.SetupGet(i => i.IcmsAliquota).Returns(10.2m);
            tributavel.SetupGet(i => i.IcmsAliquotaNormal).Returns(10.2m);
            tributavel.SetupGet(i => i.IcmsReducaoBaseCalculo).Returns(2.35m);
            tributavel.SetupGet(i => i.IcmsTextoLei).Returns("Texto lei de teste.");
            tributavel.SetupGet(i => i.EhClienteOrgaoPublico).Returns(false);

            cstProvider.Setup(i => i.IcmsCsts).Returns(new List<string>() { "10", "20", "30" });

            tributavel.SetupGet(i => i.BaseCalculoGeral).Returns(656498.96m);
            tributavel.SetupGet(i => i.IcmsBaseCalculo).Returns(656498.96m);
            tributavel.SetupSet(i => i.IcmsBaseCalculo = 641071.23m);
            tributavel.SetupSet(i => i.IcmsValor = 65389.27m);
            tributavel.SetupGet(i => i.IcmsValor).Returns(65389.27m);
            tributavel.SetupSet(i => i.IcmsValorIsento = 0);
            tributavel.SetupGet(i => i.IcmsValorOutros).Returns(0);
            tributavel.SetupSet(i => i.IcmsValorOutros = 15427.73m);
            tributavel.SetupSet(i => i.IcmsValorDiferencaComReducao = 1573.62m);
            tributavel.SetupSet(i => i.IcmsValorRetido = 0);
            tributavel.SetupSet(i => i.IcmsSTBaseCalculo = 0);
            tributavel.SetupSet(i => i.IcmsSTAliquota = 0);
            tributavel.SetupSet(i => i.IcmsSTMargemValorAgregado = 0);
            tributavel.SetupSet(i => i.IcmsTextoLei = "Texto lei de teste.");

            Tributacao tributacao = new Tributacao(tributavel.Object);
            tributacao.CstCalculoProvider = cstProvider.Object;
            tributacao.CalculaIcms();

            tributavel.VerifyAll();
        }

        [TestMethod]
        public void Calcula_Icms_Para_Empresa_Normal_Com_ST_Substituido_Com_Cfop_Tributavel_E_Cst_Tributavel_Com_RedBC_Com_TextoLei_Deve_Calcular_Icms_E_Alterar_TextoLei()
        {
            Mock<ITributavel> tributavel = new Mock<ITributavel>(MockBehavior.Loose);
            Mock<ICstCalculoProvider> cstProvider = new Mock<ICstCalculoProvider>(MockBehavior.Strict);

            tributavel.Setup(i => i.EmpresaST).Returns(EmpresaST.Normal);
            tributavel.Setup(i => i.EhSubstitutoTributario).Returns(false);
            tributavel.Setup(i => i.EhCfopTributavelEmIcms).Returns(true);
            tributavel.Setup(i => i.BaseCalculoGeral).Returns(656498.96m);
            tributavel.Setup(i => i.IcmsCST).Returns("10");
            tributavel.SetupGet(i => i.IcmsAliquota).Returns(10.2m);
            tributavel.SetupGet(i => i.IcmsAliquotaNormal).Returns(10.2m);
            tributavel.SetupGet(i => i.IcmsReducaoBaseCalculo).Returns(2.35m);
            tributavel.SetupGet(i => i.IcmsTextoLei).Returns("Texto lei de teste com valor de *valor");
            tributavel.SetupGet(i => i.EhClienteOrgaoPublico).Returns(false);
            tributavel.SetupGet(i => i.IcmsBaseCalculo).Returns(0);
            cstProvider.Setup(i => i.IcmsCsts).Returns(new List<string>() { "10", "20", "30" });

            tributavel.SetupGet(i => i.IcmsBaseCalculo).Returns(656498.96m);
            tributavel.SetupSet(i => i.IcmsValor = 65389.27m);
            tributavel.SetupGet(i => i.IcmsValor).Returns(65389.27m);
            tributavel.SetupSet(i => i.IcmsValorIsento = 0);
            tributavel.SetupSet(i => i.IcmsValorRetido = 0);
            tributavel.SetupSet(i => i.IcmsValorOutros = 15427.73m);
            tributavel.SetupSet(i => i.IcmsValorDiferencaComReducao = 1573.62m);
            tributavel.SetupGet(i => i.IcmsValorDiferencaComReducao).Returns(1573.62m);
            tributavel.SetupSet(i => i.IcmsTextoLei = "Texto lei de teste com valor de R$ 1.573,62");
            tributavel.SetupSet(i => i.IcmsValorRetido = 0);
            tributavel.SetupSet(i => i.IcmsSTBaseCalculo = 0);
            tributavel.SetupSet(i => i.IcmsSTAliquota = 0);
            tributavel.SetupSet(i => i.IcmsSTMargemValorAgregado = 0);

            Tributacao tributacao = new Tributacao(tributavel.Object);
            tributacao.CstCalculoProvider = cstProvider.Object;
            tributacao.CalculaIcms();

            tributavel.VerifyAll();
        }

        [TestMethod]
        public void Calcula_Icms_Para_Empresa_Simples_Com_ST_Substituto_Mas_Com_CSTICMS_NaoSubstituivel_Nao_Deve_Calcular_Substituicao()
        {
            Mock<ITributavel> tributavel = new Mock<ITributavel>(MockBehavior.Strict);
            Mock<ICstCalculoProvider> cstProvider = new Mock<ICstCalculoProvider>(MockBehavior.Strict);

            tributavel.Setup(i => i.EmpresaST).Returns(EmpresaST.Simples);
            tributavel.Setup(i => i.EhSubstitutoTributario).Returns(true);
            tributavel.Setup(i => i.BaseCalculoGeral).Returns(656498.96m);
            tributavel.Setup(i => i.EhICMSProprio).Returns(false);

            tributavel.Setup(i => i.IcmsCST).Returns("40");
            cstProvider.Setup(i => i.SubstituicaoTributariaCsts).Returns(new List<string>() { "10", "20", "30" });

            tributavel.SetupSet(i => i.IcmsAliquota = 0);
            tributavel.SetupSet(i => i.IcmsBaseCalculo = 0);
            tributavel.SetupSet(i => i.IcmsReducaoBaseCalculo = 0);
            tributavel.SetupSet(i => i.IcmsValor = 0);
            tributavel.SetupSet(i => i.IcmsValorIsento = 0);
            tributavel.SetupSet(i => i.IcmsValorOutros = 656498.96m);
            tributavel.SetupSet(i => i.IcmsValorRetido = 0);
            tributavel.SetupSet(i => i.IcmsSTBaseCalculo = 0);
            tributavel.SetupSet(i => i.IcmsSTAliquota = 0);
            tributavel.SetupSet(i => i.IcmsSTMargemValorAgregado = 0);

            Tributacao tributacao = new Tributacao(tributavel.Object);
            tributacao.CstCalculoProvider = cstProvider.Object;
            tributacao.CalculaIcms();

            tributavel.VerifyAll();
        }

        #endregion

        #region Icms com Substituição

        [TestMethod]
        public void Calcula_Icms_Para_Empresa_Simples_Com_ST_Substituto_Deve_Calcular_Substituicao()
        {
            Mock<ITributavel> tributavel = new Mock<ITributavel>(MockBehavior.Strict);
            Mock<ICstCalculoProvider> cstProvider = new Mock<ICstCalculoProvider>(MockBehavior.Strict);

            tributavel.Setup(i => i.EmpresaST).Returns(EmpresaST.Simples);
            tributavel.Setup(i => i.EhSubstitutoTributario).Returns(true);
            tributavel.Setup(i => i.BaseCalculoGeral).Returns(656498.96m);
            tributavel.Setup(i => i.EhICMSProprio).Returns(false);

            tributavel.SetupGet(i => i.IcmsSTReducaoBaseCalculo).Returns(3.56m);
            tributavel.SetupGet(i => i.IcmsSTMargemValorAgregado).Returns(12m);
            tributavel.SetupGet(i => i.IcmsSTAliquota).Returns(12m);
            tributavel.SetupGet(i => i.EmpresaAliquota).Returns(10m);
            tributavel.SetupGet(i => i.IcmsValor).Returns(0);

            tributavel.Setup(i => i.IcmsCST).Returns("10");
            cstProvider.Setup(i => i.SubstituicaoTributariaCsts).Returns(new List<string>() { "10", "20", "30" });

            tributavel.SetupSet(i => i.IcmsBaseCalculo = 0);
            tributavel.SetupSet(i => i.IcmsValor = 0);
            tributavel.SetupSet(i => i.IcmsValorIsento = 0);
            tributavel.SetupSet(i => i.IcmsAliquota = 0);
            tributavel.SetupSet(i => i.IcmsValorRetido = 0);
            tributavel.SetupSet(i => i.IcmsReducaoBaseCalculo = 0);
            tributavel.SetupSet(i => i.IcmsValorOutros = 656498.96m);
            tributavel.SetupSet(i => i.IcmsValorRetido = 19442.45m);
            tributavel.SetupSet(i => i.IcmsSTBaseCalculo = 709102.91m);

            Tributacao tributacao = new Tributacao(tributavel.Object);
            tributacao.CstCalculoProvider = cstProvider.Object;
            tributacao.CalculaIcms();

            tributavel.VerifyAll();
        }

        [TestMethod]
        public void Calcula_Icms_Para_Empresa_Normal_Com_ST_Substituto_Sem_Reducao_De_ICMS_Com_Icms_Isento_Deve_Calcular_Substituicao()
        {
            Mock<ITributavel> tributavel = new Mock<ITributavel>(MockBehavior.Strict);
            Mock<ICstCalculoProvider> cstProvider = new Mock<ICstCalculoProvider>(MockBehavior.Strict);

            tributavel.Setup(i => i.EmpresaST).Returns(EmpresaST.Normal);
            tributavel.Setup(i => i.EhSubstitutoTributario).Returns(true);
            tributavel.Setup(i => i.EhCfopTributavelEmIcms).Returns(false);
            tributavel.Setup(i => i.BaseCalculoGeral).Returns(656498.96m);

            tributavel.SetupGet(i => i.IcmsSTReducaoBaseCalculo).Returns(3.56m);
            tributavel.SetupGet(i => i.IcmsSTMargemValorAgregado).Returns(12m);
            tributavel.SetupGet(i => i.IcmsSTAliquota).Returns(12m);

            tributavel.Setup(i => i.IcmsCST).Returns("10");
            cstProvider.Setup(i => i.SubstituicaoTributariaCsts).Returns(new List<string>() { "10", "20", "30" });
            cstProvider.Setup(i => i.IcmsCsts).Returns(new List<string>() { "40" });

            tributavel.SetupSet(i => i.IcmsBaseCalculo = 0);
            tributavel.SetupSet(i => i.IcmsValor = 0);
            tributavel.SetupGet(i => i.IcmsValor).Returns(0);
            tributavel.SetupSet(i => i.IcmsValorIsento = 656498.96m);
            tributavel.SetupSet(i => i.IcmsValorRetido = 0);
            tributavel.SetupSet(i => i.IcmsReducaoBaseCalculo = 0);
            tributavel.SetupSet(i => i.IcmsValorOutros = 0);
            tributavel.SetupSet(i => i.IcmsValorRetido = 85092.35m);
            tributavel.SetupSet(i => i.IcmsSTBaseCalculo = 709102.91m);
            tributavel.SetupGet(i => i.EhClienteOrgaoPublico).Returns(false);

            Tributacao tributacao = new Tributacao(tributavel.Object);
            tributacao.CstCalculoProvider = cstProvider.Object;
            tributacao.CalculaIcms();

            tributavel.VerifyAll();
        }

        [TestMethod]
        public void Calcula_Icms_Para_Empresa_Normal_Com_ST_Substituto_Sem_Reducao_De_ICMS_Deve_Calcular_Substituicao()
        {
            Mock<ITributavel> tributavel = new Mock<ITributavel>(MockBehavior.Strict);
            Mock<ICstCalculoProvider> cstProvider = new Mock<ICstCalculoProvider>(MockBehavior.Strict);

            tributavel.Setup(i => i.EmpresaST).Returns(EmpresaST.Normal);
            tributavel.Setup(i => i.EhSubstitutoTributario).Returns(true);
            tributavel.Setup(i => i.EhCfopTributavelEmIcms).Returns(true);
            tributavel.Setup(i => i.BaseCalculoGeral).Returns(656498.96m);
            tributavel.Setup(i => i.IcmsCST).Returns("10");
            tributavel.SetupGet(i => i.IcmsAliquota).Returns(10.2m);
            tributavel.SetupGet(i => i.IcmsAliquotaNormal).Returns(10.2m);
            tributavel.SetupGet(i => i.IcmsReducaoBaseCalculo).Returns(0);
            tributavel.SetupGet(i => i.IcmsTextoLei).Returns("Texto lei de teste.");
            tributavel.SetupGet(i => i.IcmsSTReducaoBaseCalculo).Returns(3.56m);
            tributavel.SetupGet(i => i.IcmsSTMargemValorAgregado).Returns(12m);
            tributavel.SetupGet(i => i.IcmsSTAliquota).Returns(12m);

            cstProvider.Setup(i => i.IcmsCsts).Returns(new List<string>() { "10", "20", "30" });
            cstProvider.Setup(i => i.SubstituicaoTributariaCsts).Returns(new List<string>() { "10" });

            tributavel.SetupSet(i => i.IcmsBaseCalculo = 656498.96m);
            tributavel.SetupSet(i => i.IcmsValor = 66962.89m);
            tributavel.SetupGet(i => i.IcmsValor).Returns(66962.89m);
            tributavel.SetupSet(i => i.IcmsValorIsento = 0);
            tributavel.SetupSet(i => i.IcmsValorOutros = 0);
            tributavel.SetupSet(i => i.IcmsValorDiferencaComReducao = 0);
            tributavel.SetupSet(i => i.IcmsValorRetido = 18129.46m);
            tributavel.SetupSet(i => i.IcmsSTBaseCalculo = 709102.91m);

                        tributavel.SetupSet(i => i.IcmsTextoLei = "Texto lei de teste.");
            //            tributavel.SetupSet(i => i.IcmsValorOutros = 0);
                        tributavel.SetupGet(i => i.IcmsBaseCalculo).Returns(656498.96m);
                        tributavel.SetupGet(i => i.IcmsValorOutros).Returns(0);
            //            tributavel.SetupGet(i => i.IcmsReducaoBaseCalculo).Returns(0);
            tributavel.SetupGet(i => i.EhClienteOrgaoPublico).Returns(false);

            Tributacao tributacao = new Tributacao(tributavel.Object);
            tributacao.CstCalculoProvider = cstProvider.Object;
            tributacao.CalculaIcms();

            tributavel.VerifyAll();
        }

        [TestMethod]
        public void Calcula_Icms_Para_Empresa_Normal_Com_ST_Substituto_Com_Reducao_De_ICMS_Deve_Calcular_Substituicao()
        {
            Mock<ITributavel> tributavel = new Mock<ITributavel>(MockBehavior.Strict);
            Mock<ICstCalculoProvider> cstProvider = new Mock<ICstCalculoProvider>(MockBehavior.Strict);

            tributavel.Setup(i => i.EmpresaST).Returns(EmpresaST.Normal);
            tributavel.Setup(i => i.EhSubstitutoTributario).Returns(true);
            tributavel.Setup(i => i.EhCfopTributavelEmIcms).Returns(true);
            tributavel.Setup(i => i.BaseCalculoGeral).Returns(656498.96m);
            tributavel.Setup(i => i.IcmsCST).Returns("10");
            tributavel.SetupGet(i => i.IcmsAliquota).Returns(10.2m);
            tributavel.SetupGet(i => i.IcmsAliquotaNormal).Returns(10.2m);
            tributavel.SetupGet(i => i.IcmsReducaoBaseCalculo).Returns(2.35m);
            tributavel.SetupGet(i => i.IcmsTextoLei).Returns("Texto lei de teste.");
            tributavel.SetupGet(i => i.IcmsSTReducaoBaseCalculo).Returns(3.56m);
            tributavel.SetupGet(i => i.IcmsSTMargemValorAgregado).Returns(12m);
            tributavel.SetupGet(i => i.IcmsSTAliquota).Returns(12m);

            cstProvider.Setup(i => i.IcmsCsts).Returns(new List<string>() { "10", "20", "30" });
            cstProvider.Setup(i => i.SubstituicaoTributariaCsts).Returns(new List<string>() { "10" });

            tributavel.SetupSet(i => i.IcmsBaseCalculo = 641071.23m);
            tributavel.SetupSet(i => i.IcmsValor = 65389.27m);
            tributavel.SetupGet(i => i.IcmsValor).Returns(65389.27m);
            tributavel.SetupSet(i => i.IcmsValorIsento = 0);
            tributavel.SetupSet(i => i.IcmsValorOutros = 15427.73m);
            tributavel.SetupSet(i => i.IcmsValorDiferencaComReducao = 1573.62m);
            tributavel.SetupSet(i => i.IcmsValorRetido = 19703.08m);
            tributavel.SetupSet(i => i.IcmsSTBaseCalculo = 709102.91m);

            tributavel.SetupSet(i => i.IcmsTextoLei = "Texto lei de teste.");
            tributavel.SetupGet(i => i.IcmsBaseCalculo).Returns(656498.96m);
            tributavel.SetupGet(i => i.IcmsValorOutros).Returns(0);
            tributavel.SetupGet(i => i.EhClienteOrgaoPublico).Returns(false);

            Tributacao tributacao = new Tributacao(tributavel.Object);
            tributacao.CstCalculoProvider = cstProvider.Object;
            tributacao.CalculaIcms();

            tributavel.VerifyAll();
        }


        #endregion

        #region CreditoICMS

        [TestMethod]
        public void Nao_Calcula_Credito_Para_Empresa_Normal()
        {
            Mock<ITributavel> tributavel = new Mock<ITributavel>();
            Mock<ICstCalculoProvider> cstProvider = new Mock<ICstCalculoProvider>();

            tributavel.Setup(i => i.EmpresaST).Returns(EmpresaST.Normal);

            tributavel.SetupSet(i => i.CreditoPercentual = 0);
            tributavel.SetupSet(i => i.CreditoReducaoBasePercentual = 0);
            tributavel.SetupSet(i => i.CreditoValor = 0);

            Tributacao tributacao = new Tributacao(tributavel.Object);
            tributacao.CalculaCredito();

            tributavel.VerifyAll();
        }

        [TestMethod]
        public void Nao_Calcula_Credito_Para_Empresa_Simples_Com_Cst_Nao_Creditavel()
        {
            Mock<ITributavel> tributavel = new Mock<ITributavel>();
            Mock<ICstCalculoProvider> cstProvider = new Mock<ICstCalculoProvider>();

            tributavel.Setup(i => i.EmpresaST).Returns(EmpresaST.Simples);
            tributavel.Setup(i => i.IcmsCST).Returns("10");
            cstProvider.Setup(i => i.CreditoCsts).Returns(new List<string>() { "20", "30" });

            tributavel.SetupSet(i => i.CreditoPercentual = 0);
            tributavel.SetupSet(i => i.CreditoReducaoBasePercentual = 0);
            tributavel.SetupSet(i => i.CreditoValor = 0);

            Tributacao tributacao = new Tributacao(tributavel.Object);
            tributacao.CstCalculoProvider = cstProvider.Object;
            tributacao.CalculaCredito();

            tributavel.VerifyAll();
        }

        [TestMethod]
        public void Calcula_Credito_Para_Empresa_Simples_Com_Cst_Creditavel()
        {
            Mock<ITributavel> tributavel = new Mock<ITributavel>();
            Mock<ICstCalculoProvider> cstProvider = new Mock<ICstCalculoProvider>();

            tributavel.Setup(i => i.EmpresaST).Returns(EmpresaST.Simples);
            tributavel.Setup(i => i.IcmsCST).Returns("20");
            tributavel.Setup(i => i.BaseCalculoGeral).Returns(568485.66m);
            tributavel.Setup(i => i.CreditoPercentual).Returns(15.96m);
            tributavel.Setup(i => i.CreditoReducaoBasePercentual).Returns(55.13m);

            cstProvider.Setup(i => i.CreditoCsts).Returns(new List<string>() { "20", "30" });

            tributavel.SetupSet(i => i.CreditoPercentualReduzido = 7.16m);
            tributavel.SetupSet(i => i.CreditoValor = 40703.57m);

            Tributacao tributacao = new Tributacao(tributavel.Object);
            tributacao.CstCalculoProvider = cstProvider.Object;
            tributacao.CalculaCredito();

            tributavel.VerifyAll();
        }
        #endregion
    }
}
