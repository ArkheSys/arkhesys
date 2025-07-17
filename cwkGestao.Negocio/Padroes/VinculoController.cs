using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;
using System.Xml;
using System.Xml.Linq;

namespace cwkGestao.Negocio
{
    public class VinculoController : BaseController<Vinculo>
    {
        private int indice = 0;
        #region Singleton
        private static VinculoController instance;
        private static IRepositorioVinculo repositorioVinculo;
        private static XmlDocument doc;

        private VinculoController()
        { }

        static VinculoController()
        {
            instance = new VinculoController();
            repositorioT = RepositorioFactory<Vinculo>.GetRepositorio();
            repositorioVinculo = (IRepositorioVinculo)repositorioT;
        }

        public static VinculoController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(Vinculo objeto)
        {
            return new Dictionary<string, string>();
        }

        public void getListasXML(ref List<NotaItem> listaNotaItem, ref List<Vinculo> listaVinculo, List<decimal> dadosVinculo, List<object> lista, bool VemDepoisQueSalvou)
        {
            int idFornecedor = Convert.ToInt32(dadosVinculo[0]);

            foreach (var item in lista)
            {
                NotaItem objNotaItem = new NotaItem();

                string codigo = ((List<object>)item)[0].ToString();
                string ean = ((List<object>)item)[1].ToString();
                string descricao = ((List<object>)item)[2].ToString();
                string ncm = ((List<object>)item)[3].ToString();
                string cfop = ((List<object>)item)[4].ToString();

                bool temCFOP = true;

                // Problema aqui...
                Vinculo vinculo = null;
                if (VemDepoisQueSalvou)
                    vinculo = listaVinculo.FirstOrDefault(o => o.CodProduto == codigo);

                if (vinculo == null)
                    vinculo = repositorioVinculo.GetProdVinculo(codigo, cfop, idFornecedor, ref temCFOP) ?? new Vinculo();

                if (vinculo.ID != 0 && temCFOP)
                {
                    vinculo.CodEan = ean;
                    vinculo.CodProduto = codigo;
                    vinculo.CFOP = cfop;
                    vinculo.descricao = descricao;
                    vinculo.Ncm = ncm;

                    vinculo.UnidadeEntradaSigla = ((List<object>)item)[5].ToString();
                    vinculo.QuantidadeEntrada = Convert.ToDecimal(((List<object>)item)[6]);

                    if (!VemDepoisQueSalvou)
                    {
                        vinculo.UnidadeSaidaSigla = ((List<object>)item)[5].ToString();
                        vinculo.QuantidadeSaida = Convert.ToDecimal(((List<object>)item)[6]);
                    }
                    listaVinculo.Add(vinculo);
                    listaNotaItem.Add(AdicionaNotaItem(vinculo, (List<object>)item, VemDepoisQueSalvou));
                }
                else
                {
                    vinculo.CodEan = ean;
                    vinculo.CodProduto = codigo;
                    vinculo.CFOP = cfop;
                    vinculo.descricao = descricao;
                    vinculo.Ncm = ncm;

                    vinculo.UnidadeEntradaSigla = ((List<object>)item)[5].ToString();
                    vinculo.QuantidadeEntrada = Convert.ToDecimal(((List<object>)item)[6]);

                    vinculo.UnidadeSaidaSigla = ((List<object>)item)[5].ToString();
                    vinculo.QuantidadeSaida = Convert.ToDecimal(((List<object>)item)[6]);

                    listaVinculo.Add(vinculo);
                }
            }
        }

