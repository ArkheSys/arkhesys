using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class Ofd_ObraDiarioController : BaseController<Ofd_ObraDiario>
	{
		#region Singleton
        private static Ofd_ObraDiarioController instance;
        private static IRepositorioOfd_ObraDiario repositorioOfd_ObraDiario;

        private Ofd_ObraDiarioController()
        { }
		
		static Ofd_ObraDiarioController()
        {
            instance = new Ofd_ObraDiarioController();
            repositorioT = RepositorioFactory<Ofd_ObraDiario>.GetRepositorio();
            repositorioOfd_ObraDiario = (IRepositorioOfd_ObraDiario)repositorioT;
        }
			
		public static Ofd_ObraDiarioController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Ofd_ObraDiario objeto)
        {
            return new Dictionary<string, string>();
        }
	}
}
