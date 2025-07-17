using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio.Interfaces.Particulares;
using cwkGestao.Repositorio;
namespace cwkGestao.Negocio.Padroes
{
    public class ConfiguracaoManagerController : BaseController<ConfiguracaoManager>
    {
        #region Singleton
        private static ConfiguracaoManagerController instance;
        private static IRepositorioConfiguracaoManager repositorioConfiguracaoManager;

        private ConfiguracaoManagerController()
        { }

        static ConfiguracaoManagerController()
        {
            instance = new ConfiguracaoManagerController();
            repositorioT = RepositorioFactory<ConfiguracaoManager>.GetRepositorio();
            repositorioConfiguracaoManager = (IRepositorioConfiguracaoManager)repositorioT;
        }

        public static ConfiguracaoManagerController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion

        public override Dictionary<string, string> ValidaObjeto(ConfiguracaoManager objeto)
        {
            return new Dictionary<string, string>();
        }

        //Metodos
        
    }
}
