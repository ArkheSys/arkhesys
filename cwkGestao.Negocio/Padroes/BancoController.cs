using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class BancoController : BaseController<Banco>
	{
		#region Singleton
        private static BancoController instance;
        private static IRepositorioBanco repositorioBanco;

        private BancoController()
        { }
		
		static BancoController()
        {
            instance = new BancoController();
            repositorioT = RepositorioFactory<Banco>.GetRepositorio();
            repositorioBanco = (IRepositorioBanco)repositorioT;
        }
			
		public static BancoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Banco objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
