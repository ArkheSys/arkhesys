using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NFeX;
using NFeDataSetX;
using System.Windows.Forms;
using System.Xml;
using System.Xml.XPath;
using System.IO;
using System.Reflection;
using System.Xml.Linq;

namespace BLL
{
    public class NFe
    {
        #region Atributos
        private spdNFeX _spdNFeX = new spdNFeX();
        private spdNFeDataSetX _spdNFeDataSetX = new spdNFeDataSetX();
        public Modelo.Nota objNota { get; set; }
        private Modelo.Filial objFilial = null;

        private string servidorSMTP = "";
        private string usuEmail = "";
        private string senhaEmail = "";
        private int danfe = 1;
        private string certificado = "";
        private int tipoST = 1;
        private int formaEmissao = 1;
        private int ambiente = 2;

#pragma warning disable CS0414 // O campo "NFe.obs" é atribuído, mas seu valor nunca é usado
        private string obs = "";
#pragma warning restore CS0414 // O campo "NFe.obs" é atribuído, mas seu valor nunca é usado

#pragma warning disable CS0169 // O campo "NFe.logEnvio" nunca é usado
        private string[] logEnvio;
#pragma warning restore CS0169 // O campo "NFe.logEnvio" nunca é usado
#pragma warning disable CS0169 // O campo "NFe.logRecibo" nunca é usado
        private string[] logRecibo;
#pragma warning restore CS0169 // O campo "NFe.logRecibo" nunca é usado

        #endregion

        #region Contrutor
        public NFe()
        {
        }
        #endregion

        public bool PreencherNFe(Modelo.Filial pFilial, out string mensagem)
        {
            objFilial = pFilial;
            List<string> erros = new List<string>();

            servidorSMTP = objFilial.ServidorSMTP;
            usuEmail = objFilial.UsuarioEmail;
            senhaEmail = objFilial.SenhaEmail;
            danfe = objFilial.ModeloDanfe.Value;
            certificado = objFilial.Certificado;
            tipoST = (int)objFilial.TipoST;
            formaEmissao = (int)objFilial.FormaEmissao;
            ambiente = (int)objFilial.Ambiente;

            _spdNFeX.CNPJ = NFe.LimpaStr(objFilial.CNPJ);
            _spdNFeX.UF = objFilial.Cidade.UF.Sigla;

            _spdNFeX.DiretorioEsquemas = @"Esquemas\";
            _spdNFeX.DiretorioTemplates = @"Templates\";
            try
            {
                _spdNFeX.NomeCertificado = certificado;
            }
            catch (Exception ex)
            {
                erros.Add(ex.Message);
            }

            if (ambiente == 1)
            {
                _spdNFeX.Ambiente = Ambiente.akProducao;
                _spdNFeX.ArquivoServidoresProd = "nfeServidoresProd.ini";
            }
            else if (ambiente == 2)
            {
                _spdNFeX.Ambiente = Ambiente.akHomologacao;
                _spdNFeX.ArquivoServidoresHom = "nfeServidoresHom.ini";
            }
            else
            {
                erros.Add("O ambiente da NFe não foi informado.");
            }

            _spdNFeX.DiretorioLog = GetDiretorioNFe() + @"\Log\";
            try
            {
                _spdNFeX.TipoCertificado = (TipoCertificado)objFilial.TipoCertificadoNFe;
            }
            catch (Exception ex)
            {
                erros.Add(ex.Message);
            }

            _spdNFeX.VersaoManual = "3.0";
            _spdNFeX.FraseContingencia = "DANFE em Contingencia";
            _spdNFeX.FraseHomologacao = "SEM VALOR FISCAL";
            //_spdNFeX.ModeloRetrato = @"Templates\danfe\Retrato.rtm";
            //_spdNFeX.ModeloPaisagem = @"Templates\danfe\Paisagem.rtm";
            _spdNFeX.LogotipoEmitente = @"Templates\Logo.bmp";

            if (erros.Count > 0)
            {
                StringBuilder str = new StringBuilder("Erro ao abrir configuração da NFe:" + Environment.NewLine);
                foreach (string erro in erros)
                {
                    str.AppendLine(" -" + erro);
                }
                mensagem = str.ToString();
                return false;
            }
            mensagem = String.Empty;
            return true;
        }

        #region Métodos Publicos NFe
        public void ExportarDanfe(int pFilial, int pEntSai, DateTime pDataI, DateTime pDataF, DevExpress.XtraEditors.ProgressBarControl pProgressBar)
        {
            string xmlNota = "";

            using (Modelo.dcNotaDataContext dbNota = new Modelo.dcNotaDataContext())
            {
                var lista = (from n in dbNota.NotaImpressao
                             where n.Ent_Sai == pEntSai
                             && n.Dt >= pDataI
                             && n.Dt <= pDataF
                             && n.IDFilial == pFilial
                             select new
                             {
                                 Numero = n.Numero,
                                 ChaveNota = n.ChaveNota
                             }).ToList();

                pProgressBar.Properties.Step = 1;
                pProgressBar.Properties.PercentView = false;
                pProgressBar.Properties.Minimum = 0;
                pProgressBar.Properties.Maximum = lista.Count();

                foreach (var nota in lista)
                {
                    pProgressBar.PerformStep();
                    pProgressBar.Update();
                    Application.DoEvents();

                    xmlNota = CarregarArquivoXmlNfe(nota.ChaveNota);

                    _spdNFeX.ExportarDanfe("1", xmlNota, "", 1, GetDiretorioNFe() + "\\pdf\\" + nota.ChaveNota + ".pdf");
                }
            }
        }
        private string CarregarArquivoXmlNfe(string pChaveNota)
        {
            StringBuilder arq_ret = new StringBuilder();
            string arquivo = "";
            arquivo = GetDiretorioNFe() + @"\XmlDestinatario\" + pChaveNota + "-nfe.xml";

            StreamReader objReader = new StreamReader(arquivo);
            string sLine = "";

            while (sLine != null)
            {
                sLine = objReader.ReadLine();
                if (sLine != null)
                    arq_ret.Append(sLine);
            }
            objReader.Close();

            return arq_ret.ToString();
        }