        private NotaItem AdicionaNotaItem(Vinculo vinculo, List<object> produto, bool VemDepoisQueSalvou)
        {
            NotaItem notaItem = new NotaItem();
            NotaICMS notaICMS = new NotaICMS();

            notaItem.Produto = vinculo.Produto;
            notaItem.CFOP = vinculo.IDCFOP;
            notaItem.ProdutoNome = vinculo.Produto.Nome;

            int tamanho = produto.Count;

            KeyValuePair<string, Dictionary<string, decimal>> icms = new KeyValuePair<string, Dictionary<string, decimal>>();
            KeyValuePair<string, Dictionary<string, decimal>> ipi = new KeyValuePair<string, Dictionary<string, decimal>>();
            KeyValuePair<string, Dictionary<string, decimal>> pis = new KeyValuePair<string, Dictionary<string, decimal>>();
            KeyValuePair<string, Dictionary<string, decimal>> cofins = new KeyValuePair<string, Dictionary<string, decimal>>();

            try
            {
                icms = (KeyValuePair<string, Dictionary<string, decimal>>)produto[9];
            }
            catch (Exception)
            {
            }
            try
            {
                ipi = tamanho == 13 ? (KeyValuePair<string, Dictionary<string, decimal>>)produto[10] : (KeyValuePair<string, Dictionary<string, decimal>>)produto[-1];
            }
            catch (Exception)
            {
            }
            try
            {
                pis = tamanho == 13 ? (KeyValuePair<string, Dictionary<string, decimal>>)produto[11] : (KeyValuePair<string, Dictionary<string, decimal>>)produto[10];
            }
            catch (Exception)
            {
            }
            try
            {
                cofins = tamanho == 13 ? (KeyValuePair<string, Dictionary<string, decimal>>)produto[12] : (KeyValuePair<string, Dictionary<string, decimal>>)produto[11];
            }
            catch (Exception)
            {
            }

            //Atribui valores ICMS

            if (icms.Value != null && icms.Value.ContainsKey("orig")) notaItem.orig_N11 = Convert.ToInt32(icms.Value["orig"]);
            if (icms.Value != null && icms.Value.ContainsKey("CSOSN")) notaItem.CSOSN = Convert.ToInt32(icms.Value["CSOSN"]);
            if (icms.Value != null && icms.Value.ContainsKey("CSOSN")) notaItem.TAG_CST = notaItem.CSOSN.ToString();
            if (icms.Value != null && icms.Value.ContainsKey("pCredSN")) notaItem.PCredSN_N29 = icms.Value["pCredSN"];
            if (icms.Value != null && icms.Value.ContainsKey("vCredICMSSN")) notaItem.VCredICMSSN_N30 = icms.Value["vCredICMSSN"];
            if (icms.Value != null && icms.Value.ContainsKey("modBCST")) notaItem.modBCST_N18 = Convert.ToInt32(icms.Value["modBCST"]);
            if (icms.Value != null && icms.Value.ContainsKey("pMVAST")) notaItem.PMVAST_N19 = icms.Value["pMVAST"];
            if (icms.Value != null && icms.Value.ContainsKey("pRedBCST")) notaItem.PRedBCST_N20 = icms.Value["pRedBCST"];
            if (icms.Value != null && icms.Value.ContainsKey("vBCST")) notaItem.BaseICMSSubst = icms.Value["vBCST"];
            if (icms.Value != null && icms.Value.ContainsKey("pICMSST")) notaItem.PICMSST_N22 = icms.Value["pICMSST"];
            if (icms.Value != null && icms.Value.ContainsKey("vICMSST")) notaItem.ValorRetidoICMS = icms.Value["vICMSST"];
            if (icms.Value != null && icms.Value.ContainsKey("modBC")) notaItem.modBC_N13 = Convert.ToInt32(icms.Value["modBC"]);
            if (icms.Value != null && icms.Value.ContainsKey("vBC")) notaItem.BaseICMS = icms.Value["vBC"];
            if (icms.Value != null && icms.Value.ContainsKey("pRedBC")) notaItem.PRedBC_N14 = icms.Value["pRedBC"];
            if (icms.Value != null && icms.Value.ContainsKey("pICMS")) notaItem.AliqICMS = icms.Value["pICMS"];
            if (icms.Value != null && icms.Value.ContainsKey("vICMS")) notaItem.ValorICMS = icms.Value["vICMS"];

            //Atribui valores IPI

            if (ipi.Value != null && ipi.Value.ContainsKey("cEnq")) notaItem.cEnq_O06 = ipi.Value["cEnq"].ToString();

            Int32 ipiCst;
            if (ipi.Value != null && ipi.Value.ContainsKey("CST"))
            {
                Int32.TryParse(ipi.Value["CST"].ToString(), out ipiCst);
                notaItem.CST_Ipi = ipiCst.ToString("D2");
            }

            if (ipi.Value != null && ipi.Value.ContainsKey("vBC")) notaItem.VBC_O10 = ipi.Value["vBC"];
            if (ipi.Value != null && ipi.Value.ContainsKey("pIPI")) notaItem.PIPI_O13 = ipi.Value["pIPI"];
            if (ipi.Value != null && ipi.Value.ContainsKey("qUnid")) notaItem.Quantidade = ipi.Value["qUnid"];
            if (ipi.Value != null && ipi.Value.ContainsKey("vUnid")) notaItem.Valor = ipi.Value["vUnid"];
            if (ipi.Value != null && ipi.Value.ContainsKey("vIPI")) notaItem.VIPI_O14 = ipi.Value["vIPI"];

            //Atribui valores PIS
            Int32 pisCst;
            if (pis.Value != null && pis.Value.ContainsKey("CST"))
            {
                Int32.TryParse(pis.Value["CST"].ToString(), out pisCst);
                notaItem.CST_Pis = pisCst.ToString("D2");
            }

            if (pis.Value != null && pis.Value.ContainsKey("vBC")) notaItem.VBC_Q07 = pis.Value["vBC"];
            if (pis.Value != null && pis.Value.ContainsKey("pPIS")) notaItem.PPIS_Q08 = pis.Value["pPIS"];
            if (pis.Value != null && pis.Value.ContainsKey("vPIS")) notaItem.VPIS_Q09 = pis.Value["vPIS"];

            //atribui Cofins
            Int32 cofinsCst;
            if (cofins.Value != null && cofins.Value.ContainsKey("CST"))
            {
                Int32.TryParse(cofins.Value["CST"].ToString(), out cofinsCst);
                notaItem.CST_Cofins = cofinsCst.ToString("D2");
            }

            if (cofins.Value != null && cofins.Value.ContainsKey("vBC")) notaItem.VBC_S07 = cofins.Value["vBC"];
            if (cofins.Value != null && cofins.Value.ContainsKey("pCOFINS")) notaItem.PCOFINS_S08 = cofins.Value["pCOFINS"];
            if (cofins.Value != null && cofins.Value.ContainsKey("vCOFINS")) notaItem.VCOFINS_S11 = cofins.Value["vCOFINS"];

            if (VemDepoisQueSalvou && vinculo.QuantidadeSaida > 0)
            {
                notaItem.Unidade = vinculo.UnidadeSaida != null ? vinculo.UnidadeSaida.Sigla : ((List<object>)produto)[5].ToString();
                notaItem.Quantidade = vinculo.QuantidadeSaida;
                notaItem.Valor = Convert.ToDecimal(((List<object>)produto)[8]) / vinculo.QuantidadeSaida;
                notaItem.Total = notaItem.Valor * vinculo.QuantidadeSaida;
                notaItem.SubTotal = notaItem.Total;
            }
            else
            {
                notaItem.Unidade = vinculo.UnidadeSaida != null ? vinculo.UnidadeSaida.Sigla : ((List<object>)produto)[5].ToString();
                notaItem.Quantidade = Convert.ToDecimal(((List<object>)produto)[6]);
                notaItem.Valor = Convert.ToDecimal(((List<object>)produto)[7]);
                notaItem.Total = Convert.ToDecimal(((List<object>)produto)[8]);
                notaItem.SubTotal = notaItem.Total;
            }

            notaItem.cBenef_I05f = ((List<object>)produto)[5].ToString();
            return notaItem;
        }

