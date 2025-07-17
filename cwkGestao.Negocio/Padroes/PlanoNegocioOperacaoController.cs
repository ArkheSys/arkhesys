using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class PlanoNegocioOperacaoController : BaseController<PlanoNegocioOperacao>
	{
		#region Singleton
        private static PlanoNegocioOperacaoController instance;
        private static IRepositorioPlanoNegocioOperacao repositorioPlanoNegocioOperacao;

        private PlanoNegocioOperacaoController()
        { }
		
		static PlanoNegocioOperacaoController()
        {
            instance = new PlanoNegocioOperacaoController();
            repositorioT = RepositorioFactory<PlanoNegocioOperacao>.GetRepositorio();
            repositorioPlanoNegocioOperacao = (IRepositorioPlanoNegocioOperacao)repositorioT;
        }
			
		public static PlanoNegocioOperacaoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(PlanoNegocioOperacao objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
