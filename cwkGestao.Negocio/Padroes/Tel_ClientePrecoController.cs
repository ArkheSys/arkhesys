using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Tel_ClientePrecoController : BaseController<Tel_ClientePreco>
	{
		#region Singleton
        private static Tel_ClientePrecoController instance;
        private static IRepositorioTel_ClientePreco repositorioTel_ClientePreco;

        private Tel_ClientePrecoController()
        { }
		
		static Tel_ClientePrecoController()
        {
            instance = new Tel_ClientePrecoController();
            repositorioT = RepositorioFactory<Tel_ClientePreco>.GetRepositorio();
            repositorioTel_ClientePreco = (IRepositorioTel_ClientePreco)repositorioT;
        }
			
		public static Tel_ClientePrecoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Tel_ClientePreco objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
