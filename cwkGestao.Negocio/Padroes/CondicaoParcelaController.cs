using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class CondicaoParcelaController : BaseController<CondicaoParcela>
	{
		#region Singleton
        private static CondicaoParcelaController instance;
        private static IRepositorioCondicaoParcela repositorioCondicaoParcela;

        private CondicaoParcelaController()
        { }
		
		static CondicaoParcelaController()
        {
            instance = new CondicaoParcelaController();
            repositorioT = RepositorioFactory<CondicaoParcela>.GetRepositorio();
            repositorioCondicaoParcela = (IRepositorioCondicaoParcela)repositorioT;
        }
			
		public static CondicaoParcelaController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(CondicaoParcela objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
