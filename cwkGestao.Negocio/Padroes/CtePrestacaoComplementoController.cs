using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Modelo.Cte;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class CtePrestacaoComplementoController : BaseController<CtePrestacaoComplemento>
	{
		#region Singleton
        private static CtePrestacaoComplementoController instance;
        private static IRepositorioCtePrestacaoComplemento repositorioCtePrestacaoComplemento;

        private CtePrestacaoComplementoController()
        { }
		
		static CtePrestacaoComplementoController()
        {
            instance = new CtePrestacaoComplementoController();
            repositorioT = RepositorioFactory<CtePrestacaoComplemento>.GetRepositorio();
            repositorioCtePrestacaoComplemento = (IRepositorioCtePrestacaoComplemento)repositorioT;
        }
			
		public static CtePrestacaoComplementoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(CtePrestacaoComplemento objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
