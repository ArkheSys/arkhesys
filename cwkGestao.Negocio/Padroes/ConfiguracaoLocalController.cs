using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml;
using System.IO;
using cwkGestao.Repositorio.cwkGestaoFileSystem;

namespace cwkGestao.Negocio
{
    public class ConfiguracaoLocalController
    {
        public static XmlDocument GetArquivoConfiguracao()
        {
            return ConfiguracaoLocalRepositorio.GetArquivoConfiguracao();
        }

        public static int GetPeriodoPedido()
        {
            return ConfiguracaoLocalRepositorio.GetPeriodoPedido(GetArquivoConfiguracao());
        }

        //public static void CriaPeriodoUsuario(XmlDocument documentoXml, string periodo)
        //{

        //}

        public static void SetPeriodoPedido(int periodo)
        {
            ConfiguracaoLocalRepositorio.SetPeriodoPedido(periodo);
        }

        //private static void CriaOrcamentoVenda(XmlDocument documentoXml, string valor)
        //{

        //}

        public static void SetOrcamentoVenda(int valor)
        {
            ConfiguracaoLocalRepositorio.SetOrcamentoVenda(valor);
        }

        public static int GetModuloOrcamentoVenda()
        {
            return ConfiguracaoLocalRepositorio.GetModuloOrcamentoVenda(GetArquivoConfiguracao());
        }

        public static string GetTipoNfe()
        {
            return ConfiguracaoLocalRepositorio.GetTipoNfe(GetArquivoConfiguracao());
        }

        public static string GetValidaIE()
        {
            return ConfiguracaoLocalRepositorio.GetValidaIE(GetArquivoConfiguracao());
        }
    }
}
