using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Negocio;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace cwkGestao.UnitTest.AcrescimoTest
{
    [TestClass]
    public class CaculoMulta
    {
        [TestMethod]
        public void Documento_Nao_Vencido_Nao_Gera_Multa()
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

            decimal valorJuros = AcrescimoController.Instancia.CalculaMulta(acrescimo, 100, dataBase, vencimento);

            Assert.AreEqual(0m, valorJuros);
        }

        [TestMethod]
        public void Documento_No_Vencimento_Nao_Gera_Multa()
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

            decimal valorJuros = AcrescimoController.Instancia.CalculaMulta(acrescimo, 100, dataBase, vencimento);

            Assert.AreEqual(0m, valorJuros);
        }

        [TestMethod]
        public void Documento_Vencido_Mas_Dentro_Da_Tolerancia_Nao_Gera_Multa()
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

            decimal valorJuros = AcrescimoController.Instancia.CalculaMulta(acrescimo, 100, dataBase, vencimento);

            Assert.AreEqual(0m, valorJuros);
        }

        [TestMethod]
        public void Documento_Vencido_Fora_Tolerancia_Gera_Multa()
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

            decimal valorJuros = AcrescimoController.Instancia.CalculaMulta(acrescimo, 100, dataBase, vencimento);

            Assert.AreEqual(4.25m, valorJuros);
        }

        [TestMethod]
        public void Documento_Vencido_Fora_Tolerancia_Gera_Multa2()
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

            decimal valorJuros = AcrescimoController.Instancia.CalculaMulta(acrescimo, 100, dataBase, vencimento);

            Assert.AreEqual(4.25m, valorJuros);
        }

        [TestMethod]
        public void Documento_Vencido_Fora_Tolerancia_Gera_Multa_Com_JurosMulta_Mas_Nao_Gera_Multa()
        {
            Acrescimo acrescimo = new Acrescimo
            {
                DiasJuro = 5,
                DiasMulta = 2,
                JurosMulta = true,
                PercJuro = 6.25m,
                PercMulta = 4.25m
            };
            DateTime dataBase = new DateTime(2000, 01, 06);
            DateTime vencimento = new DateTime(2000, 01, 03);

            decimal valorJuros = AcrescimoController.Instancia.CalculaMulta(acrescimo, 100, dataBase, vencimento);

            Assert.AreEqual(4.25m, valorJuros);
        }


        [TestMethod]
        public void Documento_Vencido_Fora_Tolerancia_Gera_Multa_Com_JurosMulta_E_Gera_Multa()
        {
            Acrescimo acrescimo = new Acrescimo
            {
                DiasJuro = 2,
                DiasMulta = 2,
                JurosMulta = true,
                PercJuro = 6.25m,
                PercMulta = 4.25m
            };
            DateTime dataBase = new DateTime(2000, 01, 06);
            DateTime vencimento = new DateTime(2000, 01, 03);

            decimal valorJuros = AcrescimoController.Instancia.CalculaMulta(acrescimo, 100, dataBase, vencimento);
            //Juros gerado = 0.62 - Base de calculo = 100.62
            Assert.AreEqual(4.28m, valorJuros);
        }

        [TestMethod]
        public void Documento_Vencido_Fora_Tolerancia_Gera_Multa_Com_JurosMulta_E_Gera_Multa2()
        {
            Acrescimo acrescimo = new Acrescimo
            {
                DiasJuro = 2,
                DiasMulta = 2,
                JurosMulta = true,
                PercJuro = 6.25m,
                PercMulta = 4.25m
            };
            DateTime dataBase = new DateTime(2000, 12, 06);
            DateTime vencimento = new DateTime(2000, 01, 03);

            decimal valorJuros = AcrescimoController.Instancia.CalculaMulta(acrescimo, 100, dataBase, vencimento);
            //Juros gerado = 70.42 - Base de calculo = 170.42
            Assert.AreEqual(7.24m, valorJuros);
        }
    }
}