        private string GetDiretorioNFe()
        {
            if (String.IsNullOrEmpty(objFilial.DiretorioPadraoNFe) || !File.Exists(objFilial.DiretorioPadraoNFe))
                return objFilial.DiretorioPadraoNFe;

            string dir = Assembly.GetEntryAssembly().Location;
            return Path.Combine(Path.GetDirectoryName(dir), String.Empty);
        }
        #endregion

        #region Métodos Staticos
        public static string Conv_CST_Pis(int aIndex)
        {
            string ret = "";
            switch (aIndex)
            {
                case 0:
                    ret = "01";
                    break;
                case 1:
                    ret = "02";
                    break;
                case 2:
                    ret = "03";
                    break;
                case 3:
                    ret = "04";
                    break;
                case 4:
                    ret = "06";
                    break;
                case 5:
                    ret = "07";
                    break;
                case 6:
                    ret = "08";
                    break;
                case 7:
                    ret = "09";
                    break;
                case 8:
                    ret = "99";
                    break;
            }
            return ret;
        }
        public static string Conv_CST_Cofins(int aIndex)
        {
            string ret = "";
            switch (aIndex)
            {
                case 0:
                    ret = "01";
                    break;
                case 1:
                    ret = "02";
                    break;
                case 2:
                    ret = "03";
                    break;
                case 3:
                    ret = "04";
                    break;
                case 4:
                    ret = "06";
                    break;
                case 5:
                    ret = "07";
                    break;
                case 6:
                    ret = "08";
                    break;
                case 7:
                    ret = "09";
                    break;
                case 8:
                    ret = "99";
                    break;
            }
            return ret;
        }
        public static string Conv_TAG_CST(int aIndex)
        {
            switch (aIndex)
            {
                case 0: return "00";
                case 1: return "10";
                case 2: return "20";
                case 3: return "30";
                case 4: return "40";
                case 5: return "41";
                case 6: return "50";
                case 7: return "51";
                case 8: return "60";
                case 9: return "70";
                case 10: return "90";
                case 11: return "Part";
                case 12: return "ST";
                case 13: return "101";
                case 14: return "102";
                case 15: return "103";
                case 16: return "201";
                case 17: return "202";
                case 18: return "203";
                case 19: return "300";
                case 20: return "400";
                case 21: return "500";
                case 22: return "900";
            }
            return "";
        }
        public static string Conv_CST_IPI(int aIndex)
        {
            string ret = "";
            switch (aIndex)
            {
                case 0:
                    ret = "00";
                    break;
                case 1:
                    ret = "01";
                    break;
                case 2:
                    ret = "02";
                    break;
                case 3:
                    ret = "03";
                    break;
                case 4:
                    ret = "04";
                    break;
                case 5:
                    ret = "05";
                    break;
                case 6:
                    ret = "49";
                    break;
                case 7:
                    ret = "50";
                    break;
                case 8:
                    ret = "51";
                    break;
                case 9:
                    ret = "52";
                    break;
                case 10:
                    ret = "53";
                    break;
                case 11:
                    ret = "54";
                    break;
                case 12:
                    ret = "55";
                    break;
                case 13:
                    ret = "99";
                    break;
            }
            return ret;
        }
        public static string LimpaStr(string aTexto)
        {
            string ret = "";
            if (aTexto != null)
                ret = aTexto.Replace(".", "").Replace("/", "").Replace("-", "").Replace("(", "").Replace(")", "").Replace(" ", "");
            return ret;
        }
        public static string LimpaCaracter(string aTexto)
        {
            string ret = String.Empty;
            if (aTexto != null)
                ret = aTexto.Replace("&", "");
            return ret;
        }
        public static string LimpaMultiplosEspacos(string valor)
        {
            StringBuilder ret = new StringBuilder();
            bool espaco = false;
            for (int i = 0; i < valor.Length; i++)
            {
                if (valor[i] == ' ')
                {
                    if (espaco)
                        continue;
                    espaco = true;
                }
                else
                    espaco = false;
                ret.Append(valor[i]);
            }
            return ret.ToString();
        }
        public static string MotivoRetorno(int aErro)
        {
            string erro = "";

            using (Modelo.DataClassesDataContext db = new Modelo.DataClassesDataContext())
            {
                erro = (from e in db.NFe_Erros
                        where e.ID == aErro
                        select e).Single().Descricao;
            }

            return erro;
        }
        public static List<string> TrataRetorno(string aXml, string aTag, Modelo.TipoRetorno aTipoRetorno)
        {
            List<string> retorno = new List<string>();

            XmlDocument _xml = new XmlDocument();
            _xml.LoadXml(aXml);
            XPathNavigator nav = _xml.CreateNavigator();

            XPathExpression expr;
            expr = nav.Compile("//*");
            XPathNodeIterator iterator = nav.Select(expr);

            try
            {
                while (iterator.MoveNext())
                {
                    XPathNavigator nav2 = iterator.Current.Clone();
                    if (nav2.Name == aTag)
                    {
                        if (aTipoRetorno == Modelo.TipoRetorno.Codigo)
                        {
                            retorno.Add(nav2.Value);
                        }
                        else
                        {
                            retorno.Add(NFe.MotivoRetorno(int.Parse(nav2.Value)));
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            return retorno;
        }
        #endregion
    }
}

