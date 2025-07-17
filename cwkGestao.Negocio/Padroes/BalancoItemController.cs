using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class BalancoItemController : BaseController<BalancoItem>
	{
		#region Singleton
        private static BalancoItemController instance;
        private static IRepositorioBalancoItem repositorioBalancoItem;

        private BalancoItemController()
        { }
		
		static BalancoItemController()
        {
            instance = new BalancoItemController();
            repositorioT = RepositorioFactory<BalancoItem>.GetRepositorio();
            repositorioBalancoItem = (IRepositorioBalancoItem)repositorioT;
        }
			
		public static BalancoItemController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(BalancoItem objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