        #region Leitura Impostos XML

        public Dictionary<string, Dictionary<string, decimal>> LeituraImpostosXml()
        {
            string tagXML = "", aux = "";
            XmlNodeReader leitor = new XmlNodeReader(doc);
            Dictionary<string, Dictionary<string, decimal>> impostos = new Dictionary<string, Dictionary<string, decimal>>();
            Dictionary<string, decimal> impostoSelecionado = new Dictionary<string, decimal>();
            indice = 0;
            while (leitor.Read())
            {
                if (leitor.NodeType == XmlNodeType.XmlDeclaration)
                    continue;

                #region Adiciona Impostos

                if (leitor.NodeType == XmlNodeType.Element)
                {
                    if (leitor.LocalName.Trim().Equals("det"))
                    {
                        indice++;
                    }

                }


                if (AdicionaImposto(ref tagXML, leitor, ref impostos, ref impostoSelecionado))
                    continue;

                if (leitor.NodeType == XmlNodeType.EndElement && ImpostoItem(leitor.LocalName))
                    tagXML = "";

                #endregion

                if (leitor.NodeType == XmlNodeType.Element)
                {
                    if (ImpostoItem(leitor.LocalName))
                    {
                        tagXML = leitor.LocalName;
                        continue;
                    }

                    if (leitor.NodeType == XmlNodeType.Element)
                    {
                        aux = leitor.LocalName;
                        continue;
                    }
                }
                else if (leitor.NodeType == XmlNodeType.Text)
                {
                    decimal auxNA;
                    try
                    {
                        auxNA = Convert.ToDecimal(leitor.Value.ToString().Replace(".", ","));
                    }
                    catch (Exception)
                    {

                        auxNA = 0;
                    }

                    switch (tagXML)
                    {
                        case "ICMS":
                            impostoSelecionado.Add(aux, auxNA);
                            break;
                        case "PIS":
                            impostoSelecionado.Add(aux, auxNA);
                            break;
                        case "IPI":
                            impostoSelecionado.Add(aux, auxNA);
                            break;
                        case "COFINS":
                            impostoSelecionado.Add(aux, auxNA);
                            break;
                        default:
                            break;
                    }
                    continue;
                }
            }

            return impostos;
        }

