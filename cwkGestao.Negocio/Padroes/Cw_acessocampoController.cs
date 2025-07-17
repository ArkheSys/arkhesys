using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Cw_acessocampoController : BaseController<Cw_acessocampo>
	{
		#region Singleton
        private static Cw_acessocampoController instance;
        private static IRepositorioCw_acessocampo repositorioCw_acessocampo;

        private Cw_acessocampoController()
        { }
		
		static Cw_acessocampoController()
        {
            instance = new Cw_acessocampoController();
            repositorioT = RepositorioFactory<Cw_acessocampo>.GetRepositorio();
            repositorioCw_acessocampo = (IRepositorioCw_acessocampo)repositorioT;
        }
			
		public static Cw_acessocampoController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Cw_acessocampo objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
