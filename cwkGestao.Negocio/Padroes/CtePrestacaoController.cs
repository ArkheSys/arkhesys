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
	public class CtePrestacaoController : BaseController<CtePrestacao>
	{
		#region Singleton
        private static CtePrestacaoController instance;
        private static IRepositorioCtePrestacao repositorioCtePrestacao;

        private CtePrestacaoController()
        { }
		
		static CtePrestacaoController()
        {
            instance = new CtePrestacaoController();
            repositorioT = RepositorioFactory<CtePrestacao>.GetRepositorio();
            repositorioCtePrestacao = (IRepositorioCtePrestacao)repositorioT;
        }
			
		public static CtePrestacaoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(CtePrestacao objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
