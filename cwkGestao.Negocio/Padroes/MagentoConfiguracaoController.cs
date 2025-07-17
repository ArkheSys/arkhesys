using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
    public partial class MagentoConfiguracaoController : BaseController<MagentoConfiguracao>
    {
        #region Singleton
        private static MagentoConfiguracaoController instance;
        private static IRepositorioMagentoConfiguracao repositorioMagentoConfiguracao;

        private MagentoConfiguracaoController()
        { }

        static MagentoConfiguracaoController()
        {
            instance = new MagentoConfiguracaoController();
            repositorioT = RepositorioFactory<MagentoConfiguracao>.GetRepositorio();
            repositorioMagentoConfiguracao = (IRepositorioMagentoConfiguracao)repositorioT;
        }

        public static MagentoConfiguracaoController Instancia
        {
            get
            {
                return instance;
            }
        }
        #endregion

        public override Dictionary<string, string> ValidaObjeto(MagentoConfiguracao objeto)
        {
            return new Dictionary<string, string>();
        }

        public override Dictionary<string, string> Salvar(MagentoConfiguracao objeto, Acao acao)
        {
            lock (configuracao_locker)
            {
                var ret = base.Salvar(objeto, acao);
                configuracao = objeto;
                return ret;
            }
        }

        private volatile MagentoConfiguracao configuracao;
        private readonly object configuracao_locker = new object();
        public MagentoConfiguracao GetConfiguracao()
        {
            lock (configuracao_locker)
            {
                if (configuracao == null)
                    AtualizarInstanciaConfiguracao();

                    return configuracao;
                
            }
        }

        public void AtualizarInstanciaConfiguracao()
        {
            configuracao = repositorioMagentoConfiguracao.LoadConfiguracao();
        }
    }
}
