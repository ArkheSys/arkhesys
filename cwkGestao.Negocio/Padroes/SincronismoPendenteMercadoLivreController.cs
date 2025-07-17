using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class SincronismoPendenteMercadoLivreController : BaseController<SincronismoPendenteMercadoLivre>
	{
		#region Singleton
        private static SincronismoPendenteMercadoLivreController instance;
        private static IRepositorioSincronismoPendenteMercadoLivre repositorioSincronismoPendenteMercadoLivre;

        private SincronismoPendenteMercadoLivreController()
        { }
		
		static SincronismoPendenteMercadoLivreController()
        {
            instance = new SincronismoPendenteMercadoLivreController();
            repositorioT = RepositorioFactory<SincronismoPendenteMercadoLivre>.GetRepositorio();
            repositorioSincronismoPendenteMercadoLivre = (IRepositorioSincronismoPendenteMercadoLivre)repositorioT;
        }
			
		public static SincronismoPendenteMercadoLivreController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(SincronismoPendenteMercadoLivre objeto)
        {
            return new Dictionary<string, string>();
        }

        public SincronismoPendenteMercadoLivre hqlLoadByIdMercadoLivre(int id)
        {
            return repositorioSincronismoPendenteMercadoLivre.hqlLoadByIdMercadoLivre(id); 
        }
	}
}
