using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Cw_usuarioController : BaseController<Cw_usuario>
	{
		#region Singleton
        private static Cw_usuarioController instance;
        private static IRepositorioCw_usuario repositorioCw_usuario;

        private Cw_usuarioController()
        { }
		
		static Cw_usuarioController()
        {
            instance = new Cw_usuarioController();
            repositorioT = RepositorioFactory<Cw_usuario>.GetRepositorio();
            repositorioCw_usuario = (IRepositorioCw_usuario)repositorioT;
        }
			
		public static Cw_usuarioController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Cw_usuario objeto)
        {
            return new Dictionary<string, string>();
        }

        public Cw_usuario FindByLogin(string login)
        {
            return repositorioCw_usuario.FindByLogin(login);
        }


	}
}
