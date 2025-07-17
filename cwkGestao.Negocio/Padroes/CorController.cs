using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using cwkGestao.Modelo;
using cwkGestao.Repositorio;
using cwkGestao.Repositorio.Interfaces.Particulares;

namespace cwkGestao.Negocio
{
	public class CorController : BaseController<Cor>
	{
		#region Singleton
        private static CorController instance;
        private static IRepositorioCor repositorioCor;

        private CorController()
        { }
		
		static CorController()
        {
            instance = new CorController();
            repositorioT = RepositorioFactory<Cor>.GetRepositorio();
            repositorioCor = (IRepositorioCor)repositorioT;
        }
			
		public static CorController Instancia
        {
            get
            {
                return instance;
            }
        }
		#endregion
		
		public override Dictionary<string, string> ValidaObjeto(Cor objeto)
        {
            throw new NotImplementedException("Método não implementado.");
        }
	}
}