        private bool AdicionaImposto(ref string tagXML, XmlNodeReader leitor, ref Dictionary<string, Dictionary<string, decimal>> impostos, ref Dictionary<string, decimal> impostoSelecionado)
        {
            if (leitor.NodeType == XmlNodeType.EndElement && leitor.LocalName == tagXML)
            {
                impostos.Add(ValidaImpostos(impostoSelecionado, tagXML) + " - Item" + indice, impostoSelecionado);
                impostoSelecionado = new Dictionary<string, decimal>();
                tagXML = "";
                return true;
            }
            return false;
        }

        #endregion

        #region Validações impostos

        private string ValidaImpostos(Dictionary<string, decimal> impostoSelecionado, string tagXML)
        {
            switch (tagXML)
            {
                case "ICMS":
                    return ValidaICMS(impostoSelecionado);
                case "IPI":
                    return ValidaIPI(impostoSelecionado);
                case "COFINS":
                    return ValidaCOFINS(impostoSelecionado);
                case "PIS":
                    return ValidaPIS(impostoSelecionado);
                default:
                    return "";
            }
        }

        private string ValidaICMS(Dictionary<string, decimal> icms)
        {
            if (icms.ContainsKey("CST"))
            {
                switch (Convert.ToInt32(icms["CST"]))
                {
                    case 0:
                        return "ICMS00";

                    case 10:
                        return "ICMS10";

                    case 20:
                        return "ICMS20";

                    case 30:
                        return "ICMS30";

                    case 40:
                    case 41:
                    case 50:
                        return "ICMS40";

                    case 51:
                        return "ICMS51";

                    case 60:
                        return "ICMS60";

                    case 70:
                        return "ICMS70";

                    case 90:
                        return "ICMS90";

                    default:
                        return "";
                }
            }
            else if (icms.ContainsKey("CSOSN"))
            {
                switch (Convert.ToInt32(icms["CSOSN"]))
                {
                    case 101:
                        return "ICMSSN101";

                    case 102:
                    case 103:
                    case 300:
                    case 400:
                        return "ICMSSN102";

                    case 201:
                        return "ICMSSN201";

                    case 202:
                    case 203:
                        return "ICMSSN202";

                    case 500:
                        return "ICMSSN500";

                    case 900:
                        return "ICMSSN900";

                    default:
                        return "";
                }
            }
            return "";
        }

        private string ValidaCOFINS(Dictionary<string, decimal> cofins)
        {
            switch (Convert.ToInt32(cofins["CST"]))
            {
                case 1:
                case 2:
                    return "COFINSAliq";

                case 3:
                    return "COFINSQtde";

                case 4:
                case 6:
                case 7:
                case 8:
                case 9:
                    return "COFINSNT";

                case 49:
                case 50:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                case 56:
                case 60:
                case 61:
                case 62:
                case 63:
                case 64:
                case 65:
                case 66:
                case 67:
                case 71:
                case 72:
                case 73:
                case 74:
                case 75:
                case 98:
                case 99:
                    return "COFINSOutr";

                default:
                    return "";
            }

        }

        private string ValidaIPI(Dictionary<string, decimal> ipi)
        {
            switch (Convert.ToInt32(ipi["CST"]))
            {
                case 0:
                case 49:
                case 50:
                case 99:
                    return "IPITrib";

                case 1:
                case 2:
                case 3:
                case 4:
                case 5:
                case 51:
                case 52:
                case 53:
                case 54:
                case 55:
                    return "IPINT";

                default:
                    return "";
            }
        }

        private string ValidaPIS(Dictionary<string, decimal> pis)
        {
            switch (Convert.ToInt32(pis["CST"]))
            {
                case 1:
                case 2:
                    return "PISAliq";

                case 3:
                    return "PISQtde";

                case 4:
                case 6:
                case 7:
                case 8:
                case 9:
                    return "PISNT";

                case 99:
                    return "PISOutr";

                default:
                    return "";
            }
        }

        #endregion

        #region ImpostoItem

        private bool ImpostoItem(string leitor)
        {
            if (leitor == "ICMS" || leitor == "PIS" || leitor == "COFINS" || leitor == "IPI")
                return true;
            return false;
        }

        #endregion

        #region Validação Fornecedor

