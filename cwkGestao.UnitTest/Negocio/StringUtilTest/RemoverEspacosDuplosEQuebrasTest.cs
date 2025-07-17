using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cwkGestao.Modelo.Util;

namespace cwkGestao.UnitTest.StringUtilTest
{
    [TestClass]
    public class RemoverEspacosDuplosEQuebrasTest
    {
        [TestMethod]
        public void String_Nula_Retorna_String_Vazia()
        { 
            string input = null;
            string retorno = input.RemoverEspacosDuplosEQuebras();

            Assert.AreEqual(String.Empty, retorno);
        }

        [TestMethod]
        public void String_Com_Multiplos_Espacos_Duplos_Retorna_String_Sem_Espacos_Duplos()
        {
            string input = "  tete   do   jose     muitos  espacos duplos      :)  ";
            string retorno = input.RemoverEspacosDuplosEQuebras();
            string esperado = "tete do jose muitos espacos duplos :)";
            Assert.AreEqual(esperado, retorno);
        }

        [TestMethod]
        public void String_Com_Tab_e_Enter_Substitui_Por_Espacos()
        {
            string input = @"   tete do jose
                                muitos  espacos duplos  :)
                            asd
                            ";
            string retorno = input.RemoverEspacosDuplosEQuebras();
            string esperado = "tete do jose muitos espacos duplos :) asd";
            Assert.AreEqual(esperado, retorno);
        }
    }
}
