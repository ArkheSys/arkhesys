using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System.Xml.Linq;
using System.IO;
using cwkGestao.Negocio.Componente_NFe;
using cwkNotaFiscalEletronica;
using NFe.Utils;

namespace cwkGestao.Negocio
{
    public class NotaInutilizadaController : BaseController<NotaInutilizada>
    {
        #region Singleton
        private static NotaInutilizadaController instance;
        private static IRepositorioNotaInutilizada repositorioNotaInutilizada;

        private NotaInutilizadaController()
        { }

        static NotaInutilizadaController()
        {
            instance = new NotaInutilizadaController();
            repositorioT = RepositorioFactory<NotaInutilizada>.GetRepositorio();
            repositorioNotaInutilizada = (IRepositorioNotaInutilizada)repositorioT;
        }

        public static NotaInutilizadaController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(NotaInutilizada objeto)
        {
            return new Dictionary<string, string>();
        }

        public IList<NotaInutilizada> GetAllByFilial(int idFilial)
        {
            return repositorioNotaInutilizada.GetAllByFilial(idFilial);
        }



        public string InutilizaFaixaNFe(Filial pFilial, string pAno, string pCnpj, string pSerie, string pNumIni, string pNumFim, string pJustificativa)
        {
            string aXmlNota;

            if (pFilial.FormaEmissao != 1)
            {
                throw new Exception("Para Inutilizar Faixa de NFe a Forma de Emissão de ser igual a \"1 | Normal\"");
            }

            ConfiguracaoServico configuracoesDFe = FactoryNFeZeus.Build(pFilial, 55, AppDomain.CurrentDomain.BaseDirectory);

            NFeFacade facade = new NFeFacade(pFilial.DiretorioPadraoNFe);

            aXmlNota = facade.InutilizarNumeracao(Convert.ToInt32(pAno), pCnpj, pJustificativa, Convert.ToInt32(pNumIni), Convert.ToInt32(pNumFim), Convert.ToInt32(pSerie), configuracoesDFe).RetornoCompletoStr;

            XDocument documentoXml = XDocument.Load(new StreamReader(new MemoryStream(ASCIIEncoding.Default.GetBytes(aXmlNota))));
            var noh = (from c in documentoXml.Root.Elements() where c.Name.LocalName == "infInut" select c).Single<XElement>();

            string valorRetorno = (from c in noh.Elements() where c.Name.LocalName == "cStat" select c).Single<XElement>().Value;
            if (valorRetorno == "102")
            {
                return valorRetorno;
            }
            else
            {
                string motivoErro = (from c in noh.Elements() where c.Name.LocalName == "xMotivo" select c).Single<XElement>().Value;
                return motivoErro;
            }
        }

        public string InutilizaFaixaNFCe(Filial pFilial, string pAno, string pCnpj, string pSerie, string pNumIni, string pNumFim, string pJustificativa)
        {
            if (pFilial.FormaEmissao != 1)
            {
                throw new Exception("Para Inutilizar Faixa de NFe a Forma de Emissão de ser igual a \"1 | Normal\"");
            }

            if (pFilial.ComponenteDfe == 0)
            {
                throw new Exception("Função não implementada para este componente.");
            }

            var configuracoesDFe = FactoryNFeZeus.Build(pFilial, 65, AppDomain.CurrentDomain.BaseDirectory);
            var facade = new NFeFacade(pFilial.DiretorioPadraoNFe);
            var aXmlNota = facade.InutilizarNumeracao(Convert.ToInt32(pAno), pCnpj, pJustificativa, Convert.ToInt32(pNumIni), Convert.ToInt32(pNumFim), Convert.ToInt32(pSerie), configuracoesDFe).RetornoCompletoStr;

            var documentoXml = XDocument.Load(new StreamReader(new MemoryStream(ASCIIEncoding.Default.GetBytes(aXmlNota))));
            var noh = (from c in documentoXml.Root.Elements() where c.Name.LocalName == "infInut" select c).Single<XElement>();

            string valorRetorno = (from c in noh.Elements() where c.Name.LocalName == "cStat" select c).Single<XElement>().Value;
            if (valorRetorno == "102")
            {
                return valorRetorno;
            }

            var motivoErro = (from c in noh.Elements() where c.Name.LocalName == "xMotivo" select c).Single<XElement>().Value;
            return motivoErro;
        }

        public IList<NotaInutilizada> GetRelNotaPeriodo(String idsEmpresas, DateTime inicio, DateTime fim)
        {
            return repositorioNotaInutilizada.GetRelNotaPeriodo(idsEmpresas, inicio, fim);
        }
    }
}
