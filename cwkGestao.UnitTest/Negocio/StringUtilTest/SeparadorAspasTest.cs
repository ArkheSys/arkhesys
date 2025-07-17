using System;
using System.Text;
using System.Collections.Generic;
using System.Linq;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using cwkGestao.Modelo.Util;

namespace cwkGestao.UnitTest.StringUtilTest
{
    [TestClass]
    public class SepadadorAspasTest
    {
        [TestMethod]
        public void Palavra_Sem_Aspa_Retorna_Lista_Vazia()
        { 
            string input = "uma palavra qualquer";
            IList<string> retorno = input.SepararAspas();

            Assert.AreEqual(0, retorno.Count);
        }

        [TestMethod]
        public void Palavra_Com_Um_Par_De_Aspa_Retorna_A_Palavra()
        {
            string input = "uma 'palavra' qualquer";
            IList<string> retorno = input.SepararAspas();

            IList<string> esperado = new List<string>();
            esperado.Add("palavra");
            
            Assert.IsTrue(esperado.SequenceEqual(retorno));
        }

        [TestMethod]
        public void Palavra_Com_Uma_Aspa_Retorna_Lista_Vazia()
        {
            string input = "uma 'palavra qualquer";
            IList<string> retorno = input.SepararAspas();

            Assert.AreEqual(0, retorno.Count);
        }

        [TestMethod]
        public void Palavra_Com_Aspa_No_Final_Retorna_Lista_Vazia()
        {
            string input = "uma palavra qualquer'";
            IList<string> retorno = input.SepararAspas();

            Assert.AreEqual(0, retorno.Count);
        }

        [TestMethod]
        public void Duas_Palavra_Entra_Aspas_Retorna_As_Duas()
        {
            string input = "uma 'palavra qualquer'";
            IList<string> retorno = input.SepararAspas();

            IList<string> esperado = new List<string>();
            esperado.Add("palavra qualquer");

            Assert.IsTrue(esperado.SequenceEqual(retorno));
        }

        [TestMethod]
        public void Dois_Pares_De_Aspas_Retorna_As_Duas_Sequencias()
        {
            string input = "uma 'frase qualquer' com mais 'palavras'";
            IList<string> retorno = input.SepararAspas();

            IList<string> esperado = new List<string>();
            esperado.Add("frase qualquer");
            esperado.Add("palavras");

            Assert.IsTrue(esperado.SequenceEqual(retorno));
        }

        [TestMethod]
        public void Um_Par_De_Aspas_Com_Uma_Avulsa_Retorna_Somente_Uma()
        {
            string input = "uma 'frase qualquer' com mais 'palavras";
            IList<string> retorno = input.SepararAspas();

            IList<string> esperado = new List<string>();
            esperado.Add("frase qualquer");

            Assert.IsTrue(esperado.SequenceEqual(retorno));
        }


    }
}
