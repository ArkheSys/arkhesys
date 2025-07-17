using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Xml.Linq;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public class ConfiguracaoController : BaseController<Configuracao>
    {
        #region Singleton

        private static readonly IRepositorioConfiguracao repositorioConfiguracao;

        private ConfiguracaoController() { }

        static ConfiguracaoController()
        {
            Instancia = new ConfiguracaoController();
            repositorioT = RepositorioFactory<Configuracao>.GetRepositorio();
            repositorioConfiguracao = (IRepositorioConfiguracao) repositorioT;
        }

        public static ConfiguracaoController Instancia { get; }

        #endregion

        public override Dictionary<string, string> ValidaObjeto(Configuracao objeto)
        {
            return new Dictionary<string, string>();
        }

        private volatile Configuracao configuracao;
        private readonly object configuracao_locker = new object();

        public Configuracao GetConfiguracao()
        {
            lock (configuracao_locker)
            {
                if (configuracao == null)
                    AtualizarInstanciaConfiguracao();

                if (configuracao != null)
                    return configuracao;

                throw new Exception("A configuração geral do sistema não foi encontrada, entre em contato com o suporte.");
            }
        }

        public void AtualizarInstanciaConfiguracao()
        {
            configuracao = repositorioT.GetAll().FirstOrDefault();
        }

        public override Dictionary<string, string> Salvar(Configuracao objeto, Acao acao)
        {
            lock (configuracao_locker)
            {
                var ret = base.Salvar(objeto, acao);
                configuracao = objeto;
                return ret;
            }
        }

        public bool MostrarCamposServico()
        {
            /*ServicoProduto
             1 = Produtos
             2 = Servicos
             3 = Produtos e servicos
             */
            try
            {
                string cam = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

                return
                    Convert.ToInt32(
                        XDocument.Load(cam + "\\ConfiguracaoCwork.xml").Element("Cwork").Element("TiposNota").Value) >= 2;
            }
            catch
            {
                return true;
            }
        }

        public bool MostrarCamposProduto()
        {
            /*ServicoProduto
             1 = Produtos
             2 = Servicos
             3 = Produtos e servicos
             */
            try
            {
                string cam = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return
                    Convert.ToInt32(
                        XDocument.Load(cam + "\\ConfiguracaoCwork.xml").Element("Cwork").Element("TiposNota").Value) != 2;
            }
            catch
            {
                return true;
            }
        }

        public virtual AdesaoNFe GetTipoNfe()
        {
            try
            {
                string cam = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);
                return (AdesaoNFe) Convert.ToInt32(
                    XDocument.Load(cam + "\\ConfiguracaoCwork.xml").Element("Cwork").Element("Cwork-TipoNfe").Value);
            }
            catch
            {
                return AdesaoNFe.Total;
            }
        }

        public void InativarCliente()
        {
            repositorioConfiguracao.InativarCliente();
        }
    }
}