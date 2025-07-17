using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;
using System.Xml;
using System.IO;
using System.Threading;

namespace cwkGestao.Integracao.SISeCommerce.Modelo.XML
{
    public static class SISeCommerceXMLParser
    {
        public static pedido ParseXml(string caminho)
        {
            WaitForFile(new FileInfo(caminho));
            XmlSerializer serial = new XmlSerializer(typeof(pedido));
            XmlReader reader = XmlReader.Create(caminho);
            pedido p = new pedido();
            try
            {
                p = (pedido)serial.Deserialize(reader);
                reader.Close();
                reader = null;
            }
            catch (Exception e)
            {
                throw new Exception("XML mal-formado",e);
            }
            return p;
        }

        public static pedido ParseXml(Stream stream)
        {
            XmlSerializer serial = new XmlSerializer(typeof(pedido));
            XmlReader reader = XmlReader.Create(stream);
            pedido p = new pedido();
            try
            {
                p = (pedido)serial.Deserialize(reader);
                reader.Close();
                reader = null;
            }
            catch (Exception e)
            {

                throw new Exception("XML mal-formado", e);
            }
            return p;
        }

        public static IList<pedido> ParseMultiplosXmls(List<string> caminhos)
        {
            IList<pedido> pedidos = new List<pedido>();
            foreach (string item in caminhos)
            {
                try
                {
                    WaitForFile(new FileInfo(item));
                    pedidos.Add(ParseXml(item));
                }
                catch (Exception e)
                {
                    
                    throw e;
                }
            }
            return pedidos;
        }

        private static void WaitForFile(FileInfo file)
        {
            FileStream stream = null;
            bool FileReady = false;
            while (!FileReady)
            {
                try
                {
                    using (stream = file.Open(FileMode.Open, FileAccess.ReadWrite, FileShare.None))
                    {
                        FileReady = true;
                    }
                }
                catch (IOException)
                {
                }
                if (!FileReady) Thread.Sleep(200);
            }
        }
    }
}
