using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class MercadoLivreConfiguracaoController : BaseController<MercadoLivreConfiguracao>
	{
		#region Singleton
        private static MercadoLivreConfiguracaoController instance;
        private static IRepositorioMercadoLivreConfiguracao repositorioMercadoLivreConfiguracao;

        private MercadoLivreConfiguracaoController()
        { }
		
		static MercadoLivreConfiguracaoController()
        {
            instance = new MercadoLivreConfiguracaoController();
            repositorioT = RepositorioFactory<MercadoLivreConfiguracao>.GetRepositorio();
            repositorioMercadoLivreConfiguracao = (IRepositorioMercadoLivreConfiguracao)repositorioT;
        }
			
		public static MercadoLivreConfiguracaoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(MercadoLivreConfiguracao objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
