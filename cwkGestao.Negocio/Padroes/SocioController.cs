using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class SocioController : BaseController<Socio>
	{
		#region Singleton
        private static SocioController instance;
        private static IRepositorioSocio repositorioSocio;

        private SocioController()
        { }
		
		static SocioController()
        {
            instance = new SocioController();
            repositorioT = RepositorioFactory<Socio>.GetRepositorio();
            repositorioSocio = (IRepositorioSocio)repositorioT;
        }
			
		public static SocioController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Socio objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
