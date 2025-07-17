using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.Reflection;
using System.Xml.Linq;
using System.IO;
using MO = Modelo;

namespace cwkGestao.Negocio.Tributacao.Impl
{
    public class CstCalculoProviderFromXml : ICstCalculoProvider
    {
        private IList<string> pisCsts;
        private IList<string> ipiCsts;
        private IList<string> cofinsCsts;
        private IList<string> icmsCsts;
        private IList<string> substituicaoTributariaCsts;
        private IList<string> creditoCsts;

        public CstCalculoProviderFromXml()
        {
            pisCsts = new List<string>();
            ipiCsts = new List<string>();
            cofinsCsts = new List<string>();
            icmsCsts = new List<string>();
            substituicaoTributariaCsts = new List<string>();
            creditoCsts = new List<string>();
            
            XDocument configuracao = XDocument.Load(Path.Combine(MO.cwkGlobal.DirApp, "ConfiguracaoCwork.xml"));

            try
            {
                XElement nohCsts = (from nohCst in configuracao.Root.Elements() where nohCst.Name.LocalName == "calculaTributacao" select nohCst).Single();

                AdicionaValoresNaLista("pis", pisCsts, nohCsts);
                AdicionaValoresNaLista("ipi", ipiCsts, nohCsts);
                AdicionaValoresNaLista("cofins", cofinsCsts, nohCsts);
                AdicionaValoresNaLista("icms", icmsCsts, nohCsts);
                AdicionaValoresNaLista("substituicaoTributaria", substituicaoTributariaCsts, nohCsts);
                AdicionaValoresNaLista("credito", creditoCsts, nohCsts);
            }
            catch (Exception exc)
            {
                throw new Exception("Erro ao tentar localizar informações de cálculos na configuração do sistema.", exc);
            }
        }

        private void AdicionaValoresNaLista(string nomeNoh, IList<string> lista, XElement nohCstRaiz)
        {
            try
            {
                foreach (XElement item in (from nohPis in nohCstRaiz.Elements() where nohPis.Name.LocalName == nomeNoh select nohPis).Single().Elements())
                {
                    lista.Add(item.Value);
                }
            }
            catch (Exception exc)
            {
                throw new Exception("Não foi possível encontrar o elemento " + nomeNoh + " na configuração do sistema.", exc);
            }
        }

        public IList<string> PisCsts
        {
            get { return pisCsts; }
        }

        public IList<string> IpiCsts
        {
            get { return ipiCsts; }
        }

        public IList<string> CofinsCsts
        {
            get { return cofinsCsts; }
        }

        public IList<string> IcmsCsts
        {
            get { return icmsCsts; }
        }

        public IList<string> SubstituicaoTributariaCsts
        {
            get { return substituicaoTributariaCsts; }
        }

        public IList<string> CreditoCsts
        {
            get { return creditoCsts; }
        }
    }
}