        public bool VerificarFornecedor(string pCnpj, string pDirXml)
        {
            var isCNPJ = 14 == (pCnpj.Replace(".", "").Replace("/", "").Replace("-", "")).Length;

            string _aux = "", cnpj = "";

            doc = new XmlDocument();
            doc.Load(pDirXml);
            XmlNodeReader _leitor = new XmlNodeReader(doc);

            while (_leitor.Read())
            {
                if (_leitor.NodeType == XmlNodeType.Element)
                {
                    _aux = _leitor.LocalName;
                    continue;
                }

                if (_aux == (isCNPJ ? "CNPJ" : "CPF"))
                {
                    cnpj = _leitor.Value;
                    break;
                }
            }

            if (cnpj == pCnpj.Replace(".", "").Replace("/", "").Replace("-", ""))
                return true;
            return false;
        }

        #endregion

        #region Importação de xml p/ vínculos

        public List<object> ImportaXML(string DirXml, ref List<NotaItem> listaNotaItem, ref List<Vinculo> listaVinculo, ref List<decimal> dadosVinculo)
        {
            string aux = "";
            string chaveNota = String.Empty;

            doc = new XmlDocument();

            doc.Load(DirXml);

            XmlNodeReader leitor = new XmlNodeReader(doc);

            List<object> listaString = new List<object>();
            List<object> lista = new List<object>();

            decimal desconto = 0;

            while (leitor.Read())
            {
                if (leitor.NodeType == XmlNodeType.EndElement || leitor.NodeType == XmlNodeType.XmlDeclaration)
                    continue;

                if (leitor.NodeType == XmlNodeType.Element)
                {
                    aux = leitor.LocalName;
                    continue;
                }

                switch (aux)
                {
                    case "serie":
                        dadosVinculo.Add(Convert.ToDecimal(leitor.Value));
                        break;
                    case "nNF":
                        dadosVinculo.Add(Convert.ToDecimal(leitor.Value));
                        break;
                    case "vNF":
                        dadosVinculo.Add(Convert.ToDecimal(leitor.Value.Replace(".", ",")));
                        break;
                    case "vDesc":
                        desconto = (Convert.ToDecimal(leitor.Value.Replace(".", ",")));
                        break;
                    case "cProd":
                        listaString.Add(leitor.Value.ToString());
                        break;
                    case "CFOP":
                        listaString.Add(leitor.Value.ToString());
                        break;
                    case "xProd":
                        listaString.Add(leitor.Value.ToString());
                        break;
                    case "vProd":
                        listaString.Add(leitor.Value.ToString().Replace(".", ","));
                        break;
                    case "vUnCom":
                        listaString.Add(leitor.Value.ToString().Replace(".", ","));
                        break;
                    case "uCom":
                        listaString.Add(leitor.Value.ToString());
                        break;
                    case "qCom":
                        listaString.Add(leitor.Value.ToString().Replace(".", ","));
                        break;
                    case "NCM":
                        listaString.Add(leitor.Value.ToString());
                        break;
                    case "cEAN":
                        listaString.Add(leitor.Value.ToString());
                        break;
                    default:
                        break;
                }

                if (listaString.Count == 9)
                {
                    lista.Add(listaString);
                    listaString = new List<object>();
                }

            }

            dadosVinculo.Add(desconto);

            int x = 3;
            int indice = 0, cont = 1;
            foreach (var itemImposto in LeituraImpostosXml())
            {
                if (indice >= lista.Count)
                    continue;

                var listaProduto = (List<object>)lista[indice];

                listaProduto.Add(itemImposto);
                lista[indice] = listaProduto;

                if (itemImposto.Key.Contains("IPI"))
                    x = 4;

                if (cont % x == 0)
                    indice++;
                cont++;
            }

            getListasXML(ref listaNotaItem, ref listaVinculo, dadosVinculo, lista, false);
            return lista;
        }

        public DemaisInformações ImportaXML_Demais(string DirXml)
        {
            DemaisInformações demais = new DemaisInformações();
            string aux = "";

            doc = new XmlDocument();
            doc.Load(DirXml);

            XmlNodeReader leitor = new XmlNodeReader(doc);


            while (leitor.Read())
            {
                if (leitor.NodeType == XmlNodeType.EndElement || leitor.NodeType == XmlNodeType.XmlDeclaration)
                    continue;

                if (leitor.NodeType == XmlNodeType.Element)
                {
                    aux = leitor.LocalName;
                    continue;
                }

                switch (aux)
                {
                    case "chNFe":
                        demais.chave_nota = leitor.Value.ToString();
                        break;

                    default:
                        break;
                }
            }

            return demais;
        }
        #endregion
    }
}
