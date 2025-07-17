using System;
using System.IO;
using System.Xml;
using Modelo;

namespace cwkGestao.Repositorio.cwkGestaoFileSystem
{
    public class ConfiguracaoLocalRepositorio
    {
        public static readonly string ARQUIVO_CONFIGURACAO = cwkGlobal.DirApp + @"\ConfiguracaoCwork.xml";

        public static XmlDocument GetArquivoConfiguracao()
        {
            XmlDocument documentoXml = new XmlDocument();

            if (File.Exists(ARQUIVO_CONFIGURACAO))
            {
                documentoXml.Load(ARQUIVO_CONFIGURACAO);
                if (documentoXml.DocumentElement.Name == "Cwork-Pedido")
                {
                    XmlDocument newDocumentoXml = new XmlDocument();
                    XmlElement xmlCwork = newDocumentoXml.CreateElement("Cwork");

                    XmlElement xmlCworkPedido = newDocumentoXml.CreateElement("Cwork-Pedido");
                    xmlCworkPedido.InnerXml = documentoXml.DocumentElement.InnerXml;
                    xmlCwork.AppendChild(xmlCworkPedido);

                    XmlElement xmlTipoNfe = newDocumentoXml.CreateElement("Cwork-TipoNfe");
                    xmlTipoNfe.InnerXml = "0";
                    xmlCwork.AppendChild(xmlTipoNfe);

                    newDocumentoXml.AppendChild(xmlCwork);
                    newDocumentoXml.Save(ARQUIVO_CONFIGURACAO);
                    documentoXml = newDocumentoXml;
                }
            }
            else
            {
                XmlElement xmlCwork = documentoXml.CreateElement("Cwork");

                XmlElement xmlCworkPedido = documentoXml.CreateElement("Cwork-Pedido");
                XmlElement xmlUsuario = documentoXml.CreateElement(cwkGlobal.objUsuarioLogado.Login);
                XmlElement xmlOpcao = documentoXml.CreateElement("Opcao");
                xmlOpcao.InnerText = "8";
                xmlUsuario.AppendChild(xmlOpcao);
                xmlCworkPedido.AppendChild(xmlUsuario);
                xmlCwork.AppendChild(xmlCworkPedido);

                XmlElement xmlOrcamentoVenda = documentoXml.CreateElement("OrcamentoVenda");
                xmlOrcamentoVenda.InnerText = "1";
                xmlCwork.AppendChild(xmlOrcamentoVenda);

                XmlElement xmlTipoNfe = documentoXml.CreateElement("Cwork-TipoNfe");
                xmlTipoNfe.InnerText = "0";
                xmlCwork.AppendChild(xmlTipoNfe);

                documentoXml.AppendChild(xmlCwork);
                documentoXml.Save(ARQUIVO_CONFIGURACAO);
            }

            return documentoXml;
        }

        public static int GetPeriodoPedido(XmlDocument documentoXml)
        {
            int ret = -1;
            foreach (XmlElement item in documentoXml.DocumentElement)
                if (item.Name == "Cwork-Pedido")
                    foreach (XmlElement subItem in item.ChildNodes)
                        if (subItem.Name == cwkGlobal.objUsuarioLogado.Login)
                            foreach (XmlNode No in subItem.SelectSingleNode("Opcao"))
                                ret = Convert.ToInt16(No.InnerText);

            if (ret == -1)
            {
                ret = 8;
                CriaPeriodoUsuario(documentoXml, ret.ToString());
            }

            return ret;
        }

        private static void CriaPeriodoUsuario(XmlDocument documentoXml, string periodo)
        {
            XmlElement xmlUsuario = documentoXml.CreateElement(cwkGlobal.objUsuarioLogado.Login);
            XmlElement xmlOpcao = documentoXml.CreateElement("Opcao");
            xmlOpcao.InnerText = periodo;
            xmlUsuario.AppendChild(xmlOpcao);
            documentoXml.DocumentElement.AppendChild(xmlUsuario);
            foreach (XmlNode item in documentoXml.DocumentElement.ChildNodes)
                if (item.Name == "Cwork-Pedido")
                {
                    item.AppendChild(xmlUsuario);
                    break;
                }

            documentoXml.Save(ARQUIVO_CONFIGURACAO);
        }

        public static void SetPeriodoPedido(int periodo)
        {
            XmlDocument documentoXml = GetArquivoConfiguracao();
            bool achou = false;
            foreach (XmlElement item in documentoXml.DocumentElement)
                if (item.Name == "Cwork-Pedido")
                    foreach (XmlElement subItem in item.ChildNodes)
                        if (subItem.Name == cwkGlobal.objUsuarioLogado.Login)
                            foreach (XmlNode No in subItem.SelectSingleNode("Opcao"))
                            {
                                No.InnerText = periodo.ToString();
                                achou = true;
                            }

            if (achou)
                documentoXml.Save(ARQUIVO_CONFIGURACAO);
            else
                CriaPeriodoUsuario(documentoXml, periodo.ToString());
        }

        public static int GetModuloOrcamentoVenda(XmlDocument documentoXml)
        {
            int ret = -1;
            foreach (XmlElement item in documentoXml.DocumentElement)
                if (item.Name == "OrcamentoVenda")
                    ret = Convert.ToInt32(item.InnerText);

            if (ret == -1)
            {
                ret = 0;
                CriaOrcamentoVenda(documentoXml, ret.ToString());
            }

            return ret;
        }

        private static void CriaOrcamentoVenda(XmlDocument documentoXml, string valor)
        {
            XmlElement xmlOrcamento = documentoXml.CreateElement("OrcamentoVenda");
            xmlOrcamento.InnerText = valor;
            documentoXml.DocumentElement.AppendChild(xmlOrcamento);
            documentoXml.Save(ARQUIVO_CONFIGURACAO);
        }

        public static void SetOrcamentoVenda(int valor)
        {
            XmlDocument documentoXml = GetArquivoConfiguracao();
            bool achou = false;
            foreach (XmlElement item in documentoXml.DocumentElement)
                if (item.Name == "OrcamentoVenda")
                {
                    item.InnerText = valor.ToString();
                    achou = true;
                }

            if (achou)
                documentoXml.Save(ARQUIVO_CONFIGURACAO);
            else
                CriaOrcamentoVenda(documentoXml, valor.ToString());
        }

        public static string GetTipoNfe(XmlDocument documentoXml)
        {
            string ret = "0";
            foreach (XmlElement item in documentoXml.DocumentElement)
                if (item.Name == "Cwork-TipoNfe")
                {
                    ret = item.InnerText;
                    break;
                }

            return ret;
        }

        public static string GetValidaIE(XmlDocument documentoXml)
        {
            string validar = "0";
            foreach (XmlElement item in documentoXml.DocumentElement)
                if (item.Name == "Cwork-ValidaIE")
                {
                    validar = item.InnerText;
                    break;
                }

            return validar;
        }
    }
}