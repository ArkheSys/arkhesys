using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cwkGestao.Modelo.Util;

namespace cwkGestao.UnitTest.StringUtilTest
{
    [TestClass]
    public class GetNoMaximoXCaracteresTest
    {
        [TestMethod]
        public void String_Null_Retorna_String_Vazia()
        {
            string valor = null;
            Assert.AreEqual(String.Empty, valor.GetNoMaximoXCaracteres(10));
        }

        [TestMethod]
        public void String_Maior_Que_X_Retorna_X_Caracteres()
        {
            string valor = "01234567890123456789";
            int x = 10;
            Assert.AreEqual(valor.Substring(0, x), valor.GetNoMaximoXCaracteres(x));
        }

        [TestMethod]
        public void String_Menor_Que_X_Retorna_Propria_String()
        {
            string valor = "01234567890123456789";
            int x = 30;
            Assert.AreEqual(valor, valor.GetNoMaximoXCaracteres(x));
        }
    }
}
