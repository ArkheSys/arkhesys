using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class SHLConfiguracaoController : BaseController<SHLConfiguracao>
	{
		#region Singleton
        private static SHLConfiguracaoController instance;
        private static IRepositorioSHLConfiguracao repositorioSHLConfiguracao;

        private SHLConfiguracaoController()
        { }
		
		static SHLConfiguracaoController()
        {
            instance = new SHLConfiguracaoController();
            repositorioT = RepositorioFactory<SHLConfiguracao>.GetRepositorio();
            repositorioSHLConfiguracao = (IRepositorioSHLConfiguracao)repositorioT;
        }
			
		public static SHLConfiguracaoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(SHLConfiguracao objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
