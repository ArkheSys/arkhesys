using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class SisECommerceConfiguracaoController : BaseController<SisECommerceConfiguracao>
	{
		#region Singleton
        private static SisECommerceConfiguracaoController instance;
        private static IRepositorioSisECommerceConfiguracao repositorioSisECommerceConfiguracao;

        private SisECommerceConfiguracaoController()
        { }
		
		static SisECommerceConfiguracaoController()
        {
            instance = new SisECommerceConfiguracaoController();
            repositorioT = RepositorioFactory<SisECommerceConfiguracao>.GetRepositorio();
            repositorioSisECommerceConfiguracao = (IRepositorioSisECommerceConfiguracao)repositorioT;
        }
			
		public static SisECommerceConfiguracaoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(SisECommerceConfiguracao objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
