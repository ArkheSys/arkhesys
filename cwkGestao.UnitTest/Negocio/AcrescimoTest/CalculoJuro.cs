using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cwkGestao.Modelo;
using cwkGestao.Negocio;

namespace cwkGestao.UnitTest.AcrescimoTest
{
    [TestClass]
    public class CalculoJuro
    {
        [TestMethod]
        public void Documento_Nao_Vencido_Nao_Gera_Juro()
        {
            Acrescimo acrescimo = new Acrescimo
                                      {
                                          DiasJuro = 2,
                                          DiasMulta = 2,
                                          JurosMulta = false,
                                          PercJuro = 6.25m,
                                          PercMulta = 4.25m
                                      };
            DateTime dataBase = new DateTime(2000, 01, 01);
            DateTime vencimento = new DateTime(2000, 01, 03);

            decimal valorJuros = AcrescimoController.Instancia.CalculaJuros(acrescimo, 100, dataBase, vencimento);

            Assert.AreEqual(0m, valorJuros);
        }

        [TestMethod]
        public void Documento_No_Vencimento_Nao_Gera_Juro()
        {
            Acrescimo acrescimo = new Acrescimo
            {
                DiasJuro = 2,
                DiasMulta = 2,
                JurosMulta = false,
                PercJuro = 6.25m,
                PercMulta = 4.25m
            };
            DateTime dataBase = new DateTime(2000, 01, 03);
            DateTime vencimento = new DateTime(2000, 01, 03);

            decimal valorJuros = AcrescimoController.Instancia.CalculaJuros(acrescimo, 100, dataBase, vencimento);

            Assert.AreEqual(0m, valorJuros);
        }

        [TestMethod]
        public void Documento_Vencido_Mas_Dentro_Da_Tolerancia_Nao_Gera_Juro()
        {
            Acrescimo acrescimo = new Acrescimo
            {
                DiasJuro = 2,
                DiasMulta = 2,
                JurosMulta = false,
                PercJuro = 6.25m,
                PercMulta = 4.25m
            };
            DateTime dataBase = new DateTime(2000, 01, 05);
            DateTime vencimento = new DateTime(2000, 01, 03);

            decimal valorJuros = AcrescimoController.Instancia.CalculaJuros(acrescimo, 100, dataBase, vencimento);

            Assert.AreEqual(0m, valorJuros);
        }

        [TestMethod]
        public void Documento_Vencido_Sem_Tolerancia_Gera_Juro()
        {
            Acrescimo acrescimo = new Acrescimo
            {
                DiasJuro = 0,
                DiasMulta = 0,
                JurosMulta = false,
                PercJuro = 6.25m,
                PercMulta = 4.25m
            };
            DateTime dataBase = new DateTime(2000, 01, 04);
            DateTime vencimento = new DateTime(2000, 01, 03);

            decimal valorJuros = AcrescimoController.Instancia.CalculaJuros(acrescimo, 100, dataBase, vencimento);

            Assert.AreEqual(0.21m, valorJuros);
        }

        [TestMethod]
        public void Documento_Vencido_Sem_Tolerancia_Gera_Juro2()
        {
            Acrescimo acrescimo = new Acrescimo
            {
                DiasJuro = 0,
                DiasMulta = 0,
                JurosMulta = false,
                PercJuro = 6.25m,
                PercMulta = 4.25m
            };
            DateTime dataBase = new DateTime(2000, 01, 05);
            DateTime vencimento = new DateTime(2000, 01, 03);

            decimal valorJuros = AcrescimoController.Instancia.CalculaJuros(acrescimo, 100, dataBase, vencimento);

            Assert.AreEqual(0.42m, valorJuros);
        }

        [TestMethod]
        public void Documento_Vencido_Sem_Tolerancia_Gera_Juro3()
        {
            Acrescimo acrescimo = new Acrescimo
            {
                DiasJuro = 2,
                DiasMulta = 2,
                JurosMulta = false,
                PercJuro = 6.25m,
                PercMulta = 4.25m
            };
            DateTime dataBase = new DateTime(2000, 01, 06);
            DateTime vencimento = new DateTime(2000, 01, 03);

            decimal valorJuros = AcrescimoController.Instancia.CalculaJuros(acrescimo, 100, dataBase, vencimento);

            Assert.AreEqual(0.62m, valorJuros);
        }

        [TestMethod]
        public void Documento_Vencido_Sem_Tolerancia_Gera_Juro4()
        {
            Acrescimo acrescimo = new Acrescimo
            {
                DiasJuro = 2,
                DiasMulta = 2,
                JurosMulta = false,
                PercJuro = 6.25m,
                PercMulta = 4.25m
            };
            DateTime dataBase = new DateTime(2000, 12, 06);
            DateTime vencimento = new DateTime(2000, 01, 03);

            decimal valorJuros = AcrescimoController.Instancia.CalculaJuros(acrescimo, 100, dataBase, vencimento);

            Assert.AreEqual(70.42m, valorJuros);
        }

        [TestMethod]
        public void Nao_Gera_Juros_Quando_Percentual_Eh_Zero()
        {
            Acrescimo acrescimo = new Acrescimo
            {
                DiasJuro = 2,
                DiasMulta = 2,
                JurosMulta = false,
                PercJuro = 0m,
                PercMulta = 4.25m
            };
            DateTime dataBase = new DateTime(2000, 12, 06);
            DateTime vencimento = new DateTime(2000, 01, 03);

            decimal valorJuros = AcrescimoController.Instancia.CalculaJuros(acrescimo, 100, dataBase, vencimento);

            Assert.AreEqual(0m, valorJuros);
        }

        [TestMethod]
        public void Nao_Gera_Juros_Quando_Valor_Eh_Zero()
        {
            Acrescimo acrescimo = new Acrescimo
            {
                DiasJuro = 2,
                DiasMulta = 2,
                JurosMulta = false,
                PercJuro = 6.25m,
                PercMulta = 4.25m
            };
            DateTime dataBase = new DateTime(2000, 12, 06);
            DateTime vencimento = new DateTime(2000, 01, 03);

            decimal valorJuros = AcrescimoController.Instancia.CalculaJuros(acrescimo, 0, dataBase, vencimento);

            Assert.AreEqual(0m, valorJuros);
        }
    }
}
