using System.Collections.Generic;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System.Xml;


namespace cwkGestao.Negocio
{
    public partial class CartaCorrecaoController : BaseController<CartaCorrecao>
    {
        #region Singleton
        private static CartaCorrecaoController instance;
        private static IRepositorioCartaCorrecao repositorioCartaCorrecao;

        private CartaCorrecaoController()
        { }

        static CartaCorrecaoController()
        {
            instance = new CartaCorrecaoController();
            repositorioT = RepositorioFactory<CartaCorrecao>.GetRepositorio();
            repositorioCartaCorrecao = (IRepositorioCartaCorrecao)repositorioT;
        }

        public static CartaCorrecaoController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(CartaCorrecao objeto)
        {
            return new Dictionary<string, string>();
        }

        public string EnviarCartaCorrecao(CartaCorrecao cartacorrecao)
        {
            Filial empresa = cartacorrecao.Nota.Filial;
            string xml;

            var configuracoesDFe = cwkNotaFiscalEletronica.INotaFiscalEletronica._configuracoes.CfgServico;

            cwkNotaFiscalEletronica.NFeFacade facade = new cwkNotaFiscalEletronica.NFeFacade(empresa.DiretorioPadraoNFe);
            xml = facade.CartaCorrecao(cartacorrecao.Nota.Numero, cartacorrecao.Sequencia, cartacorrecao.Nota.ChaveNota, cartacorrecao.TextoCorrecao.ToString(), empresa.CNPJ.Replace(".", "").Replace("/", "").Replace("-", ""), configuracoesDFe).RetornoCompletoStr;

            return xml;
        }

        public CartaCorrecao VerificaSituacaoCCe(string xml, CartaCorrecao cc)
        {
            string _aux = "";
            List<string> cStat = new List<string>();

            XmlDocument doc = new XmlDocument();
            doc.LoadXml(xml);

            XmlNodeReader _leitor = new XmlNodeReader(doc);

            while (_leitor.Read())
            {
                if (_leitor.NodeType == XmlNodeType.EndElement || _leitor.NodeType == XmlNodeType.XmlDeclaration)
                    continue;

                if (_leitor.NodeType == XmlNodeType.Element)
                {
                    _aux = _leitor.LocalName;
                    continue;
                }

                if (_aux == "cStat")
                {
                    cStat.Add(_leitor.Value);
                }
                if (_aux == "nProt")
                {
                    cc.Protocolo = _leitor.Value;
                }

            }
            cc.cStat = cStat[1];
            return cc;
        }

        public string PegaxMotivo(string xml)
        {
            string _aux = "", xMotivo = "";

            XmlDocument doc = new XmlDocument();

            doc.LoadXml(xml);



            XmlNodeReader _leitor = new XmlNodeReader(doc);

            while (_leitor.Read())
            {
                if (_leitor.NodeType == XmlNodeType.EndElement || _leitor.NodeType == XmlNodeType.XmlDeclaration)
                    continue;

                if (_leitor.NodeType == XmlNodeType.Element)
                {
                    _aux = _leitor.LocalName;
                    continue;
                }

                if (_aux == "xMotivo")
                {
                    xMotivo = _leitor.Value;
                }

            }
            return xMotivo;
        }
    }
}
